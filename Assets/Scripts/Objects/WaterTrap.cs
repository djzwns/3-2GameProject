using UnityEngine;
using System.Collections;

public class WaterTrap : MonoBehaviour {
    public int iWaterNumber;

    void OnTriggerEnter(Collider coll)
    {
        if (coll.tag == "Player")
        {
            Flood.Instance.NextWaterCount(iWaterNumber);
        }
    }
}
