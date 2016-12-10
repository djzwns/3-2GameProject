using UnityEngine;
using System.Collections;

public class Flood : Singleton<Flood> {
    public enum RainType
    {
        DRIZZLE = 0,
        MODERATE = 1,
        HEAVY = 2,
        NONE = 4,
    }

    float spendTime = 0f;
    float limitTime = 2.0f;

    Player player;
    Transform playerTransform;
    Transform myTransform;
    float fPlayerYSize;

    public GameObject[] rain;
    public Transform[] floodPosition;
    public float fFloodSpeed = 1f;
    int iFloodCount = 0;

    public RainType eRainType { get; private set; }

    void Start()
    {
        playerTransform = PlayerController.Instance.transform;
        myTransform = transform;
        fPlayerYSize = playerTransform.GetComponent<Collider>().bounds.size.y * 0.5f;
        eRainType = RainType.NONE;
        if (floodPosition.Length != 0)
            StartCoroutine(RainDrop());
    }

    // 비의 변화에 따라 활성/비활성화
    IEnumerator RainDrop()
    {
        while (true)
        {
            switch (eRainType)
            {
                case RainType.DRIZZLE:
                    rain[(int)RainType.DRIZZLE].SetActive(true);
                    rain[(int)RainType.MODERATE].SetActive(false);
                    rain[(int)RainType.HEAVY].SetActive(false);
                    break;

                case RainType.MODERATE:
                    rain[(int)RainType.DRIZZLE].SetActive(false);
                    rain[(int)RainType.MODERATE].SetActive(true);
                    rain[(int)RainType.HEAVY].SetActive(false);
                    break;

                case RainType.HEAVY:
                    rain[(int)RainType.DRIZZLE].SetActive(false);
                    rain[(int)RainType.MODERATE].SetActive(false);
                    rain[(int)RainType.HEAVY].SetActive(true);
                    break;

                case RainType.NONE:
                    foreach (var _rain in rain)
                    {
                        if (_rain.activeSelf)
                            _rain.SetActive(false);
                    }
                    break;
            }
            yield return new WaitForSeconds(0.5f);
        }
    }

    RainType Swell()
    {
        myTransform.position = Vector3.Lerp(myTransform.position, floodPosition[iFloodCount].position, Time.deltaTime * fFloodSpeed);

        float fRainFall = floodPosition[iFloodCount].position.y - myTransform.position.y;

        // 강수량 따라 비 타입 변경
        if (fRainFall > 20)
            return RainType.HEAVY;
        else if (6 < fRainFall && fRainFall <= 20)
            return RainType.MODERATE;
        else if (3 < fRainFall && fRainFall <= 6)
            return RainType.DRIZZLE;
        else
            return RainType.NONE;
    }

    void FixedUpdate()
    {
        if(floodPosition.Length != 0)
            eRainType = Swell();
        if (player != null)
        {
            if (myTransform.position.y >= playerTransform.position.y + fPlayerYSize)
            {
                spendTime += Time.deltaTime;
                // 일정 시간마다 피 깎
                if (spendTime > limitTime)
                {
                    player.TakeDamage(1);
                    spendTime = 0f;
                }
            }
            else
                spendTime = 0f;
        }
    }

    public void NextWaterCount(int _count)
    {
        if(floodPosition.Length > _count)
            iFloodCount = _count;
    }

    void OnTriggerEnter(Collider coll)
    {
        if (coll.gameObject.tag == "Player")
            player = GameManager.Instance.getplayer;

        if (coll.gameObject.tag == "Fire")
            Destroy(coll.gameObject);
    }

    void OnTriggerExit(Collider coll)
    {
        if (coll.gameObject.tag == "Player")
        {
            if (myTransform.position.y <= playerTransform.position.y)
                player = null;
        }
    }

    void OnTriggerStay(Collider coll)
    {
        // 물에 뜨는 오브젝트는 물의 높이와 같게
        if (coll.gameObject.tag == "Object" && coll.GetComponent<Object>() != null)
        {
            if (coll.transform.position.y <= myTransform.position.y &&
                EnumFlagAttribute.HasFlag(coll.GetComponent<Object>().eInter, EEnvironmentAction.Water))
            {
                Transform obj = coll.transform;
                obj.position = new Vector3(obj.position.x, myTransform.position.y, obj.position.z);
            }
        }


        // 플레이어가 물에 빠졌을 때
        if (coll.gameObject.tag == "Player")
        {
        }
    }
}
