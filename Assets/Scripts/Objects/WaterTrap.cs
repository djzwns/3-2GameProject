using UnityEngine;
using System.Collections;

public class WaterTrap : MonoBehaviour {

    void OnTriggerEnter(Collider coll)
    {
        if (coll.tag == "Player")
        {
            Flood.Instance.NextWaterCount(int.Parse(transform.name));
        }
    }
}
