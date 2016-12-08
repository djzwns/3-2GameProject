using UnityEngine;
using System.Collections;

public class ParticleAutoDestroy : MonoBehaviour
{

    ParticleSystem particle;

    void Awake()
    {
        particle = GetComponent<ParticleSystem>();
    }

    void Update()
    {
        if (particle.IsAlive() == false)
        {
            Destroy(gameObject);
        }
    }
}
