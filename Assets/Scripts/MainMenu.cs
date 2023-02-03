using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using Unity.UI;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public AudioMixer masterMixer;

    public Dropdown ResolutionDropdown;

    Resolution[] resolutions;

    private void Start()
    {
        resolutions = Screen.resolutions;

        ResolutionDropdown.ClearOptions();

        List<string> Options = new List<string>();

        int currentResolutionIndex = 0;
        for (int i = 0; i  < resolutions.Length; i++)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height;
            Options.Add(option);

            if (resolutions[i].width == Screen.currentResolution.width &&
                resolutions[i].height == Screen.currentResolution.height) { 
                currentResolutionIndex= i;
            }
        }

        ResolutionDropdown.AddOptions(Options);
        ResolutionDropdown.value = currentResolutionIndex;
        ResolutionDropdown.RefreshShownValue();

    }
    public void PlayGameEndless()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        WorldInfo.Endless = true;
    }

    public void PlayGameSeed()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        WorldInfo.Endless = false;
    }


    public void QuitGame() { 
        Application.Quit();
    }

    public void SetVolume(float volume) {
        masterMixer.SetFloat("MasterVolume", volume);
    }

    public void SetQuality(int qualityIndex) { 
        QualitySettings.SetQualityLevel(qualityIndex);
    }

    public void SetFullScreen(bool isFullScreen) { 
        Screen.fullScreen = isFullScreen;
    }

    public void SetResolution(int resolutionIndex) { 
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width,resolution.height, Screen.fullScreen);
    }

}
