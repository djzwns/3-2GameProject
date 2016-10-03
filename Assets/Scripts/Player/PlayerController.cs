using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

    public float fSpeed = 5f;
    public float fSlowly = 0.5f;
    public float fJumpForce = 5f;

    public float fPushPower = 2.0f;

    Vector3 vMovement;
    Vector3 vGravity;
    CharacterController cController;
    
    bool bJumping = false;

    
	void Awake () {
        cController = GetComponent<CharacterController>();
	}

    void Update()
    {
        if (Input.GetButtonDown("Jump")/* && cController.isGrounded*/)
            bJumping = true;
    }
	
	void FixedUpdate ()
    {
        float horizon = Input.GetAxisRaw("Horizontal");

        // 좌우 이동
        Move(horizon);

        // 점프
        Jump();

        // 최종 위치 적용
        cController.Move(vMovement * Time.deltaTime);
    }

    void Move(float horizon)
    {
        vMovement.Set(horizon, 0f, 0f);
        
        vMovement = vMovement.normalized * fSpeed;

        vMovement = transform.TransformDirection(vMovement);
    }

    void Jump()
    {
        // 땅에 있지 않으면 중력작용
        if (!cController.isGrounded)
        {
            vGravity += Physics.gravity * Time.deltaTime;
        }
        // 땅에서는 중력 x
        else
        {
            vGravity = Vector3.zero;

            if (bJumping)
            {
                vGravity.y = fJumpForce;
                bJumping = false;
            }
        }

        vMovement += vGravity;
    }

    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        Rigidbody body = hit.collider.attachedRigidbody;

        // 리지드바디 없거나 키네틱이면 안밈
        if (body == null || body.isKinematic)
            return;

        if (hit.moveDirection.y < -0.3f)
            return;

        Vector3 pushDir = new Vector3(hit.moveDirection.x, 0, 0);
        body.velocity = pushDir * fPushPower;
    }
}
