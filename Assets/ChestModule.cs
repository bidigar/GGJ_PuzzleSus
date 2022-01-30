using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestModule : MonoBehaviour
{
    public GameObject Lid;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DestroyLid () {
        Destroy(Lid);
    }
}
