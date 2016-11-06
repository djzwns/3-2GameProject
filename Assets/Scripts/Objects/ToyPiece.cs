using UnityEngine;
using System.Collections;

public class ToyPiece : MonoBehaviour {

    void OnTriggerEnter(Collider coll)
    {
        if (coll.tag == "Player")
        {
            GameManager.Instance.CollectToy();
            PlayerAnimManager.Instance.Dance();
            Destroy(gameObject);
        }
    }
}
