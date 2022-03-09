using UnityEngine;
using UnityEngine.InputSystem;

namespace Movement_System.StateMachine.Characters.Player.StateMachines.Movement.States.Grounded.Moving {
    public class PlayerWalkingState : PlayerMovingState {
        public PlayerWalkingState(PlayerMovementStateMachine playerMovementStateMachine) :
            base(playerMovementStateMachine){
        }

    #region IState Methods

        public override void Enter(){
            base.Enter();

            stateMachine.ReusableData.MovementSpeedModifier = movementData.WalkData.SpeedModifier;
        }

    #endregion


    #region Input Methods

        protected override void OnWalkToggleStarted(InputAction.CallbackContext context){
            base.OnWalkToggleStarted(context);

            Debug.Log("Walking to running");
            stateMachine.ChangeState(stateMachine.RunningState);
        }

    #endregion
    }
}
