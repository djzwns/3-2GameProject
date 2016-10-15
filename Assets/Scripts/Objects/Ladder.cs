using UnityEngine;
using System.Collections;

public class Ladder : MonoBehaviour {

    bool bClimbing = false;

    public float fSpeed = 3.0f;

    Transform target;

    void OnTriggerStay(Collider coll)
    {
        if (Input.GetKeyDown(KeyCode.UpArrow) && !bClimbing)
        {
            target = coll.transform;
            target.GetComponent<PlayerController>().SetClimbing(bClimbing = true);
            return;
        }
        // x키 누르면 사다리 내려옴
        else if (Input.GetKeyDown(KeyCode.X) && bClimbing)
        {
            target.GetComponent<PlayerController>().SetClimbing(bClimbing = false);
            target.GetComponent<CharacterController>().stepOffset = 0.3f;
            target = null;
        }

        if (bClimbing)
        {
            target.GetComponent<CharacterController>().stepOffset = 0f;

            target.position = new Vector3(transform.position.x, target.position.y, 0f);

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


    void OnTriggerExit(Collider coll)
    {
        if (target != null)
        {
            target.GetComponent<CharacterController>().stepOffset = 0.3f;
            target.Translate(Vector3.forward * Time.deltaTime * 20);
            target.GetComponent<PlayerController>().SetClimbing(bClimbing = false);
            target.position = new Vector3(target.position.x, target.position.y, 0f);
            target = null;
        }
    }
}
