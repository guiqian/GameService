using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Text;
using System.Net;
using System.Net.Sockets;

namespace GameService {
    public class NetworkUDP_Sender : IGameServiceUpdate {

        private string ServerIP;
        private int ServerPort;
        private Socket socket;      //目标socket
        private EndPoint serverEnd; //服务端
        private IPEndPoint ipEnd;   //服务端端口
        private Queue<byte[]> msgQueue = new Queue<byte[]>();

        //初始化
        public void Init(string serverIp, int serverPort, System.Action finish = null) {
            //定义连接的服务器ip和端口
            ServerIP = serverIp;
            ServerPort = serverPort;
            if (socket == null) {
                ipEnd = new IPEndPoint(IPAddress.Parse(serverIp), serverPort);
                //定义套接字类型,在主线程中定义
                socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
                //定义服务端
                IPEndPoint sender = new IPEndPoint(IPAddress.Any, 0);
                serverEnd = (EndPoint)sender;
            }
            //建立初始连接，这句非常重要，第一次连接初始化了serverEnd后面才能收到消息
            if (finish != null) // 初始化完毕
                finish();
        }

        public void Stop() {
            if (socket != null) {
                socket.Shutdown(SocketShutdown.Send);
                socket.Disconnect(true);
                socket.Dispose();
                socket = null;
            }
            if (msgQueue != null) {
                msgQueue.Clear();
            }
        }

        public void SendMsg(byte[] msgCont) {
            if (socket != null) {
                msgQueue.Enqueue(msgCont);
            }
        }

        void IGameServiceUpdate.OnUpdate() {
            if (msgQueue.Count > 0 && socket != null) {
                byte[] msgCont = msgQueue.Dequeue();
                socket.SendTo(msgCont, msgCont.Length, SocketFlags.None, ipEnd);
            }
        }

    }

}