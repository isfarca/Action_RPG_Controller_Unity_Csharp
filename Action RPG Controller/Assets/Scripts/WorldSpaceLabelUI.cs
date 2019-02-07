using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class WorldSpaceLabelUI : MonoBehaviour
{
    // Value types
    [SerializeField] private float fadeInDuration = 0.2f;
    [SerializeField] private float fadeOutDuration = 1f;

    // Reference types
    [SerializeField] private Vector3 fadeInOffset;
    [SerializeField] private Vector3 fadeOutOffset;
    [SerializeField] private Text textComponent;
    private CanvasGroup canvasGroup;

    private void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
    }

    public void Init(Vector3 position, string text)
    {
        transform.position = position;
        textComponent.text = text;

        StartCoroutine(Animation());
    }

    IEnumerator Animation()
    {
        yield return StartCoroutine(FadeIn(fadeInDuration));
        // Wait for x seconds.
        yield return StartCoroutine(FadeOut(fadeOutDuration));

        Destroy(gameObject);
    }

    IEnumerator FadeIn(float duration)
    {
        // Declare variables
        Vector3 fromPosition = transform.position;
        Vector3 targetPosition = transform.position + fadeInOffset;

        Debug.Log("FadeIn start");

        canvasGroup.alpha = 0f;

        // Fade in animation.
        for (float t = 0f; t < duration; t += Time.deltaTime)
        {
            transform.position = Vector3.Lerp(fromPosition, targetPosition, t / duration);
            canvasGroup.alpha = t / duration;

            yield return new WaitForEndOfFrame();
        }

        transform.position = targetPosition;
        canvasGroup.alpha = 1f;

        Debug.Log("FadeIn end");
    }

    IEnumerator FadeOut(float duration)
    {
        Vector3 fromPosition = transform.position;
        Vector3 targetPosition = transform.position + fadeOutOffset;

        Debug.Log("FadeOut start");

        canvasGroup.alpha = 1f;

        // fade out animation
        for (float t = 0; t < duration; t += Time.deltaTime)
        {
            transform.position = Vector3.Lerp(fromPosition, targetPosition, t / duration);
            canvasGroup.alpha = 1f - (t / duration);
            yield return new WaitForEndOfFrame();
            // yield return null;
        }

        transform.position = targetPosition;
        canvasGroup.alpha = 0;


        Debug.Log("FadeOut end");
    }
}
