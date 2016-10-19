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

    bool bPlayerPulling = false;

    PlayerController player;
    EarthQuake eq;

    float fDistance;
    float fCanFollowDist = 2.0f;
    public float fCanFollowDistMargin = 1.0f;

    // Use this for initialization
    void Awake () {
        player = GameObject.Find("Player").GetComponent<PlayerController>();

        if (EnumFlagAttribute.HasFlag(eInter, EEnvironmentAction.Earth))
            eq = GameObject.Find("Camera").GetComponent<EarthQuake>();

        fCanFollowDist = player.GetComponent<Collider>().bounds.size.x * 0.5f + gameObject.GetComponent<Collider>().bounds.size.x * 0.5f + fCanFollowDistMargin;
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
            bPlayerPulling = player.IsPull();
            Debug.Log(CanPull());
            if (CanPull() && bPlayerPulling)
            {
                transform.position = Vector3.Lerp(transform.position, player.transform.position, Time.deltaTime * 3f);
            }
        }
    }

    void QuakeMove()
    {
        if (eq != null && eq.Quaking())
        {
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
                transform.GetComponent<Rigidbody>().useGravity = true;

                if (EnumFlagAttribute.HasFlag(eEarthAction, EQuakeAction.Destroy))
                    Destroy(gameObject, fDestroyTime);
            }
        }
    }
}
