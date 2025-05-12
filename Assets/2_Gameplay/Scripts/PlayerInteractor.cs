using System;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Gameplay
{
    public class PlayerInteractor : MonoBehaviour, IInteractor
    {
        [SerializeField] private InputActionReference interactInput;
        [SerializeField] private TMP_Text interactionText;
        private IInteractable _interactable;
        private string _textFormat = string.Empty;

        private void Awake()
        {
            if (interactionText)
            {
                _textFormat = interactionText.text;
                interactionText.SetText(""); // FIX: Instead of enabling/disabling the whole GameObject and causing stutters, we set the text component to nothing for faster loading time.
            }
        }

        private void OnEnable()
        {
            if (interactInput?.action != null)
                interactInput.action.started += HandleInteractInput;
        }
        
        private void OnDisable()
        {
            if (interactInput?.action != null)
                interactInput.action.started -= HandleInteractInput;
        }

        private void HandleInteractInput(InputAction.CallbackContext ctx)
        {
            if (_interactable == null)
                return;
            _interactable.Interact(this);
            interactionText.SetText("");
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out _interactable)
                && interactionText)
            {
                /*
                 * FIX: Instead of formatting the _interactable only and causing the text to appear as "(Item Class) GameObject.(Item Class)" we use _interactable.name so that only
                 * the name of the item appears "(Item Name)"
                */
                interactionText.SetText(string.Format(_textFormat, _interactable.Name));
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent(out IInteractable otherInteractable)
                && otherInteractable == _interactable)
            {
                _interactable = null;
                interactionText.SetText("");
            }
        }
    }
}