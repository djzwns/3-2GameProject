using UnityEngine;
using System.Collections;

public class NightmareManager : Singleton<NightmareManager> {
    
    public Transform[] firePositions;
    public GameObject[] fire;
    public int iWaterNumber;

    public void Quake()
    {
        EarthQuake.Instance.Quake();
    }

    public void HellFire()
    {
        StartCoroutine(Fire());
    }

    IEnumerator Fire()
    {
        for (var i = 0; i < firePositions.Length; ++i)
        {
            Instantiate(fire[Random.Range(0, fire.Length)], firePositions[i].position, Quaternion.identity);
            yield return new WaitForFixedUpdate();
        }
    }

    public void Swell()
    {
        Flood.Instance.NextWaterCount(iWaterNumber);
    }
}
