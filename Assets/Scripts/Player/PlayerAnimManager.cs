using UnityEngine;
using System.Collections;

public class PlayerAnimManager : Singleton<PlayerAnimManager> {

    float fDanceTime = 2f;

    public Animator anim;
    
    public void Push(bool _push)
    {
        anim.SetBool("IsPush", _push);
    }

    public void Walk(float _horizon)
    {
        if (!anim.GetBool("IsClimb"))
        {
            if (_horizon > 0)
            {
                anim.SetBool("IsWalk", true);
                if (PlayerController.Instance.IsPull() == 0)
                    transform.rotation = Quaternion.Euler(0f, 90f, 0f);
                //transform.Rotate(0f, 90f * Time.deltaTime, 0f);
            }
            else if (_horizon < 0)
            {
                anim.SetBool("IsWalk", true);
                if (PlayerController.Instance.IsPull() == 0)
                    transform.rotation = Quaternion.Euler(0f, -90f, 0f);
                //transform.Rotate(0f, -90f * Time.deltaTime, 0f);
            }
            else
            {
                anim.SetBool("IsWalk", false);
            }
        }
    }

    public void Hit()
    {
        StartCoroutine(Hit_());
    }

    public bool IsHit()
    {
        return anim.GetBool("IsHit");
    }
    IEnumerator Hit_()
    {
        anim.SetBool("IsHit", true);

        yield return new WaitForFixedUpdate();

        anim.SetBool("IsHit", false);
    }

    public void Jump(bool _jump)
    {
        anim.SetBool("IsJump", _jump);
    }

    public void Climb(bool _climb)
    {
        anim.SetBool("IsClimb", _climb);
        if(_climb)
            transform.rotation = Quaternion.Euler(0f, 180, 0f);

    }

    public void Die()
    {
        anim.SetBool("IsDead", true);
    }

    public void Dance()
    {
        anim.SetBool("IWannaDance", true);
        StartCoroutine(DanceStop());
    }

    IEnumerator DanceStop()
    {
        yield return new WaitForSeconds(fDanceTime);
        anim.SetBool("IWannaDance", false);
    }
}
