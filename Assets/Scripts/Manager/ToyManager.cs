using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class ToyManager : Singleton<ToyManager>
{
    private int iPiecesOfToy;
    private int iSceneIndex;
    public int iPiecesCount { get; private set; }
    public int iCurrentToyCount { get; private set; }

    // Use this for initialization
    void Awake ()
    {
        iCurrentToyCount = 3;
        iPiecesOfToy = FindObjectsOfType(typeof(ToyPiece)).Length;
        iPiecesCount = iPiecesOfToy;
        iSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
    }

    public void CollectToy()
    {
        if (iPiecesCount > 0)
            --iPiecesCount;
    }

    void OnGUI()
    {
        // 장난감 조각 현황?
        GUI.Box(new Rect(Screen.width - 150, 30, 120, 30), iPiecesOfToy - iPiecesCount + "/" + iPiecesOfToy);

        // clear 시
        if (iPiecesCount == 0)
        {
            if (GUI.Button(new Rect(Screen.width * 0.5f - 60, Screen.height * 0.5f, 120, 30), "Clear"))
            {
                int index = iSceneIndex;
                if (index != 100)
                    SceneManager.LoadScene(index);
                else
                {
                }
            }
        }
    }
}
