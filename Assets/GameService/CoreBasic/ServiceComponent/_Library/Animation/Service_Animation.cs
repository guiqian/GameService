using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameService {
    public class Service_Animation : IGameService {

        public bool IsPlaying(Animation animation, string name) {
            if (animation != null) {
                return animation.IsPlaying(name);
            }
            return false;
        }

        public float NormalizedTime(Animation animation, string name) {
            if (animation != null) {
                if (animation.IsPlaying(name)) {
                    AnimationState current = animation[name];
                    return current.normalizedTime;
                }
            }
            return 1.0f;
        }

        public bool IsPlayFinish(Animation animation, string name) {
            float time = NormalizedTime(animation, name);
            return time >= 1.0f;
        }

        public bool IsPlayFinish(Animator animator, string name) {
            if (animator != null) {
                AnimatorStateInfo info = animator.GetCurrentAnimatorStateInfo(0);
                return animator.enabled && info.IsName(name) && info.normalizedTime >= 1.0f;
            }
            return true;
        }

        void System.IDisposable.Dispose() {
        }

    }
}
