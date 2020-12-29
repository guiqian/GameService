using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameService {
    public interface IParser {
        IProto Parse(params object[] param);
    }
}