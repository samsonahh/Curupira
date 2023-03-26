using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseCanvasManager : MonoBehaviour
{
    public Button quitButton;
    public Button resumeButton;
    public Button settingsButton;

    public void HoverQuit(float size)
    {
        quitButton.transform.LeanScale(new Vector2(size, size), 0.5f).setEaseOutQuart().setIgnoreTimeScale(true);
    }

    public void HoverResume(float size)
    {
        resumeButton.transform.LeanScale(new Vector2(size, size), 0.5f).setEaseOutQuart().setIgnoreTimeScale(true);
    }

    public void HoverSettings(float size)
    {
        settingsButton.transform.LeanScale(new Vector2(size, size), 0.5f).setEaseOutQuart().setIgnoreTimeScale(true);
    }
}
