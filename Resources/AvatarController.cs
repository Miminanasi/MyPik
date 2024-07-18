using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AvatarController : MonoBehaviourPunCallbacks, IPunObservable
{

    private Ray ray;
    private GameObject clickedGameObject;
    private RaycastHit hit;
    private Vector3 networkPosition;
    private Quaternion networkRotation;
    private float flag = 0;
    private int num = 0;
    private int count1 = 0;
    private int Life = 5;

    [SerializeField] float speed;
    [SerializeField] float rotSpeed;
    [SerializeField] float gravSpeed;
    [SerializeField] GameObject PikminPrefab;
    [SerializeField] Transform playerGathPos;
    [SerializeField] Rigidbody rigid;
    [SerializeField] Animator anim;

    public Transform onionPos;
    public Slider slider;
    public List<Pikmin> followingPikmins;
    public List<Pikmin> MyPikmins;
   
    public static class Define
    {
        public const int pik_id_size = 100;//Lifeが0命令
    }


    [PunRPC]
    private void Onion_Message(string onion)
    {
        GameObject onion_game_object = GameObject.Find(onion).gameObject;
        onionPos = onion_game_object.transform;
        Debug.Log(onion);
    }

    //自分がダメージを受ける際に呼ばれる
    [PunRPC]
    private void Damage2()
    {

        if (slider.value == 0)
        {
            //Debug.Log("負け");
        }
        else
        {
            slider.value--;
        }
    }
    
    public void Damage()
    {
        photonView.RPC(nameof(Damage2), RpcTarget.All);
    }

    //pikminがagentの目的地を変える際に呼ばれる
    [PunRPC]
    private void Pik_Agent_Message2(int pikmin_id,Vector3 purpose_position)
    {
        Pikmin pik = MyPikmins[pikmin_id - 1];
        pik.Agent_change(purpose_position);
    }

    public void Pik_Agent_Message(int pikmin_id, Vector3 purpose_position)
    {
        photonView.RPC(nameof(Pik_Agent_Message2), RpcTarget.All, pikmin_id,purpose_position);
    }

    //pikminがダメージを受ける際に呼ばれる
    [PunRPC]
    private void Damage_Pik2(int pikmin_damaged_num)
    {
        Pikmin pik = MyPikmins[pikmin_damaged_num-1];
        pik.Life--;
        if (pik.Life == 0)
        {
            pik.Life_0();
        }
    }

    public void Damage_Pik(int pikmin_damaged_num)
    {
        photonView.RPC(nameof(Damage_Pik2), RpcTarget.All, pikmin_damaged_num);
    }

    //pikminを作る際に呼ばれる
    [PunRPC]
    private void Makepik()
    {
        
        string name;
        Pikmin pik = Instantiate(PikminPrefab, new Vector3(onionPos.position.x, onionPos.position.y-5, onionPos.position.z), Quaternion.identity).GetComponent<Pikmin>();
        num++;
        pik.name = "Pikmin" + num;
        pik.identitynum = num;

        pik.PlayerGathPos = playerGathPos;
        pik.homePos = onionPos;
        pik.avatarcontroller = this;
        pik.player_position = this.transform;
        MyPikmins.Add(pik);

        if (photonView.IsMine)
        {
            pik.ally = true;
            pik.tag = "Ally_Pikmin";
        }
        else if (photonView.IsMine == false)
        {
            pik.ally = false;
            pik.tag = "Enemy_Pikmin";
        }

        if (count1 < 10)
        {
            //this.Pikmin_follow_message(num);//Pikmin's followさせる
            followingPikmins.Add(pik);
            pik.follow = true;
        }
        else
        {
            pik.follow = false;
        }

        count1 = followingPikmins.Count;
    }

    [PunRPC]
    private void Click_Order(Vector3 hit_position)
    {
        Pikmin pik = followingPikmins[0];
        pik.follow = false;
        followingPikmins.RemoveAt(0);
        Debug.Log("hit_positionは");
        Debug.Log(hit_position);
        pik.Agent_change(hit_position);
        //pik.Click_order_from_avatar(hit_position);
        count1--;
    }

    private void Start()
    {
        if (photonView.IsMine == false)
        {
            GameObject camera = transform.Find("MainCamera").gameObject;
            Destroy(camera);
            GameObject life_bar = transform.Find("Canvas").gameObject;
            Destroy(life_bar);
            //slider.value = 10;
            transform.tag = "Your_Avatar";
        }

        else 
        {
            transform.tag = "My_Avatar";
            slider.value = 10;
            photonView.RPC(nameof(Onion_Message),RpcTarget.All,onionPos.name);
        }
    }

    private void Update()
    {
        
        if (photonView.IsMine)
        {
            if (Input.GetMouseButtonDown(0))
            {
                ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                Debug.Log("Raycast");
                Debug.Log(ray);
                Debug.DrawRay(ray.origin, ray.direction * 1000, Color.yellow, 10, true);
                //コライダーに当たるためアバターのコライダーの見えていない部分に反応する
                if (Physics.Raycast(ray, out hit, 1000f))
                {
                    clickedGameObject = hit.collider.gameObject;
                    Debug.Log(clickedGameObject.name);

                    if (clickedGameObject.CompareTag("Ally_Pikmin"))
                    {
                        Pikmin pik = clickedGameObject.GetComponent<Pikmin>();
                        pik.follow = true;
                        followingPikmins.Add(pik);
                        count1++;
                    }

                    else
                    {
                        if (count1 != 0)
                        {
                            Debug.Log("Raycasthit");
                            Debug.Log(hit.point);
                            photonView.RPC(nameof(Click_Order), RpcTarget.All, hit.point);
                        }
                    }
                }
            }

            if (Input.GetKeyDown(KeyCode.X))
            {
                flag = 1;
                photonView.RPC(nameof(Makepik),RpcTarget.All);
                //Debug.Log(onionPos.name);
            }

            float x = Input.GetAxis("Horizontal") * speed;
            float y = Input.GetAxis("Vertical") * speed;

            rigid.velocity = (transform.forward * y + Vector3.down * gravSpeed);
            transform.Rotate(new Vector3(0, x, 0) * rotSpeed * Time.deltaTime);

        }

    }

  
    void IPunObservable.OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info){}
}