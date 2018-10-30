using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ConnectServerScript : MonoBehaviour {

	public void OnClick_ConnectServerButton()
	{
		PhotonNetwork.ConnectUsingSettings(gameVersion: "v1.0");
	}

	public void OnJoinedLobby()
	{
		Debug.Log("=== OnJoinedLobby ===");
        SceneManager.LoadScene("TwoPlayerScene");
	}

	#region MonoBehaviour callbacks

	// Use this for initialization
	private void Start () {
		
	}
	
	// Update is called once per frame
	private void Update () {
		
	}
	#endregion
	
}
