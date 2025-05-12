using UnityEngine;
using UnityEngine.InputSystem;

namespace Gameplay
{
    public class PlayerJumpState : PlayerMovementState
    {
        /*
         * FIX: Instead of using booleans for jump checks, we use a jump counter. If we want to add more jumps, we simply add more to the jumpCount.
         * The variable baseJumpCount is in charge of setting the jumpCount back to its base value when it touches the ground, and checking
         * for directional movement blocking when jumping.
        */
        private int jumpCount = 3;
        private int baseJumpCount;
        
        public PlayerJumpState(PlayerController player) : base(player) 
        {
            baseJumpCount = jumpCount;
        }

        public void ResetJumpCount()
            => jumpCount = baseJumpCount;

        public bool CanJump()
            => jumpCount > 0;

        public override void OnEnter()
        {
            OnJump();
            player.MoveAirborne();
        }

        public override void OnExit()
        {

        }

        public override void Update()
        {
            if (player.FallCheck())
                player.ChangeMovementState(player.playerFallState);
        }

        public override void OnJump()
        {
            if (jumpCount > 0)
            {
                player.RunJumpCoroutine();
                jumpCount--;
            }
        }

        public override void OnMove(InputAction.CallbackContext ctx)
            => player.MoveAirborne();
    }
}
