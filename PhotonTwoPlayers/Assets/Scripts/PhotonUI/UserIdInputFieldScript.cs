using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace PhotonUI
{
    public class UserIdInputFieldScript : MonoBehaviour {
      
      #region Variable Definition
      public InputField UserIdInputField;
      static string playerNamePrefKey = "PlayerName";
      #endregion
      //####################################################################################################
      #region MonoBehaviour callbacks
      // Use this for initialization
      void Start () {
        //前回プレイ開始時に入力した名前をロードして表示
        if (UserIdInputField != null)
        {
          if (PlayerPrefs.HasKey(playerNamePrefKey))
          {
            UserIdInputField.text = PlayerPrefs.GetString(playerNamePrefKey);
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
        Debug.Log(message: "=== SetPlayerName ===\n" + value + "\n");
        PhotonNetwork.playerName = value + " ";     //今回ゲームで利用するプレイヤーの名前を設定
        PlayerPrefs.SetString(playerNamePrefKey, value);    //今回の名前をセーブ
        Debug.Log(PhotonNetwork.player.NickName);   //playerの名前の確認。（動作が確認できればこの行は消してもいい）
      }
      #endregion
    }
}
