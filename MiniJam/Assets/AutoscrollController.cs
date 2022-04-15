using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoscrollController : MonoBehaviour
{
    // TODO make this const for safety once we decide on a value
    public float CAMERA_SPEED = 1f; // Camera speed in UU per second

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // Move the camera up slowly
        transform.position = new Vector3(transform.position.x, transform.position.y + (CAMERA_SPEED * Time.deltaTime), transform.position.z);
    }
}
