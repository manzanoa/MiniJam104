using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waterfall : MonoBehaviour
{
    SpriteRenderer r;
    public Color init;
    public Color final;
    void Start()
    {
        r = gameObject.GetComponent<SpriteRenderer>();
        init = r.color;
    }

    // Update is called once per frame
    void Update()
    {
        float y = transform.position.y;

        r.color = Color.Lerp(init, final, (y - 30) / 100f);
    }
}
