using UnityEngine;

public class ToyManager : Singleton<ToyManager>
{
    private int iPiecesOfToy;
    public int iPiecesCount { get; private set; }
    public int iCurrentToyCount { get; private set; }

    public GUIStyle style;
    float fWidth;
    float fHeight;
    float fMarginx = 10f;
    float fMarginy = 10f;
    bool bClear = false;

    StageManager sm;

    // Use this for initialization
    void Awake ()
    {
        iCurrentToyCount = 3;
        iPiecesOfToy = FindObjectsOfType(typeof(ToyPiece)).Length;
        iPiecesCount = iPiecesOfToy;

        fWidth = Screen.width * 0.25f;
        fHeight = Screen.height * 0.2f;

        sm = StageManager.Instance;
    }

    public void CollectToy()
    {
        if (iPiecesCount > 0)
            --iPiecesCount;
    }

    void OnGUI()
    {
        // 장난감 조각 GUI
        if (!sm.bEnd)
        {
            GUI.BeginGroup(new Rect(Screen.width - fWidth - fMarginx, fMarginy, fWidth, fHeight));
            GUI.Label(new Rect(0, 0, fWidth, fHeight), iPiecesOfToy - iPiecesCount + " / " + iPiecesOfToy, style);
            GUI.EndGroup();
        }

        // clear 시 GUI
        if (iPiecesCount == 0)
        {
            sm.Clear();
        }
    }
}
