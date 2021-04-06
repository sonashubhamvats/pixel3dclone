using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;
public class LoungeManager : MonoBehaviourPunCallbacks
{
    public Button enterGame;
    public GameObject EntryGamePanel,ConnectionStatusPanel,LobbyPanel;
    // Start is called before the first frame update

    #region  UnityMethods
    private void Awake() {
        enterGame.onClick.AddListener(()=>ConnectToPhotonNetwork());
        //the level is synced automatically for all others 
        PhotonNetwork.AutomaticallySyncScene=true;
    }
    
    private void Start() {
        EntryGamePanel.SetActive(true);
        ConnectionStatusPanel.SetActive(false);
        LobbyPanel.SetActive(false);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    //on button click connect to the network
    

    #endregion
    #region Photon Callbacks
    //on connected to master
    public override void OnConnectedToMaster()
    {
        EntryGamePanel.SetActive(false);
        ConnectionStatusPanel.SetActive(false);
        LobbyPanel.SetActive(true);
        Debug.Log(PhotonNetwork.NickName+"Connected to photon server");
    }
    //when connected to internet
    public override void OnConnected()
    {
        Debug.Log("Internet connected");
    }
    //if the user failed to join the room 
    public override void OnJoinRandomFailed(short returnCode, string message)
    {
       
        base.OnJoinRoomFailed(returnCode, message);
        CreateAndJoinRoom();
    }
    //when the master joins the room
    public override void OnJoinedRoom()
    {
        //for other clients other than thw master client the level will be loaded even before this joined room method
        base.OnJoinedRoom();
        Debug.Log(PhotonNetwork.NickName+" joined to "+PhotonNetwork.CurrentRoom.Name);
        PhotonNetwork.LoadLevel("GameScene");
    }
    //when other clients join the room 
    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        base.OnPlayerEnteredRoom(newPlayer);
        Debug.Log(newPlayer.NickName+" joined to "+PhotonNetwork.CurrentRoom.Name+" "+PhotonNetwork.CurrentRoom.PlayerCount);
    }
    #endregion
    #region Private Methods
    void CreateAndJoinRoom()
    {
        string randomRoomName="Room "+Random.Range(0,1000);
        RoomOptions roomOptions=new RoomOptions();
        roomOptions.IsOpen=true;
        roomOptions.IsVisible=true;
        roomOptions.MaxPlayers=20;
        PhotonNetwork.CreateRoom(randomRoomName,roomOptions);
    }
    #endregion
    #region Public Methods
    public void ConnectToPhotonNetwork()
    {
        if(!PhotonNetwork.IsConnected)
        {
            PhotonNetwork.ConnectUsingSettings();    
            EntryGamePanel.SetActive(false);
            ConnectionStatusPanel.SetActive(true);
        }
    }
    public void JoinRandomRoom()
    {
        //joining a room  
        PhotonNetwork.JoinRandomRoom();
    }
    #endregion
}
