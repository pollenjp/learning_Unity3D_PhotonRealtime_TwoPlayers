using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LobbyUIScript : MonoBehaviour {
	
	//部屋作成ウインドウ表示用ボタン
	public Button OpenRoomPanelButton;
 
	//部屋作成ウインドウ
	public GameObject CreateRoomPanel;    //部屋作成ウインドウ
	public Text       RoomNameText;       //作成する部屋名
	public Slider     PlayerNumberSlider; //最大入室可能人数用Slider
	public Text       PlayerNumberText;   //最大入室可能人数表示用Text
	public Button     CreateRoomButton;   //部屋作成ボタン
	
	//########################################
	//部屋作成ウインドウ表示用ボタンを押したときの処理
	public void OnClick_OpenRoomPanelButton()
	{
		// 部屋作成ウインドウが表示していれば => 部屋作成ウインドウを非表示に
		// そうでなければ                 => 部屋作成ウインドウを表示
		if (CreateRoomPanel.activeSelf)
		{
			CreateRoomPanel.SetActive(false);
		}
		else
		{
			CreateRoomPanel.SetActive(true);
		}
	}
	//########################################
	//部屋作成ボタンを押したときの処理
	public void OnClick_CreateRoomButton()
	{
		//作成する部屋の設定
		RoomOptions roomOptions = new RoomOptions();
		roomOptions.IsVisible = true;   //ロビーで見える部屋にする
		roomOptions.IsOpen = true;      //他のプレイヤーの入室を許可する
		roomOptions.MaxPlayers = (byte)PlayerNumberSlider.value;    //入室可能人数を設定
		//ルームカスタムプロパティで部屋作成者を表示させるため、作成者の名前を格納
		roomOptions.CustomRoomProperties = new ExitGames.Client.Photon.Hashtable()
		{
			{ "RoomCreator",PhotonNetwork.playerName }
		};
		//ロビーにカスタムプロパティの情報を表示させる
		roomOptions.CustomRoomPropertiesForLobby = new string[] {
			"RoomCreator",
		};
 
		//部屋作成
		PhotonNetwork.CreateRoom(RoomNameText.text,roomOptions,null);
	}

	//####################################################################################################
	// Use this for initialization
	private void Start () {
		
	}
	
	// Update is called once per frame
	private void Update () {
		//部屋人数Sliderの値をTextに代入
		PlayerNumberText.text = PlayerNumberSlider.value.ToString();
	}
 
}
