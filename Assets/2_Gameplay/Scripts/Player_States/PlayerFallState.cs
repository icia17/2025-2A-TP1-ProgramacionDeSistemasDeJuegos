using UnityEngine;
using UnityEngine.InputSystem;

namespace Gameplay
{
    public class PlayerFallState : PlayerMovementState
    {
        public PlayerFallState(PlayerController player) : base(player) { }

        public override void OnEnter()
        {

        }

        public override void OnExit()
        {

        }

        public override void Update() 
        {
            if (player.GroundCheck())
                player.ChangeMovementState(player.IdleCheck() ? player.playerIdleState : player.playerWalkState);
        }

        public override void OnJump()
            => player.ChangeMovementState(player.playerJumpState);

        public override void OnMove(InputAction.CallbackContext ctx)
            => player.Move(ctx, false);
    }
}
