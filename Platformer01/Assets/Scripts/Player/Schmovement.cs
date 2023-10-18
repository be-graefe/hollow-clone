using UnityEngine;
using UnityEngine.Tilemaps;

public class Schmovement : MonoBehaviour
{
    Rigidbody2D _rb;
    BoxCollider2D _bc;
    [Range(1f, 10f)]
    public float jumpHeight;
    [Range(1f, 10f)]
    public float moveSpeed;

    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _bc = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (IsTouchingGroundCheck())
        {
            Jump();
        }
        Move();
    }

    private void Jump()
    {
        if (Input.GetButtonDown("Jump"))
        {
            _rb.velocity *= new Vector2(1, jumpHeight);
        }
    }

    private bool IsTouchingGroundCheck()
    {
        return (_bc.IsTouching(Grid.FindAnyObjectByType<Tilemap>().GetComponent<TilemapCollider2D>()));
    }

    private void Move()
    { 
        if (Input.GetKey(KeyCode.RightArrow)) 
        {
            Vector2 vMove = new Vector2(moveSpeed, 1);
            if (GetComponent<SpriteRenderer>().flipX)
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
