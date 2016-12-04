using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class EndCredit : MonoBehaviour {
    
    public float fSpeed = 0.05f;
    public Text[] texts;
    public Image logo;
    Text parent;

    IEnumerator Start()
    {
        yield return new WaitForSecondsRealtime(0.5f);

        foreach (Text text in texts)
        {
            if (text.gameObject.transform.parent.name != "Canvas")
            {
                parent = text.gameObject.transform.parent.GetComponent<Text>();
                Debug.Log(parent.text);
            }

            // 나타남
            while (text.color.a < 1f)
            {
                text.color = new Color(1, 1, 1, text.color.a + 0.04f);

                if(parent != null)
                    parent.color = new Color(1, 1, 1, parent.color.a + 0.04f);

                yield return new WaitForSecondsRealtime(fSpeed);
            }

            // 사라짐
            while (text.color.a > 0f)
            {
                text.color = new Color(1, 1, 1, text.color.a - 0.08f);

                if (parent != null)
                    parent.color = new Color(1, 1, 1, parent.color.a - 0.08f);

                yield return new WaitForSecondsRealtime(fSpeed);
            }

            yield return new WaitForSecondsRealtime(0.5f);
        }


        if (logo.GetComponentInChildren<Text>() != null)
        {
            parent = logo.GetComponentInChildren<Text>();
        }

        while (logo.color.a < 1f)
        {
            logo.color = new Color(1, 1, 1, logo.color.a + 0.04f);

            if (parent != null)
                parent.color = new Color(1, 1, 1, parent.color.a + 0.04f);

            yield return new WaitForSecondsRealtime(fSpeed);
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene(0);
        }
    }
}
