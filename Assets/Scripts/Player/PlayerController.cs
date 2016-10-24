using UnityEngine;
using System.Collections;

public class PlayerController : Singleton<PlayerController> {

    public float fSpeed = 5f;
    public float fSlowly = 0.5f;
    public float fJumpForce = 5f;
    float fHorizon;

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
        fHorizon = Input.GetAxisRaw("Horizontal");

        // 좌우 이동
        Move(fHorizon);

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
        
        if(IsPull() != 0)
            vMovement = vMovement.normalized * fSpeed * 0.6f;
        else
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

    public int IsPull()
    {
        if (bPulling)
        {
            if(fHorizon < 0)
                return -1;
            if (fHorizon > 0)
                return 1;

            return 0;
        }
        return 0;
    }

    public void SetClimbing(bool _bClimbing)
    {
        bClimbing = _bClimbing;
    }
}
