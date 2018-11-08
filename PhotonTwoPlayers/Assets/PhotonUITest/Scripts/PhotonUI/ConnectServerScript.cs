using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace PhotonUI
{
  public class ConnectServerScript : MonoBehaviour
  {
    public GameObject Menu1, Menu2;
    
    public Text PhotonMasterServerAddress;
    public Text PhotonMasterServerPort;
    public Text PhotonAppId;
    public Text GameVersion;
    
    public Text PlayerIdText;
    static string playerNamePrefKey = "PlayerName";

    //####################################################################################################

    #region MonoBehaviour callbacks

    //########################################
    // Use this for initialization
    private void Start()
    {
      // 初期で表示するCanvasの設定
      Menu1.SetActive(value: true);
      Menu2.SetActive(value: true);
      Menu1.GetComponent<Canvas>().enabled = true;
      Menu2.GetComponent<Canvas>().enabled = false;
      
      //
      PhotonMasterServerPort.text = "5055";
      GameVersion.text = "1.0";
    }

    //########################################
    // Update is called once per frame
    private void Update()
    {
      
    }

    #endregion

    //####################################################################################################
    public void OnClick_ConnectServerButton()
    {
      //PhotonNetwork.ConnectUsingSettings(gameVersion: "v1.0");
      PhotonNetwork.ConnectToMaster(
        masterServerAddress: PhotonMasterServerAddress.text,
        port: int.Parse(PhotonMasterServerPort.text),
        appID: PhotonAppId.text,
        gameVersion: GameVersion.text);
    }

    //####################################################################################################

    #region Photon Callbacks

    private void OnConnectedToMaster()
    {
      PhotonNetwork.JoinLobby();
    }
    
    private void OnJoinedLobby()
    {
      Debug.Log("=== OnJoinedLobby ===\n");
      GameObject userIdInputField = UnityEngine.GameObject.Find("UserIdInputField");
      GameObject photonPlayerName = UnityEngine.GameObject.FindWithTag(tag: "PhotonPlayerName");
      Debug.Log(message: "== photonPlayerNameArr ==\n" + photonPlayerName);
      Text playerIdText = photonPlayerName.GetComponent<Text>();
      Debug.Log(message: "== PlayerIdText.text ==\n" + playerIdText.text);
      SetPlayerName(value: playerIdText.text);
      
      GameObject[] connectServerInputFieldTag = UnityEngine.GameObject.FindGameObjectsWithTag(tag: "ConnectServerInputField");
      foreach (var inputFieldGameObject in connectServerInputFieldTag)
      {
        var inputKeyword = inputFieldGameObject.GetComponent<InputFieldScript>().InputKeyword;
        var inputValue   = inputFieldGameObject.transform.Find(n: "Text").gameObject.GetComponent<Text>().text;
        Debug.Log(message: "== inputKeyword ==\n" + inputKeyword + "\n");
        Debug.Log(message: "== inputValue.text ==\n" + playerIdText.text + "\n");
        UnityEngine.PlayerPrefs.SetString(key: inputKeyword, value: inputValue);
      }
      
      //PlayerPrefs.SetString(InputKeyword, value);
      Debug.Log(message: "== PhotonNetwork.isMasterClient ==\n" + PhotonNetwork.isMasterClient);
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
      PhotonNetwork.playerName = value + " "; //今回ゲームで利用するプレイヤーの名前を設定
      Debug.Log(PhotonNetwork.player.NickName); //playerの名前の確認。（動作が確認できればこの行は消してもいい）
    }

    #endregion
  }
}