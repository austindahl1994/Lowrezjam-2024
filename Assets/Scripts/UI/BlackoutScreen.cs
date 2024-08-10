using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class BlackoutScreen : MonoBehaviour
{
    public Image image;
    private bool fading;
    public Button[] btns;

    void Start()
    {
        fading = false;
        RectTransform rt = image.GetComponent<RectTransform>();
        rt.anchorMin = new Vector2(0, 0);
        rt.anchorMax = new Vector2(1, 1);
        rt.offsetMin = Vector2.zero;
        rt.offsetMax = Vector2.zero;
    }

    public void FadeInBlackout() {
        if (!fading) { 
            StartCoroutine(FadeIn(image, 1.0f));
        }
    }

    public void FadeOutBlackout() {
        if (!fading) { 
            StartCoroutine(FadeOut(image, 0.5f));
        }
    }

    IEnumerator FadeIn(Image image, float duration)
    {
        fading = true;
        Color color = image.color;
        color.a = 0f;
        image.color = color;

        for (float t = 0f; t < duration; t += Time.deltaTime)
        {
            float normalizedTime = t / duration;
            color.a = Mathf.Lerp(0f, 0.5f, normalizedTime);
            image.color = color;
            yield return null;
        }

        color.a = 0.5f;
        image.color = color;
        fading = false;
        foreach (Button btn in btns) { 
            btn.gameObject.SetActive(true);
        }
    }

    IEnumerator FadeOut(Image image, float duration) {
        fading = true;
        foreach (Button btn in btns)
        {
            btn.gameObject.SetActive(false);
        }
        Color color = image.color;
        color.a = 0.5f;
        image.color = color;

        for (float t = 0f; t < duration; t += Time.deltaTime)
        {
            float normalizedTime = t / duration;
            color.a = Mathf.Lerp(0.5f, 0f, normalizedTime);
            image.color = color;
            yield return null;
        }

        color.a = 0f;
        image.color = color;
        fading = false;
    }
}
