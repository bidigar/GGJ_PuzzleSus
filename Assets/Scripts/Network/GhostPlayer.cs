using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class GhostPlayer : NetworkBehaviour
{
    public GameObject localPlayerOnly;

    void Start()
    {
        localPlayerOnly.SetActive(isLocalPlayer);
    }
}
