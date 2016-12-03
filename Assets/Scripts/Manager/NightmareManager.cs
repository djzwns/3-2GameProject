using UnityEngine;
using System.Collections;

public class NightmareManager : Singleton<NightmareManager> {
    
    public Transform[] firePositions;
    public GameObject[] fire;

    public Transform[] floodPositions;

    public void Quake()
    {
        EarthQuake.Instance.Quake();
    }

    public void HellFire()
    {
        for (var i = 0; i < firePositions.Length; ++i)
        {
            Instantiate(fire[Random.Range(0, fire.Length)], firePositions[i].position, Quaternion.identity);
        }
    }

    public void Flood()
    {
    }
}
