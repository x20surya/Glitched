using UnityEngine;
using UnityEngine.UI;

public class possessTimer : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public Slider slider;
    private float timeForDash = 0;
    private float maxDuration = 2f;
    public void SetMaxDuration(float duration)
    {
        slider.maxValue = duration;
        slider.value = 0;
        maxDuration = duration;
    }
    public void StartTimer()
    {
        timeForDash = maxDuration;
    }

    void Update()
    {
        timeForDash = Mathf.Max(0,timeForDash - Time.deltaTime);
        slider.value = timeForDash;
    }
}
