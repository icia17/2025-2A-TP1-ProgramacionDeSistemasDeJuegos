using System.Collections.Generic;
using UnityEngine;

namespace Gameplay
{
    public class PlayerStateManager
    {
        /*
         * FIX: to ease the expansion of player movement capabilities, I added a State Pattern that switches the current Player Movement State
         * depending on the players input. This will make it easier to expand and create new movement mechanics while keeping code clean and
         * understandable.
        */
        private PlayerMovementState currentMovementState;

        private Dictionary<string, PlayerMovementState> availableMovementStates = new Dictionary<string, PlayerMovementState>();

        public PlayerStateManager(PlayerMovementState startingState)
            => currentMovementState = startingState;

        public void AddMovementState(string stateId, PlayerMovementState newState)
        {
            if (!availableMovementStates.TryAdd(stateId, newState))
                Debug.LogWarning($"Couldn't store new state! State: {newState} - State ID: {stateId}");
        }

        public void ChangeMovementState(string stateId)
        {
            if (availableMovementStates.ContainsKey(stateId))
            {
                PlayerMovementState newState;
                availableMovementStates.TryGetValue(stateId, out newState);

                if (newState.IsAvailable())
                {
                    currentMovementState.OnExit();
                    currentMovementState = newState;
                    currentMovementState.OnEnter();

                    Debug.Log($"New Movement State: {currentMovementState.GetType().Name}");
                }
                else
                {
                    Debug.Log($"You do not meet conditions to change state to {stateId}!");
                }
            }
            else
            {
                Debug.LogWarning($"There is no {stateId} movement state inside the available movement states!");
            }
        }

        public void HandleUpdate()
            => currentMovementState.Update();

        public void HandleMove(Vector3 moveDirection)
            => currentMovementState.OnMove(moveDirection);

        public void HandleJump()
            => currentMovementState.OnJump();
    }
}
