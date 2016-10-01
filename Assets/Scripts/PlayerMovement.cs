using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {

    public float fSpeed = 5f;
    public float fSlowly = 0.5f;
    public float fJumpForce = 300f;

    Vector3 vMovement;
    Rigidbody rigidPlayer;

    bool bCanJump = false;
    bool bJumping = false;

    
	void Awake () {
        rigidPlayer = GetComponent<Rigidbody>();
	}

    void Update()
    {
        if (Input.GetButtonDown("Jump") && !bJumping)
            bCanJump = true;
    }
	
	void FixedUpdate ()
    {
        float horizon = Input.GetAxisRaw("Horizontal");

        // 좌우 이동
        Move(horizon);

        // 점프
        Jump();
	}

    void Move(float horizon)
    {
        vMovement.Set(horizon, 0f, 0f);

        if (!bJumping)
            vMovement = vMovement.normalized * fSpeed * Time.deltaTime;
        else// 점프 중에 좌우로 편하게 움직이는건 비정상적으로 보이기에 속도를 늦춤
            vMovement = vMovement.normalized * (fSpeed * fSlowly) * Time.deltaTime;

        rigidPlayer.MovePosition(transform.position + vMovement);
    }

    void Jump()
    {
        if (bCanJump)
        {
            rigidPlayer.AddForce(Vector3.up * fJumpForce);
            bJumping = true;
        }
        bCanJump = false;
    }
}
