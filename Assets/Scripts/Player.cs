using UnityEngine;

public class Player : MonoBehaviour
{
        private PlayerInputSet input;
        private StateMachine stateMachine {get; set; }
        public Player_IdleState idleState { get; private set; }
        public Player_MoveState moveState { get; private set; }

        public Vector2 moveInput { get; private set; }
        
        private void Awake() {
          stateMachine = new StateMachine();
          idleState = new Player_IdleState(this,stateMachine, "idle");
          moveState = new Player_MoveState(this,stateMachine, "move");
          input = new PlayerInputSet();
        }

        private void OnEnable()
        {
            input.Enable();
            input.Player.Movement.performed += ctx => moveInput = ctx.ReadValue<Vector2>();
            input.Player.Movement.canceled += ctx => moveInput = Vector2.zero;
        }

        private void OnDisable()
        {
            input.Disable();
        }

        private void Start()
        {
            stateMachine.Initialize(idleState);
        }

        private void Update()
        {
            stateMachine.currentState.Update();
        }
               
}
