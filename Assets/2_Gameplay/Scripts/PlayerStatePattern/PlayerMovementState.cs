using UnityEngine;
using UnityEngine.InputSystem;

namespace Gameplay
{
    public abstract class PlayerMovementState
    {
        protected PlayerController player;

        public PlayerMovementState(PlayerController player)
            => this.player = player;

        // FIX: IsAvailable added to add obligatory conditions to meet before confirming a state transition
        public abstract bool IsAvailable();
        public abstract void OnEnter();
        public abstract void OnExit();
        public abstract void Update();

        public abstract void OnMove(Vector3 direction);
        public abstract void OnJump();
    }
}
