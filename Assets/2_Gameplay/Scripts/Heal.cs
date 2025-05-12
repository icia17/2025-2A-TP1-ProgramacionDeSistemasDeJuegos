using Excercise1;
using UnityEngine;

namespace Gameplay
{
    public class Heal : MonoBehaviour, IInteractable
    {
        public string Name { get => this.GetType().Name; }

        public void Interact(IInteractor target)
        {
            Debug.Log($"{name}({Name.Colored("#555555")}): Healed {target.transform.name} :)");
            gameObject.SetActive(false);
        }
    }
}