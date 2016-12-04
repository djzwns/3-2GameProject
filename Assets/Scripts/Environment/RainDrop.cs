using UnityEngine;
using System.Collections;

public class RainDrop : MonoBehaviour {

    Transform player;
    Transform myTransform;

    void Start()
    {
        player = GameObject.Find("Player").transform;
        myTransform = transform;
    }

    // 플레이어 머리 위를 따라다닌다.
    void FixedUpdate()
    {
        myTransform.position = new Vector3(player.position.x, player.position.y + 10f);
    }
}
