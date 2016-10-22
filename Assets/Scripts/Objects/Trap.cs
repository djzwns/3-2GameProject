using UnityEngine;
using System.Collections;

public class Trap : MonoBehaviour {

    // 함정 발동 위치
    public GameObject trapPosition;

    // 발동 될 함정
    public GameObject trap;

    GameObject tempTrap;

    // 생성된 트랩 삭제용.. 아마 다른 스크립트에서 불러올 듯.
    void TrapDelete()
    {
        Destroy(tempTrap);
    }

    void OnTriggerEnter(Collider coll)
    {
        if (coll.gameObject.tag == "Player")
        {
            if (tempTrap == null)
                tempTrap = (GameObject)Instantiate(trap, trapPosition.transform.position, Quaternion.identity);
        }
    }
}
