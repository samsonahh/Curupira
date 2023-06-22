using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class Snake : MonoBehaviour
{
    public GameObject head; // head prefab
    public GameObject segment; // segment prefab
    public GameObject tail; // tail prefab
    public GameObject target;

    public int numberOfSegments;
    public float speed;

    private GameObject[] segments; // stores the segments

    private void Start() // updates everytime something is chnaged in the inspector
    {
        segments = new GameObject[numberOfSegments];

        for (int i = 0; i < numberOfSegments; i++)
        {
            if(i == 0)
            {
                segments[i] = Instantiate(head, transform);
                segments[i].name = "Segment " + i.ToString();
                segments[i].transform.position = transform.position;
                continue;
            }
            if(i == numberOfSegments - 1)
            {
                segments[i] = Instantiate(tail, transform);
                segments[i].name = "Segment " + i.ToString();
                segments[i].transform.position = transform.position;
                continue;
            }
            segments[i] = Instantiate(segment, transform);
            segments[i].name = "Segment " + i.ToString();
            segments[i].transform.position = transform.position;
        }
    }

    private void Update()
    {
        if (segment != null)
        {
            segments[0].transform.LookAt(target.transform);
            segments[0].transform.position = Vector3.MoveTowards(segments[0].transform.position, target.transform.position, speed * Time.deltaTime);

            for (int i = 1; i < numberOfSegments; i++)
            {
                GameObject current = segments[i];
                GameObject prev = segments[i - 1];

                Vector3 prevTailPos = prev.transform.position - 0.5f * prev.transform.forward;
                current.transform.LookAt(prevTailPos);
                while (Vector3.Distance(current.transform.position, prevTailPos) >= 1)
                {
                    current.transform.position = Vector3.MoveTowards(current.transform.position, prevTailPos, 2 * Time.deltaTime);
                }
            }
        }
    }
}
