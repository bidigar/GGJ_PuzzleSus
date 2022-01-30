using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    public static ItemManager Instance
    {
        get
        {
            if(m_instance == null)
            {
                m_instance = GameObject.FindObjectOfType<ItemManager>();
            }
            return m_instance;
        }
    }
    protected static ItemManager m_instance = null;
    public float mySizeX = 0.4f;
    public float mySizeY = 2;
    protected bool canUse = false;
    public KeyItem holdingItem;

    void Start()
    {
        if(m_instance == null)
            m_instance = this;
    }

    void Update()
    {
        if (Input.GetButtonDown("Fire2")) {
            if (holdingItem != null && canUse) {
                holdingItem.ReleaseItem();
                canUse = false;
                holdingItem = null;
            }
        }
    }

    private void FixedUpdate() {
        if (holdingItem != null) {
            UpdateItemPosition();
        }
    }

    void UpdateItemPosition() {
        float itemSize = (mySizeX + holdingItem.itemSize);
        holdingItem.gameObject.transform.position = transform.position + transform.forward * (mySizeX + itemSize) * 0.5f;
    }

    public void InteractingWithItems(GameObject item) {
        KeyItem pickedItem = item.GetComponent<KeyItem>();
        if (holdingItem == null && pickedItem != null) {
            holdingItem = pickedItem.PickItem().GetComponent<KeyItem>();
            StartCoroutine(WaitForUse());
            return;
        }

        InteractableObject interactable = item.GetComponent<InteractableObject>();
        if (interactable != null) {
            interactable.ObjectInteraction(this);
        }
    }
    
    public bool UseHoldingItem(InteractableObject callingObject) {
        if (holdingItem != null && canUse && holdingItem.ItemAction(callingObject)) {
            canUse = false;
            holdingItem = null;
            return true;
        }
        return false;
    }

    private IEnumerator WaitForUse() {
        yield return new WaitForSeconds(.1f);
        canUse = true;
    }
}
