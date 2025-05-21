using UnityEngine;

namespace Excercise1
{
    public class ServiceInitializer : MonoBehaviour
    {
        // For now a simple initializer with two lines of code, more services could be added if expanded.
        private void Awake()
            => ServiceLocator.AddService<ICharacterService>(new CharacterService(), true);       
    }
}
