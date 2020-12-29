using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameService {
    [System.Serializable]
    public class FSMSystem : MonoBehaviour {

        private List<FSMState> _states;

        private FSMState _previousState;
        private FSMState _previousStateForPop;

        public bool _statePushed;

        private FSMState _currentState;
        private FSMState _nextState;

        public FSMState CurrentState {
            get { return _currentState; }
        }

        public FSMState NextState {
            get { return _nextState; }
        }

        public FSMSystem() {
            _states = new List<FSMState>();
        }

        #region The Basic MonoBehaviour Methods

        public void Update() { _currentState.OnUpdate(); }

        public void FixedUpdate() { _currentState.OnFixedUpdate(); }

        public void LateUpdate() {
            _currentState.OnLateUpdate();
            _currentState.Reason();
        }

        #endregion

        public void AddState(FSMState inState) {
            if (inState == null) {
                Debug.LogError("FSM ERROR: Null reference is not allowed");
            }

            if (_states.Count == 0) {
                _states.Add(inState);
                _currentState = inState;
                return;
            }

            foreach (FSMState st in _states) {
                if (st == inState) {
                    Debug.LogError(string.Format("{0}: {1} {2}",
                                                 "Unable to Add State",
                                                 inState.ToString(),
                                                 "because state has already been added."));
                    return;
                }
            }

            _previousState = _currentState;
            _states.Add(inState);
        }// AddState()

        public void DeleteState(FSMState inState) {
            if (inState == null) {
                Debug.LogError("FSM ERROR: Null reference is not allowed");
                return;
            }

            foreach (FSMState st in _states) {
                if (st == inState) {
                    _states.Remove(st);
                    return;
                }
            }
            Debug.LogError(string.Format("{0}: {1} {2}",
                                         "Unable to Delete State",
                                         inState.ToString(),
                                         "because state is not in List."));
        } // DeleteState()

        public void GoToState(FSMState inState) {
            if (inState == null) {
                Debug.LogError("FSM ERROR: Null reference is not allowed");
                return;
            }

            foreach (FSMState st in _states) {
                if (st == inState) {
                    _nextState = st;

                    _previousState = _currentState;
                    _previousState.OnExit();

                    _currentState = _nextState;
                    _currentState.OnEnter();

                    _nextState = null;
                    return;
                }
            }
            Debug.LogError(string.Format("{0}: {1} {2}",
                                         "Unable to GoToState(",
                                         inState.ToString(),
                                         ") because state is not in List."));
        }// GoToState()

        public void GoToState(FSMState inState, object userData) {
            if (inState == null) {
                Debug.LogError("FSM ERROR: Null reference is not allowed");
                return;
            }

            foreach (FSMState st in _states) {
                if (st == inState) {
                    _nextState = st;

                    _previousState = _currentState;
                    _previousState.OnExit();

                    _currentState = _nextState;
                    _currentState.OnEnter(userData);

                    _nextState = null;
                    return;
                }
            }
            Debug.LogError(string.Format("{0}: {1} {2}",
                                         "Unable to GoToState(",
                                         inState.ToString(),
                                         ") because state is not in List."));
        }// GoToState(passing object)

        public void GoToPreviousState() {
            if (_previousState == null) {
                Debug.LogError("FSM ERROR: Null reference is not allowed. _previousState returned Null.");
                return;
            }

            foreach (FSMState st in _states) {
                if (st == _previousState) {
                    FSMState tempHolderState = _currentState;

                    _currentState.OnExit();
                    _previousState.OnEnter();

                    _currentState = _previousState;

                    _previousState = tempHolderState;

                    return;
                }
            }

            Debug.LogError(string.Format("{0}: {1} {2}",
                                         "Unable to GoToPrevious",
                                         _previousState.ToString(),
                                         " because state is not in List anymore."));
        }// GoToPreviousState()

        public void PushState(FSMState inState) {
            if (inState == null) {
                Debug.LogError("FSM ERROR: Null reference is not allowed");
                return;
            }

            foreach (FSMState st in _states) {
                if (st == inState) {
                    _previousStateForPop = _currentState;

                    _currentState = st;
                    _currentState.OnEnter();
                    _statePushed = true;
                    return;
                }
            }
            Debug.LogError(string.Format("{0}: {1} {2}",
                                         "Unable to PushState(",
                                         inState.ToString(),
                                         ") because state is not in List."));
        }// PushState()

        public void PushState(FSMState inState, object userData) {
            if (inState == null) {
                Debug.LogError("FSM ERROR: Null reference is not allowed");
                return;
            }

            foreach (FSMState st in _states) {
                if (st == inState) {
                    _previousStateForPop = _currentState;

                    _currentState = st;
                    _currentState.OnEnter();
                    _statePushed = true;
                    return;
                }
            }
            Debug.LogError(string.Format("{0}: {1} {2}",
                                         "Unable to PushState(",
                                         inState.ToString(),
                                         ") because state is not in List."));
        }// PushState(passing object)

        public void PopState() {
            if (_previousStateForPop == null) {
                Debug.LogError("FSM ERROR: Null reference is not allowed. _previousStateForPop returned Null.");
                return;
            }

            foreach (FSMState st in _states) {
                if (st == _previousStateForPop) {
                    _currentState.OnExit();
                    _currentState = _previousStateForPop;
                    _statePushed = false;
                    _previousStateForPop = null;
                    return;
                }
            }

            Debug.LogError(string.Format("{0}: {1} {2}",
                                         "Unable to GoToPrevious",
                                         _previousStateForPop.ToString(),
                                         " because state is not in List anymore."));
        }// PopState()
    }
}

