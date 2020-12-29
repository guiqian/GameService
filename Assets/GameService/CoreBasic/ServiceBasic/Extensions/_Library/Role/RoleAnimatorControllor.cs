using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameService {

    public enum RoleAnimatorEnum {
        None = 0,
        ToIdle = 1,
        ToWalk = 2,
        ToRun = 3,
    }

    public enum RoleAnimatorEnumCondition {
        None = 0,
        Idle = 1,
        Walk = 2,
        Run = 3,
    }

    public class RoleAnimatorControllor : MonoBehaviour {

        private Animator mAnimator = null;
        private const string mCurrentState = "CurrentState";
        private bool mIsPlayFinish = false;
        private RoleAnimatorEnum mRoleAni = RoleAnimatorEnum.None, mRoleCurrentAni = RoleAnimatorEnum.None;

        void Start() {
            mAnimator = this.GetComponent<Animator>();
            if (mAnimator == null)
                mAnimator = this.GetComponentInChildren<Animator>();
        }

        private void Reset() {
            mAnimator.SetBool(System.Enum.GetName(typeof(RoleAnimatorEnum), RoleAnimatorEnum.ToIdle), false);
            mAnimator.SetBool(System.Enum.GetName(typeof(RoleAnimatorEnum), RoleAnimatorEnum.ToWalk), false);
            mAnimator.SetBool(System.Enum.GetName(typeof(RoleAnimatorEnum), RoleAnimatorEnum.ToRun), false);
        }

        public void ToXXX(RoleAnimatorEnum value) {
            Reset();
            mIsPlayFinish = false;
            mRoleAni = value;
            mAnimator.SetBool(System.Enum.GetName(typeof(RoleAnimatorEnum), value), true);
        }

        public bool IsCurrentStateFinish {
            get { return mIsPlayFinish; }
        }

        void Update() {
            AnimatorStateInfo stateInfo = mAnimator.GetCurrentAnimatorStateInfo(0);
            if (stateInfo.IsName(System.Enum.GetName(typeof(RoleAnimatorEnumCondition), RoleAnimatorEnumCondition.Idle))) {
                mAnimator.SetInteger(mCurrentState, (int)RoleAnimatorEnum.ToIdle);
                mRoleCurrentAni = RoleAnimatorEnum.ToIdle;
            } else if (stateInfo.IsName(System.Enum.GetName(typeof(RoleAnimatorEnumCondition), RoleAnimatorEnumCondition.Walk))) {
                mAnimator.SetInteger(mCurrentState, (int)RoleAnimatorEnum.ToWalk);
                mRoleCurrentAni = RoleAnimatorEnum.ToWalk;
            } else if (stateInfo.IsName(System.Enum.GetName(typeof(RoleAnimatorEnumCondition), RoleAnimatorEnumCondition.Run))) {
                mAnimator.SetInteger(mCurrentState, (int)RoleAnimatorEnum.ToRun);
                mRoleCurrentAni = RoleAnimatorEnum.ToRun;
            }
            if (mRoleCurrentAni == mRoleAni)
                UpdateIsFinish();
        }

        private void UpdateIsFinish() {
            if (mIsPlayFinish == false) {
                AnimatorStateInfo stateInfo = mAnimator.GetCurrentAnimatorStateInfo(0);
                if (stateInfo.normalizedTime >= 1.0f) {
                    mIsPlayFinish = true;
                }
            }
        }

    }

}