using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameService {
    public interface IExecutorSchedule<VALUE> {

        void Schedule(VALUE value);

        void ShutDown(VALUE value);

    }
}
