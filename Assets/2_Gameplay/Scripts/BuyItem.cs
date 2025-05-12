using Excercise1;
using UnityEngine;

namespace Gameplay
{
    public class BuyItem : MonoBehaviour, IInteractable
    {
        [SerializeField] private string itemName;
        public string Name { get => itemName; }

        public void Interact(IInteractor target)
        {
            // FIX: Now the item name is correctly shown inside the parentheses instead of "Heal"
            Debug.Log($"{name}({Name.Colored("#555555")}): {target.transform.name} now has item {Name}");
            gameObject.SetActive(false);
        }
    }
}