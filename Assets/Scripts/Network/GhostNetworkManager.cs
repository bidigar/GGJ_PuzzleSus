using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using System.Linq;

public class GhostNetworkManager : NetworkRoomManager
{
    public GameObject humanPlayerPrefab;
    public GameObject ghostPlayerPrefab;

    protected bool humanIsRandom = true;

    protected NetworkConnection playerToBeHuman;


    /// <summary>
    /// Override to choose player before starting server
    /// </summary>
    public override void OnRoomServerPlayersReady()
    {
        if(humanIsRandom)
        {
            int randomPlayer = Random.Range(0,NetworkServer.connections.Count);
            playerToBeHuman = NetworkServer.connections.ElementAt(0).Value;
        }
        
        // calling the base method calls ServerChangeScene as soon as all players are in Ready state.
        #if UNITY_SERVER
                    base.OnRoomServerPlayersReady();
        #else
                    showStartButton = true;
        #endif
    }
    

    /// <summary>
    /// Change default player object
    /// </summary>
    /// <param name="conn"></param>
    /// <param name="roomPlayer"></param>
    /// <returns></returns>
    public override GameObject OnRoomServerCreateGamePlayer(NetworkConnection conn, GameObject roomPlayer)
    {
        // get start position from base class
        Transform startPos = GetStartPosition();
        GameObject prefabToInstantiate = ShouldPlayerBeHuman(conn) ? humanPlayerPrefab : ghostPlayerPrefab;

        if(startPos != null)
            return Instantiate(prefabToInstantiate, startPos.position, startPos.rotation);
        else
            return Instantiate(prefabToInstantiate, Vector3.zero, Quaternion.identity);
    }

    //Just to find if player should be ghost. Can be custom logic
    protected bool ShouldPlayerBeHuman(NetworkConnection conn)
    {
        return conn == playerToBeHuman;
    }

    /*
            This code below is to demonstrate how to do a Start button that only appears for the Host player
            showStartButton is a local bool that's needed because OnRoomServerPlayersReady is only fired when
            all players are ready, but if a player cancels their ready state there's no callback to set it back to false
            Therefore, allPlayersReady is used in combination with showStartButton to show/hide the Start button correctly.
            Setting showStartButton false when the button is pressed hides it in the game scene since NetworkRoomManager
            is set as DontDestroyOnLoad = true.
        */

        bool showStartButton;

        public override void OnGUI()
        {
            base.OnGUI();

            if (allPlayersReady && showStartButton && GUI.Button(new Rect(150, 300, 120, 20), "START GAME"))
            {
                // set to false to hide it in the game scene
                showStartButton = false;

                ServerChangeScene(GameplayScene);
            }
        }

}
