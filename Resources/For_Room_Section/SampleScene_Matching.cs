using Photon.Pun;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Photon.Realtime;

public class SampleScene_Matching : MonoBehaviourPunCallbacks
{
    [SerializeField]
    private Button cancelRoomButton = default;
    [SerializeField]
    private GameObject Scroll_bar_content = default;

    private bool flag;
    private int playerCount;

    public TMP_Text Room_Member_prefab;

    private void Start()
    {
        flag = true;
        cancelRoomButton.interactable = false;
        PhotonNetwork.ConnectUsingSettings();
        cancelRoomButton.onClick.AddListener(OnCancelRoomButtonClick);
    }

    private void Room_Member_List_Update(string name)
    {
        TMP_Text member_text = Instantiate(Room_Member_prefab, Vector3.zero, Quaternion.identity).GetComponent<TMP_Text>();
        member_text.text = name;
        member_text.name = name;
        member_text.transform.parent = Scroll_bar_content.transform;
    }

    private void Room_Member_List_Delete(string name)
    {
        GameObject unactive_text = Scroll_bar_content.transform.Find(name).gameObject;
        Destroy(unactive_text);
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        Room_Member_List_Update(newPlayer.NickName);

        if (PhotonNetwork.IsMasterClient)
        {
            if (PhotonNetwork.CurrentRoom.PlayerCount == 2)
            {
                gameObject.SetActive(false);
                PhotonNetwork.IsMessageQueueRunning = false;
                Debug.Log("対戦相手が揃いました。バトルシーンに移動します。");
                SceneManager.LoadScene("GameScene");
            }
        }
    }

    public override void OnPlayerLeftRoom(Player other)
    {
        Room_Member_List_Delete(other.NickName);
    }

    private void OnCancelRoomButtonClick()
    {
        PhotonNetwork.LeaveRoom();
        foreach(Transform child in Scroll_bar_content.transform)
        {
            Destroy(child.gameObject);
        }
    }

    public override void OnJoinedRoom()
    {
        cancelRoomButton.interactable = true;
        Debug.Log("complete");
        foreach (var player in PhotonNetwork.PlayerList)
        {
            Room_Member_List_Update(player.NickName);
        }

        playerCount = PhotonNetwork.CurrentRoom.PlayerCount;
        if (playerCount != 2)
        {
            Debug.Log("待機中");
        }
        else
        {
            gameObject.SetActive(false);
            PhotonNetwork.IsMessageQueueRunning = false;
            SceneManager.LoadScene("GameScene");
            flag = false;
        }
        
            /*   var position = new Vector3(Random.Range(-3f, 3f), Random.Range(-3f, 3f));
           PhotonNetwork.Instantiate("Avatar", position, Quaternion.identity);

           if (PhotonNetwork.IsMasterClient)
           {
               PhotonNetwork.CurrentRoom.SetStartTime(PhotonNetwork.ServerTimestamp);
           }*/
    }
}
