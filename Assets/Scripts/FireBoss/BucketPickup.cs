using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BucketPickup : MonoBehaviour
{
    FireBossManager fireBossManager;

    public bool isInteractable;
    public bool pickedUp;
    public bool isNearWater;
    public bool minionNear;
    public Canvas overheadLabel;
    public Slider fillBar;
    public TMP_Text fillText;

    public PlayerManager pmScript;

    public float fillLevel;
    GameObject water;
    float emptyTimer;

    // Start is called before the first frame update
    void Start()
    {
        overheadLabel = GetComponentInChildren<Canvas>();
        overheadLabel.gameObject.SetActive(false);

        fillBar = gameObject.GetComponentInChildren<Slider>();
        fillText = GameObject.Find("FillText").GetComponent<TMP_Text>();

        pmScript = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerManager>();
        fireBossManager = GameObject.Find("FireBoss").GetComponent<FireBossManager>();
        water = GameObject.Find("WaterLevel");
        EmptyBucket();
    }

    // Update is called once per frame
    void Update()
    {
        HandleOverheadText();
        HandlePlayerManager();
        HandleFillBar();

        if (isInteractable)
        {
            HandlePlayerInput();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            isInteractable = true;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            isInteractable = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            isInteractable = false;
        }
    }

    void HandleOverheadText()
    {
        if (pickedUp)
        {
            if (isNearWater && fillLevel < 2f)
            {
                overheadLabel.gameObject.SetActive(true);
            }
            else
            {
                overheadLabel.gameObject.SetActive(false);
            }

            return;
        }

        if (isInteractable)
        {
            overheadLabel.gameObject.SetActive(true);
        }
        else
        {
            overheadLabel.gameObject.SetActive(false);
        }
    }

    void HandleFillBar()
    {
        ChangeWaterLevel(fillLevel);

        if (!isInteractable)
        {
            fillBar.gameObject.SetActive(false);
            return;
        }
        fillBar.gameObject.SetActive(true);
        fillBar.value = fillLevel / 2f;
        fillText.text = (int)(100 * fillLevel / 2f) + "%";
    }

    void HandlePlayerInput()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (!pmScript.isHolding)
            {
                PickupBucket();
            }
        }

        if (Input.GetKey(KeyCode.E))
        {
            if (pmScript.isHolding && isNearWater && fillLevel < 2f)
            {
                if(fillLevel > 2f)
                {
                    pmScript.isCollecting = false;
                    fillLevel = 2f;
                    return;
                }
                pmScript.isCollecting = true;
                fillLevel += Time.deltaTime;
            }

            if(pmScript.isHolding && fireBossManager.playerCanWater && fireBossManager.IsVulnerable && !isNearWater && fillLevel > 0f)
            {
                emptyTimer += Time.deltaTime;
                pmScript.isCollecting = true;
                if(emptyTimer > 1f)
                {
                    AttackBoss(fillLevel);
                    EmptyBucket();
                    emptyTimer = 0f;
                    CameraShakeManager.Instance.ShakeCamera(10f, 1.5f);
                }
            }

            if (pmScript.isHolding && minionNear && !isNearWater && fillLevel > 0f)
            {
                emptyTimer += Time.deltaTime;
                pmScript.isCollecting = true;
                if (emptyTimer > 1f)
                {
                    Destroy(GameObject.Find("BigMinion"));
                    minionNear = false;
                    EmptyBucket();
                    emptyTimer = 0f;
                    CameraShakeManager.Instance.ShakeCamera(5f, 0.5f);
                }
            }
        }

        if (Input.GetKeyUp(KeyCode.E))
        {
            emptyTimer = 0f;
            pmScript.isCollecting = false;
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            PutDownBucket();
        }
    }

    void ChangeWaterLevel(float level)
    {
        if (fillLevel < 0f)
        {
            fillLevel = 0f;
        }

        float scale = level * 0.15f / 2f;
        water.transform.localPosition = new Vector3(0, scale, 0);
        water.transform.localScale = new Vector3(0.3f, scale, 0.3f);

    }

    public void EmptyBucket()
    {
        fillLevel = 0f;
        ChangeWaterLevel(0);
        water.transform.position.Set(water.transform.position.x, 0.01f, water.transform.position.z);
    }

    void AttackBoss(float waterLevel)
    {
        float damage = waterLevel * 10f / 2f;
        if(damage > 10f)
        {
            damage = 10f;
        }
        fireBossManager.currentHealth -= damage;
    }

    void PickupBucket()
    {
        if (pmScript.isKnocked)
        {
            return;
        }

        gameObject.transform.SetParent(pmScript.gameObject.transform);

        gameObject.transform.localPosition = new Vector3(0, 0, 0.6f);
        gameObject.transform.localEulerAngles = Vector3.zero;

        gameObject.GetComponent<Collider>().enabled = false;
        transform.GetChild(0).GetComponent<Collider>().enabled = false;
        gameObject.transform.GetChild(2).GetComponent<Collider>().enabled = false;

        pickedUp = true;
    }

    public void PutDownBucket()
    {
        if (pmScript.isHolding)
        {
            gameObject.transform.SetParent(pmScript.gameObject.transform.parent);

            gameObject.GetComponent<Collider>().enabled = true;
            gameObject.transform.GetChild(0).GetComponent<Collider>().enabled = true;
            transform.GetChild(2).GetComponent<Collider>().enabled = true;
            pickedUp = false;
        }
    }

    void HandlePlayerManager()
    {
        if (pickedUp)
        {
            pmScript.isHolding = true;
        }
        else
        {
            pmScript.isHolding = false;
        }
    }
}
