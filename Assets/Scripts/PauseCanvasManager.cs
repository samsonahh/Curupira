using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseCanvasManager : MonoBehaviour
{
    public Button resetButton;
    public Button quitButton;
    public Button resumeButton;
    public Button settingsButton;
    public GameObject confirmPanel;
    public Button yesButtonQuit;
    public Button yesButtonReset;
    public Button noButton;
    public void HoverReset(float size)
    {
        resetButton.transform.LeanScale(new Vector2(size, size), 0.5f).setEaseOutQuart().setIgnoreTimeScale(true);
    }

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

    public void HoverYesQuit(float size)
    {
        yesButtonQuit.transform.LeanScale(new Vector2(size, size), 0.5f).setEaseOutQuart().setIgnoreTimeScale(true);
    }

    public void HoverYesReset(float size)
    {
        yesButtonReset.transform.LeanScale(new Vector2(size, size), 0.5f).setEaseOutQuart().setIgnoreTimeScale(true);
    }

    public void HoverNo(float size)
    {
        noButton.transform.LeanScale(new Vector2(size, size), 0.5f).setEaseOutQuart().setIgnoreTimeScale(true);
    }

    public void CloseConfirm()
    {
        StopAllCoroutines();
        StartCoroutine(CloseConfirmAnimate());
    }

    public IEnumerator CloseConfirmAnimate()
    {
        confirmPanel.LeanScale(Vector3.zero, 0.5f).setEaseOutQuart().setIgnoreTimeScale(true);
        yield return new WaitForSeconds(0.5f);
        confirmPanel.SetActive(false);
    }

    public void OpenConfirmQuit()
    {
        confirmPanel.SetActive(true);

        yesButtonReset.gameObject.SetActive(false);
        yesButtonQuit.gameObject.SetActive(true);

        confirmPanel.transform.localScale = Vector3.zero;
        confirmPanel.LeanScale(new Vector3(1, 1, 1), 0.5f).setEaseOutQuart().setIgnoreTimeScale(true);
    }

    public void OpenConfirmReset()
    {
        confirmPanel.SetActive(true);

        yesButtonReset.gameObject.SetActive(true);
        yesButtonQuit.gameObject.SetActive(false);

        confirmPanel.transform.localScale = Vector3.zero;
        confirmPanel.LeanScale(new Vector3(1, 1, 1), 0.5f).setEaseOutQuart().setIgnoreTimeScale(true);
    }
}
