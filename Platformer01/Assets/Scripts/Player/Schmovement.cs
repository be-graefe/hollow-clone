using UnityEngine;

public class Schmovement : MonoBehaviour
{
    private Rigidbody2D _rb;
    private BoxCollider2D _bc;
    private SpriteRenderer _sr;
    private Animator _anim;
    
    [SerializeField]
    [Tooltip("The LayerMask which is jumpable")]
    private LayerMask jumpableGround;
    
    [Range(1f, 10f)]
    public float jumpHeight;
    [Range(1f, 10f)]
    public float moveSpeed;

    private static readonly int Running = Animator.StringToHash("running");
    private float _dirX;

    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _bc = GetComponent<BoxCollider2D>();
        _sr = GetComponent<SpriteRenderer>();
        _anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        _dirX = Input.GetAxisRaw("Horizontal");
        Move();
        AnimationUpdate();
        Jump();
    }

    private void Move()
    {
        _rb.velocity = new Vector2(_dirX * moveSpeed, _rb.velocity.y);
    }

    private void AnimationUpdate()
    {
        if (_dirX > 0f)
        {
            _anim.SetBool(Running, true);
            _sr.flipX = false;
        }
        else if (_dirX < 0f)
        {
            _anim.SetBool(Running, true);
            _sr.flipX = true;
        }
        else
        {
            _anim.SetBool(Running, false);
        }
    }

    private void Jump()
    {
        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            _rb.velocity = new Vector2(_rb.velocity.x, jumpHeight);
        }
    }

    private bool IsGrounded()
    {
        var bounds = _bc.bounds;
        return Physics2D.BoxCast(bounds.center, bounds.size, 0f, Vector2.down, .1f, jumpableGround);
    }
}
