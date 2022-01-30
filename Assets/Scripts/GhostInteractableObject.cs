using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GhostInteractableObject : MonoBehaviour
{
    [SerializeField] bool isGhost;
    [SerializeField] UnityEvent objectUsed = new UnityEvent();
    public bool wasUsed = false;
    public bool isDestroyable = false;
    UnityEvent<GameObject> wasClicked = new UnityEvent<GameObject>();

    void Start()
    {
        if (!isGhost) wasClicked.AddListener(GameObject.FindGameObjectWithTag("Ghost").GetComponent<GhostItemManager>().InteractingWithItems);
        else wasClicked.AddListener(GameObject.FindGameObjectWithTag("Ghost").GetComponent<GhostItemManager>().InteractingWithItems);
    }

    public void ObjectInteraction(GhostItemManager manager) {
        if (!wasUsed) {
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
