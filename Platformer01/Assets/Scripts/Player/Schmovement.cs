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
    [SerializeField] private LayerMask enemyLayer;
    [SerializeField] private Transform attackPointRight;
    [SerializeField] private Transform attackPointLeft;
    
    [Range(1f, 10f)]
    public float jumpHeight;
    [Range(1f, 10f)]
    public float moveSpeed;
    public float fastFallSpeed = -20f;
    public float attackRadius = 1f;
    
    private static readonly int Running = Animator.StringToHash("running");
    private static readonly int Jumping = Animator.StringToHash("jumping");
    private static readonly int Falling = Animator.StringToHash("falling");
    private static readonly int FastFall = Animator.StringToHash("fastFall");
    private static readonly int Attacking = Animator.StringToHash("attacking");
    private float _dirX;
    private readonly Collider2D[] _results = new Collider2D[10];

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
        if (IsGrounded())
        {
            if (_rb.velocity.y < 0f)
            {
                _rb.velocity = new Vector2(_rb.velocity.x, 0f);
            }
        }
        if (Input.GetKeyDown(KeyCode.T) && !_anim.GetBool(Attacking))
        {
            _anim.SetBool(Attacking, true);
        }
        _dirX = Input.GetAxisRaw("Horizontal");
        Move();
        Jump();
        AnimationUpdate();
    }

    private void Move()
    {
        _rb.velocity = new Vector2(_dirX * moveSpeed, _rb.velocity.y);
    }

    private void AnimationUpdate()
    {
        if (_dirX > 0f)
        {
            if (IsGrounded())
            {
                _anim.SetBool(Running, true);
            }
            _sr.flipX = false;
        }
        else if (_dirX < 0f)
        {
            if (IsGrounded())
            {
                _anim.SetBool(Running, true);
            }
            _sr.flipX = true;
        }
        else
        {
            _anim.SetBool(Running, false);
        }
        _anim.SetBool(Jumping, (_rb.velocity.y > 0f) && (!IsGrounded()));
        _anim.SetBool(Falling, (_rb.velocity.y < 0f) && (!IsGrounded()));
        _anim.SetBool(FastFall, (_rb.velocity.y < fastFallSpeed) && (!IsGrounded()));
    }

    private void Jump()
    {
        if (!Input.GetButtonDown("Jump") || !IsGrounded()) return;
        _rb.velocity = new Vector2(_rb.velocity.x, jumpHeight);
    }

    private bool IsGrounded()
    {
        var bounds = _bc.bounds;
        return Physics2D.BoxCast(bounds.center, bounds.size, 0f, Vector2.down, .1f, jumpableGround);
    }

    public void AttackSide()
    {
        var temp = Physics2D.OverlapCircleNonAlloc(_sr.flipX ? attackPointLeft.position : attackPointRight.position, attackRadius, _results, enemyLayer);
        for (int i = 0; i < temp; i++)
        {
            Debug.Log("Hit " + _results[i].name);
        }
    }
    
    public void EndAttack()
    {
        _anim.SetBool(Attacking, false);
    }
    
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPointRight.position, attackRadius);
        Gizmos.DrawWireSphere(attackPointLeft.position, attackRadius);
    }
}
