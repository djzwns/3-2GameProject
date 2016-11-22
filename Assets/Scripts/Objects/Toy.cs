using UnityEngine;
using System.Collections;

public class Toy : MonoBehaviour {
    Transform trTarget;
    Transform[] childTarget;
    Transform toyTransform;

    Vector3 vTarget;

    public float fXMargin;
    public float fSmoothMove;

    public int index { get; private set; }

    void Start()
    {
        toyTransform = transform;

        childTarget = GameObject.Find("Toy").GetComponentsInChildren<Transform>();
        if (childTarget.Length == 1)
        {
            trTarget = GameObject.Find("Player").GetComponent<Transform>();
        }
        else
        {
            for (int i = 1; i < childTarget.Length; ++i)
            {
                if (toyTransform == childTarget[i])
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
        float fToyX = toyTransform.position.x;
        float fToyY = toyTransform.position.y;

        vTarget = trTarget.transform.position;

        if (Mathf.Abs(toyTransform.position.x - trTarget.transform.position.x) > fXMargin)
        {
            fToyX = Mathf.Lerp(toyTransform.position.x, vTarget.x, Time.deltaTime * fSmoothMove);
        }
        fToyY = Mathf.Lerp(toyTransform.position.y, vTarget.y, Time.deltaTime * fSmoothMove);

        toyTransform.position = new Vector3(fToyX, fToyY, trTarget.position.z);
    }
}
