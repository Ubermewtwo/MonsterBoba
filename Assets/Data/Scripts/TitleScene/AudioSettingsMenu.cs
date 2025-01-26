using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class AudioSettingsMenu : MonoBehaviour
{
    [Header("UI Sliders")]
    public Slider masterSlider;
    public Slider musicSlider;
    public Slider sfxSlider;

    [Header("Audio Mixer")]
    public AudioMixer audioMixer; // Assign your Audio Mixer here
    private const string MasterVolumeKey = "MasterVolume"; // Exposed parameter in the Audio Mixer
    private const string MusicVolumeKey = "MusicVolume";   // Exposed parameter in the Audio Mixer
    private const string SfxVolumeKey = "SfxVolume";       // Exposed parameter in the Audio Mixer

    private const string MasterSliderKey = "MasterSliderValue"; // For saving slider value
    private const string MusicSliderKey = "MusicSliderValue";
    private const string SfxSliderKey = "SfxSliderValue";

    void Start()
    {
        // Load saved values or set default slider values (1f = 0 dB)
        masterSlider.value = PlayerPrefs.GetFloat(MasterSliderKey, 1f);
        musicSlider.value = PlayerPrefs.GetFloat(MusicSliderKey, 1f);
        sfxSlider.value = PlayerPrefs.GetFloat(SfxSliderKey, 1f);

        // Apply loaded settings to Audio Mixer
        ApplyMasterVolume(masterSlider.value);
        ApplyMusicVolume(musicSlider.value);
        ApplySfxVolume(sfxSlider.value);

        // Add listeners to sliders
        masterSlider.onValueChanged.AddListener(ApplyMasterVolume);
        musicSlider.onValueChanged.AddListener(ApplyMusicVolume);
        sfxSlider.onValueChanged.AddListener(ApplySfxVolume);
    }

    public void ApplyMasterVolume(float value)
    {
        // Convert slider (0–1) to decibels (-80 to 0 dB) and set on Audio Mixer
        audioMixer.SetFloat(MasterVolumeKey, SliderToDecibels(value));
        PlayerPrefs.SetFloat(MasterSliderKey, value); // Save slider value
    }

    public void ApplyMusicVolume(float value)
    {
        audioMixer.SetFloat(MusicVolumeKey, SliderToDecibels(value));
        PlayerPrefs.SetFloat(MusicSliderKey, value); // Save slider value
    }

    public void ApplySfxVolume(float value)
    {
        audioMixer.SetFloat(SfxVolumeKey, SliderToDecibels(value));
        PlayerPrefs.SetFloat(SfxSliderKey, value); // Save slider value
    }

    public void ResetToDefaults()
    {
        // Reset sliders to default values
        masterSlider.value = 1f;
        musicSlider.value = 1f;
        sfxSlider.value = 1f;

        // Apply default volumes to Audio Mixer
        ApplyMasterVolume(1f);
        ApplyMusicVolume(1f);
        ApplySfxVolume(1f);
    }

    private float SliderToDecibels(float value)
    {
        // Converts slider value (0–1) to a logarithmic decibel scale
        // Clamp value to avoid Log10(0) issues
        return Mathf.Clamp01(value) > 0f ? Mathf.Log10(value) * 20f : -80f;
    }
}
