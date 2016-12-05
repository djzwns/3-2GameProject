using UnityEngine;
using System.Collections;

public class QuakeTrap : MonoBehaviour {
    bool bUsed = false;

    void OnTriggerEnter(Collider coll)
    {
        if (!bUsed && coll.tag == "Player")
        {
            bUsed = true;
            EarthQuake.Instance.Quake();
        }
    }
}
