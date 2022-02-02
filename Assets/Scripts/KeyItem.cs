using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class KeyItem : MonoBehaviour, IPickableItem
{
    public InteractableObject interactableObject;
    public float mySizeX = 0.25f;
    public float mySizeY = 0.5f;
    public bool isDestroyable = true;
    [SerializeField] UnityEvent itemUsed = new UnityEvent();
    UnityEvent<GameObject> wasPicked = new UnityEvent<GameObject>();
    public bool canBePickedByGhost{
        get{ return false; }
    }
    public bool wasUsed{
        get{ return false; }
    }
    public float itemSize{
        get{ return mySizeX; }
    }

    protected bool alreadySubscribed = false;
    

    IEnumerator Start()
    {
        yield return new WaitForSeconds(1);
        Debug.Log("Item manager is " + ItemManager.Instance);
        if(GhostPlayer.localPlayer != null)
        {
            SubscribeToItemManager(GhostPlayer.localPlayer.itemManager);
        }
    }

    public void SubscribeToItemManager(ItemManager itemManager)
    {
        if(!alreadySubscribed)
        {
            wasPicked.AddListener(itemManager.InteractingWithItems);
            alreadySubscribed = true;
        }
    }

    public GameObject PickItem () {
        GetComponent<BoxCollider>().enabled = false;
        return this.gameObject;
    }

    public void ReleaseItem () {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit))
        {
            transform.position = hit.point + Vector3.up * mySizeY * 0.5f;
        }
        GetComponent<BoxCollider>().enabled = true;
    }

    public bool ItemAction (InteractableObject interactable = null) {
        if (interactable == null || (interactable != null && 
                    GameObject.ReferenceEquals(interactableObject.gameObject, interactable.gameObject))) {
            itemUsed.Invoke();
            if (isDestroyable) {
                Destroy(gameObject);
            }
            return true;
        }
        return false;
    }

    private void OnMouseDown() {
        RaycastHit hit;
        Vector3 direction = Camera.main.transform.position;
        Debug.Log("On mouse down");
        if (Physics.Raycast(transform.position, direction, out hit, 20f))
        {
            Debug.DrawRay(transform.position, direction, Color.red, 20f);
            Debug.Log("On mouse down " + hit.transform.gameObject.name);
            wasPicked.Invoke(gameObject);
        }
    }
}
