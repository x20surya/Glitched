using UnityEngine;
using UnityEngine.UI;

public class dashTimer : MonoBehaviour
{
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
