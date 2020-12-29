using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;
using GameService;

public class DemoLauncher : GameServiceLauncher {

    private void Awake() {
        // your logic
    }

    protected override void OnPreStart() {
        base.OnPreStart();
        // your logic
    }

    protected async override Task<int> OnLaunch() {
        int code = await base.OnLaunch();
        // your logic
        return 0;
    }

    protected override void OnPostStart() {
        base.OnPostStart();
        // your logic
    }

}
