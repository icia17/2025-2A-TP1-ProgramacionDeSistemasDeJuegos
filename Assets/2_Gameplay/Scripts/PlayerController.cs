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
            => currentMovementState.Update();

        // Move Logic Moved to Move(), Added State Pattern to open movement capabilities for expansion
        private void HandleMoveInput(InputAction.CallbackContext ctx)
            => currentMovementState.OnMove(ctx.ReadValue<Vector2>().ToHorizontalPlane());

        // Jump Logic Moved to Jump(), Added State Pattern to open movement capabilities for expansion
        private void HandleJumpInput(InputAction.CallbackContext ctx)
            => currentMovementState.OnJump();
        
        public void ChangeMovementState(PlayerMovementState newState)
        {
            currentMovementState.OnExit();
            currentMovementState = newState;
            currentMovementState.OnEnter();

            Debug.Log($"New Movement State: {newState.GetType().Name}");
        }

        public bool GroundCheck()
            => _character.GroundCheck();

        public bool FallCheck()
            => _character.FallCheck();

        public bool IdleCheck()
            => _character.IdleCheck();

        // Some code simplification was done with isGrounded boolean and a ternary operator.
        public void Move(Vector3 direction, bool isGrounded)
            => _character?.SetDirection(isGrounded ? direction.normalized : direction.normalized * airborneSpeedMultiplier);

        public Vector3 GetDirection()
            => _character._direction;

        /*
         * FIX: Reworked Jump function, logic is now handled in a separate PlayerJumpState class belonging to an application of the State Pattern called
         * PlayerMovementState. The only logic handled inside of here is making the Character Jump. Jumping does not start a coroutine anymore as its 
         * pointless since we have a way to detect the current movement state
        */
        public void Jump()
            => _character.Jump();

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