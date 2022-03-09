using System;
using Movement_System.StateMachine.Characters.Player.Data.States.Grounded;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Movement_System.StateMachine.Characters.Player.StateMachines.Movement.States {
    public class PlayerMovementState : IState {
        protected PlayerGroundedData         movementData;
        protected PlayerMovementStateMachine stateMachine;


        public PlayerMovementState(PlayerMovementStateMachine playerMovementStateMachine){
            stateMachine = playerMovementStateMachine;

            movementData = stateMachine.Player.Data.GroundedData;

            InitializeData();
        }

        private void InitializeData(){
            stateMachine.ReusableData.TimeToReachTargetRotation = movementData.BaseRotationData.TargetRotationReachTime;
        }

    #region Input Methods

        protected virtual void OnWalkToggleStarted(InputAction.CallbackContext context){
            stateMachine.ReusableData.ShouldWalk = !stateMachine.ReusableData.ShouldWalk;
        }

    #endregion

    #region Reusable Method

        protected void ResetVelocity(){
            stateMachine.Player.Rigidbody.velocity = Vector3.zero;
        }

        protected Vector3 GetMovementInputDirection(){
            return new Vector3(stateMachine.ReusableData.MovementInput.x, 0f,
                               stateMachine.ReusableData.MovementInput.y);
        }

        protected float GetMovementSpeed(){
            return movementData.BaseSpeed * stateMachine.ReusableData.MovementSpeedModifier;
        }

        protected Vector3 GetPlayerHorizontalVelocity(){
            var playerHorizontalVelocity = stateMachine.Player.Rigidbody.velocity;

            playerHorizontalVelocity.y = 0f;
            return playerHorizontalVelocity;
        }

        protected void RotateTowardsTargetRotation(){
            var currentYAngle = stateMachine.Player.Rigidbody.rotation.eulerAngles.y;

            //fix float compare
            if (Math.Abs(currentYAngle - stateMachine.ReusableData.CurrentTargetRotation.y) == 0f) return;

            var smoothedYAngle = Mathf.SmoothDampAngle(currentYAngle, stateMachine.ReusableData.CurrentTargetRotation.y,
                                                       ref stateMachine.ReusableData.DampedTargetRotationCurrentVelocity
                                                                    .y,
                                                       stateMachine.ReusableData.TimeToReachTargetRotation.y -
                                                       stateMachine.ReusableData.DampedTargetRotationPassedTime.y);
            stateMachine.ReusableData.DampedTargetRotationPassedTime.y += Time.deltaTime;

            var targetRotation = Quaternion.Euler(0f, smoothedYAngle, 0f);

            stateMachine.Player.Rigidbody.MoveRotation(targetRotation);
        }

        protected float UpdateTargetRotation(Vector3 direction, bool shouldConsiderCameraRotation = true){
            var directionAngle = GetDirectionAngle(direction);

            if (shouldConsiderCameraRotation) directionAngle = AddCameraRotationToAngle(directionAngle);

            //fix float compare
            if (Math.Abs(directionAngle - stateMachine.ReusableData.CurrentTargetRotation.y) != 0)
                UpdateTargetRotationData(directionAngle);

            RotateTowardsTargetRotation();
            return directionAngle;
        }

        protected Vector3 GetTargetRotationDirection(float targetAngle){
            return Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
        }

        protected virtual void RemoveInputActionsCallbacks(){
            stateMachine.Player.Input.PlayerActions.WalkToggle.started += OnWalkToggleStarted;
        }


        protected virtual void AddInputActionsCallbacks(){
            stateMachine.Player.Input.PlayerActions.WalkToggle.started -= OnWalkToggleStarted;
        }

    #endregion

    #region Main Methods

        private void ReadMovementInput(){
            stateMachine.ReusableData.MovementInput
                = stateMachine.Player.Input.PlayerActions.Movement.ReadValue<Vector2>();
        }

        private void Move(){
            if (stateMachine.ReusableData.MovementInput         == Vector2.zero ||
                stateMachine.ReusableData.MovementSpeedModifier == 0f) return;

            var movementDirection = GetMovementInputDirection();

            var targetRotationYAngle = Rotate(movementDirection);

            var targetRotationDirection = GetTargetRotationDirection(targetRotationYAngle);

            var movementSpeed = GetMovementSpeed();

            var currentPlayerHorizontalVelocity = GetPlayerHorizontalVelocity();

            stateMachine.Player.Rigidbody
                     .AddForce(targetRotationDirection * movementSpeed - currentPlayerHorizontalVelocity,
                               ForceMode.VelocityChange);
        }


        private float Rotate(Vector3 direction){
            var directionAngle = UpdateTargetRotation(direction);

            return directionAngle;
        }


        private void UpdateTargetRotationData(float targetAngle){
            stateMachine.ReusableData.CurrentTargetRotation.y          = targetAngle;
            stateMachine.ReusableData.DampedTargetRotationPassedTime.y = 0f;
        }


        private float AddCameraRotationToAngle(float angle){
            angle += stateMachine.Player.MainCameraTransform.eulerAngles.y;
            if (angle > 360f) angle -= 360f;
            return angle;
        }

        private static float GetDirectionAngle(Vector3 direction){
            /*
             * HEHE XD
             * documentation like: public static float Atan2(float y, float x)
             * reality: float x, float y
             * soo good ❤️
             */
            var directionAngle                      = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
            if (directionAngle < 0f) directionAngle += 360f;
            return directionAngle;
        }

    #endregion

    #region IState Methods

        public virtual void Enter(){
            Debug.Log($"State {GetType().Name}");

            AddInputActionsCallbacks();
        }

        public virtual void Exit(){
            RemoveInputActionsCallbacks();
        }

        public virtual void HandleInput(){
            ReadMovementInput();
        }

        public virtual void Update(){
        }

        public virtual void PhysicsUpdate(){
            Move();
        }

    #endregion
    }
}
