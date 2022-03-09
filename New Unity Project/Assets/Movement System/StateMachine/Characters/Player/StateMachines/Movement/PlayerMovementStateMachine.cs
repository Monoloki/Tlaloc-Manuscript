using Movement_System.StateMachine.Characters.Player.Data.States;
using Movement_System.StateMachine.Characters.Player.StateMachines.Movement.States.Grounded;
using Movement_System.StateMachine.Characters.Player.StateMachines.Movement.States.Grounded.Moving;

namespace Movement_System.StateMachine.Characters.Player.StateMachines.Movement {
    public class PlayerMovementStateMachine : StateMachine {
        public PlayerMovementStateMachine(Player player){
            Player         = player;
            ReusableData   = new PlayerStateReusableData();
            IdlingState    = new PlayerIdlingState(this);
            WalkingState   = new PlayerWalkingState(this);
            RunningState   = new PlayerRunningState(this);
            SprintingState = new PlayerSprintingState(this);
        }

        public PlayerStateReusableData ReusableData { get; }

        public Player Player { get; }

        public PlayerIdlingState    IdlingState    { get; }
        public PlayerWalkingState   WalkingState   { get; }
        public PlayerRunningState   RunningState   { get; }
        public PlayerSprintingState SprintingState { get; }
    }
}
