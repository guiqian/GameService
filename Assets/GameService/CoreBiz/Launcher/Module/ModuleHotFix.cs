﻿using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace GameService {
    public class ModuleHotFix : IModule<int> {

        async Task<int> IModule<int>.OnInit(object[] param) {
            return 0;
        }

    }
}
