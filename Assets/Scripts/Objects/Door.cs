using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Door : MonoBehaviour {
    // 연결되는 문. 문열고 드가면 이 문으로 나옴
    public Transform linkedDoor;

    // 이동 중 체크용 불리언 변수
    static bool bMoving = false;

    PlayerController player;

    void Start()
    {
        player = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    IEnumerator MoveNextDoor()
    {
        bMoving = true;

        // 어두짐
        yield return StageManager.Instance.FadeOut(0.05f);

        // 플레이어 위치 이동
        player.transform.position = linkedDoor.position;
        player.fFixedZ = linkedDoor.GetComponent<Collider>().bounds.center.z;

        // 밝아짐
        yield return StageManager.Instance.FadeIn(0.05f);

        bMoving = false;
    }

    void OnTriggerStay(Collider coll)
    {
        if (coll.tag == "Player" && player.bRotating && !bMoving)
        {
            StartCoroutine("MoveNextDoor");
        }
    }
}
