using UnityEngine;
using UnityEngine.InputSystem;

namespace Gameplay
{
    public class PlayerIdleState : PlayerMovementState
    {
        public PlayerIdleState(PlayerController player) : base(player) { }

        public override void OnEnter()
        {

        }

        public override void OnExit()
        {

        }

        public override void Update() 
        {
            if (!player.GroundCheck())
                player.ChangeMovementState(player.playerFallState);
            else if (!player.IdleCheck())
                player.ChangeMovementState(player.playerWalkState);
        }

        public override void OnJump()
        {
            if (player.playerJumpState.CanJump())
                player.ChangeMovementState(player.playerJumpState);
        }

        public override void OnMove(InputAction.CallbackContext ctx)
        {
            player.ChangeMovementState(player.playerWalkState);
            player.Move(ctx);
        }
    }
}
