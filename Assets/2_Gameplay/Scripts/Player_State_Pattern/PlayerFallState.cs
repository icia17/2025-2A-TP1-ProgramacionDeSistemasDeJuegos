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
            player.Move(player.GetDirection(), player.GroundCheck());
        }

        public override void Update() 
        {
            if (player.GroundCheck())
                player.ChangeMovementState(player.IdleCheck() ? player.playerIdleState : player.playerWalkState);
        }

        public override void OnJump()
        {
            if (player.playerJumpState.CanJump())
                player.ChangeMovementState(player.playerJumpState);
        }

        public override void OnMove(Vector3 direction)
            => player.Move(direction, false);
    }
}
