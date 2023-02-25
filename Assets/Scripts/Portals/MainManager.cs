using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainManager : MonoBehaviour
{
    public static MainManager Instance;

    public string previousScene = "";

    public Quest currentQuest;
    public int mainQuestIndex = 0;

    public bool isQuestActive()
    {
        return currentQuest.isActive;
    }
    
    private void Awake()
    {
        if(Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
}
