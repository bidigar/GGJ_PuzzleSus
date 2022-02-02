using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;


public class ColorBoxManager : NetworkBehaviour
{
    [SerializeField] GameObject key;
    [SerializeField] GameObject holeOne;
    [SerializeField] GameObject holeTwo;
    [SerializeField] GameObject holeThree;
    [SerializeField] ParticleSystem ghostParticle;
    ColorChange holeOne_ColorChange;
    ColorChange holeTwo_ColorChange;
    ColorChange holeThree_ColorChange;

    private void Start()
    {
        holeOne_ColorChange = holeOne.GetComponent<ColorChange>();
        holeTwo_ColorChange = holeTwo.GetComponent<ColorChange>();
        holeThree_ColorChange = holeThree.GetComponent<ColorChange>();
        StartCoroutine(LookForColorChange());
    }

    public void GhostTouch()
    {
        CmdGhostTouch();
    }

    private void Update()
    {
        if (holeOne_ColorChange.ThisColor == Color.Lerp(Color.red, Color.yellow, 0.5f) && holeTwo_ColorChange.ThisColor == Color.green && holeThree_ColorChange.ThisColor == Color.blue)
        {
            key.SetActive(true);
            CmdDestroyBox();
        }
    }

    IEnumerator LookForColorChange()
    {
        yield return new WaitForSeconds(0.5f);
        var waitEOF = new WaitForEndOfFrame();
        Color fisrtColor = holeOne_ColorChange.ThisColor;
        Color secondColor = holeTwo_ColorChange.ThisColor;
        Color thirdColor = holeThree_ColorChange.ThisColor;
        while (true)
        {
            if (fisrtColor != holeOne_ColorChange.ThisColor || secondColor != holeTwo_ColorChange.ThisColor || thirdColor != holeThree_ColorChange.ThisColor)
            {
                fisrtColor = holeOne_ColorChange.ThisColor;
                secondColor = holeTwo_ColorChange.ThisColor;
                thirdColor = holeThree_ColorChange.ThisColor;

                CmdColorChange(fisrtColor, secondColor, thirdColor);
            }
            yield return waitEOF;
        }
    }

    [Command(requiresAuthority = false)]
    public void CmdDestroyBox()
    {
        RpcDestroyBox();
    }

    [ClientRpc]
    void RpcDestroyBox()
    {
        Destroy(gameObject);
    }

    [Command(requiresAuthority = false)]
    public void CmdGhostTouch()
    {
        RpcGhostTouch();
    }

    [ClientRpc]
    void RpcGhostTouch()
    {
        holeOne_ColorChange.Enable();
        holeTwo_ColorChange.Enable();
        holeThree_ColorChange.Enable();
        CmdColorChange(holeOne_ColorChange.ThisColor, holeTwo_ColorChange.ThisColor, holeThree_ColorChange.ThisColor);
        ghostParticle.gameObject.SetActive(false);
    }

    [Command(requiresAuthority = false)]
    public void CmdColorChange(Color first, Color second, Color third)
    {
        RpcColorChange(first, second, third);
    }

    [ClientRpc]
    void RpcColorChange(Color first, Color second, Color third)
    {
        holeOne_ColorChange.ThisMeshRenderer.material.color = first;
        holeTwo_ColorChange.ThisMeshRenderer.material.color = second;
        holeThree_ColorChange.ThisMeshRenderer.material.color = third;
    }
}
