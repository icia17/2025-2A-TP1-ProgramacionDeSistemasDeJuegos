using UnityEngine;

namespace Excercise1
{
    public interface ICharacterService
    {
        bool TryAddCharacter(string id, ICharacter character);
        bool TryRemoveCharacter(string id);
        bool TryGetCharacter(string id, out ICharacter character);
    }
}
