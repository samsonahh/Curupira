using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockerTrigger : MonoBehaviour
{
    public PathBlocker pathBlocker;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            if(transform.parent.name == "BeaverBlock")
            {
                pathBlocker.beaverBlockOnTop = true;
            }
            if (transform.parent.name == "FireBlock")
            {
                pathBlocker.fireBlockOnTop = true;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            if (transform.parent.name == "BeaverBlock")
            {
                pathBlocker.beaverBlockOnTop = false;
            }
            if (transform.parent.name == "FireBlock")
            {
                pathBlocker.fireBlockOnTop = false;
            }
        }
    }
}
