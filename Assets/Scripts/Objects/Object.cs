using UnityEngine;
using System.Collections;

public class Object : MonoBehaviour {

    bool bPlayerPulling = false;

    PlayerController player;

    float fDistance;
    float fCanFollowDist = 2.0f;

    // Use this for initialization
    void Awake () {
        player = GameObject.Find("Player").GetComponent<PlayerController>();
    }
	
	// Update is called once per frame
	void Update () {
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
        bPlayerPulling = player.IsPull();
        Debug.Log(CanPull());
        if (CanPull() && bPlayerPulling)
        {
            transform.position = Vector3.Lerp(transform.position, player.transform.position, Time.deltaTime * 3);
        }
    }
}
