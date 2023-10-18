using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsTweaks : MonoBehaviour
{
    Rigidbody2D rb;
    public float maxFallSpeed = 30f;

    // Start is called before the first frame update
    void Start()
    {
       rb = GetComponent<Rigidbody2D>(); 
    }

    // Update is called once per frame
    void Update()
    {
        if (rb.velocity.y < (1-maxFallSpeed))
        {
            rb.velocity = rb.velocity.normalized * maxFallSpeed;
        }
    }
}
