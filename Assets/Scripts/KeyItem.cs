using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyItem : MonoBehaviour, IPickableItem
{
    public float mySizeX = 0.25f;
    public float mySizeY = 0.5f;
    public bool canBePickedByGhost{
        get{ return false; }
    }
    public bool wasUsed{
        get{ return false; }
    }
    public float itemSize{
        get{ return mySizeX; }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
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

    public void ItemAction () {
        Destroy(gameObject);
    }
}
