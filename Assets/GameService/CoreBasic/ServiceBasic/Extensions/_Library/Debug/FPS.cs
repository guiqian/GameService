using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameService {
    public class FPS : MonoBehaviour {

        [SerializeField]
        private float mTimeIntervalSeconds = 1f;
        [SerializeField]
        private bool mIsShow = false;

        private float mCurrentTime = 0f, mLastTime = 0f;
        private int mFrameCount = 0;
        private float mFrame = 0f;

        void Start() {
        }

        void Update() {
            if (mIsShow) {
                if (mCurrentTime - mLastTime > mTimeIntervalSeconds) {
                    mFrame = mFrameCount / (mCurrentTime - mLastTime);
                    mLastTime = mCurrentTime;
                    mFrameCount = 0;
                }
                mCurrentTime += Time.deltaTime;
                mFrameCount++;
            }
            if (Input.GetKeyUp(KeyCode.Z)) {
                mIsShow = !mIsShow;
                if (mIsShow == false) {
                    mFrame = 0f;
                    mFrameCount = 0;
                    mCurrentTime = mLastTime = 0f;
                }
            }
        }

        void OnGUI() {
            if (mIsShow) {
                GUIStyle style = new GUIStyle();
                style.fontSize = 32;
                style.fontStyle = FontStyle.Bold;
                GUI.Label(new Rect(0f, 0f, Screen.width << 1, 20f), "fps:" + mFrame, style);
            }
        }

    }

}