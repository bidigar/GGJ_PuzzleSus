using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class Door : NetworkBehaviour
{
    AudioSource audioSource;
    [SerializeField] CanvasGroup canvasGroup;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    bool called = false;
    public void OpenDoor()
    {
        called = true;
        CmdOpenDoor();
    }

    IEnumerator WaitAndReturnToMenu()
    {
        var transition = new WaitForSeconds(0.1f);
        while (canvasGroup.alpha != 1)
        {
            canvasGroup.alpha += 0.1f;
            yield return transition;
        }
        if(called)
            CmdReturnToLobby();
    }

    [Command(requiresAuthority = false)]
    public void CmdReturnToLobby()
    {
        NetworkRoomManager nm = NetworkManager.singleton as NetworkRoomManager;
        nm.ServerChangeScene(nm.RoomScene);
    }

    [Command(requiresAuthority = false)]
    public void CmdOpenDoor()
    {
        RpcOpenDoor();
    }

    [ClientRpc]
    void RpcOpenDoor()
    {
        audioSource.Play();
        StartCoroutine(WaitAndReturnToMenu());
    }
}
