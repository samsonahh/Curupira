using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SpawnManager : MonoBehaviour
{
    public GameObject player;
    public string prevScene;
    public GameObject TPSpot;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        //Set default scene to Spawn and tp there if you load a different scene.
        if (MainManager.Instance == null)
        {
            SceneManager.LoadScene("Menu");
        }

        //Disable the character controller to prevent player position being reset
        player.GetComponent<CharacterController>().enabled = false;
    }
    // Start is called before the first frame update
    void Start()
    {
        prevScene = MainManager.Instance.previousScene;
        TPSpot = GameObject.Find(prevScene + " TP");

        //Tp to the correct portal spot and renable character controller
        player.transform.position = TPSpot.transform.position;
        player.GetComponent<CharacterController>().enabled = true;
    }
}
