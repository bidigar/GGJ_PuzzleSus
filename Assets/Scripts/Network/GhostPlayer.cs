using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class GhostPlayer : NetworkBehaviour
{
    public static GhostPlayer localPlayer;
    public GameObject localPlayerOnly;
    public bool isGhost;

    public Camera playerCamera;

    void Start()
    {
        if(isLocalPlayer)
            localPlayer = this;

        localPlayerOnly.SetActive(isLocalPlayer);

        if(isLocalPlayer && isGhost)
            RenderSettings.fog = false;
    }

    public void SetMovement(bool movement)
    {
        IMovement[] movables = GetComponentsInChildren<IMovement>();
        for (int i = 0; i < movables.Length; i++)
        {
            movables[i].CanMove = movement;
        }
    }
}
