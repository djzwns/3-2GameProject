using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

    public float fSpeed = 5f;
    public float fSlowly = 0.5f;
    public float fJumpForce = 5f;

    Vector3 vMovement;
    Vector3 vGravity;
    CharacterController cController;
    
    bool bJumping = false;
    bool bPulling = false;
    bool bClimbing = false;

    
	void Awake () {
        cController = GetComponent<CharacterController>();
	}

    void Update()
    {
        if (Input.GetButtonDown("Jump")/* && cController.isGrounded*/)
            bJumping = true;

        bPulling = Input.GetKey(KeyCode.Space);
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
        
        // z축 고정
        if (transform.position.z != 0)
            transform.position = new Vector3(transform.position.x, transform.position.y, 0f);
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
        if (!cController.isGrounded && !bClimbing)
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

    public bool IsPull()
    {
        return bPulling;
    }

    public void SetClimbing(bool _bClimbing)
    {
        bClimbing = _bClimbing;
    }
}
