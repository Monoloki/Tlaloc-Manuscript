using UnityEngine.InputSystem;

namespace Movement_System.StateMachine.Characters.Player.StateMachines.Movement.States.Grounded {
    public class PlayerGroundedState : PlayerMovementState {
        public PlayerGroundedState(PlayerMovementStateMachine playerMovementStateMachine) :
            base(playerMovementStateMachine){
        }

    #region Input Methods

        protected virtual void OnMovementCanceled(InputAction.CallbackContext context){
            stateMachine.ChangeState(stateMachine.IdlingState);
        }

    #endregion

    #region Reusable Methods

        protected override void AddInputActionsCallbacks(){
            base.AddInputActionsCallbacks();

            stateMachine.Player.Input.PlayerActions.Movement.canceled += OnMovementCanceled;
        }


        protected override void RemoveInputActionsCallbacks(){
            base.RemoveInputActionsCallbacks();

            stateMachine.Player.Input.PlayerActions.Movement.canceled -= OnMovementCanceled;
        }

        protected virtual void OnMove(){
            if (stateMachine.ReusableData.ShouldWalk) {
                stateMachine.ChangeState(stateMachine.WalkingState);
                return;
            }

            stateMachine.ChangeState(stateMachine.RunningState);
        }

    #endregion
    }
}
