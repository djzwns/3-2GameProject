using UnityEngine;
using System.Collections;

public class ConfusionTrap : MonoBehaviour {
    public float confuseTime = 0.35f;

    PlayerController player;

    void Start()
    {
        player = PlayerController.Instance;
    }

    IEnumerator Confuse(bool confuse)
    {
        yield return new WaitForSeconds(confuseTime);
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
