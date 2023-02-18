using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FireBossManager : MonoBehaviour
{
    public float currentHealth;
    [SerializeField]
    private float maxHealth = 100f;

    public bool IsVulnerable;
    public bool playerCanWater;

    Slider healthBar;
    TMP_Text healthText;

    // Start is called before the first frame update
    void Start()
    {
        healthBar = gameObject.GetComponentInChildren<Slider>();
        healthText = healthBar.GetComponentInChildren<TMP_Text>();

        currentHealth = maxHealth;
        SetHealthUI();
    }

    // Update is called once per frame
    void Update()
    { 
        SetHealthUI();
    }

    void SetHealthUI()
    {
        healthBar.value = currentHealth;
        healthText.text = (int)currentHealth + "/" + maxHealth;
    }
}
