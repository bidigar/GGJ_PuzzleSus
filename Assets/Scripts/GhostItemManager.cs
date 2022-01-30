using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostItemManager : MonoBehaviour
{
    public void InteractingWithItems(GameObject item) {
        GhostInteractableObject interactable = item.GetComponent<GhostInteractableObject>();
        if (interactable != null) {
            interactable.ObjectInteraction(this);
        }
    }
}
