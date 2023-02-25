using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeaverManager : MonoBehaviour
{
    public ChargeState chargeState;
    public ChaseState chaseState;
    public WallManager wall;
    public Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        chargeState = GetComponentInChildren<ChargeState>();
        wall = GameObject.Find("Wall").GetComponent<WallManager>();
        animator = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(transform.position.x, 0, transform.position.z);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(GetComponent<StateManager>().currentState.gameObject.name == chargeState.name)
        {
            if (other.gameObject == wall.gameObject)
            { 
                wall.hp-=chargeState.chargeVelocity / 15f;
                chargeState.doneWithCharge = true;
                chargeState.direction = Vector3.zero;
                chargeState.playerLagPosition = Vector3.zero;
                //GameObject.Find("Eyes").transform.localRotation = Quaternion.identity;
                GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
                animator.Play("Get Hit");
                chargeState.chargeVelocity = 0f;
                CameraShakeManager.Instance.ShakeCamera(10f, 0.7f);
            }
            if(other.tag == "CanHit")
            {
                chargeState.doneWithCharge = true;
                chargeState.direction = Vector3.zero;
                chargeState.playerLagPosition = Vector3.zero;
                //GameObject.Find("Eyes").transform.localRotation = Quaternion.identity;
                GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
                animator.Play("Get Hit");
                chargeState.chargeVelocity = 0f;
                CameraShakeManager.Instance.ShakeCamera(5f, 0.5f);
            }
        }
    }
}
