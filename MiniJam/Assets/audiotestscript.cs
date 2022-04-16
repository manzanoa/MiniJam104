using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class audiotestscript : MonoBehaviour
{
    public float t = 0;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<AudioSource>().time = 200;
    }

    // Update is called once per frame
    void Update()
    {
        t = GetComponent<AudioSource>().time;
    }
}
