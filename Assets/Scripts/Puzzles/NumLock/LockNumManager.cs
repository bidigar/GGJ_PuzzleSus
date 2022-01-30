using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LockNumManager : MonoBehaviour
{
    [SerializeField] GameObject wardrobeDoor;
    //[SerializeField] Animator wheelOne;
    //[SerializeField] Animator wheelTwo;
    //[SerializeField] Animator wheelThree;
    //[SerializeField] Animator wheelFour;
    [SerializeField] TextMeshPro wheelOne_TextMeshPro;
    [SerializeField] TextMeshPro wheelTwo_TextMeshPro;
    [SerializeField] TextMeshPro wheelThree_TextMeshPro;
    [SerializeField] TextMeshPro wheelFour_TextMeshPro;

    int[] password = new int[4] {3,6,9,2};
    int[] currentTry = new int[4] {0,0,0,0};

    public void OpenDoor()
    {
        //wardrobe.Play("OpenDoor");
        Destroy(wardrobeDoor);
    }

    public void TurnWheelOne()
    {
        //wheelOne.Play("Turn");
        currentTry[0]++;
        if (currentTry[0] > 9) currentTry[0] = 0;
        wheelOne_TextMeshPro.text = currentTry[0].ToString();
    }

    public void TurnWheelTwo()
    {
        //wheelTwo.Play("Turn");
        currentTry[1]++;
        if (currentTry[1] > 9) currentTry[1] = 0;
        wheelTwo_TextMeshPro.text = currentTry[1].ToString();
    }

    public void TurnWheelThree()
    {
        //wheelThree.Play("Turn");
        currentTry[2]++;
        if (currentTry[2] > 9) currentTry[2] = 0;
        wheelThree_TextMeshPro.text = currentTry[2].ToString();
    }

    public void TurnWheelFour()
    {
        //wheelFour.Play("Turn");
        currentTry[3]++;
        if (currentTry[3] > 9) currentTry[3] = 0;
        wheelFour_TextMeshPro.text = currentTry[3].ToString();
    }

    private void Update()
    {
        if (currentTry[0] == password[0] && currentTry[1] == password[1] && currentTry[2] == password[2] && currentTry[3] == password[3])
        {
            print("entrou");
            OpenDoor();
            Destroy(gameObject);
        }
    }
}
