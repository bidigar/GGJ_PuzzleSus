using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPickableItem
{
    float itemSize{
        get;
    }
    bool canBePickedByGhost{
        get;
    }
    bool wasUsed{
        get;
    }
    GameObject PickItem ();
    void ReleaseItem();
    void ItemAction();
}
