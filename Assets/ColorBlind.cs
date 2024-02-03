using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ColorBlind : MonoBehaviour
{
    public static int CB;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    public void Normal()
    {
        CB = 0;
    }
    public void Protanopia()
    {
        CB = 1;
    }

    public void Protanomally()
    {
        CB = 2;
    }

    public void Deutranopia()
    {
        CB = 3;
    }

    public void Deuteranomaly()
    {
        CB = 4;
    }

    public void Tritanopia()
    {
        CB = 5;
    }

    public void Tritanomaly()
    {
        CB = 6;
    }

    public void Achromatopsia()
    {
        CB = 7;
    }
}
