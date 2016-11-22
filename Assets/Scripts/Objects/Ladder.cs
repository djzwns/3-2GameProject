using UnityEngine;
using System.Collections;

public class Ladder : MonoBehaviour {

    bool bClimbing = false;

    public float fSpeed = 3.0f;

    PlayerController player;
    Transform target;

    void Start()
    {
        player = PlayerController.Instance;
    }

    void OnTriggerStay(Collider coll)
    {
        if (coll.tag == "Player")
        {
            if (Input.GetKeyDown(KeyCode.UpArrow) && !bClimbing)
            {
                target = coll.transform;
                player.SetClimbing(bClimbing = true);
                PlayerAnimManager.Instance.Climb(bClimbing);
                return;
            }
            // x키 누르면 사다리 내려옴
            else if (Input.GetButton("Jump") && bClimbing)
            {
                player.SetClimbing(bClimbing = false);
                target.GetComponent<CharacterController>().stepOffset = 0.3f;
                PlayerAnimManager.Instance.Climb(bClimbing);
                target = null;
            }

            if (bClimbing)
            {
                target.GetComponent<CharacterController>().stepOffset = 0f;

                if (Input.GetKey(KeyCode.UpArrow))
                {
                    target.Translate(Vector3.up * Time.deltaTime * fSpeed);
                }
                else if (Input.GetKey(KeyCode.DownArrow))
                {
                    target.Translate(Vector3.down * Time.deltaTime * fSpeed);
                }
            }
        }
    }


    void OnTriggerExit(Collider coll)
    {
        if (target != null)
        {
            target.GetComponent<CharacterController>().stepOffset = 0.3f;
            target.Translate(Vector3.forward * Time.deltaTime * 20);
            player.SetClimbing(bClimbing = false);
            PlayerAnimManager.Instance.Climb(bClimbing);
            target = null;
        }
    }
}
