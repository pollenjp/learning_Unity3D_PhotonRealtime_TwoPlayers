using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PhotonManager : Photon.MonoBehaviour
{

	private string _userName, _userId;
	private InputField _inputFieldUserId;

	// Use this for initialization
	private void Start () {
	}
	
	// Update is called once per frame
	private void Update () {
		
	}
	
	//################################################################################
	public void ConnectPhoton()
	{
		PhotonNetwork.ConnectUsingSettings("v1.0");
	}
	
	public void OnJoinedLobby ()
	{
		Debug.Log ("PhotonManager OnJoinedLobby");
		ShowRoomLists();
	}
	
	//########################################
	public void ShowRoomLists()
	{
		Debug.Log(message: "===PhotonNetwork.GetRoomList===\n" +
		                   "ルーム一覧を表示\n" + PhotonNetwork.GetRoomList() + "\n\n");
	}
	
	//########################################
	// Check UserId(Name)
	// get userId, userName
	private void GetUserInfo()
	{
        _inputFieldUserId = GetComponent<InputField>();
		_userId = _inputFieldUserId.text;
		Debug.Log(_userId);
	}

	// ルームの作成
	public void CreateRoomButton(){
		GetUserInfo();
		
		// ref: http://sleepnel.hatenablog.com/entry/2016/05/29/120200
		var userName = "userName1";
		var userId   = "userId1";
		var RoomName = "test01";
		PhotonNetwork.autoCleanUpPlayerObjects = false;
		//カスタムプロパティ
		ExitGames.Client.Photon.Hashtable customProp = new ExitGames.Client.Photon.Hashtable();
		customProp.Add ("userName", userName); //ユーザ名
		customProp.Add ("userId", userId); //ユーザID
		PhotonNetwork.SetPlayerCustomProperties(customProp);

		RoomOptions roomOptions = new RoomOptions ();
		roomOptions.CustomRoomProperties = customProp;
		//ロビーで見えるルーム情報としてカスタムプロパティのuserName,userIdを使いますよという宣言
		roomOptions.CustomRoomPropertiesForLobby = new string[]{ "UserName","userId"};
		roomOptions.MaxPlayers = 2; //部屋の最大人数
		roomOptions.IsOpen = true; //入室許可する
		roomOptions.IsVisible = true; //ロビーから見えるようにする
		//userIdが名前のルームがなければ作って入室、あれば普通に入室する。
		Debug.Log(message: "====JoinOrCreateRoom==");
		PhotonNetwork.JoinOrCreateRoom(roomName: userId, roomOptions: roomOptions, typedLobby: null);
	}


}
