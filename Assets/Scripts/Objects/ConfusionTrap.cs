using UnityEngine;
using System.Collections;

public class ConfusionTrap : MonoBehaviour {
    PlayerController player;

    void Start()
    {
        player = PlayerController.Instance;
    }

    IEnumerator Confuse(bool confuse)
    {
        yield return new WaitForSeconds(0.3f);
        player.Confuse(confuse);
    }

    void OnTriggerEnter(Collider coll)
    {
        if (coll.tag == "Player")
        {
            StartCoroutine(Confuse(true));
        }
    }

    void OnTriggerExit(Collider coll)
    {
        if (coll.tag == "Player")
        {
            StartCoroutine(Confuse(false));
        }
    }
}
