using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowYou : MonoBehaviour
{
    void Update()
    {
        Transform target = GameObject.Find("HeadsetAlias").transform;
        transform.LookAt(target);
    }
}
