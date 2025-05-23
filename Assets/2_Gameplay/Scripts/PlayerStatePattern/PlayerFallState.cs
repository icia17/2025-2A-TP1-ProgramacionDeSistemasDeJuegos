using UnityEngine;
using UnityEngine.InputSystem;

namespace Gameplay
{
    public class PlayerFallState : PlayerMovementState
    {
        public PlayerFallState(PlayerController player) : base(player) { }

        public override bool IsAvailable()
            => player.character.FallCheck();

        public override void OnEnter()
        {

        }

        public override void OnExit()
        {
            player.Move(player.character._direction, player.character.GroundCheck());
        }

        public override void Update() 
        {
            if (player.character.GroundCheck())
                player.playerStateManager.ChangeMovementState(player.character.IdleCheck() ? "Idle" : "Walk");
        }

        public override void OnJump()
        {
            player.playerStateManager.ChangeMovementState("Jump");
        }

        public override void OnMove(Vector3 direction)
            => player.Move(direction, false);
    }
}
