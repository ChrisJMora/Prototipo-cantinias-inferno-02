using UnityEngine;
using UnityEngine.UI;

public class VolumeManager : MonoBehaviour
{
    [SerializeField] private float defaultVolume;
    [SerializeField] private Slider slider;
    [SerializeField] private Image imageMute;
    private float sliderValue;

    void Start()
    {
        slider.value = PlayerPrefs.GetFloat("volume", defaultVolume);
        AudioListener.volume = slider.value;
        IsMuted();
    }

    public void ChangeSlider(float value)
    {
        sliderValue = value;
        PlayerPrefs.SetFloat("volume", sliderValue);
        AudioListener.volume = sliderValue;
        IsMuted();
    }

    private void IsMuted()
    {
        if (sliderValue == 0)
        {
            imageMute.enabled = true;
        }
        else
        {
            imageMute.enabled = false;
        }
    }
}
