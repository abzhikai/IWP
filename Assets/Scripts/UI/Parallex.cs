using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallex : MonoBehaviour
{
    float length, startPos;
    [SerializeField] GameObject targetCamera;
    [SerializeField] GameObject player;
    public float parallexEffect;
    
    void Start()
    {
        startPos = transform.position.x;
        length = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    void FixedUpdate()
    {
        float temp = (targetCamera.transform.position.x * (1 - parallexEffect));
        float dist = (targetCamera.transform.position.x * parallexEffect);

        transform.position = new Vector3(startPos + dist, transform.position.y, transform.position.z);

        if (temp > startPos + length)
        {
            startPos += length;
        }
        else if (temp < startPos - length)
        {
            startPos -= length;
        }
    }
}
