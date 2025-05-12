using UnityEngine;
using UnityEngine.InputSystem;

namespace Gameplay
{
    [RequireComponent(typeof(Character))]
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private InputActionReference moveInput;
        [SerializeField] private InputActionReference jumpInput;
        [SerializeField] private float airborneSpeedMultiplier = .5f;

        /*
         * FIX: to ease the expansion of player movement capabilities, I added a State Pattern that switches the current Player Movement State
         * depending on the players input. This will make it easier to expand and create new movement mechanics while keeping code clean and
         * understandable.
        */
        private PlayerMovementState currentMovementState;

        public PlayerJumpState playerJumpState { get; private set; }
        public PlayerFallState playerFallState { get; private set; }
        public PlayerIdleState playerIdleState { get; private set; }
        public PlayerWalkState playerWalkState { get; private set; }

        private Character _character;
        private Coroutine _jumpCoroutine;

        private void Awake()
        { 
            _character = GetComponent<Character>();

            // We set all possible Player Movement States beforehand to avoid excessive instantiation.
            playerJumpState = new PlayerJumpState(this);
            playerFallState = new PlayerFallState(this);
            playerIdleState = new PlayerIdleState(this);
            playerWalkState = new PlayerWalkState(this);

            // The base movement state is set to idle.
            currentMovementState = playerIdleState;
        }

        private void OnEnable()
        {
            if (moveInput?.action != null)
            {
                moveInput.action.started += HandleMoveInput;
                moveInput.action.performed += HandleMoveInput;
                moveInput.action.canceled += HandleMoveInput;
            }
            if (jumpInput?.action != null)
                jumpInput.action.performed += HandleJumpInput;
        }
        private void OnDisable()
        {
            if (moveInput?.action != null)
            {
                moveInput.action.performed -= HandleMoveInput;
                moveInput.action.canceled -= HandleMoveInput;
            }
            if (jumpInput?.action != null)
                jumpInput.action.performed -= HandleJumpInput;
        }

        private void Update()
        {
            currentMovementState.Update();
            Debug.Log($"Current Movement State is : {currentMovementState.GetType().Name}");    
        }

        // Move Logic Moved to Move(), Added State Pattern to open movement capabilities for expansion
        private void HandleMoveInput(InputAction.CallbackContext ctx)
            => currentMovementState.OnMove(ctx);

        // Jump Logic Moved to Jump(), Added State Pattern to open movement capabilities for expansion
        private void HandleJumpInput(InputAction.CallbackContext ctx)
            => currentMovementState.OnJump();
        
        /* 
         * While typically we use functions to run code depending on whether we enter or exit a state, currently the movement state change
         * doesnt require OnExitState() and OnEnterState(), so to keep things simple I decided not to add them. 
        */
        public void ChangeMovementState(PlayerMovementState newState)
        {
            currentMovementState.OnExit();
            currentMovementState = newState;
            currentMovementState.OnEnter();
        }

        public bool GroundCheck()
            => _character.GroundCheck();

        public bool FallCheck()
            => _character.FallCheck();

        public bool IdleCheck()
            => _character.IdleCheck();

        // Some code simplification was done with isGrounded boolean and a ternary operator.
        public void Move(InputAction.CallbackContext ctx)
        {
            var direction = ctx.ReadValue<Vector2>().ToHorizontalPlane();
            _character?.SetDirection(direction);
        }

        public void MoveAirborne()
        {
            _character?.SetDirection(_character._direction * airborneSpeedMultiplier);
        }

        public void RunJumpCoroutine()
        {
            /*
             * FIX: Removed Jump function, logic is now handled in a separate PlayerJumpState class belonging to an application of the State Pattern called
             * PlayerMovementState. The only logic handled inside of here is starting the actual Jump Coroutine.
            */
            if (_jumpCoroutine != null)
                StopCoroutine(_jumpCoroutine);
            _jumpCoroutine = StartCoroutine(_character.Jump());
        }

        private void OnCollisionEnter(Collision other)
        {
            foreach (var contact in other.contacts)
            {
                if (Vector3.Angle(contact.normal, Vector3.up) < 5)
                {
                    playerJumpState.ResetJumpCount();
                }
            }
        }
    }
}