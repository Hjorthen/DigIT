using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowObject : MonoBehaviour
{
    public Transform FollowTarget;
    public Vector2 Max;
    public Vector2 Min;
    // Update is called once per frame
    void Update()
    {
        Vector3 newPosition = new Vector3(
                Mathf.Clamp(FollowTarget.position.x, Min.x, Max.x), 
                Mathf.Clamp(FollowTarget.position.y, Min.y, Max.y), 
                this.transform.position.z
            );
        this.transform.position = newPosition;
    }
}
