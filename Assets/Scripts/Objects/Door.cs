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
    Image blind;

    void Start()
    {
        player = GameObject.Find("Player").GetComponent<PlayerController>();
        blind = GameObject.Find("blind").GetComponent<Image>();
    }

    IEnumerator MoveNextDoor()
    {
        bMoving = true;

        // 어두짐
        while (blind.color.a < 1f)
        {
            blind.color = new Color(blind.color.r, blind.color.g, blind.color.b, blind.color.a + 0.04f);
            yield return new WaitForSeconds(0.05f);
        }

        // 플레이어 위치 이동
        player.transform.position = linkedDoor.position;
        player.fFixedZ = linkedDoor.position.z - 0.5f;

        // 밝아짐
        while (blind.color.a > 0f)
        {
            blind.color = new Color(blind.color.r, blind.color.g, blind.color.b, blind.color.a - 0.04f);
            yield return new WaitForSeconds(0.05f);
        }

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
