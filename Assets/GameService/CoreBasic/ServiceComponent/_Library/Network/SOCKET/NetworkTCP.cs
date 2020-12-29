using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using UnityEngine;

namespace GameService {
    public class NetworkTCP : IGameService, IGameServiceUpdate {

        private TcpClient mClient = null;
        private TcpClient mConnecting = null;
        private IAsyncResult mConnectResult = null, mRecvResult = null;
        private readonly int mTimeOut = 2000;
        private byte[] mRecvByteBuffer = new byte[2 * 1024 * 1024];
        private Queue<byte[]> mSendBuffer = new Queue<byte[]>();
        private List<KeyValuePair<int, byte[]>> mRecvBuffer = new List<KeyValuePair<int, byte[]>>();
        private int mRecvPos = 0;
        private EventNetwork _event = null;

        public bool Connect(string ip, int port) {
            try {
                if (mConnecting == null) {
                    IPAddress ipAddress = IPAddress.Parse(ip);
                    mConnecting = new TcpClient();
                    mConnectResult = mConnecting.BeginConnect(ip, port, null, null);
                    return true;
                }
                return false;
            } catch (System.Exception ex) {
                ShutDown();
                return false;
            }
        }

        void System.IDisposable.Dispose() {
            ShutDown();
        }

        void IGameServiceUpdate.OnUpdate() {
            OnUpdate_HandleSendMsg();
            OnUpdate_HandleRecvMsg_TCPClient();
            OnUpdate_InitTCPClient();
        }

        private void OnUpdate_HandleSendMsg() {
            if (mSendBuffer.Count > 0) {
                byte[] data = null;
                lock (mSendBuffer) {
                    data = mSendBuffer.Dequeue();
                }
                if (data != null && mClient != null) {
                    mClient.GetStream().Write(data, 0, data.Length);
                }
            }
        }

        private void OnUpdate_HandleRecvMsg_TCPClient() {
            if (mClient != null) {
                OnUpdate_ReadRecvMessage();
                OnUpdate_HandlePackage();
            }
        }

        private void OnUpdate_HandlePackage() {
            for (int i = 0, count = mRecvBuffer.Count; i < count; i++) {
                KeyValuePair<int, byte[]> one = mRecvBuffer[i];
                if (_event != null) {
                    _event.Dispatch((uint)one.Key, one.Value);
                }
                mRecvBuffer.Remove(one);
                count--;
                i--;
            }
        }

        internal void SetEventNetwork(EventNetwork _event) {
            this._event = _event;
        }

        private void OnUpdate_ReadRecvMessage() {
            if (mRecvResult != null) {
                if (mRecvResult.IsCompleted) {
                    int n32BytesRead = mClient.GetStream().EndRead(mRecvResult);
                    if (n32BytesRead == 0) {
                        return;
                    }
                    mRecvPos += n32BytesRead;
                    OnUpdate_ParsePackage();
                    if (mClient != null) {
                        mRecvResult = mClient.GetStream().BeginRead(mRecvByteBuffer, mRecvPos, mRecvByteBuffer.Length - mRecvPos, null, null);
                    }
                }
            }
        }

        private void OnUpdate_ParsePackage() {
            int currentPos = 0;
            while (mRecvPos - currentPos > 8) {
                int len = BitConverter.ToInt32(mRecvByteBuffer, currentPos); // A包体长度占1个int长度
                int type = BitConverter.ToInt32(mRecvByteBuffer, currentPos + 4); // B消息类型占1个int长度
                if (len > mRecvByteBuffer.Length) {
                    break;
                }
                if (len > mRecvPos - currentPos) {
                    break;
                }

                using (System.IO.MemoryStream tempStream = new System.IO.MemoryStream()) {
                    tempStream.Position = 0;
                    tempStream.Write(mRecvByteBuffer, currentPos + 8, len - 8); // 根据A和B,所以是8
                    tempStream.Flush();

                    byte[] data = tempStream.ToArray();
                    mRecvBuffer.Add(new KeyValuePair<int, byte[]>(type, data));

                    tempStream.Close();
                    tempStream.Dispose();

                    currentPos += len;
                }
            }
            if (currentPos > 0) {
                mRecvPos = mRecvPos - currentPos;
                if (mRecvPos > 0) {
                    Buffer.BlockCopy(mRecvByteBuffer, currentPos, mRecvByteBuffer, 0, mRecvPos);
                }
            }
        }

        private void OnUpdate_InitTCPClient() {
            if (mConnectResult != null) {
                if (mConnectResult.IsCompleted) {
                    mClient = mConnecting;
                } else {
                    // nothing
                }
                if (mClient != null) {
                    if (mClient.Connected) {
                        mClient.NoDelay = true;
                        mClient.Client.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.SendTimeout, mTimeOut);
                        mClient.Client.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReceiveTimeout, mTimeOut);
                        mClient.Client.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.KeepAlive, true);
                        mRecvResult = mClient.GetStream().BeginRead(mRecvByteBuffer, 0, mRecvByteBuffer.Length, null, null);
                    } else {
                        ShutDown();
                    }
                    mConnectResult = null;
                }
            }
        }

        public void SendMessage(byte[] data) {
            if (data != null) {
                lock (mSendBuffer) {
                    mSendBuffer.Enqueue(data);
                }
            }
        }

        private void ShutDown(TcpClient tcpClient) {
            if (tcpClient != null) {
                tcpClient.Client.Shutdown(SocketShutdown.Both);
                tcpClient.Close();
                tcpClient.Dispose();
                tcpClient = null;
            }
        }

        public void ShutDown() {
            ShutDown(mConnecting);
            ShutDown(mClient);
            mRecvPos = 0;
            mConnectResult = mRecvResult = null;
            if (mRecvBuffer != null) {
                mRecvBuffer.Clear();
            }
            if (mSendBuffer != null) {
                mSendBuffer.Clear();
            }
        }

        public void Stop() {
            ShutDown();
        }

    }
}

