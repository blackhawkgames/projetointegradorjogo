using UnityEngine;
using System.Collections;

public class FloatingIcon : MonoBehaviour
{
    [SerializeField] private CanvasGroup canvasGroup;

    [SerializeField] private float fadeDuration = 0.15f;

    private Vector3 basePosition;

    Coroutine routine;

    float localTime;

    private void Awake()
    {
        canvasGroup.alpha = 0f;
    }

    public void Show()
    {
        gameObject.SetActive(true);

        basePosition = transform.localPosition;

        if (routine != null)
            StopCoroutine(routine);

        routine = StartCoroutine(FadeRoutine(
            canvasGroup.alpha,
            1f));
    }

    public void Hide()
    {
        if (!gameObject.activeSelf)
            return;

        if (routine != null)
            StopCoroutine(routine);

        routine = StartCoroutine(HideRoutine());
    }

    IEnumerator FadeRoutine(
        float start,
        float end)
    {
        float t = 0f;

        while (t < fadeDuration)
        {
            t += Time.deltaTime;

            canvasGroup.alpha =
                Mathf.Lerp(
                    start,
                    end,
                    t / fadeDuration);

            yield return null;
        }

        canvasGroup.alpha = end;
    }

    IEnumerator HideRoutine()
    {
        yield return FadeRoutine(
            canvasGroup.alpha,
            0f);

        gameObject.SetActive(false);
    }

    private void Update()
    {
        localTime += Time.deltaTime;

        transform.localPosition =
            basePosition +
            Vector3.up *
            Mathf.Sin(localTime * 2f) *
            0.03f;
    }
}