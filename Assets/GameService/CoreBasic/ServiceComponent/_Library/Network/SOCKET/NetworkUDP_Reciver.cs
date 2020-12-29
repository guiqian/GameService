using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Text;
using System.Threading;
using System.Net;
using System.Net.Sockets;

namespace GameService {

    public class NetworkUDP_Reciver : IGameServiceUpdate {

        private string Ip;
        private int Port;

        private readonly char mBufferKeySplit = '_';
        private object mBufferLock = new object();
        private Dictionary<string, byte[]> mBuffer = new Dictionary<string, byte[]>();
        // private Dictionary<uint, byte[]> mBuffer = new Dictionary<uint, byte[]>(); // 20190404

        private UdpClient client;
        private Thread readThread;
        private EventNetwork _event = null;

        public void Init(string ip, int port) {
            Ip = ip;
            Port = port;
            if (client == null) {
                //client = new UdpClient(port); // 地址端口绑定一次
                //client.JoinMulticastGroup(IPAddress.Parse(ip));

                client = new UdpClient(); // 地址端口复用
                client.Client.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, 1);
                client.Client.Bind(new IPEndPoint(IPAddress.Any, port));
                client.JoinMulticastGroup(IPAddress.Parse(ip));

                readThread = new Thread(new ThreadStart(ReceiveData));
                readThread.IsBackground = true;
                readThread.Start();
            }
        }

        internal void SetEventNetwork(EventNetwork _event) {
            this._event = _event;
        }

        public void Stop() { // Stop reading UDP messages
            if (readThread != null && readThread.IsAlive)
                readThread.Abort();
            if (client != null)
                client.Close();
            if (mBuffer != null)
                mBuffer.Clear();
        }

        private void ReceiveData() { // receive thread function
            IPEndPoint anyIP = null;
            try {
                while (true) {
                    byte[] data = client.Receive(ref anyIP);
                    DecodeMessage(data);
                }
            } catch (Exception err) {
                Debug.LogError((err.ToString()));
            }
        }

        private void DecodeMessage(byte[] buffer) {
            int dstOffset = 0;
            uint msg_type = BitConverter.ToUInt32(buffer, dstOffset);

            int _dstOffset = 16; // header length is 16
            byte[] content = new byte[buffer.Length - _dstOffset];
            System.Array.Copy(buffer, _dstOffset, content, 0, content.Length);

            lock (mBufferLock) {
                string key = BufferKey(msg_type, content);
                mBuffer[key] = content;
                // mBuffer[msg_type] = content; // 20190404
            }
        }

        private uint ParseBufferKey(string str) {
            string result = str.Substring(0, str.IndexOf(mBufferKeySplit));
            return uint.Parse(result);
        }

        private string BufferKey(uint msg_type, byte[] content) {
            uint terminalId = BitConverter.ToUInt32(content, 0);
            return msg_type + mBufferKeySplit.ToString() + terminalId;
        }

        void IGameServiceUpdate.OnUpdate() {
            lock (mBufferLock) {
                if (_event != null) {
                    Dictionary<string, byte[]>.Enumerator ie = mBuffer.GetEnumerator();
                    while (ie.MoveNext()) {
                        KeyValuePair<string, byte[]> pair = ie.Current;
                        uint msg_type = ParseBufferKey(pair.Key);
                        _event.Dispatch(msg_type, pair.Value);
                    }
                    mBuffer.Clear();
                }
            }
        }

    }
}