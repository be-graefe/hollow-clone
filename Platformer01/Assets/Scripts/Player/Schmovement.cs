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
        Jump();
        Move();
    }

    private void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _rb.velocity *= new Vector2(1, jumpHeight);
        }
    }   

    private bool IsTouchingGroundCheck()
    {
        return (_bc.IsTouching(FindAnyObjectByType<Tilemap>().GetComponent<TilemapCollider2D>()));
    }

    private void Move()
    {
        float dirX = Input.GetAxisRaw("Horizontal");
        if (Input.GetKey(KeyCode.RightArrow)) 
        {
            if (GetComponent<SpriteRenderer>().flipX)
            {
                GetComponent<SpriteRenderer>().flipX = false;
            }
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            if (GetComponent<SpriteRenderer>().flipX == false)
            {
                GetComponent<SpriteRenderer>().flipX = true;
            }
        }
        _rb.velocity = new Vector2(dirX * moveSpeed, _rb.velocity.y);
    }
}
