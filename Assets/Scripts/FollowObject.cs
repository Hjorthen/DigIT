using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowObject : MonoBehaviour
{
    public Transform FollowTarget;
    // Update is called once per frame
    void Update()
    {
        this.transform.position = new Vector3(FollowTarget.position.x, FollowTarget.position.y, this.transform.position.z);
    }
}
