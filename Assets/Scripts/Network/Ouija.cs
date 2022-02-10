using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Mirror;

public class Ouija : NetworkBehaviour
{
    public float cameraDistance = .3f;

    [SyncVar(hook = nameof(OnChangeControl))]
    protected bool IsUnderControl;

    public GameObject mainObject;
    public Transform cursorTransform;
    public Collider cursorCollider;

    public UnityEvent OnStartRemoteControl = new UnityEvent();
    protected bool visible;
    protected Camera mainCamera;
    protected Transform cameraTrasform;
    protected bool isClicking;
    protected Transform myTransform;

    public bool CanUseOuija
    {
        get
        {
            return GhostPlayer.localPlayer != null && GhostPlayer.localPlayer.isGhost;
        }
    }

    

    IEnumerator Start()
    {
        yield return null; //Wait one frame
        myTransform = transform;
        mainCamera = Camera.main;
        cameraTrasform = mainCamera.transform;
    }

    public void SetVisible(bool isVisible)
    {
        mainObject.SetActive(isVisible);
        visible = isVisible;
        
        if(GhostPlayer.localPlayer != null)
        {
            GhostPlayer.localPlayer.SetMovement(!isVisible);
            Cursor.lockState = (isVisible) ? CursorLockMode.None : CursorLockMode.Locked;
        }
    }

    public void OnChangeControl(bool wasControled, bool isControled)
    {
        if(!hasAuthority && isControled && OnStartRemoteControl != null)
            OnStartRemoteControl.Invoke();
    }

    public override void OnStartAuthority()
    {
        MeshRenderer renderer = cursorTransform.GetComponentInChildren<MeshRenderer>();
        if(renderer != null)
        {
            renderer.material.color = Color.red;
        }

        cursorCollider.gameObject.SetActive(false);
        base.OnStartAuthority();
    }

    public override void OnStopAuthority()
    {
        MeshRenderer renderer = cursorTransform.GetComponentInChildren<MeshRenderer>();
        if(renderer != null)
        {
            renderer.material.color = Color.white;
        }

        cursorCollider.gameObject.SetActive(true);
        base.OnStopAuthority();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Tab) && !hasAuthority)
            SetVisible(!visible);

        if(!visible)
        {
            return;
        }

        if(isClicking && Input.GetMouseButtonUp(0))
            isClicking = false;

        //Try to get control. Should check to hit cursor
        if(CanUseOuija && !IsUnderControl && Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;

            if(Physics.Raycast(mainCamera.ScreenPointToRay(Input.mousePosition), out hit, 1))
            {
                if(hit.transform == cursorCollider.transform)
                {
                    CmdTakeControl();
                    isClicking = true;
                }
            }
        }


        if(hasAuthority)
        {
            if(!isClicking)
            {
                CmdRemoveControl(cursorTransform.position);
            }
            else
            {
                RaycastHit hit;

                if(Physics.Raycast(mainCamera.ScreenPointToRay(Input.mousePosition), out hit, 1))
                {
                    if(hit.transform != null)
                    {
                        Vector3 projectOnPlane = myTransform.InverseTransformPoint(hit.point);
                        projectOnPlane.y = 0;
                        cursorTransform.localPosition = projectOnPlane;
                    }
                }

            }
        }
    }

    void LateUpdate()
    {
        if(visible)
        {
            myTransform.position = cameraTrasform.position + cameraTrasform.forward * cameraDistance;
            myTransform.rotation = Quaternion.LookRotation(cameraTrasform.up, -cameraTrasform.forward);
        }
    }

    [Command(requiresAuthority = false)]
    protected void CmdTakeControl(NetworkConnectionToClient sender = null)
    {
        if(sender != null)
        {
            netIdentity.AssignClientAuthority(sender);
            IsUnderControl = true;
        }
    }

    [Command]
    protected void CmdRemoveControl(Vector3 endPosition)
    {
        netIdentity.RemoveClientAuthority();
        IsUnderControl = false;

        // GetComponent<NetworkTransformBase>()?.ServerTeleport(endPosition);
    }
}
