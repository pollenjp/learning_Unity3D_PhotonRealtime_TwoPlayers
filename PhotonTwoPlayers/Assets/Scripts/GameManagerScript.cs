using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManagerScript : MonoBehaviour
{

	public GameObject PlayerPrefab;
	
	// Use this for initialization
	private void Start () {
		if (!PhotonNetwork.connected)  // Photonに接続されていなければ
		{
			SceneManager.LoadScene("PhotonLobbyScene");
			return;
		}

		GameObject player = PhotonNetwork.Instantiate(
			prefabName: this.PlayerPrefab.name,
			position: new Vector3(0f, 0f, 0f),
			rotation: Quaternion.identity,
			group: 0);
	}
	
	// Update is called once per frame
	private void Update () {
		
	}
}
