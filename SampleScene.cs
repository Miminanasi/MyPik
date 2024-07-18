using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

// MonoBehaviourPunCallbacks���p�����āAPUN�̃R�[���o�b�N���󂯎���悤�ɂ���
public class SampleScene : MonoBehaviourPunCallbacks
{

    [SerializeField] Transform onionPos1;
    [SerializeField] Transform onionPos2;
    ///////////////////////////////////////////////////////////////////////////////////////////////////�e�X�g����
    private void Start()
    {
        PhotonNetwork.IsMessageQueueRunning = true;
        var localPlayer = PhotonNetwork.LocalPlayer;
        // �����_���ȍ��W�Ɏ��g�̃A�o�^�[�i�l�b�g���[�N�I�u�W�F�N�g�j�𐶐�����
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

        //else if (PhotonNetwork.CountOfPlayers == 2)//���ꂪ2��s���Ă���\������
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
        // �v���C���[���g�̖��O��"Player"�ɐݒ肷��
        PhotonNetwork.NickName = "Player";

        // PhotonServerSettings�̐ݒ���e���g���ă}�X�^�[�T�[�o�[�֐ڑ�����
        PhotonNetwork.ConnectUsingSettings();
    }

    // �}�X�^�[�T�[�o�[�ւ̐ڑ��������������ɌĂ΂��R�[���o�b�N
    public override void OnConnectedToMaster()
    {
        // "Room"�Ƃ������O�̃��[���ɎQ������i���[�������݂��Ȃ���΍쐬���ĎQ������j
        PhotonNetwork.JoinOrCreateRoom("Room", new RoomOptions(), TypedLobby.Default);
    }

    // �Q�[���T�[�o�[�ւ̐ڑ��������������ɌĂ΂��R�[���o�b�N
    public override void OnJoinedRoom()
    {
        var localPlayer = PhotonNetwork.LocalPlayer;
        // �����_���ȍ��W�Ɏ��g�̃A�o�^�[�i�l�b�g���[�N�I�u�W�F�N�g�j�𐶐�����
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
        
        //else if (PhotonNetwork.CountOfPlayers == 2)//���ꂪ2��s���Ă���\������
        else if(localPlayer.ActorNumber==2)
        {
            GameObject avatar_object_2=PhotonNetwork.Instantiate("Avatar", onionPos2.position, Quaternion.identity);
            AvatarController avatar_2 = avatar_object_2.GetComponent<AvatarController>();
            avatar_2.onionPos = onionPos2;
            Debug.Log(avatar_2.onionPos);
        }
    }*/
}