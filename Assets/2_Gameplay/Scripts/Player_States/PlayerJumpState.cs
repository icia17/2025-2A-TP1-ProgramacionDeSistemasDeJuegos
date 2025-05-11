using UnityEngine;
using UnityEngine.InputSystem;

namespace Gameplay
{
    public class PlayerJumpState : PlayerMovementState
    {
        //TODO: These booleans are not flexible enough. If we want to have a third jump or other things, it will become a hazzle.
        /*
         * FIX 1: Instead of using booleans, we use a jump counter. If we want to add more jumps, we simply add more to the jumpCount.
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

        public override void OnEnter()
        {
            if (!player.FallCheck())
                ResetJumpCount();

            OnJump();
        }

        public override void OnExit()
        {
            if (!player.FallCheck())
                ResetJumpCount();
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
            => player.Move(ctx);
    }
}
