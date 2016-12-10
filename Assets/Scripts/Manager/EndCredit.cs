using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class EndCredit : MonoBehaviour {

    public float fFadeSpeed = 0.03f;
    public float fHoldTime = 1.5f;
    public float fSpeed = 0.08f;

    public Text[] fadingTexts;
    Text parent;

    public RectTransform scrollUI;
    public RectTransform scrollEnd;
    public float scrollSpeed = 0.5f;

    public Image logo;

    void Awake()
    {
        BGMManager.Instance.BGMChange(SceneManager.GetActiveScene().buildIndex);
    }

    IEnumerator Start()
    {
        yield return new WaitForSecondsRealtime(0.5f);

        yield return TextFade();

        yield return Scroll();

        yield return LogoFade();
    }

    IEnumerator TextFade()
    {
        foreach (Text text in fadingTexts)
        {
            if (text.gameObject.transform.parent.name != "Canvas")
            {
                parent = text.gameObject.transform.parent.GetComponent<Text>();
            }

            // 나타남
            while (text.color.a < 1f)
            {
                text.color = new Color(1, 1, 1, text.color.a + fFadeSpeed);

                if (parent != null)
                    parent.color = new Color(1, 1, 1, parent.color.a + fFadeSpeed);

                text.transform.localScale += Vector3.one * (fFadeSpeed * 0.1f);

                yield return new WaitForSecondsRealtime(fSpeed);
            }

            float time = fHoldTime / fSpeed;
            for (int i = 0; i < time; ++i)
            {
                text.transform.localScale += Vector3.one * (fFadeSpeed * 0.1f);
                yield return new WaitForSecondsRealtime(fSpeed);
            }

            // 사라짐
            while (text.color.a > 0f)
            {
                text.color = new Color(1, 1, 1, text.color.a - fFadeSpeed * 2f);

                if (parent != null)
                    parent.color = new Color(1, 1, 1, parent.color.a - fFadeSpeed * 2f);

                text.transform.localScale += Vector3.one * (fFadeSpeed * 0.1f);

                yield return new WaitForSecondsRealtime(fSpeed);
            }

            yield return new WaitForSecondsRealtime(fHoldTime);
        }
    }

    IEnumerator LogoFade()
    {
        if (logo.GetComponentInChildren<Text>() != null)
        {
            parent = logo.GetComponentInChildren<Text>();
        }

        while (true)
        {
            logo.color = new Color(1, 1, 1, logo.color.a + fFadeSpeed);

            if (parent != null)
                parent.color = new Color(1, 1, 1, parent.color.a + fFadeSpeed);

            logo.transform.localScale += Vector3.one * (fFadeSpeed * 0.1f);
            yield return new WaitForSecondsRealtime(fSpeed);
        }
    }

    IEnumerator Scroll()
    {
        while (scrollUI.position.y < scrollEnd.position.y)
        {
            scrollUI.position =  new Vector3(scrollUI.position.x, scrollUI.position.y + scrollSpeed, scrollUI.position.z);
            yield return new WaitForFixedUpdate();
        }
    }

    void Update()
    {
        if (Input.anyKeyDown)
        {
            BGMManager.Instance.Mute();
            SceneManager.LoadScene(0);
        }
    }
}
