using UnityEditor.Build.Content;
using UnityEngine;
using UnityEngine.UI;

public class VolumeSlider : MonoBehaviour
{
    [SerializeField]
    Slider slider;
    float curValue;
    public bool isBgm;

    private void Start()
    {
        if (isBgm)
            curValue = AudioManager.instance.bgmSound;
        else
            curValue = AudioManager.instance.seSound;

        slider.value = curValue;
    }

    public void OnSliderEvent(float volume)
    {
        curValue = volume;

        if (isBgm)
            AudioManager.instance.SetBgmSound(volume);
        else
            AudioManager.instance.SetSeSound(volume);
    }
}
