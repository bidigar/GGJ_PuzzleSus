using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    public bool isGhost = false;
    public float mySizeX = 0.4f;
    public float mySizeY = 2;
    protected bool canUse = false;
    public GameObject holdingItem;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Jump")) {
            if (holdingItem != null && canUse) {
                holdingItem.GetComponent<IPickableItem>().ItemAction();
                canUse = false;
                holdingItem = null;
            }
        }
        if (Input.GetButtonDown("Fire1")) {
            if (holdingItem != null && canUse) {
                holdingItem.GetComponent<IPickableItem>().ReleaseItem();
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

    public void GrabbingItem(GameObject newItem) {
        IPickableItem pickableItem = newItem.GetComponent<IPickableItem>();
        if (pickableItem != null && pickableItem.canBePickedByGhost == isGhost) {
            holdingItem = pickableItem.PickItem();
        }
    }

    void UpdateItemPosition() {
        float itemSize = (mySizeX + holdingItem.GetComponent<IPickableItem>().itemSize);
        holdingItem.gameObject.transform.position = transform.position + transform.forward * (mySizeX + itemSize) * 0.5f;
    }

    private void OnTriggerStay(Collider other) {
        if(Input.GetButtonDown("Jump")){
            IPickableItem newItem = other.gameObject.GetComponent<IPickableItem>();
            if (holdingItem == null) {
                holdingItem = newItem.PickItem();
                StartCoroutine(WaitForUse());
            }
        }
    }

    private IEnumerator WaitForUse() {
        yield return new WaitForSeconds(.1f);
        canUse = true;
    }
}
