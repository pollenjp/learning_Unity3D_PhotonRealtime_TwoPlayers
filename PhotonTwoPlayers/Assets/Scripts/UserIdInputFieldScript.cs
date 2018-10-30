using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UserIdInputFieldScript : MonoBehaviour {
	
	#region Private Variable Definition
	static string playerNamePrefKey = "PlayerName";
	#endregion

	//####################################################################################################
	#region MonoBehaviour callbacks
	// Use this for initialization
	void Start () {
		InputField inputField = this.GetComponent<InputField>();
 
		//前回プレイ開始時に入力した名前をロードして表示
		if (inputField != null)
		{
			if (PlayerPrefs.HasKey(playerNamePrefKey))
			{
				inputField.text = PlayerPrefs.GetString(playerNamePrefKey);
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	#endregion
	
	//####################################################################################################
	#region Public Method

	public void SetPlayerName(string value)
	{
		PhotonNetwork.playerName = value + " ";     //今回ゲームで利用するプレイヤーの名前を設定
		PlayerPrefs.SetString(playerNamePrefKey, value);    //今回の名前をセーブ
		Debug.Log(PhotonNetwork.player.NickName);   //playerの名前の確認。（動作が確認できればこの行は消してもいい）
	}

	#endregion
}
