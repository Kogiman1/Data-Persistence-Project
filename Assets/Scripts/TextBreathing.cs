using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextBreathing : MonoBehaviour
{
    private Vector3 scale;
    private float change = 0.2f;
    private Vector3 maxScale;
    private Vector3 minScale;

    private float sizeChanger = 0.02f;
    private int i;
    // Start is called before the first frame update
    void Start()
    {
        scale = gameObject.transform.localScale;
        maxScale = scale * (1 + change);
        minScale = scale * (1 - change);

        i = 0;


    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (i % 2 == 0)
        {

            gameObject.transform.localScale *= 1.008f;
            if (gameObject.transform.localScale.x > maxScale.x)
            {
                i++;
            }
        }
        else
        {
            gameObject.transform.localScale *= 0.992f;

            if (gameObject.transform.localScale.x < minScale.x)
            {
                i++;
            }

        }



    }
}
