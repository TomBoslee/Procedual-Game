using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using UnityEngine.UI;
using TMPro;

public class MainMenu : MonoBehaviour
{
    public AudioMixer masterMixer;

    public Dropdown ResolutionDropdown;

    public TMP_InputField SeedInput;

    Resolution[] resolutions;

    private void Start()
    {
        //Gets auto Resolution
        resolutions = Screen.resolutions;

        ResolutionDropdown.ClearOptions();

        List<string> Options = new List<string>();

        //Creates list for resolutions
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
    //Below is button options that are called in buttons.
    public void PlayGame() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1); 
        WorldInfo.SetSeed(SeedInput.text);
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

    public void SetDifficuty(int diff) { WorldInfo.SetDifficulty(diff);}

    public void SetGamemode(int index) {
        if (index == 0) { WorldInfo.Endless = false; }
        else if (index == 1) { WorldInfo.Endless = true; }
    }

    public void SetResolution(int resolutionIndex) { 
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width,resolution.height, Screen.fullScreen);
    }

}
