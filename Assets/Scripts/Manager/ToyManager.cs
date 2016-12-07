using UnityEngine;

public class ToyManager : Singleton<ToyManager>
{
    public int iPiecesOfToy { get; private set; }
    public int iPiecesCount { get; private set; }
    public int iCurrentToyCount { get; private set; }

    StageManager sm;

    // Use this for initialization
    void Awake ()
    {
        iCurrentToyCount = 3;
        iPiecesOfToy = FindObjectsOfType(typeof(ToyPiece)).Length;
        iPiecesCount = iPiecesOfToy;        

        sm = StageManager.Instance;
    }

    public void CollectToy()
    {
        if (iPiecesCount > 0)
            --iPiecesCount;
    }

    void Update()
    {
        if (iPiecesCount == 0)
        {
            sm.Clear();
        }
    }
}
