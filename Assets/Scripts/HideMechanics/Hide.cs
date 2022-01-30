using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Monster), typeof(Collider))]
public class Hide : MonoBehaviour
{
    [SerializeField] UnityEvent playerDeath = new UnityEvent();
    [SerializeField] UnityEvent<bool> playerHidding = new UnityEvent<bool>();

    Monster monster;
    
    bool playerHidden;
    bool hideTimerEnded;
    bool death;

    void Start()
    {
        monster = GetComponent<Monster>();
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
            if (Input.GetButton("Jump") && hideTimerEnded)
            {
                playerHidden = !playerHidden;
                playerHidding.Invoke(playerHidden);
                StartCoroutine(HidePlayer());
            }
        }
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
}
