using UnityEngine;
using System.Collections;

public class Nightmare : MonoBehaviour {
    // 등장을 간지나게 하기 위한..
    BGMManager BM;
    public AudioClip bgm;
    public GameObject appear;

    // 플레이어 정보
    Transform player;

    Transform nightmare;

    Vector3 vdist;

    float speed = 2f;
    float height = 0.02f;

    public float marginX = 10f;
    public float marginY = 10f;

    void Awake()
    {
        BM = BGMManager.Instance;
        player = GameObject.Find("Player").transform;
        nightmare = transform;
        nightmare.position = new Vector3(player.position.x + marginX, player.position.y + marginY);
        vdist = player.position - nightmare.position;
        
        gameObject.SetActive(false);
    }

    IEnumerator Move()
    {
        float radius = 0;
        Follow();
        while (true)
        {
            radius += Time.deltaTime * speed;
            nightmare.Translate(Vector3.back * Mathf.Cos(radius) * height);
            yield return new WaitForFixedUpdate();
        }
    }

    void Follow()
    {
        //Debug.Log(player.right);
        // 디버그 결과 x축이 아니고 z축을 기준으로 방향이 표기된.. ㅡㅡ 삽질했네
        // 음수일 때 오른쪽을 바라봄
        if (player.right.z < 0)
        {
            nightmare.position = new Vector3(player.position.x + marginX, player.position.y + marginY, player.position.z);
            //nightmare.Rotate(-90f, 0f, -160f);
            nightmare.rotation = Quaternion.Euler(new Vector3(-90f, 0f, -150f));
        }
        else
        {
            nightmare.position = new Vector3(player.position.x - marginX, player.position.y + marginY, player.position.z);
            //nightmare.Rotate(-90f, 0f, - 220f);
            nightmare.rotation = Quaternion.Euler(new Vector3(-90f, 0f, -220f));
        }
    }

    void OnEnable()
    {
        BM.BGMChange(bgm);
        StartCoroutine(Move());
        Poof();
    }

    void OnDisable()
    {
        BM.PrevBGM();
        StopCoroutine(Move());
    }

    public void Poof()
    {
        Instantiate(appear, nightmare.position, Quaternion.identity);
    }
}
