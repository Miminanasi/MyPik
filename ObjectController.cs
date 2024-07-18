using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectController : MonoBehaviour
{
    public bool selected;
    AvatarController avatarcontroller;
    private void Start()
    {
        avatarcontroller = GameObject.Find("orima").GetComponent<AvatarController>();
    }
    public void Selected()
    {
        if (!selected && avatarcontroller.followingPikmins.Count >= 1)
        {
            avatarcontroller.followingPikmins[0].targetObject = this;
            avatarcontroller.followingPikmins.RemoveAt(0);
            selected = true;
        }
    }
}