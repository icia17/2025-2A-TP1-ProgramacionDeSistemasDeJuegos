using System.Collections.Generic;
using UnityEngine;

namespace Excercise1
{
    // This class used to implement Monobehaviour, which is not needed as we can instantiate this object without needing a Gameobject for it to reside in.
    // Now it implements ICharacterService, which is a service for the Service Locator. Now we can get this service from any class using the SL.
    public class CharacterService : ICharacterService
    {
        private static readonly Dictionary<string, ICharacter> _charactersById = new();

        public bool TryAddCharacter(string id, ICharacter character)
            => _charactersById.TryAdd(id, character);
        public bool TryRemoveCharacter(string id)
            => _charactersById.Remove(id);
        public bool TryGetCharacter(string id, out ICharacter character)
            => _charactersById.TryGetValue(id, out character);
    }
}
