using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PhotonManager : Photon.MonoBehaviour
{

	private string _userName, _userId;
	public InputField InputFieldUserId;
	public GameObject Menu1, Menu2;
	public Text RoomList;

	// Use this for initialization
	private void Start () {
	
	}
	
	// Update is called once per frame
	private void Update () {
		
	}
	
	//####################################################################################################
	// Connect Photon server
	public void ConnectPhotonServer()
	{
		PhotonNetwork.ConnectUsingSettings("v1.0");
	}
	// Join lobby (set "Auto-Join Lobby" at "PhotonServerSettings")
	public void OnJoinedLobby ()
	{
		Debug.Log ("PhotonManager OnJoinedLobby");
		ChangeCanvas(canvasGameObject1: Menu1, canvasGameObject2: Menu2);
		//ShowRoomLists();
	}
	//########################################
	// Connect server Menu --> Join Room Menu
	public void ChangeCanvas(GameObject canvasGameObject1, GameObject canvasGameObject2)
	{
		canvasGameObject1.GetComponent<Canvas>().enabled = false;
		canvasGameObject2.GetComponent<Canvas>().enabled = true;
	}
	
	//####################################################################################################
	public void ShowRoomLists()
	{
		Debug.Log(message: "===PhotonNetwork.GetRoomList===\n" +
		                   "ルーム一覧を表示\n" + PhotonNetwork.GetRoomList() + "\n\n");
		RoomList.text = "";  // init
		foreach (var roomInfo in PhotonNetwork.GetRoomList())
		{
			Debug.Log(roomInfo.Name);
			RoomList.text += roomInfo.ToString();
		}

		Debug.Log(RoomList.text);
	}
	
	public void OnReceivedRoomListUpdate() 
	{
		ShowRoomLists();
	} 

	
	//########################################
	// Check UserId(Name)
	// get userId, userName
	private void GetUserInfo()
	{
		_userId = InputFieldUserId.text;
		Debug.Log(_userId);
	}

	public void CreateRoomAndJoin()
	{
		var roomName = "test01";
		CreateRoom(roomName: roomName);
		//PhotonNetwork.JoinRoom(roomName: roomName);
	}
	
	public void OnPhotonCreateRoomFailed()
	{
		Debug.Log("OnPhotonCreateRoomFailed got called. This can happen if the room exists (even if not visible). Try another room name.");
		PhotonNetwork.JoinRoom(roomName: "test01");
	}


	// ルームの作成
	public void CreateRoom(string roomName){
		//GetUserInfo();
		
		// ref: http://sleepnel.hatenablog.com/entry/2016/05/29/120200
		var userName = "userName1";
		var userId   = "userId1";
		
		//PhotonNetwork.autoCleanUpPlayerObjects = false;
		//カスタムプロパティ
		ExitGames.Client.Photon.Hashtable customProp = new ExitGames.Client.Photon.Hashtable();
		customProp.Add ("userName", userName); //ユーザ名
		customProp.Add ("userId", userId); //ユーザID
		PhotonNetwork.SetPlayerCustomProperties(customProp);

		RoomOptions roomOptions = new RoomOptions ();
		roomOptions.CustomRoomProperties = customProp;
		//ロビーで見えるルーム情報としてカスタムプロパティのuserName,userIdを使いますよという宣言
		roomOptions.CustomRoomPropertiesForLobby = new string[]{ "userName","userId"};
		roomOptions.MaxPlayers = 2; //部屋の最大人数
		roomOptions.IsOpen = true; //入室許可する
		roomOptions.IsVisible = true; //ロビーから見えるようにする
		//userIdが名前のルームがなければ作って入室、あれば普通に入室する。
		Debug.Log(message: "===JoinOrCreateRoom===");
		PhotonNetwork.JoinOrCreateRoom(roomName: roomName, roomOptions: roomOptions, typedLobby: null);
		Debug.Log(message: "===JoinOrCreateRoom===");
	}
}
