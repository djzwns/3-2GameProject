using UnityEngine;
using System.Collections;

public class Flood : MonoBehaviour {
    float spendTime = 0f;
    float limitTime = 2.0f;

    Player player;

    void OnTriggerEnter(Collider coll)
    {
        if (coll.gameObject.tag == "Player")
            player = GameManager.Instance.getplayer;
    }

    void OnTriggerStay(Collider coll)
    {
        // 물에 뜨는 오브젝트는 물의 높이와 같게
        if (coll.gameObject.tag == "Object")
        {
            if (coll.transform.position.y <= transform.position.y &&
                EnumFlagAttribute.HasFlag(coll.GetComponent<Object>().eInter, Object.EEnvironmentAction.Water))
            {
                Transform obj = coll.transform;
                obj.position = new Vector3(obj.position.x, transform.position.y);
            }
        }


        // 플레이어가 물에 빠졌을 때
        if (coll.gameObject.tag == "Player")
        {
            if (transform.position.y >= coll.transform.position.y + coll.bounds.size.y * 0.5f)
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
}
