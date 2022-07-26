using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    [SerializeField] Slider volumeMusicSlider;
    [SerializeField] Slider volumeSoundSlider;


    [SerializeField] AudioMixer audioMixer;
    // Start is called before the first frame update

    private void Awake()
    {
        volumeMusicSlider.onValueChanged.AddListener(SetMusicVolume);
        volumeSoundSlider.onValueChanged.AddListener(SetSoundVolume);
    }
    void Start()
    {

        if (!PlayerPrefs.HasKey("musicVolume"))
        {
            PlayerPrefs.SetFloat("musicVolume", 0.2f);
        }
        if (!PlayerPrefs.HasKey("soundVolume"))
        {
            PlayerPrefs.SetFloat("soundVolume", 0.2f);
        }
        Load();

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            PlayerPrefs.DeleteAll();
        }
    }

    private void Load()
    {
        volumeMusicSlider.value = PlayerPrefs.GetFloat("musicVolume");
        volumeSoundSlider.value = PlayerPrefs.GetFloat("soundVolume");
        audioMixer.SetFloat("MusicVolume", Mathf.Log10(volumeMusicSlider.value) * 20);
        audioMixer.SetFloat("SoundVolume", Mathf.Log10(volumeSoundSlider.value) * 20);
    }

    private void Save()
    {
        PlayerPrefs.SetFloat("musicVolume", volumeMusicSlider.value);
        PlayerPrefs.SetFloat("soundVolume", volumeSoundSlider.value);
    }

    private void SetMusicVolume(float value)
    {
        audioMixer.SetFloat("MusicVolume", Mathf.Log10(value) * 20);
        Save();
    }

    private void SetSoundVolume(float value)
    {
        audioMixer.SetFloat("SoundVolume", Mathf.Log10(value) * 20);
        Save();
    }


}



