using UnityEngine;
using System.Collections;

public class NightmareManager : Singleton<NightmareManager> {
    public Transform[] firePositions;
    public GameObject[] fire;
    EarthQuake eq = EarthQuake.Instance;

    public void Quake()
    {
        eq.Quake();
    }

    public void HellFire()
    {
        for (var i = 0; i < firePositions.Length; ++i)
        {
            Instantiate(fire[Random.Range(0, fire.Length)], firePositions[i].position, Quaternion.identity);
        }
    }

    public void Tornado()
    {
    }
}
