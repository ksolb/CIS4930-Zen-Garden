using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioSettingsController : MonoBehaviour
{
    [SerializeField] private AudioMixer mixer;
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider ambienceSlider;

    private void Start()
    {
        float savedMusic = PlayerPrefs.GetFloat("MusicVolume", 0.75f);
        float savedAmbience = PlayerPrefs.GetFloat("AmbienceVolume", 0.75f);

        musicSlider.value = savedMusic;
        ambienceSlider.value = savedAmbience;
    }

    public void SetMusicVolume(float sliderValue)
    {
        float dB = sliderValue > 0.0001f ? Mathf.Log10(sliderValue) * 20 : -80f;
        bool success = mixer.SetFloat("MusicVolume", dB);
        Debug.Log($"SetMusicVolume called: slider={sliderValue}, dB={dB}");
        mixer.SetFloat("MusicVolume", dB);
        PlayerPrefs.SetFloat("MusicVolume", sliderValue);
    }

    public void SetAmbienceVolume(float sliderValue)
    {
        float dB = sliderValue > 0.0001f ? Mathf.Log10(sliderValue) * 20 : -80f;
        mixer.SetFloat("AmbienceVolume", dB);
        PlayerPrefs.SetFloat("AmbienceVolume", sliderValue);
    }
}