using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InteractableObject : MonoBehaviour
{
    [SerializeField] bool isGhost;
    [SerializeField] UnityEvent objectUsed = new UnityEvent();
    public bool wasUsed = false;
    public bool isDestroyable = false;
    public bool isUsed = false;
    UnityEvent<GameObject> wasClicked = new UnityEvent<GameObject>();
    public bool needKeyItem = true;

    IEnumerator Start()
    {
        yield return null;
        yield return null;

        if (!isGhost) wasClicked.AddListener(ItemManager.Instance.InteractingWithItems);
        else wasClicked.AddListener(ItemManager.Instance.InteractingWithItems);
    }

    public void ObjectInteraction(ItemManager manager) {
        if (!wasUsed && ((needKeyItem && manager.UseHoldingItem(this)) || !needKeyItem)) {
            objectUsed.Invoke();
            if (isUsed) wasUsed = true;
            if (isDestroyable) Destroy(this.gameObject);
        }
    }

    private void OnMouseDown() {
        RaycastHit hit;
        Vector3 direction = Camera.main.transform.position;
        if (Physics.Raycast(transform.position, direction, out hit, 20f))
        {
            wasClicked.Invoke(gameObject);
        }
    }

}
