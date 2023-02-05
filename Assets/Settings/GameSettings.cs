using System;
using UnityEngine;

public static class GameSettings
{
    public static bool SoundOn { get; set; }

    public static float SoundVolume { get; set; }
    //TODO: взаимодействие со звуко-музыкой в самой игре

    public static float MusicVolume { get; set; }

    private static bool fullScreen;
    public static bool FullScreen
    {
        get => fullScreen;
        set
        {
            fullScreen = value;
            Screen.fullScreen = value;
        }
    }

    private static Resolution sR = Screen.currentResolution;
    public static Resolution ScreenResolution
    {
        get => sR;
        set
        {
            sR = value;
            Screen.SetResolution(sR.width, sR.height, FullScreen, sR.refreshRate);
        }
    }
}
