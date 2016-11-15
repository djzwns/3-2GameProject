using UnityEngine;
using System.Collections;

public class ToyPiece : MonoBehaviour {

    void OnTriggerEnter(Collider coll)
    {
        if (coll.tag == "Player")
        {
            ToyManager.Instance.CollectToy();
            PlayerAnimManager.Instance.Dance();
            Destroy(gameObject);
        }
    }
}
