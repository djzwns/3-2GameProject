using UnityEngine;
using System.Collections;

public class Fire : MonoBehaviour {
    Object obj; // 충돌한 오브젝트 컴포넌트

    void OnTriggerEnter(Collider coll)
    {
        if (coll.gameObject.tag == "Object")
        {
            obj = coll.GetComponent<Object>();

            // 오브젝트가 불에 영향을 받으면 실행
            if (EnumFlagAttribute.HasFlag(obj.eInter, Object.EInteraction.Fire))
            {
                Destroy(coll.gameObject);
            }
        }
    }
}
