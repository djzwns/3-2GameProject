using UnityEngine;
using System.Collections;

public class ToyPiece : MonoBehaviour {
    bool bUsed = false;

    void OnTriggerEnter(Collider coll)
    {
        if (coll.tag == "Player" && !bUsed)
        {
            bUsed = true;
            ToyManager.Instance.CollectToy();
            PlayerAnimManager.Instance.Dance();
            Destroy(gameObject);
        }
    }
}
