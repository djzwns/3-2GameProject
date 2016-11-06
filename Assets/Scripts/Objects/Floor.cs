using UnityEngine;
using System.Collections;

public class Floor : MonoBehaviour {

    void OnTriggerEnter(Collider coll)
    {
        if (coll.gameObject.tag == "Player")
        {
            GameManager.Instance.getplayer.TakeDamage(1000);
        }
    }
}
