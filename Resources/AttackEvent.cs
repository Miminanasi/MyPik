using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackEvent : MonoBehaviour
{
    [SerializeField] Animator anim;

    IEnumerator One_Second_Stop(Pikmin script)
    {
        //終わるまで待ってほしい処理を書く
        //例：敵が倒れるアニメーションを開始

        //2秒待つ
        yield return new WaitForSeconds(2);

     //   anim.enabled = true;
        //再開してから実行したい処理を書く
        script.Damage();
        //例：敵オブジェクトを破壊
    }

    IEnumerator Avatar_One_second_Stop(AvatarController avatar)
    {
        //2秒待つ
        yield return new WaitForSeconds(2);
        Debug.Log(avatar.tag);
        Debug.Log("test1");
        avatar.Damage();

    }

    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.CompareTag("Your_Avatar"))
        {
            if (transform.parent.gameObject.CompareTag("Ally_Pikmin"))
            {
                GameObject enemy_avatar_object = other.gameObject;
                AvatarController avatar = enemy_avatar_object.GetComponent<AvatarController>();
                StartCoroutine(Avatar_One_second_Stop(avatar));
            }           
        }

        if (other.gameObject.CompareTag("Enemy_Pikmin") || other.gameObject.CompareTag("Ally_Pikmin"))
        {
            if (!other.gameObject.CompareTag(transform.parent.tag))
            {
  //              Debug.Log("attack_test");
                GameObject enemy_object = other.gameObject;
    //            Debug.Log(other.gameObject.name);
                Pikmin script = enemy_object.GetComponent<Pikmin>();
                StartCoroutine(One_Second_Stop(script));


                
            }
        }
    }
}
