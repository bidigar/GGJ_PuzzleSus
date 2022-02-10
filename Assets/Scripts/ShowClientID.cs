using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using EpicTransport;

public class ShowClientID : MonoBehaviour
{
    TextMeshProUGUI text;
    void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
        if (text != null) text.text = EOSSDKComponent.LocalUserProductIdString;
    }

}
