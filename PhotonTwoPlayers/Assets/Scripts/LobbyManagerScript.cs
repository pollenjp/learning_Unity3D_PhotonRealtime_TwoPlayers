﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LobbyManagerScript : Photon.MonoBehaviour
{
	
	#region Public Variables
    //部屋一覧表示用オブジェクト
    public GameObject RoomParent;//ScrolViewのcontentオブジェクト
    public GameObject RoomElementPrefab;//部屋情報Prefab

    //ルーム接続情報表示用Text
    public Text InfoText;
    #endregion

    #region MonoBehaviour CallBacks
    void Awake()
    {
        //ルーム内のクライアントがMasterClientと同じシーンをロードするように設定
        PhotonNetwork.automaticallySyncScene = true;
    }

    void Start()
    {
        //ルーム一覧取得
        GetRooms();
    }
    #endregion

    #region Public Methods
    public void GetRooms()
    {
    //roomInfoに現在存在するルーム情報を格納・更新
    RoomInfo[] roomInfo = PhotonNetwork.GetRoomList();

        //ルームが無ければreturn
        if (roomInfo == null || roomInfo.Length == 0) return;

        //ルームがあればRoomElementでそれぞれのルーム情報を表示
        for (int i = 0; i < roomInfo.Length; i++)
        {
            Debug.Log(roomInfo[i].Name + " : " + roomInfo[i].Name + "–" + roomInfo[i].PlayerCount + " / " + roomInfo[i].MaxPlayers /*+ roomInfo[i].CustomProperties["roomCreator"].ToString()*/);
                
            //ルーム情報表示用RoomElementを生成
            GameObject RoomElement = GameObject.Instantiate(RoomElementPrefab);
            
            //RoomElementをcontentの子オブジェクトとしてセット    
            RoomElement.transform.SetParent(RoomParent.transform);
            //RoomElementにルーム情報をセット
            RoomElement.GetComponent<RoomElementScript>().SetRoomInfo(roomInfo[i].Name, roomInfo[i].PlayerCount, roomInfo[i].MaxPlayers, roomInfo[i].CustomProperties["RoomCreator"].ToString());
        }
    }

    //RoomElementを一括削除
    public static void DestroyChildObject(Transform parent_trans)
    {
        for (int i = 0; i < parent_trans.childCount; ++i)
        {
            GameObject.Destroy(parent_trans.GetChild(i).gameObject);
        }
    }
    #endregion

//    #region Photon.PunBehaviour CallBacks
//
//    //GetRoomListは一定時間ごとに更新され、その更新のタイミングで実行する処理
//    public override void OnReceivedRoomListUpdate()
//    {            
//        DestroyChildObject(RoomParent.transform);   //RoomElementを削除
//        GetRooms();     //RoomElementを再生成
//    }
//    
//    //ルーム作成失敗した場合
//    public override void OnPhotonCreateRoomFailed(object[] codeAndMsg)
//    {
//        //テキストを表示
//        InfoText.text = "ルームの作成に失敗しました";
//    }
//
//    //ルームの入室に失敗した場合
//    public override void OnPhotonJoinRoomFailed(object[] codeAndMsg)
//    {
//        //テキストを表示
//        InfoText.text = "ルームの入室に失敗しました";
//    }
//    #endregion
}
