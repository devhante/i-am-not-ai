using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsUI : UI<SettingsUI>
{
    public Slider sliderBackgroundSound;
    public Slider sliderEffectSound;
    public Slider sliderSensitivity;
    public Button buttonOK;

    protected override void Awake()
    {
        base.Awake();
        buttonOK.onClick.AddListener(() => { Close(); });
        sliderBackgroundSound.onValueChanged.AddListener(OnSliderBackgroundSoundValueChanged);
        sliderEffectSound.onValueChanged.AddListener(OnSliderEffectSoundValueChanged);
        sliderSensitivity.onValueChanged.AddListener(OnSliderSensitivityValueChanged);
    }

    private void OnSliderBackgroundSoundValueChanged(float value)
    {
        Debug.Log("1 SliderBackgroundSound = " + value);
    }

    private void OnSliderEffectSoundValueChanged(float value)
    {
        Debug.Log("2 SliderEffectSound = " + value);
    }
    private void OnSliderSensitivityValueChanged(float value)
    {
        Debug.Log("3 SliderSensitivity = " + value);
    }
}
