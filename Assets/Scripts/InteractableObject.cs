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
    UnityEvent<GameObject> wasClicked = new UnityEvent<GameObject>();
    public bool needKeyItem = true;

    void Start()
    {
        if (!isGhost) wasClicked.AddListener(GameObject.FindGameObjectWithTag("Player").GetComponent<ItemManager>().InteractingWithItems);
        else wasClicked.AddListener(GameObject.FindGameObjectWithTag("Player").GetComponent<ItemManager>().InteractingWithItems);
    }

    public void ObjectInteraction(ItemManager manager) {
        if (!wasUsed && ((needKeyItem && manager.UseHoldingItem(this)) || !needKeyItem)) {
            objectUsed.Invoke();
            wasUsed = true;
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
