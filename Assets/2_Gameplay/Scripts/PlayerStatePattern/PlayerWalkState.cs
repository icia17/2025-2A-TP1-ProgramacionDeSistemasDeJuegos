using UnityEngine;
using UnityEngine.InputSystem;

namespace Gameplay
{
    public class PlayerWalkState : PlayerMovementState
    {
        public PlayerWalkState(PlayerController player) : base(player) { }

        public override bool IsAvailable()
            => player.character.GroundCheck();

        public override void OnEnter()
        {

        }

        public override void OnExit()
        {

        }

        public override void Update() 
        {
            if (player.character.IdleCheck())
                player.playerStateManager.ChangeMovementState("Idle");
            else if (!player.character.GroundCheck())
                player.playerStateManager.ChangeMovementState("Fall");
        }

        public override void OnJump()
        {
            player.playerStateManager.ChangeMovementState("Jump");
        }

        public override void OnMove(Vector3 direction)
            => player.Move(direction, true);
    }
}
