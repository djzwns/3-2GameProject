using UnityEngine;
using System.Collections;

public class GameManager : Singleton<GameManager> {
    private int iPiecesOfToy;
    private int iToyCount;

    void Start()
    {
        iPiecesOfToy = FindObjectsOfType(typeof(ToyPiece)).Length;
        iToyCount = iPiecesOfToy;
    }

    public void CollectToy()
    {
        if (iToyCount > 0)
            --iToyCount;
    }

    void OnGUI()
    {
        GUI.Box(new Rect(Screen.width-240, Screen.height-80, 240, 70), iPiecesOfToy - iToyCount + "/" + iPiecesOfToy);

        if (iToyCount == 0)
        {
            GUI.Box(new Rect(Screen.width * 0.5f - 120, Screen.height * 0.5f, 240, 70), "clear");
        }
    }
}
