using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Door : MonoBehaviour {
    public string sceneName;

    PlayerController player;
    Image blind;

    void Start()
    {
        player = GameObject.Find("Player").GetComponent<PlayerController>();
        blind = GameObject.Find("blind").GetComponent<Image>();
    }

    IEnumerator MoveScene()
    {
        while (blind.color.a < 1f)
        {
            blind.color = new Color(blind.color.r, blind.color.g, blind.color.b, blind.color.a + 0.01f);
            yield return new WaitForSeconds(0.05f);
        }
        SceneManager.LoadScene(sceneName);

    }

    void OnTriggerStay(Collider coll)
    {
        if (coll.tag == "Player" && player.bRotating)
        {
            StartCoroutine(MoveScene());
        }
    }
}
