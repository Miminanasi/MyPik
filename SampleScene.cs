using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

// MonoBehaviourPunCallbacksを継承して、PUNのコールバックを受け取れるようにする
public class SampleScene : MonoBehaviourPunCallbacks
{

    [SerializeField] Transform onionPos1;
    [SerializeField] Transform onionPos2;
    ///////////////////////////////////////////////////////////////////////////////////////////////////テスト部分
    private void Start()
    {
        PhotonNetwork.IsMessageQueueRunning = true;
        var localPlayer = PhotonNetwork.LocalPlayer;
        // ランダムな座標に自身のアバター（ネットワークオブジェクト）を生成する
        var position = new Vector3(Random.Range(-3f, 3f), Random.Range(-3f, 3f));
        //PhotonNetwork.Instantiate("Avatar", position, Quaternion.identity);
        //       if (PhotonNetwork.CountOfPlayers < 2)
        if (localPlayer.ActorNumber == 1)
        {
            GameObject avatar_object_1 = PhotonNetwork.Instantiate("main_character", onionPos1.position, Quaternion.identity);
            AvatarController avatar_1 = avatar_object_1.GetComponent<AvatarController>();
            avatar_1.onionPos = onionPos1;
            //   Debug.Log(avatar_1.onionPos);
        }

        //else if (PhotonNetwork.CountOfPlayers == 2)//これが2回行われている可能性あり
        else if (localPlayer.ActorNumber == 2)
        {
            GameObject avatar_object_2 = PhotonNetwork.Instantiate("main_character", onionPos2.position, Quaternion.identity);
            AvatarController avatar_2 = avatar_object_2.GetComponent<AvatarController>();
            avatar_2.onionPos = onionPos2;
            Debug.Log(avatar_2.onionPos);
        }
    }
    //////////////////////////////////////////////////////////////////////////////////////////////////////////
   /* private void Start()
    {
        // プレイヤー自身の名前を"Player"に設定する
        PhotonNetwork.NickName = "Player";

        // PhotonServerSettingsの設定内容を使ってマスターサーバーへ接続する
        PhotonNetwork.ConnectUsingSettings();
    }

    // マスターサーバーへの接続が成功した時に呼ばれるコールバック
    public override void OnConnectedToMaster()
    {
        // "Room"という名前のルームに参加する（ルームが存在しなければ作成して参加する）
        PhotonNetwork.JoinOrCreateRoom("Room", new RoomOptions(), TypedLobby.Default);
    }

    // ゲームサーバーへの接続が成功した時に呼ばれるコールバック
    public override void OnJoinedRoom()
    {
        var localPlayer = PhotonNetwork.LocalPlayer;
        // ランダムな座標に自身のアバター（ネットワークオブジェクト）を生成する
        var position = new Vector3(Random.Range(-3f, 3f), Random.Range(-3f, 3f));
        //PhotonNetwork.Instantiate("Avatar", position, Quaternion.identity);
 //       if (PhotonNetwork.CountOfPlayers < 2)
        if(localPlayer.ActorNumber==1)
        {
            GameObject avatar_object_1=PhotonNetwork.Instantiate("Avatar", onionPos1.position, Quaternion.identity);
            AvatarController avatar_1 = avatar_object_1.GetComponent<AvatarController>();
            avatar_1.onionPos = onionPos1;
         //   Debug.Log(avatar_1.onionPos);
        }
        
        //else if (PhotonNetwork.CountOfPlayers == 2)//これが2回行われている可能性あり
        else if(localPlayer.ActorNumber==2)
        {
            GameObject avatar_object_2=PhotonNetwork.Instantiate("Avatar", onionPos2.position, Quaternion.identity);
            AvatarController avatar_2 = avatar_object_2.GetComponent<AvatarController>();
            avatar_2.onionPos = onionPos2;
            Debug.Log(avatar_2.onionPos);
        }
    }*/
}