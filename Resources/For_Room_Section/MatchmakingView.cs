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
        // �}�X�^�[�T�[�o�[�ɐڑ�����܂ł́A���͂ł��Ȃ��悤�ɂ���
        canvasGroup.interactable = false;

        // �p�X���[�h����͂���O�́A���[���Q���{�^���������Ȃ��悤�ɂ���
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
        // �}�X�^�[�T�[�o�[�ɐڑ�������A���͂ł���悤�ɂ���
        canvasGroup.interactable = true;
    }

    private void OnPasswordInputFieldValueChanged(string value)
    {
        // �p�X���[�h��6�����͂������̂݁A���[���Q���{�^����������悤�ɂ���
        joinRoomButton.interactable = (value.Length == 6);
    }

    private void OnJoinRoomButtonClick()
    {

        // ���[�������J�ɐݒ肷��i�V�K�Ń��[�����쐬����ꍇ�j
        var roomOptions = new RoomOptions();
        roomOptions.MaxPlayers = 2;
        roomOptions.IsVisible = false;

        roomText.text = passwordInputField.text;

        // �p�X���[�h�Ɠ������O�̃��[���ɎQ������i���[�������݂��Ȃ���΍쐬���Ă���Q������j
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
        // ���[���ւ̎Q��������������AUI���\���ɂ���
        gameObject.SetActive(false);
    }*/

    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        // ���[���ւ̎Q�������s������A�p�X���[�h���Ăѓ��͂ł���悤�ɂ���
        passwordInputField.text = string.Empty;
        canvasGroup.interactable = true;
        roomText.text = "error";
    }
}
