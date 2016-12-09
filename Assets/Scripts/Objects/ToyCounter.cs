using UnityEngine;
using System.Collections;

public class ToyCounter : MonoBehaviour {
    public int totalCollectToy { get; private set; }

    void Start()
    {
        totalCollectToy = 0;
        DontDestroyOnLoad(gameObject);
    }

    public void ToyCollet()
    {
        ++totalCollectToy;
    }

    public void LoseToy(int _amount)
    {
        totalCollectToy -= _amount;
    }
}
