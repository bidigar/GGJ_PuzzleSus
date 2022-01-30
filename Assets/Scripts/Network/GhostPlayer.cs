using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class GhostPlayer : NetworkBehaviour
{
    public static GhostPlayer localPlayer;
    public GameObject localPlayerOnly;
    public ItemManager itemManager;
    public bool isGhost;

    public Camera playerCamera;

    void Start()
    {
        if(isLocalPlayer)
            localPlayer = this;

        localPlayerOnly.SetActive(isLocalPlayer);

        if(isLocalPlayer && isGhost)
            RenderSettings.fog = false;

        if(isLocalPlayer)
            FindIteractables();
    }

    public void SetMovement(bool movement)
    {
        IMovement[] movables = GetComponentsInChildren<IMovement>();
        for (int i = 0; i < movables.Length; i++)
        {
            movables[i].CanMove = movement;
        }
    }

    public void FindIteractables()
    {
        KeyItem[] keys = GameObject.FindObjectsOfType<KeyItem>();
        for (int i = 0; i < keys.Length; i++)
            keys[i].SubscribeToItemManager(itemManager);

        InteractableObject[] ios = GameObject.FindObjectsOfType<InteractableObject>();
        for (int i = 0; i < ios.Length; i++)
            ios[i].SubscribeToItemManager(itemManager,isGhost);
    }
}
