using Mirror;
using UnityEngine;

public class ChestModule : NetworkBehaviour
{
    public GameObject Lid;

    public void DestroyLid ()
    {
        CmdDestroyLid();
    }

    [Command(requiresAuthority = false)]
    public void CmdDestroyLid()
    {
        RpcDestroyLid();
    }

    [ClientRpc]
    void RpcDestroyLid()
    {
        Destroy(Lid);
    }
}
