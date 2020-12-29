using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameService {
    public interface IGameServiceRegister<VALUE> {

        bool Register(string key, VALUE value);

        bool UnRegister(string key);

    }
}
