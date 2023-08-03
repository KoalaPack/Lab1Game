using System.Collections;
using UnityEngine;
using TMPro;

public class Fading : MonoBehaviour
{
    private bool fadingOut, fadingIn;
    public float fadeSpeed;
    public float delayBetweenFades;

    private TMP_Text textMeshProText;

    private void Start()
    {
        textMeshProText = GetComponent<TMP_Text>();
        StartCoroutine(FadeInOut());
    }

    private IEnumerator FadeInOut()
    {
        while (true)
        {
            if (!fadingOut && !fadingIn)
            {
                fadingOut = true;
                StartCoroutine(FadeTextAlpha(textMeshProText, 1f, 0f));
                yield return new WaitForSeconds(delayBetweenFades);
                fadingOut = false;
            }

            if (!fadingIn && !fadingOut)
            {
                fadingIn = true;
                StartCoroutine(FadeTextAlpha(textMeshProText, 0f, 1f));
                yield return new WaitForSeconds(delayBetweenFades);
                fadingIn = false;
            }

            yield return null;
        }
    }

    private IEnumerator FadeTextAlpha(TMP_Text textMeshProText, float startAlpha, float targetAlpha)
    {
        float currentTime = 0f;
        float fadeDuration = 1f / fadeSpeed;

        while (currentTime < fadeDuration)
        {
            currentTime += Time.deltaTime;
            float alpha = Mathf.Lerp(startAlpha, targetAlpha, currentTime / fadeDuration);
            textMeshProText.color = new Color(textMeshProText.color.r, textMeshProText.color.g, textMeshProText.color.b, alpha);
            yield return null;
        }

        textMeshProText.color = new Color(textMeshProText.color.r, textMeshProText.color.g, textMeshProText.color.b, targetAlpha);
    }
}
