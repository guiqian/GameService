using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameService {
    public class Service_NetWork : IGameService, IGameServiceUpdate {

        private NetworkUDP_Reciver mUDP_Reciver = null;
        private NetworkUDP_Sender mUDP_Sender = null;
        private NetworkTCP mTCP = null;
        private EventNetwork mEventNetwork = null;

        public NetworkUDP_Reciver UDP_Reciver { get { return mUDP_Reciver; } }
        public NetworkUDP_Sender UDP_Sender { get { return mUDP_Sender; } }
        public NetworkTCP TCP { get { return mTCP; } }
        public EventNetwork Event { get { return mEventNetwork; } }

        public Service_NetWork() {
            mEventNetwork = new EventNetwork();
            // ====================
            mUDP_Reciver = new NetworkUDP_Reciver();
            mUDP_Reciver.SetEventNetwork(mEventNetwork);
            mUDP_Sender = new NetworkUDP_Sender();
            // ====================
            mTCP = new NetworkTCP();
            mTCP.SetEventNetwork(mEventNetwork);
        }

        void System.IDisposable.Dispose() {
            UDP_Reciver.Stop();
            UDP_Sender.Stop();
            TCP.Stop();
        }

        void IGameServiceUpdate.OnUpdate() {
            (mUDP_Reciver as IGameServiceUpdate).OnUpdate();
            (mUDP_Sender as IGameServiceUpdate).OnUpdate();
            (mTCP as IGameServiceUpdate).OnUpdate();
        }

    }
}
