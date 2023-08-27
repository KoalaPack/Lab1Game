using UnityEngine;
using UnityEngine.UI;

public class VolumeSlider : MonoBehaviour
{
    public Slider musicSlider;
    public Slider SFXSlider;

    private void Start()
    {
        // Set the slider's value to the saved volume level when the scene starts
        musicSlider.value = VolumeManager.instance.GetVolumeLevel();
        SFXSlider.value = VolumeManager.instance.GetVolumeLevel();
    }

    public void OnVolumeSliderValueChanged(float volume)
    {
        VolumeManager.instance.SetVolumeLevel(volume);
    }
}
