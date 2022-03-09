using UnityEngine;

namespace Movement_System.StateMachine.Characters.Player.StateMachines.Movement.States.Grounded {
    public class PlayerIdlingState : PlayerGroundedState {
        public PlayerIdlingState(PlayerMovementStateMachine playerMovementStateMachine) :
            base(playerMovementStateMachine){
        }

    #region IState Methods

        public override void Enter(){
            base.Enter();

            stateMachine.ReusableData.MovementSpeedModifier = 0f;

            ResetVelocity();
        }

        public override void Update(){
            base.Update();

            //jumping -> walking issue
            if (stateMachine.ReusableData.MovementInput == Vector2.zero) return;

            OnMove();
        }

    #endregion
    }
}
