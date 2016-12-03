using UnityEngine;
using System.Collections;

public class Flood : Singleton<Flood> {
    float spendTime = 0f;
    float limitTime = 2.0f;

    Player player;
    Transform playerTransform;
    Transform myTransform;
    float fPlayerYSize;

    public Transform[] floodPosition;
    public float fFloodSpeed = 1f;
    int iFloodCount = 0;

    void Start()
    {
        playerTransform = PlayerController.Instance.transform;
        myTransform = transform;
        fPlayerYSize = playerTransform.GetComponent<Collider>().bounds.size.y * 0.5f;
    }

    void Swell()
    {
        myTransform.position = Vector3.Lerp(myTransform.position, floodPosition[iFloodCount].position, Time.deltaTime * fFloodSpeed);
    }

    void FixedUpdate()
    {
        Swell();
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
        if (coll.gameObject.tag == "Object")
        {
            if (coll.transform.position.y <= myTransform.position.y &&
                EnumFlagAttribute.HasFlag(coll.GetComponent<Object>().eInter, Object.EEnvironmentAction.Water))
            {
                Transform obj = coll.transform;
                obj.position = new Vector3(obj.position.x, myTransform.position.y);
            }
        }


        // 플레이어가 물에 빠졌을 때
        if (coll.gameObject.tag == "Player")
        {
        }
    }
}
