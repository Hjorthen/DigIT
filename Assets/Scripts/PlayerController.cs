using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    private Rigidbody2D Rigidbody;

    [SerializeField]
    private Vector2 Multipler;

    [SerializeField]
    private float MaxVelocity;

    [SerializeField]
    private Vector2 InputVelocity;

    [SerializeField]
    private float DeaccelerationMultiplier;

    void Start()
    {
        Rigidbody = GetComponent<Rigidbody2D>();
    }

    PhysicsMaterial2D CreatePhysicsMaterial(Rigidbody2D rigidbody)
    {
        PhysicsMaterial2D material = new PhysicsMaterial2D();
        rigidbody.sharedMaterial = material;
        return material;
    }

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Mathf.Max(0, Input.GetAxis("Vertical"));
        InputVelocity = new Vector2(horizontal, vertical);

        if(horizontal > 0)
        {
            transform.rotation = Quaternion.AngleAxis(0, Vector3.up);
        }   
        else if (horizontal < 0)
        {
            transform.rotation = Quaternion.AngleAxis(180, Vector3.up);
        }
    }


    void OnDrawGizmos()
    {
        if(Application.isPlaying)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawRay(transform.position, Rigidbody.velocity);
        }
            
    }

    void FixedUpdate()
    {
        Vector2 moveDirection = new Vector2(InputVelocity.x * Multipler.x, InputVelocity.y * Multipler.y);


        // Check if movement contradicts current direction
        if (Vector2.Dot(moveDirection, Rigidbody.velocity) < 0)
        {
            moveDirection *= DeaccelerationMultiplier;
        }

        Rigidbody.AddForce(moveDirection);
       
        Rigidbody.velocity = Vector2.ClampMagnitude(Rigidbody.velocity, MaxVelocity);
    }

    private void ApplyBreaking()
    {
       
    }
}
