using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MatchmakingView : MonoBehaviourPunCallbacks
{
    [SerializeField]
    private TMP_InputField passwordInputField = default;
    [SerializeField]
    private Button joinRoomButton = default;
    [SerializeField]
    private Button createRoomButton = default;
    [SerializeField]
    private TMP_Text playerText = default;
    [SerializeField]
    private Button selectButton = default;
    [SerializeField]
    private TMP_InputField playerInputField = default;
    [SerializeField]
    private TMP_Text roomText = default;

    private CanvasGroup canvasGroup;

  //  public TMP_Text Room_Member_prefab;

    private void Start()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        // マスターサーバーに接続するまでは、入力できないようにする
        canvasGroup.interactable = false;

        // パスワードを入力する前は、ルーム参加ボタンを押せないようにする
        joinRoomButton.interactable = false;
        createRoomButton.interactable = false;

        passwordInputField.interactable = false;

        selectButton.interactable = false;

        passwordInputField.onValueChanged.AddListener(OnPasswordInputFieldValueChanged);
        joinRoomButton.onClick.AddListener(OnJoinRoomButtonClick);
        //createRoomButton.onClick.AddListener(OnCreateRoomButtonClick);
        playerInputField.onValueChanged.AddListener(OnPlayerInputFieldChanged);
        selectButton.onClick.AddListener(OnCreateSelectButtonClick);
    }


    public override void OnConnectedToMaster()
    {
        // マスターサーバーに接続したら、入力できるようにする
        canvasGroup.interactable = true;
    }

    private void OnPasswordInputFieldValueChanged(string value)
    {
        // パスワードを6桁入力した時のみ、ルーム参加ボタンを押せるようにする
        joinRoomButton.interactable = (value.Length == 6);
    }

    private void OnJoinRoomButtonClick()
    {

        // ルームを非公開に設定する（新規でルームを作成する場合）
        var roomOptions = new RoomOptions();
        roomOptions.MaxPlayers = 2;
        roomOptions.IsVisible = false;

        roomText.text = passwordInputField.text;

        // パスワードと同じ名前のルームに参加する（ルームが存在しなければ作成してから参加する）
        PhotonNetwork.JoinOrCreateRoom(passwordInputField.text, roomOptions, TypedLobby.Default);
       
    }

    private void OnPlayerInputFieldChanged(string value)
    {
        selectButton.interactable = (value.Length > 0);
    }

    private void OnCreateSelectButtonClick()
    {
        PhotonNetwork.NickName = playerInputField.text;
        playerText.text = "player name is "+playerInputField.text;
        passwordInputField.interactable = true;
    }

    /*
    public override void OnJoinedRoom()
    {
        // ルームへの参加が成功したら、UIを非表示にする
        gameObject.SetActive(false);
    }*/

    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        // ルームへの参加が失敗したら、パスワードを再び入力できるようにする
        passwordInputField.text = string.Empty;
        canvasGroup.interactable = true;
        roomText.text = "error";
    }
}
