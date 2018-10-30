using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace PhotonUI
{
	
    public class ConnectServerScript : MonoBehaviour {
        
        public GameObject Menu1, Menu2;
        public Text PlayerIdText;
        static string playerNamePrefKey = "PlayerName";

        //####################################################################################################
        #region MonoBehaviour callbacks
        //########################################
        // Use this for initialization
        private void Start () {
            // 初期で表示するCanvasの設定
            Menu1.GetComponent<Canvas>().enabled = true;
            Menu2.GetComponent<Canvas>().enabled = false;
        }
        //########################################
        // Update is called once per frame
        private void Update () {
        
        }
        #endregion

        //####################################################################################################
        public void OnClick_ConnectServerButton()
        {
            PhotonNetwork.ConnectUsingSettings(gameVersion: "v1.0");
        }

        //####################################################################################################
        #region Photon Callbacks
        public void OnJoinedLobby()
        {
            Debug.Log("=== OnJoinedLobby ===\n");
            SetPlayerName(value: PlayerIdText.text);
            Debug.Log(message: PlayerIdText.text);
            ChangeCanvas(canvasGameObject1: Menu1, canvasGameObject2: Menu2);
        }
        #endregion
        
        //####################################################################################################
        #region Public Functions
        // Connect server Menu --> Join Room Menu
        public void ChangeCanvas(GameObject canvasGameObject1, GameObject canvasGameObject2)
        {
            canvasGameObject1.GetComponent<Canvas>().enabled = false;
            canvasGameObject2.GetComponent<Canvas>().enabled = true;
        }
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
