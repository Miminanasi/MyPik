using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class Pikmin : MonoBehaviour
{
    private RaycastHit hit;
    private Ray ray;
    private int Destroy_flag=0;
    private int time_interval = 1;

    [SerializeField] NavMeshAgent agent;
    [SerializeField] public bool follow;
    //[SerializeField] Animator anim;

    
    public Transform player_position; 
    public AvatarController avatarcontroller;
    public Transform PlayerGathPos;
    public Transform homePos;
    public int identitynum;
    public bool ally;
    public bool click;
    public int Life = 5;
    public CapsuleCollider Attack_capsuleCollider;

    public ObjectController targetObject;
    public bool goHome;
    //public GameObject Avatar;

    /*    public void Follow()
        {
            //agentをplayerGathposに設定
            agent.SetDestination(PlayerGathPos.position);
        }*/

    IEnumerator Stop_time(int time)
    {
        yield return new WaitForSeconds(time);
    }

    public void Life_0()
    {
        gameObject.SetActive(false);

        transform.position = homePos.position;
       
    }

    public void Agent_change(Vector3 position)
    {
        if (gameObject.activeSelf)
        {
            Debug.Log("agentのポイントは");
            Debug.Log(position);
            agent.SetDestination(position);
        }
    }

    
    public void Damage()
    {
        avatarcontroller.Damage_Pik(this.identitynum);
    }
    
    private void OnTriggerEnter(Collider other)
    {
       // Attack_capsuleCollider.enabled = true;
    }

    private void OnTriggerStay(Collider other)
    {

        if (other.gameObject.CompareTag("Your_Avatar"))
        {
            if (gameObject.CompareTag("Ally_Pikmin"))
            {
                Debug.Log(other.gameObject.tag);
                Debug.Log("testtesttesttest");
                follow = false;
                avatarcontroller.Pik_Agent_Message(this.identitynum, other.transform.position);
            }
        }

        if (other.gameObject.CompareTag("Enemy_Pikmin") || other.gameObject.CompareTag("Ally_Pikmin"))
        {
            if (!other.CompareTag(transform.tag))
            {

                follow = false;
                avatarcontroller.Pik_Agent_Message(this.identitynum,other.transform.position);
                //agent.SetDestination(other.transform.position);
                //   Debug.Log("test");
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy_Pikmin") || other.gameObject.CompareTag("Ally_Pikmin"))
        {
            if (!other.CompareTag(transform.tag))
            {
                
            }
        }
    }

    private void Start()
    {
        //follow = true;
    }
    private void Update()
    {

        float arrivedDistance = 10;
        float followDistance = 12;

        if (this.gameObject.CompareTag("Ally_Pikmin"))
        {
            time_interval++;
            Stop_time(time_interval);
        }
        if (follow)//プレイヤーについてくる
        {
            transform.LookAt(player_position);
            Debug.Log("followはture");
            agent.SetDestination(PlayerGathPos.position);
            //avatarcontroller.Pik_Agent_Message(this.identitynum, PlayerGathPos.position);//ここが怪しい
            if (agent.remainingDistance < arrivedDistance)
            {
                //  agent.isStopped = true;
                //follow = false;
            }
            else if (agent.remainingDistance > followDistance)
            {
                //agent.isStopped = false;
                //follow = true;
            }
        }
        /*
        if (goHome)//拠点に戻る
        {
            //agent.SetDestination(homePos.transform.position);
            avatarcontroller.Pik_Agent_Message(this.identitynum, homePos.transform.position);
            if (Vector3.Distance(transform.position, homePos.transform.position) <= 0.85f)
            {
                avatarcontroller.followingPikmins.Add(this);
                avatarcontroller.followingPikmins.Add(this);
                follow = true;
                goHome = false;
                Destroy(targetObject.gameObject);
            }
            return;
        }*/
        /*
        if (targetObject != null)
        {
            //agent.SetDestination(targetObject.transform.position);
            avatarcontroller.Pik_Agent_Message(this.identitynum, targetObject.transform.position);
            if (Vector3.Distance(transform.position, targetObject.transform.position) <= 0.75f)
            {
                targetObject.transform.position = transform.position + Vector3.forward * 0.9f;
                targetObject.transform.SetParent(transform);
                Destroy(targetObject.GetComponent<Rigidbody>());
                goHome = true;
            }
        }*/
    }
    
}