using UnityEngine;
using UnityEngine.InputSystem;

namespace Movement_System.StateMachine.Characters.Player.StateMachines.Movement.States.Grounded.Moving {
    public class PlayerRunningState : PlayerMovingState {
        public PlayerRunningState(PlayerMovementStateMachine playerMovementStateMachine) :
            base(playerMovementStateMachine){
        }

    #region IState Methods

        public override void Enter(){
            base.Enter();

            stateMachine.ReusableData.MovementSpeedModifier = movementData.RunData.SpeedModifier;
        }

    #endregion


    #region Input Methods

        protected override void OnWalkToggleStarted(InputAction.CallbackContext context){
            base.OnWalkToggleStarted(context);

            Debug.Log("running to walking");
            stateMachine.ChangeState(stateMachine.WalkingState);
        }

    #endregion
    }
}
