using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;

[RequireComponent(typeof(Monster), typeof(Collider))]
public class Hide : MonoBehaviour
{
    [SerializeField] UnityEvent playerDeath = new UnityEvent();
    [SerializeField] UnityEvent<bool> playerHidding = new UnityEvent<bool>();
    [SerializeField] TextMeshProUGUI pressTo;
    [SerializeField] GameObject pressSpace;

    Monster monster;

    bool showText;
    bool playerHidden;
    bool hideTimerEnded;
    bool death;

    void Start()
    {
        monster = GetComponent<Monster>();
        showText = false;
        playerHidden = false;
        hideTimerEnded = true;
        death = false;
    }

    void Update()
    {
        if (monster.MonsterOn && !playerHidden && !death)
        {
            playerDeath.Invoke();
            death = true;
            StartCoroutine(DeathReset());
            print("u dead son");
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (playerHidden)
            {
                pressTo.text = "to leave"; 
            }
            else
            {
                pressTo.text = "to hide";
            }
            if (!showText) StartCoroutine(ShowText());
            if (Input.GetButton("Jump") && hideTimerEnded)
            {
                playerHidden = !playerHidden;
                playerHidding.Invoke(playerHidden);
                StartCoroutine(HidePlayer());
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        showText = false;
    }


    IEnumerator HidePlayer()
    {
        hideTimerEnded = false;
        yield return new WaitForSeconds(1f);
        hideTimerEnded = true;
    }

    IEnumerator DeathReset()
    {
        yield return new WaitForSeconds(5f);
        death = false;
    }

    IEnumerator ShowText()
    {
        showText = true;
        var wait = new WaitForSeconds(0.1f);
        while (showText)
        {
            pressSpace.SetActive(true);
            yield return wait;
        }
        pressSpace.SetActive(false);
    }
}
