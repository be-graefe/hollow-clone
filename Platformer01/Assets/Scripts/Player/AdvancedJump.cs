using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdvancedJump : MonoBehaviour
{
    public float fallMultiplier = 2.5f;
    public float lowJumpMultiplier = 2f;

    Rigidbody2D rb2;

    private void Awake()
    {
        rb2 = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (rb2.velocity.y < 0)
        {
            rb2.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        } else if (rb2.velocity.y > 0 && !Input.GetButton("Jump")) {
            rb2.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
        }
    }
}
