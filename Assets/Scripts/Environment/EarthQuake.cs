using UnityEngine;
using System.Collections;

public class EarthQuake : Singleton<EarthQuake> {

    public float coef = 0.2f;

    // 흔드는 강도 초기 값
    public float fCoefQuakeIntensity = 0.3f;

    // 흔드는 강도
    float fQuakeIntensity;

    // 흔들리는 강도가 서서히 잦아들게 함
    float fQuakeDecay = 0.002f;

    // 초기 위치
    Vector3 vOriginPosition;

    // 초기 회전 값
    Quaternion qOriginRotation;

    // 지진을 내야 할 때 호출
    public void Quake()
    {
        vOriginPosition = transform.position;
        qOriginRotation = transform.rotation;
        fQuakeIntensity = fCoefQuakeIntensity;
    }

    // 지진희 멈춤을 판단
    public bool Quaking()
    {
        if (fQuakeIntensity > 0)
            return true;
        else
            return false;
    }

    void Update()
    {
        // 흔들림 강도가 0보다 클동안은 계속 흔들
        if (fQuakeIntensity > 0)
        {
            transform.position = vOriginPosition + (Random.insideUnitSphere * fQuakeIntensity);
            transform.rotation = new Quaternion(
                qOriginRotation.x + Random.Range(-fQuakeIntensity, fQuakeIntensity) * coef,
                qOriginRotation.y + Random.Range(-fQuakeIntensity, fQuakeIntensity) * coef,
                qOriginRotation.z + Random.Range(-fQuakeIntensity, fQuakeIntensity) * coef,
                qOriginRotation.w + Random.Range(-fQuakeIntensity, fQuakeIntensity) * coef
            );

            fQuakeIntensity -= fQuakeDecay;
        }
    }
}
