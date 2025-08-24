using System;
using UnityEngine;


public class PlayerInteract : MonoBehaviour
{
    [SerializeField] private float interactRange = 1.5f;
    [SerializeField] private LayerMask interactableLayer;
    [SerializeField] Player player;
    [SerializeField] CircleCollider2D interactionCollider;
    
    private Interactable currentInteractable;

    
    private void Start()
    {
        interactionCollider.radius = interactRange;
        interactionCollider.includeLayers += interactableLayer;
        InputManager.Instance.OnInteract += HandleInteract;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Interactable interactable;
        if (other.TryGetComponent(out interactable))
        {
            currentInteractable = interactable;
        }
        else
        {
            currentInteractable = null;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        Interactable interactable;
        if (other.TryGetComponent(out interactable))
        {
            if (currentInteractable == interactable)
            {
                currentInteractable = null;
            }
        }
    }

    private void HandleInteract(object sender, EventArgs e)
    {
        if (currentInteractable != null)
        {
            currentInteractable.Interact(player);
        }
    }
}
