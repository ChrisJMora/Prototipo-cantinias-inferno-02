using UnityEngine;
using UnityEngine.UI;

public class BrightnessManager : MonoBehaviour
{
    [SerializeField] private float defaultBrightness;
    [SerializeField] private Slider slider;
    [SerializeField] private Image brightnessPanel;
    private float sliderValue;

    void Start()
    {
        slider.value = PlayerPrefs.GetFloat("brightness", defaultBrightness);
        brightnessPanel.color = new Color(
            brightnessPanel.color.r,
            brightnessPanel.color.g,
            brightnessPanel.color.b,
            slider.value
        );
    }

    public void ChengeSlider(float value)
    {
        sliderValue = value;
        PlayerPrefs.SetFloat("brightness", sliderValue);
        brightnessPanel.color = new Color(
            brightnessPanel.color.r,
            brightnessPanel.color.g,
            brightnessPanel.color.b,
            slider.value
        );
    }
}
