using UnityEngine;
using UnityEngine.InputSystem;

namespace Gameplay
{
    public class PlayerWalkState : PlayerMovementState
    {
        public PlayerWalkState(PlayerController player) : base(player) { }

        public override void OnEnter()
        {

        }

        public override void OnExit()
        {

        }

        public override void Update() 
        {
            if (player.IdleCheck())
                player.ChangeMovementState(player.playerIdleState);
            else if (!player.GroundCheck())
                player.ChangeMovementState(player.playerFallState);
        }

        public override void OnJump()
        {
            if (player.playerJumpState.CanJump())
                player.ChangeMovementState(player.playerJumpState);
        }

        public override void OnMove(Vector3 direction)
            => player.Move(direction, true);
    }
}
