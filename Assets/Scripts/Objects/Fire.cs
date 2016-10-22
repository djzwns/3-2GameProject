using UnityEngine;
using System.Collections;

public class Fire : MonoBehaviour {
    Object obj; // 충돌한 오브젝트 컴포넌트
    CharacterController character;
    Vector3 vForce = Vector3.zero;

    public float fKnockBackPower = 7f;

    void Update()
    {
        // 부딫힌 플레이어 있고 충력량이 남았을 떄 밀어냄
        if (character != null && vForce.magnitude > 0.2)
        {
            character.Move(vForce * Time.deltaTime);

        }

        // 힘이 점점 약해지도록.. 
        vForce = Vector3.Lerp(vForce, Vector3.zero, 5f * Time.deltaTime);

        // 힘이 일정량 떨어지면 플레이어 정보 지움
        if (vForce.magnitude < 0.2 && character != null)
        {
            character = null;
        }
    }

    void OnTriggerEnter(Collider coll)
    {
        if (coll.gameObject.tag == "Object")
        {
            obj = coll.GetComponent<Object>();

            // 오브젝트가 불에 영향을 받으면 실행
            if (EnumFlagAttribute.HasFlag(obj.eInter, Object.EEnvironmentAction.Fire))
            {
                Destroy(coll.gameObject);
            }
        }

        // 플레이어가 불에 닿으면 반대방향으로 힘을 줌
        if (coll.gameObject.tag == "Player")
        {
            GameManager.Instance.getplayer.TakeDamage(1);

            character = coll.GetComponent<CharacterController>();
            Vector3 vKnockBackDirection = character.velocity.normalized * -1f + Vector3.up;
            vForce += vKnockBackDirection.normalized * fKnockBackPower;
        }
    }
}
