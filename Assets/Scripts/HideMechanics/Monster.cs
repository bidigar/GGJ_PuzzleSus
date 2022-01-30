using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Hide))]
public class Monster : MonoBehaviour
{
    bool _monsterOn;
    public bool MonsterOn => _monsterOn;
    [SerializeField] float minRandomTime;
    [SerializeField] float maxRandomTime;
    //Red Eyes

    private void Start()
    {
        StartCoroutine(MonsterAppearenceTimer());
    }

    IEnumerator MonsterAppearenceTimer()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(minRandomTime, maxRandomTime));
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
