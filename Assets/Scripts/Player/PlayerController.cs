using UnityEngine;
using System.Collections;

public class PlayerController : Singleton<PlayerController> {
    PlayerAnimManager anim;

    public float fSpeed = 5f;
    public float fSlowly = 0.5f;
    public float fJumpForce = 5f;
    public float fFixedZ { get; set; }
    float fHorizon;

    Vector3 vMovement;
    Vector3 vGravity;
    CharacterController cController;
    
    bool bJumping = false;
    bool bPulling = false;
    bool bClimbing = false;
    bool bDead = false;
    public bool bTalking { get; set; }
    public bool bRotating {get; private set;}

    bool bReverse;

    
	void Awake () {
        cController = GetComponent<CharacterController>();
        anim = PlayerAnimManager.Instance;
        bRotating = false;
        bTalking = false;

        fFixedZ = transform.position.z;
        bReverse = GameObject.Find("Camera").GetComponent<FollowCamera>().bReverse;
	}

    void Update()
    {
        bPulling = Input.GetButton("PullPush");
        bRotating = Input.GetButton("Rotation");
    }
	
	void FixedUpdate ()
    {
        fHorizon = Input.GetAxisRaw("Horizontal");
        if (bReverse) fHorizon = -fHorizon;

        // 좌우 이동
        Move(fHorizon);

        // 점프
        Jump();

        // 최종 위치 적용
        cController.Move(vMovement * Time.deltaTime);

        // z축 고정
        if (transform.position.z != 0)
            transform.position = new Vector3(transform.position.x, transform.position.y, fFixedZ);
    }

    void Move(float horizon)
    {
        if (!bDead && !bTalking)
        {
            vMovement.Set(horizon, 0f, 0f);

            if (IsPull() != 0)
                vMovement = vMovement.normalized * fSpeed * 0.6f;
            else
                vMovement = vMovement.normalized * fSpeed;

            anim.Walk(horizon);

            //vMovement = transform.TransformDirection(vMovement);
        }
        else
        {
            vMovement.Set(0f, 0f, 0f);
            anim.Walk(0);
        }
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

            if (!Input.GetButton("Jump"))
            {
                bJumping = true;
                anim.Jump(false);
            }
            else if (bJumping && !bDead && !bTalking)
            {
                vGravity.y = fJumpForce;
                anim.Jump(true);
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

    public void Die()
    {
        bDead = true;
    }
}
