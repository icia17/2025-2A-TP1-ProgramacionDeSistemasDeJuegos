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
        private int jumpCount;
        private int baseJumpCount;

        public PlayerJumpState(PlayerController player, int jumpLimit) : base(player) 
        {
            jumpCount = jumpLimit;
            baseJumpCount = jumpCount;
        }

        public void ResetJumpCount()
            => jumpCount = baseJumpCount;

        public override bool IsAvailable()
            => jumpCount > 0;

        public override void OnEnter()
        {
            OnJump();
            player.character.SetDirection(player.character._direction);
        }

        public override void OnExit()
        {
            player.Move(player.character._direction, player.character.GroundCheck());
        }

        public override void Update()
        {
            if (player.character.FallCheck())
                player.playerStateManager.ChangeMovementState("Fall");
        }

        public override void OnJump()
        {
            if (jumpCount > 0)
            {
                player.character.Jump();
                jumpCount--;
            }
        }

        public override void OnMove(Vector3 direction)
            => player.character.SetDirection(direction);
    }
}
