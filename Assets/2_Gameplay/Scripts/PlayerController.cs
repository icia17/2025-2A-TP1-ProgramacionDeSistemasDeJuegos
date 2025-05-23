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
        [SerializeField] private int jumpLimit = 3;

        public Character character { get; private set; }
        public PlayerStateManager playerStateManager { get; private set; }

        private PlayerJumpState jumpStateInstance;

        private void Awake()
        { 
            character = GetComponent<Character>();

            jumpStateInstance = new PlayerJumpState(this, jumpLimit);

            playerStateManager = new PlayerStateManager(new PlayerIdleState(this));

            playerStateManager.AddMovementState("Jump", jumpStateInstance);
            playerStateManager.AddMovementState("Fall", new PlayerFallState(this));
            playerStateManager.AddMovementState("Idle", new PlayerIdleState(this));
            playerStateManager.AddMovementState("Walk", new PlayerWalkState(this));
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

            character.OnGrounded += HandleCharacterGrounded;
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

            character.OnGrounded -= HandleCharacterGrounded;
        }

        private void Update()
            => playerStateManager.HandleUpdate();

        // Move Logic Moved to HandleMove(), Added State Pattern to open movement capabilities for expansion
        private void HandleMoveInput(InputAction.CallbackContext ctx)
            => playerStateManager.HandleMove(ctx.ReadValue<Vector2>().ToHorizontalPlane());

        // Jump Logic Moved to HandleJump(), Added State Pattern to open movement capabilities for expansion
        private void HandleJumpInput(InputAction.CallbackContext ctx)
            => playerStateManager.HandleJump();

        // Some code simplification was done with isGrounded boolean and a ternary operator.
        public void Move(Vector3 direction, bool isGrounded)
            => character.SetDirection(isGrounded ? direction.normalized : direction.normalized * airborneSpeedMultiplier);
        
        private void HandleCharacterGrounded()
            => jumpStateInstance.ResetJumpCount();
    }
}