using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FireBossManager : MonoBehaviour
{
    public float currentHealth;
    [SerializeField]
    private int maxHealth = 100;

    public bool IsVulnerable;
    public bool playerCanWater;

    Slider healthBar;
    TMP_Text healthText;
    GameObject vulnerableIndicator;

    // Start is called before the first frame update
    void Start()
    {
        healthBar = gameObject.GetComponentInChildren<Slider>();
        healthText = healthBar.GetComponentInChildren<TMP_Text>();
        vulnerableIndicator = GameObject.Find("VulnerableIndicator");

        currentHealth = maxHealth;
        SetHealthUI();
    }

    // Update is called once per frame
    void Update()
    { 
        SetHealthUI();
        HandleVulnerableIndicator();
    }

    void SetHealthUI()
    {
        healthBar.value = currentHealth;
        healthText.text = currentHealth + "/" + maxHealth;
    }

    void HandleVulnerableIndicator()
    {
        if (IsVulnerable)
        {
            vulnerableIndicator.SetActive(false);
        }
        else
        {
            vulnerableIndicator.SetActive(true);
        }
    }
}
