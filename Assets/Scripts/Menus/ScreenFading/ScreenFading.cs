using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ScreenFading : MonoBehaviour
{
    public Image fadeImage;
    public float fadeSpeed = 0.5f;

    private float targetAlpha = 0f;
    private bool isFading = false;
    private Coroutine currentFadeRoutine;

    public void FadeToBlack()
    {
        if (currentFadeRoutine != null)
            StopCoroutine(currentFadeRoutine);

        currentFadeRoutine = StartCoroutine(FadeRoutine(1f));
    }

    public void FadeToClear()
    {
        if (currentFadeRoutine != null)
            StopCoroutine(currentFadeRoutine);

        currentFadeRoutine = StartCoroutine(FadeRoutine(0f));
    }

    private IEnumerator FadeRoutine(float target)
    {
        isFading = true;
        targetAlpha = target;

        Color currentColor = fadeImage.color;

        while (!Mathf.Approximately(currentColor.a, targetAlpha))
        {
            float newAlpha = Mathf.MoveTowards(
            currentColor.a,
            targetAlpha,
            fadeSpeed * Time.deltaTime);

            currentColor.a = newAlpha;
            fadeImage.color = currentColor;

            yield return null;
        }

        currentColor.a = targetAlpha;
        fadeImage.color = currentColor;

        isFading = false;
    }

    public bool IsFadingComplete()
    {
        //Debug.Log("Current alpha: " + fadeImage.color.a);
        return Mathf.Approximately(fadeImage.color.a, 0f) || Mathf.Approximately(fadeImage.color.a, 1f);
    }

}
