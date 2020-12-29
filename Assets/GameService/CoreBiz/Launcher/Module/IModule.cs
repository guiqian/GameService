using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace GameService {
    public interface IModule<VALUE> {
        Task<VALUE> OnInit(object[] param);
    }
}