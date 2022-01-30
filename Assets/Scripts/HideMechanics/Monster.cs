using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Hide))]
public class Monster : MonoBehaviour
{
    bool _monsterOn;
    public bool MonsterOn => _monsterOn;
    //Red Eyes

    private void Start()
    {
        StartCoroutine(MonsterAppearenceTimer());
    }

    IEnumerator MonsterAppearenceTimer()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(50f, 150f));
            //play sounds approaching
            yield return new WaitForSeconds(5f);
            //play sounds grownling sound
            _monsterOn = true;
            yield return new WaitForSeconds(5f);
            _monsterOn = false;
            //play sounds leaving
            yield return new WaitForSeconds(1f);
        }
    }
}
