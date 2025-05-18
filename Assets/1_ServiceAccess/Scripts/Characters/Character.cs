using System;
using UnityEngine;

namespace Excercise1
{
    public class Character : MonoBehaviour, ICharacter
    {
        [SerializeField] protected string id;

        protected virtual void OnEnable()
        {
            // DONE: Add to CharacterService. The id should be the given serialized field. 
            // FIX 1: Made CharacterService a Singleton and called the TryAddCharacter function directly from here.
            CharacterService.instance.TryAddCharacter(id, this);
        }

        protected virtual void OnDisable()
        {
            // DONE: Remove from CharacterService.
            // FIX 1: Made CharacterService a Singleton and called the TryRemoveCharacter function directly from here.
            CharacterService.instance.TryRemoveCharacter(id);
        }
    }
}