using UnityEngine;

namespace Movement_System.StateMachine.Characters.Player.Data.States {
    public class PlayerStateReusableData {
        private Vector3 _currentTargetRotation;
        private Vector3 _dampedTargetRotationCurrentVelocity;
        private Vector3 _dampedTargetRotationPassedTime;
        private Vector3 _timeToReachTargetRotation;
        public  Vector2 MovementInput         { get; set; }
        public  float   MovementSpeedModifier { get; set; } = 1f;
        public  bool    ShouldWalk            { get; set; }

        public ref Vector3 CurrentTargetRotation => ref _currentTargetRotation;

        public ref Vector3 TimeToReachTargetRotation => ref _timeToReachTargetRotation;

        public ref Vector3 DampedTargetRotationCurrentVelocity => ref _dampedTargetRotationCurrentVelocity;

        public ref Vector3 DampedTargetRotationPassedTime => ref _dampedTargetRotationPassedTime;
    }
}
