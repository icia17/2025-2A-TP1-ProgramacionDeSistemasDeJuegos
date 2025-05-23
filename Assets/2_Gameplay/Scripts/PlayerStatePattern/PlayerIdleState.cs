using UnityEngine;
using UnityEngine.InputSystem;

namespace Gameplay
{
    public class PlayerIdleState : PlayerMovementState
    {
        public PlayerIdleState(PlayerController player) : base(player) { }

        public override bool IsAvailable()
            => player.character.IdleCheck();

        public override void OnEnter()
        {

        }

        public override void OnExit()
        {

        }

        public override void Update() 
        {
            if (!player.character.GroundCheck())
                player.playerStateManager.ChangeMovementState("Fall");
            else if (!player.character.IdleCheck())
                player.playerStateManager.ChangeMovementState("Walk");
        }

        public override void OnJump()
        {
            player.playerStateManager.ChangeMovementState("Jump");
        }

        public override void OnMove(Vector3 direction)
        {
            player.playerStateManager.ChangeMovementState("Walk");
            player.Move(direction, true);
        }
    }
}
