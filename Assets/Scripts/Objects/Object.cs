using UnityEngine;
using System.Collections;

public class Object : MonoBehaviour {
    
    public enum EEnvironmentAction
    {        // 16진수    // 2진수
        Fire    = 0x01,   // 00000001
        Water   = 0x02,   // 00000010
        Earth   = 0x04,   // 00000100
        Wind    = 0x08,   // 00001000
    }

    public enum EMoveAction
    {
        Move    = 0x01,   // 00000001
        Rotate  = 0x02    // 00000010
    }

    public enum EQuakeAction
    {
        Left    = 0x01,   // 00000001
        Right   = 0x02,   // 00000010
        Drop    = 0x04,   // 00000100
        Destroy = 0x08    // 00001000
    }

    [EnumFlag] public EEnvironmentAction eInter;
    [EnumFlag] public EMoveAction eMove;
    [EnumFlag] public EQuakeAction eEarthAction;

    public float fMovePower = 2.0f;
    public float fDestroyTime = 1.0f;

    int iPlayerPulling;
    bool bRotating = false;

    PlayerController player;
    EarthQuake eq;

    Quaternion target;

    float fGapX;
    float fDistance;
    float fCanFollowDist = 2.0f;
    float fCanFollowDistMargin = 0.7f;

    // Use this for initialization
    void Awake () {
        player = GameObject.Find("Player").GetComponent<PlayerController>();

        if (EnumFlagAttribute.HasFlag(eInter, EEnvironmentAction.Earth))
            eq = GameObject.Find("Camera").GetComponent<EarthQuake>();

        fCanFollowDist = player.GetComponent<Collider>().bounds.size.x * 0.5f + gameObject.GetComponent<Collider>().bounds.size.x * 0.5f + fCanFollowDistMargin;

        StartCoroutine(RotateObject());
    }
	
	// Update is called once per frame
	void FixedUpdate () {
        Follow();

        QuakeMove();
	}

    bool CanPull()
    {
        fDistance = Vector3.Distance(player.transform.position, transform.position);

        if (-fCanFollowDist < fDistance && fDistance < fCanFollowDist)
            return true;
        else
            return false;
    }

    void Follow()
    {
        if (EnumFlagAttribute.HasFlag(eMove, EMoveAction.Move))
        {
            iPlayerPulling = player.IsPull();
            if (iPlayerPulling != 0 && CanPull())
            {
                //transform.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;// ^ RigidbodyConstraints.FreezePositionX ^ RigidbodyConstraints.FreezePositionY;

                if (PullnPush() < 0)
                {
                    Vector3 vTargetPosition = new Vector3(player.transform.position.x, transform.position.y);
                    transform.position = Vector3.Lerp(transform.position, vTargetPosition, Time.deltaTime * 4f);
                }
                else if (PullnPush() > 0)
                {
                    transform.position = new Vector3(transform.position.x - fGapX * 0.04f, transform.position.y);
                }
            }
        }
    }

    void QuakeMove()
    {
        if (eq != null && eq.Quaking())
        {
            transform.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll ^ RigidbodyConstraints.FreezePositionX ^ RigidbodyConstraints.FreezePositionY ^ RigidbodyConstraints.FreezeRotationZ;
            if (EnumFlagAttribute.HasFlag(eEarthAction, EQuakeAction.Left))
            {
                transform.Translate(Vector3.left * fMovePower * Time.deltaTime);
            }
            else if (EnumFlagAttribute.HasFlag(eEarthAction, EQuakeAction.Right))
            {
                transform.Translate(Vector3.right * fMovePower * Time.deltaTime);
            }


            if (EnumFlagAttribute.HasFlag(eEarthAction, EQuakeAction.Drop))
            {

                if (EnumFlagAttribute.HasFlag(eEarthAction, EQuakeAction.Destroy))
                    Destroy(gameObject, fDestroyTime);
            }
        }
    }

    void UnFreezeObject()
    {
        transform.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll ^ RigidbodyConstraints.FreezePositionX ^ RigidbodyConstraints.FreezePositionY ^ RigidbodyConstraints.FreezeRotationZ;
    }

    void FreezeObject()
    {
        transform.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
    }

    
    int PullnPush()
    {
        // x좌표의 차이, 음수 값이면 플레이어가 왼쪽
        fGapX = player.transform.position.x - transform.position.x;

        // 당길 때
        if (fGapX < 0 && iPlayerPulling < 0)
            return -1;

        if (fGapX > 0 && iPlayerPulling > 0)
            return -1;


        // 밀 때
        if (fGapX < 0 && iPlayerPulling > 0)
            return 1;

        if (fGapX > 0 && iPlayerPulling < 0)
            return 1;

        // 아무것도 아닐 때
        return 0;
    }

    IEnumerator RotateObject()
    {
        if (EnumFlagAttribute.HasFlag(eMove, EMoveAction.Rotate))
        {
            while (true)
            {
                // 회전 시킬 때 마다 원래의 로테이션 값보다 90도 갱신된 타겟 로테이션으로
                if (player.bRotating && !bRotating && CanPull())
                {
                    target = Quaternion.Euler(Vector3.up * 90f) * transform.rotation;
                    bRotating = true;
                }
                
                // 회전 시키는 부분.
                if(bRotating)
                {
                    transform.rotation = Quaternion.Lerp(transform.rotation, target, Time.deltaTime * 10f);

                    if (transform.rotation == target)
                        bRotating = false;
                }

                yield return new WaitForFixedUpdate();
            }
        }
    }
}
