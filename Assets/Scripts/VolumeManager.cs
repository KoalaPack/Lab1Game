using UnityEngine;

public class VolumeManager : MonoBehaviour
{
    public static VolumeManager instance;

    // Add any additional variables you need
    private float volumeLevel = 1.0f; // Default volume level

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public float GetVolumeLevel()
    {
        return volumeLevel;
    }

    public void SetVolumeLevel(float volume)
    {
        volumeLevel = Mathf.Clamp01(volume); // Ensure volume is between 0 and 1
    }
}
