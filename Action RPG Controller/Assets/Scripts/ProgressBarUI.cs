using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBarUI : MonoBehaviour
{
    // Value types
    [SerializeField, Range(0.01f, 10f)] private float duration = 5f;

    // Reference types
    private Slider slider;

    private void Awake()
    {
        slider = GetComponent<Slider>();
    }

    private void OnEnable()
    {
        StartCoroutine(FillSlider(duration));
    }

    IEnumerator FillSlider(float duration)
    {
        // Declare variables
        float progress = 0f;

        slider.value = 0f;

        for (float t = 0f; t < duration; t += Time.deltaTime)
        {
            progress = t / duration;
            slider.value = progress;

            yield return new WaitForEndOfFrame();
        }

        slider.value = 1f;
    }
}
