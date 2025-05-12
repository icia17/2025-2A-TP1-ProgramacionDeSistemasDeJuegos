using UnityEngine;
using UnityEngine.InputSystem;

namespace Gameplay
{
    public abstract class PlayerMovementState
    {
        protected PlayerController player;

        public PlayerMovementState(PlayerController player)
            => this.player = player;

        public abstract void OnEnter();
        public abstract void OnExit();
        public abstract void Update();

        public abstract void OnMove(Vector3 direction);
        public abstract void OnJump();
    }
}
