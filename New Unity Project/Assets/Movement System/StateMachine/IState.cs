namespace Movement_System.StateMachine {
    public interface IState {
        /*
         * Runs whenever a ***State*** becomes ***Current State***
         */
        public void Enter();

        /*
         * Runs whenever a ***State*** becomes ***Previous State***
         */
        public void Exit();

        public void HandleInput();

        /*
         * Unity - Update (MonoBehaviour) 
         */
        public void Update();

        /*
         * Unity - FixedUpdate (MonoBehaviour)
         */
        public void PhysicsUpdate();
    }
}
