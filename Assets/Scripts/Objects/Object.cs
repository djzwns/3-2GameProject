using UnityEngine;
using System.Collections;

public class Object : MonoBehaviour {
    
    public enum EInteraction
    {        // 16진수    // 2진수
        Fire    = 0x01,   // 00001
        Water   = 0x02,   // 00010
        Earth   = 0x04,   // 00100
        Wind    = 0x08,   // 01000
        Move    = 0x10,   // 10000
    }

    [EnumFlag] public EInteraction eInter;
    bool bPlayerPulling = false;

    PlayerController player;

    float fDistance;
    float fCanFollowDist = 2.0f;

    // Use this for initialization
    void Awake () {
        player = GameObject.Find("Player").GetComponent<PlayerController>();
    }
	
	// Update is called once per frame
	void FixedUpdate () {
        Follow();
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
        if (EnumFlagAttribute.HasFlag(eInter, EInteraction.Move))
        {
            bPlayerPulling = player.IsPull();
            Debug.Log(CanPull());
            if (CanPull() && bPlayerPulling)
            {
                transform.position = Vector3.Lerp(transform.position, player.transform.position, Time.deltaTime * 3);
            }
        }
    }
}
