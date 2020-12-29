using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;

namespace GameService {

    public class MMOMemoryStream : MemoryStream {

        public MMOMemoryStream() : base() {
        }
        public MMOMemoryStream(byte[] buffer) : base(buffer) {
        }

        public short ReadShort() {
            byte[] arr = new byte[2];
            base.Read(arr, 0, 2);
            return System.BitConverter.ToInt16(arr, 0);
        }

        public void WriteShort(short value) {
            byte[] arr = System.BitConverter.GetBytes(value);
            base.Write(arr, 0, arr.Length);
        }

        public ushort ReadUShort() {
            byte[] arr = new byte[2];
            base.Read(arr, 0, 2);
            return System.BitConverter.ToUInt16(arr, 0);
        }

        public void WriteUShort(ushort value) {
            byte[] arr = System.BitConverter.GetBytes(value);
            base.Write(arr, 0, arr.Length);
        }

        public int ReadInt() {
            byte[] arr = new byte[4];
            base.Read(arr, 0, 4);
            return System.BitConverter.ToInt32(arr, 0);
        }

        public void WriteInt(int value) {
            byte[] arr = System.BitConverter.GetBytes(value);
            base.Write(arr, 0, arr.Length);
        }

        public uint ReadUInt() {
            byte[] arr = new byte[4];
            base.Read(arr, 0, 4);
            return System.BitConverter.ToUInt32(arr, 0);
        }

        public void WriteUInt(uint value) {
            byte[] arr = System.BitConverter.GetBytes(value);
            base.Write(arr, 0, arr.Length);
        }

        public long ReadLong() {
            byte[] arr = new byte[8];
            base.Read(arr, 0, 8);
            return System.BitConverter.ToInt64(arr, 0);
        }

        public void WriteLong(long value) {
            byte[] arr = System.BitConverter.GetBytes(value);
            base.Write(arr, 0, arr.Length);
        }

        public ulong ReadULong() {
            byte[] arr = new byte[8];
            base.Read(arr, 0, 8);
            return System.BitConverter.ToUInt64(arr, 0);
        }

        public void WriteULong(ulong value) {
            byte[] arr = System.BitConverter.GetBytes(value);
            base.Write(arr, 0, arr.Length);
        }

        public float ReadFloat() {
            byte[] arr = new byte[4];
            base.Read(arr, 0, 4);
            return System.BitConverter.ToSingle(arr, 0);
        }

        public void WriteFloat(float value) {
            byte[] arr = System.BitConverter.GetBytes(value);
            base.Write(arr, 0, arr.Length);
        }

        public double ReadDouble() {
            byte[] arr = new byte[8];
            base.Read(arr, 0, 8);
            return System.BitConverter.ToDouble(arr, 0);
        }

        public void WriteDouble(double value) {
            byte[] arr = System.BitConverter.GetBytes(value);
            base.Write(arr, 0, arr.Length);
        }

        public bool ReadBool() {
            return base.ReadByte() == 1;
        }

        public void WriteBool(bool value) {
            base.WriteByte((byte)(value ? 1 : 0));
        }

        public string ReadUTF8String() {
            uint len = this.ReadUInt();  // uint length is 4
            byte[] buffer = new byte[len];
            base.Read(buffer, 0, buffer.Length);
            return Encoding.UTF8.GetString(buffer);
        }

        public void WriteUTF8String(string value) {
            byte[] arr = Encoding.UTF8.GetBytes(value);
            this.WriteUInt((uint)arr.Length);
            base.Write(arr, 0, arr.Length);
        }

    }
}