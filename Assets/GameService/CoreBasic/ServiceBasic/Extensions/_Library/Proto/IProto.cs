using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameService {

    public interface IProto_ToProto<T> {
        T ToProto(byte[] data);
    }

    public interface IProto_ToByteArray {
        byte[] ToByteArray();
    }

    public interface IProto_ToSend {
        byte[] ToSend();
    }

    public interface IProto : IProto_ToByteArray, IProto_ToProto<IProto>, IProto_ToSend {
        uint msgType { get; set; }
    }

}