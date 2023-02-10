using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UI.Extensions;

public class SettingsMenu : MonoBehaviour
{

    Resolution[] resolutions;

    public Dropdown resolutionDropwdown;

    void Start()
    {
        resolutions = Screen.resolutions;

        resolutionDropwdown.ClearOptions();

        List<string> options = new List<string>();

        int resIndex = 0;
        for(int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height;
            options.Add(option);
            
            if(resolutions[i].width == Screen.width && resolutions[i].height == Screen.height)
            {
                resIndex = i;
            }

        }

        resolutionDropwdown.AddOptions(options);
        resolutionDropwdown.value = resIndex;
        resolutionDropwdown.RefreshShownValue();
    }

    public void SetFullScreen(bool isFullScreen)
    {
        Screen.fullScreen = isFullScreen;
    }

    public void SetResolution(int resIndex)
    {
        Resolution resolution = resolutions[resIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }
}
