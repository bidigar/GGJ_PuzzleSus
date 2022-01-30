using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mirror;

public class MainMenu : MonoBehaviour
{
    public InputField addressInputField;
    public GameObject loadingCanvas;
    /*
    void OnGUI()
        {
            GUILayout.BeginArea(new Rect(10 + offsetX, 40 + offsetY, 215, 9999));
            if (!NetworkClient.isConnected && !NetworkServer.active)
            {
                StartButtons();
            }
            else
            {
                StatusLabels();
            }

            // client ready
            if (NetworkClient.isConnected && !NetworkClient.ready)
            {
                if (GUILayout.Button("Client Ready"))
                {
                    NetworkClient.Ready();
                    if (NetworkClient.localPlayer == null)
                    {
                        NetworkClient.AddPlayer();
                    }
                }
            }

            StopButtons();

            GUILayout.EndArea();
        }
    */

    public void JoinGame()
    {
        NetworkManager.singleton.networkAddress = addressInputField.text;
        NetworkManager.singleton.StartClient();
    }

    public void HostGame()
    {
        NetworkManager.singleton.StartHost();
    }
    

    void Update()
    {
        loadingCanvas.SetActive(NetworkClient.active);
    }
}
