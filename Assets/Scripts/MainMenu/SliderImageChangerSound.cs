using UnityEngine;
using UnityEngine.UI;

public class SliderImageChangerSound : MonoBehaviour
{
    public Image image;
    public Sprite[] imagesToChange;
    public Slider slider;

    void Start()
    {
        // Add a listener to the slider to call the method when the value changes
        slider.onValueChanged.AddListener(OnSliderValueChanged);
    }

    void OnSliderValueChanged(float value)
    {
        int index = Mathf.RoundToInt(value); // Round to the nearest integer to select an image from the array
        if (index >= 0 && index < imagesToChange.Length)
        {
            image.sprite = imagesToChange[index];
        }
    }

}
