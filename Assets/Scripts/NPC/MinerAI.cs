using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinerAI : MonoBehaviour
{
    private World _world;
    private TickedCooldownTimer _movementTimer;
    private Vector3 _moveTargetPosition;
    private Rigidbody2D _rigidbody;
    [SerializeField]
    private float _moveSpeed;
    void OnEnable()
    {
        _world = ServiceRegistry.GetService<World>();
        _movementTimer = new TickedCooldownTimer();
        _rigidbody = GetComponent<Rigidbody2D>();
        _moveTargetPosition = transform.position;
    }

    void Update()
    {
        _movementTimer.AdvanceBy(Time.deltaTime);

        if(!IsMovingTowardsTarget() && _movementTimer.Expired) {
            UpdateTargetPosition();
            _movementTimer.WaitFor(5);
        }

        if(_rigidbody.velocity.x > 0) {
            transform.rotation = Quaternion.AngleAxis(0, Vector3.up);
        }   
        else if (_rigidbody.velocity.x < 0)
        {
            transform.rotation = Quaternion.AngleAxis(180, Vector3.up);
        }
    }

    private void UpdateTargetPosition() {
        Vector3 moveDirection;
        float movementDistance = Random.Range(1, 3);
        if(Random.Range(0, 2) == 1) {
            moveDirection = new Vector3(-movementDistance, 0, 0);
        } else {
            moveDirection = new Vector3(movementDistance, 0, 0);
        }
        Vector3 newPosition = transform.position + moveDirection;

        TileModel tile = _world.GetTileAt(newPosition);
        if(tile == null) {
            _moveTargetPosition = newPosition;
        }     
    }

    void OnDrawGizmos() {
        if(Application.isPlaying) {
            Gizmos.DrawRay(transform.position, GetTargetDirection());
            Gizmos.DrawSphere(_moveTargetPosition, 0.25f);
        }
    }

    private bool IsMovingTowardsTarget() {
        float distanceToTarget = Vector2.Distance(transform.position, _moveTargetPosition);
        return  distanceToTarget > 0.2;
    }

    private Vector2 GetTargetDirection() {
        return (_moveTargetPosition - transform.position).normalized;
    }

    void FixedUpdate()
    {
        if(IsMovingTowardsTarget()) {
            Vector2 movementDirection = GetTargetDirection();
            _rigidbody.velocity = movementDirection * _moveSpeed;
        } else {
            _rigidbody.velocity = Vector2.zero;
        }
    }
}
