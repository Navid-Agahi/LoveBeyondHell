using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SettingMenu : MonoBehaviour
{
    

    public void Setquality (int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
    }
    public void SetFullscreen (bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
    }
}
