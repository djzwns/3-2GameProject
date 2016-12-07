using UnityEngine;
using System.Collections;

public class NightmareManager : Singleton<NightmareManager>
{
    public GameObject nightmare;

    // 불의 위치와 불 프리팹
    public Transform[] firePositions;
    public GameObject[] fire;
    
    // 물이 차오르는 수준
    public int iWaterLevel;

    Flood floodInstance;

    void Start()
    {
        floodInstance = Flood.Instance;
    }

    // 대화창에서 사용될 함수들.. ===
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
        floodInstance.NextWaterCount(iWaterLevel);
    }
    // ================================

    // 악몽의 등장
    public void HeisComing()
    {
        if(!nightmare.activeSelf)
            nightmare.SetActive(true);
    }

    // 퇴장
    public void HeisGone()
    {
        if (nightmare.activeSelf)
            nightmare.SetActive(false);
    }
}
