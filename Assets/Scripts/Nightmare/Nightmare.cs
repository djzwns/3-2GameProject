using UnityEngine;
using System.Collections;

public class Nightmare : MonoBehaviour {
    // bgm
    BGMManager BM;
    public AudioClip bgm;

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
        while (true)
        {
            Follow();
            radius += Time.deltaTime * speed;
            nightmare.Translate(Vector3.back * Mathf.Cos(radius) * height);
            yield return new WaitForFixedUpdate();
        }
    }

    void Follow()
    {
        nightmare.position = new Vector3(player.position.x + marginX, nightmare.position.y, player.position.z);
    }

    void OnEnable()
    {
        BM.BGMChange(bgm);
        StartCoroutine(Move());
    }

    void OnDisable()
    {
        BM.PrevBGM();
        StopCoroutine(Move());
    }
}
