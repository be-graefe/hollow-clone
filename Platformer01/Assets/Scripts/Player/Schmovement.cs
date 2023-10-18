using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Schmovement : MonoBehaviour
{
    Rigidbody2D rb;
    BoxCollider2D bc;
    [Range(1f, 10f)]
    public float jumpHeight;
    [Range(1f, 10f)]
    public float moveSpeed;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        bc = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isTouchingGroundCheck())
        {
            jump();
        }
        move();
    }

    private void FixedUpdate()
    {
        
    }

    private void jump()
    {
        if (Input.GetButtonDown("Jump"))
        {
            rb.velocity *= new Vector2(1, jumpHeight);
        }
    }

    private Boolean isTouchingGroundCheck()
    {
        if (bc.IsTouching(Grid.FindAnyObjectByType<Tilemap>().GetComponent<TilemapCollider2D>()))
        {
            return true;
        }
        return false;
    }

    private void move()
    { 
        if (Input.GetKey(KeyCode.RightArrow)) 
        {
            Vector2 vMove = new Vector2(moveSpeed, 1);
            if (GetComponent<SpriteRenderer>().flipX == true)
            {
                GetComponent<SpriteRenderer>().flipX = false;
            }
            transform.Translate(vMove * Time.deltaTime);
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            Vector2 vMove = new Vector2((1-moveSpeed), 1);
            if (GetComponent<SpriteRenderer>().flipX == false)
            {
                GetComponent<SpriteRenderer>().flipX = true;
            }
            transform.Translate(vMove * Time.deltaTime);
        }
    }
}
