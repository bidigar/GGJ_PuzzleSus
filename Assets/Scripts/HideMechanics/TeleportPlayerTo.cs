using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportPlayerTo : MonoBehaviour
{
    [SerializeField] Transform hideTransform;
    [SerializeField] Transform leaveHideTransform;
    [SerializeField] Transform deathTransform;
    GhostPlayer ghostPlayer;
    public Ouija ouija;
    [SerializeField] CanvasGroup canvasGroup;
    [SerializeField] bool Debug;
    private void Start()
    {
        ghostPlayer = GetComponent<GhostPlayer>();
    }

    public void HidePosition(bool isHiding)
    {
        if (isHiding)
        {
            //DESATIVAR OUIJA
            if (!Debug) ghostPlayer.SetMovement(false);
            transform.position = hideTransform.position;
        }
        else
        {
            transform.position = leaveHideTransform.position;
            StartCoroutine(WaitForMovement());
        }
    }

    IEnumerator WaitForMovement()
    {
        yield return new WaitForSeconds(0.1f);
        if (!Debug) ghostPlayer.SetMovement(true);
        //REATIVAR OUIJA
    }
    public void DeathPosition()
    {
        // ScramblePuzzleLocation
        StartCoroutine(DeathCycle());
    }

    IEnumerator DeathCycle()
    {
        //DESATIVAR OUIJA
        if (!Debug) ghostPlayer.SetMovement(false);
        var transition = new WaitForSeconds(0.1f);
        while (canvasGroup.alpha != 1)
        {
            canvasGroup.alpha += 0.1f;
            yield return transition;
        }
        transform.position = deathTransform.position;
        while (canvasGroup.alpha != 0)
        {
            canvasGroup.alpha -= 0.1f;
            yield return transition;
        }
        yield return new WaitForSeconds(2f);
        if (!Debug) ghostPlayer.SetMovement(true);
    }

}
