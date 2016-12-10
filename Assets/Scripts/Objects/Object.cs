using UnityEngine;
using System.Collections;


public enum EEnvironmentAction
{       // 16진수   // 2진수
    Fire = 0x01,    // 00000001
    Water = 0x02,   // 00000010
    Earth = 0x04,   // 00000100
    Wind = 0x08,    // 00001000
}

public enum EMoveAction
{
    Move = 0x01,    // 00000001
    Rotate = 0x02   // 00000010
}

public enum EQuakeAction
{
    Left = 0x01,    // 00000001
    Right = 0x02,   // 00000010
    Drop = 0x04,    // 00000100
    Destroy = 0x08  // 00001000
}

[RequireComponent(typeof(Rigidbody))]
public class Object : MonoBehaviour {

    [EnumFlag] public EEnvironmentAction eInter;
    [EnumFlag] public EMoveAction eMove;
    [EnumFlag] public EQuakeAction eEarthAction;

    public float fMovePower = 2.0f;
    public float fDestroyTime = 1.0f;
    float power;

    int iPlayerPulling;
    bool bRotating = false;
    bool CanPull = false;

    PlayerController player;
    CapsuleCollider attachCollider;
    PlayerAnimManager anim;
    EarthQuake eq;

    Quaternion target;

    Transform myTransform;
    Collider myCollider;

    float fGapX;

    // Use this for initialization
    void Awake () {
        myTransform = transform;
        myCollider = GetComponent<Collider>();

        player = GameObject.Find("Player").GetComponent<PlayerController>();
        attachCollider = player.GetComponent<CapsuleCollider>();
        anim = PlayerAnimManager.Instance;

        if (EnumFlagAttribute.HasFlag(eInter, EEnvironmentAction.Earth))
            eq = GameObject.Find("Camera").GetComponent<EarthQuake>();
        
        StartCoroutine(RotateObject());
    }
	
	// Update is called once per frame
	void FixedUpdate () {
        Follow();

        QuakeMove();
    }

    void Follow()
    {
        if (EnumFlagAttribute.HasFlag(eMove, EMoveAction.Move))
        {
            iPlayerPulling = player.IsPull();
            if (iPlayerPulling != 0 && CanPull)
            {
                //transform.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;// ^ RigidbodyConstraints.FreezePositionX ^ RigidbodyConstraints.FreezePositionY;

                if (PullnPush() < 0)
                {
                    //vTargetPosition = new Vector3(player.transform.position.x, myTransform.position.y);

                    myTransform.position = new Vector3(myTransform.position.x + fGapX * power, myTransform.position.y, myTransform.position.z); //Vector3.Lerp(myTransform.position, vTargetPosition, Time.deltaTime * 2.2f);
                    anim.Push(true);
                }
                else if (PullnPush() > 0)
                {
                    myTransform.position = new Vector3(myTransform.position.x - fGapX * power, myTransform.position.y, myTransform.position.z);
                    anim.Push(true);
                }
            }
        }
    }

    void QuakeMove()
    {
        if (eq != null && eq.Quaking())
        {
            myTransform.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll ^ RigidbodyConstraints.FreezePositionY;
            if (EnumFlagAttribute.HasFlag(eEarthAction, EQuakeAction.Left))
            {
                myTransform.Translate(Vector3.left * fMovePower * Time.deltaTime);
            }
            else if (EnumFlagAttribute.HasFlag(eEarthAction, EQuakeAction.Right))
            {
                myTransform.Translate(Vector3.right * fMovePower * Time.deltaTime);
            }


            if (EnumFlagAttribute.HasFlag(eEarthAction, EQuakeAction.Drop))
            {

                if (EnumFlagAttribute.HasFlag(eEarthAction, EQuakeAction.Destroy))
                    Destroy(gameObject, fDestroyTime);
            }
        }
    }
    
    int PullnPush()
    {
        // x좌표의 차이, 음수 값이면 플레이어가 왼쪽
        fGapX = attachCollider.bounds.center.x - myCollider.bounds.center.x;
        // 약 0.056 의 비율로 힘을 넣어줘야 밀고 당기는게 자연스러워진다. ㅂㄷㅂㄷ
        power = Mathf.Abs(0.056f / fGapX);

        // 왼쪽에서 당길 때
        if (fGapX < 0 && iPlayerPulling < 0)
            return -1;
        // 오른쪽에서 당길 때
        if (fGapX > 0 && iPlayerPulling > 0)
            return -1;


        // 왼쪽에서 밀 때
        if (fGapX < 0 && iPlayerPulling > 0)
            return 1;
        // 오른쪽에서 밀 때
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
                if (player.bRotating && !bRotating && CanPull)
                {
                    target = Quaternion.Euler(Vector3.up * 90f) * myTransform.rotation;
                    bRotating = true;
                    anim.Hit();
                }
                
                // 회전 시키는 부분.
                if(bRotating)
                {
                    myTransform.rotation = Quaternion.Lerp(myTransform.rotation, target, Time.deltaTime * 10f);

                    if (myTransform.rotation == target)
                        bRotating = false;
                }

                yield return new WaitForFixedUpdate();
            }
        }
    }

    void OnTriggerStay(Collider coll)
    {
        if (coll.tag == "Player")
        {
            CanPull = true;
        }
    }

    void OnTriggerExit(Collider coll)
    {
        if (coll.tag == "Player")
        {
            CanPull = false;
        }
    }
}
