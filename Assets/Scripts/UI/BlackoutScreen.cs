using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;

public class BlackoutScreen : MonoBehaviour
{
    private Image image;
    private bool fading;
    public Button[] btns;
    private bool startFaded = false;
    [SerializeField] private TMP_Text text;

    private void Awake()
    {
        image = GetComponent<Image>();
    }
    void Start()
    {
        fading = false;
        RectTransform rt = image.GetComponent<RectTransform>();
        rt.anchorMin = new Vector2(0, 0);
        rt.anchorMax = new Vector2(1, 1);
        rt.offsetMin = Vector2.zero;
        rt.offsetMax = Vector2.zero;
    }

    private void OnEnable()
    {
        if (UIManager.Instance.deathscreenOpen) { 
            startFaded = true;
        }
        if (startFaded)
        {
            Color newColor = new(image.color.r, image.color.g, image.color.b, 0.8f);
            fading = false;
            image.color = newColor;
            foreach (Button btn in btns)
            {
                btn.gameObject.SetActive(true);
            }
            text.gameObject.SetActive(true);
        }
        else {
            Color newColor = new(image.color.r, image.color.g, image.color.b, 0f);
            fading = false;
            image.color = newColor;
            foreach (Button btn in btns)
            {
                btn.gameObject.SetActive(false);
            }
            text.gameObject.SetActive(false);
        }
    }

    private void OnDisable()
    {
        startFaded = false;
        Color newColor = new(image.color.r, image.color.g, image.color.b, 0f);
        fading = false;
        image.color = newColor;
        foreach (Button btn in btns)
        {
            btn.gameObject.SetActive(false);
        }
        text.gameObject.SetActive(false);
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
            color.a = Mathf.Lerp(0f, 0.8f, normalizedTime);
            image.color = color;
            yield return null;
        }

        color.a = 0.8f;
        image.color = color;
        fading = false;
        foreach (Button btn in btns) { 
            btn.gameObject.SetActive(true);
        }
        text.gameObject.SetActive(false);
    }

    IEnumerator FadeOut(Image image, float duration) {
        fading = true;
        foreach (Button btn in btns)
        {
            btn.gameObject.SetActive(false);
        }
        text.gameObject.SetActive(false);
        Color color = image.color;
        color.a = 0.8f;
        image.color = color;

        for (float t = 0f; t < duration; t += Time.deltaTime)
        {
            float normalizedTime = t / duration;
            color.a = Mathf.Lerp(0.8f, 0f, normalizedTime);
            image.color = color;
            yield return null;
        }

        color.a = 0f;
        image.color = color;
        fading = false;
    }
}
