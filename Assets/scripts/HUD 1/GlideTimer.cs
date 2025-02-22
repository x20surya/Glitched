using UnityEngine;
using UnityEngine.UI;

public class GlideTimer : MonoBehaviour
{
    public Slider slider;
    private float timeForGlide = 0;
    private float maxDuration = 2f;
    public void SetMaxDuration(float duration)
    {
        slider.maxValue = duration;
        slider.value = 0;
        maxDuration = duration;
    }
    public void SetTimer(float time)
    {
        timeForGlide = time;
        slider.value = timeForGlide;
    }
}
