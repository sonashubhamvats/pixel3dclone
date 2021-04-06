using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class PlayerNameInputManager : MonoBehaviour
{
    public void SetPlayerName(string playerName)
    {
         Debug.Log("Here");
        if(string.IsNullOrEmpty(playerName))
        {
            Debug.Log("Player name is null");
            return;
        }
        PhotonNetwork.NickName=playerName;
    
    }
}
