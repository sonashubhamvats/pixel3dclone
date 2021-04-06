using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.SceneManagement;
public class PixelGunGameManager : MonoBehaviourPunCallbacks
{
    [SerializeField]
    private GameObject playerPrefab;
    // Start is called before the first frame update
    public static PixelGunGameManager instance;
    private void Awake() {
        if(instance!=null)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance=this;
        }
    }
    void Start()
    {
        if(PhotonNetwork.IsConnected)
        {
            if(playerPrefab!=null)
            {
                PhotonNetwork.Instantiate(playerPrefab.name,new Vector3(Random.Range(-40,40),-0.74f,Random.Range(-30,30)),Quaternion.identity);

            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    //master class
    public override void OnJoinedRoom()
    {
        base.OnJoinedRoom();
        Debug.Log(PhotonNetwork.NickName+" joined "+PhotonNetwork.CurrentRoom.Name);
    }
    //other  clients 
    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        base.OnPlayerEnteredRoom(newPlayer);
        Debug.Log(newPlayer.NickName+" joined "+PhotonNetwork.CurrentRoom.Name+" with a count of "+PhotonNetwork.CurrentRoom.PlayerCount);
    }
    public override void OnLeftRoom()
    {
        base.OnLeftRoom();
        SceneManager.LoadScene("GameLauncherScene");
    }
    public void LeaveRoom()
    {
        PhotonNetwork.LeaveRoom();
    }
}
