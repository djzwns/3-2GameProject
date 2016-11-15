using UnityEngine;
using System.Collections;

public class Toy : MonoBehaviour {
    Transform trTarget;
    Transform[] childTarget;

    Vector3 vTarget;

    public float fXMargin;
    public float fSmoothMove;

    public int index { get; private set; }

    void Start()
    {
        childTarget = GameObject.Find("Toy").GetComponentsInChildren<Transform>();
        if (childTarget.Length == 1)
        {
            trTarget = GameObject.Find("Player").GetComponent<Transform>();
        }
        else
        {
            for (int i = 1; i < childTarget.Length; ++i)
            {
                if (transform == childTarget[i])
                {
                    if (i == 1)
                        trTarget = GameObject.Find("Player").GetComponent<Transform>();
                    else
                    {
                        trTarget = childTarget[i - 1];
                    }
                    index = i;
                }
            }
        }
        //Debug.Log(index + ":" + transform.name);
    }

    void FixedUpdate()
    {
        float fToyX = transform.position.x;
        float fToyY = transform.position.y;

        vTarget = trTarget.transform.position;

        if (Mathf.Abs(transform.position.x - trTarget.transform.position.x) > fXMargin)
        {
            fToyX = Mathf.Lerp(transform.position.x, vTarget.x, Time.deltaTime * fSmoothMove);
        }
        fToyY = Mathf.Lerp(transform.position.y, vTarget.y, Time.deltaTime * fSmoothMove);

        transform.position = new Vector3(fToyX, fToyY);
    }
}
