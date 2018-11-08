using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WaitStart
{
  public class StartManagerScript : MonoBehaviour
  {
    public string PhotonLobbySceneName;
    public string LoadSceneName;
    public GameObject SceneLoaderObject;

    private PhotonView sceneLoader = null;

    #region MonoBehaviour Callbacks

    void Awake()
    {
      sceneLoader = GetComponent<PhotonView>();
    }

    // Use this for initialization
    private void Start()
    {
      //########################################
      if (!PhotonNetwork.connected) // Photonに接続されていなければ
      {
        if (!string.IsNullOrEmpty(value: PhotonLobbySceneName))
        {
          UnityEngine.SceneManagement.SceneManager.LoadScene(PhotonLobbySceneName);
        }

        return;
      }

      //########################################
      if (PhotonNetwork.isMasterClient)
      {
        // buttonを生成

        // sceneLoaderを生成
        GameObject sceneLoader = PhotonNetwork.Instantiate(
          prefabName: this.SceneLoaderObject.name,
          position: new Vector3(0f, 0f, 0f),
          rotation: Quaternion.identity,
          group: 0);
      }
    }

    // Update is called once per frame
    void Update()
    {
      
    }

    #endregion

    #region OnClick Callbacks

    public void OnClick_StartButton()
    {
      Debug.Log(message: "=== OnClick_StartButton ===\n");
      sceneLoader.RPC(methodName: "LoadScene", target: PhotonTargets.All);
    }

    #endregion
    
    [PunRPC]
    private void LoadScene()
    {
      // エフェクトを生成.
      // 適当な時間が経過したら消すように設定.
      UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName: LoadSceneName);
    }

  }
}