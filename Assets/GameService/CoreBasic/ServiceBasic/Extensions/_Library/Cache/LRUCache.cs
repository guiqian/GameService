using System.Collections.Generic;

namespace GameService {

    public class LRUCache<T> where T : UnityEngine.Object {

        private readonly object _lock = new object();
        private readonly int capacity = 0;
        private readonly Dictionary<string, Entry> cache;
        private readonly Entry head = null;
        private readonly Entry tail = null;

        public LRUCache(int capacity) {
            this.capacity = capacity;
            this.cache = new Dictionary<string, Entry>();

            this.head = new Entry("HEAD", null);
            this.tail = new Entry("TAIL", null);
            head.Next = tail;
            tail.Previous = head;
        }

        public int Count {
            get { return cache.Count; }
        }

        public T Get(string key) {
            lock (_lock) {
                if (!cache.ContainsKey(key))
                    return null;

                Entry entry = cache[key];
                MoveToTail(entry);
                return entry.Value;
            }
        }

        public void Put(string key, T value) {
            lock (_lock) {
                if (cache.ContainsKey(key)) {
                    Entry entry = cache[key];
                    MoveToTail(entry);
                    return;
                }

                Entry newEntry = new Entry(key, value);
                if (cache.Count >= capacity) {
                    Entry first = RemoveFirst();
                    cache.Remove(first.Key);
                    first.Dispose();
                }

                AddToTail(newEntry);
                cache.Add(key, newEntry);
            }
        }

        public void Clear() {
            lock (_lock) {
                foreach (var kv in cache) {
                    kv.Value.Dispose();
                }
                head.Next = tail;
                tail.Previous = head;
                this.cache.Clear();
            }
        }

        private void MoveToTail(Entry entry) {
            Entry prev = entry.Previous;
            Entry next = entry.Next;
            prev.Next = next;
            next.Previous = prev;
            Entry last = tail.Previous;
            last.Next = entry;
            tail.Previous = entry;
            entry.Previous = last;
            entry.Next = tail;
        }

        private Entry RemoveFirst() {
            Entry first = head.Next;
            Entry second = first.Next;
            head.Next = second;
            second.Previous = head;
            return first;
        }

        private void AddToTail(Entry entry) {
            Entry last = tail.Previous;
            last.Next = entry;
            tail.Previous = entry;
            entry.Previous = last;
            entry.Next = tail;
        }

        class Entry : System.IDisposable {

            public string Key { get; private set; }
            public T Value { get; private set; }
            public Entry Previous { get; set; }
            public Entry Next { get; set; }

            public Entry(string key, T value) {
                Value = value;
                Key = key;
            }

            public void Dispose() {
                Key = null;
                Value = null;
            }

        }
    }
}