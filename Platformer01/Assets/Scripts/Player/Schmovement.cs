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

    private readonly Collider2D[] _results = new Collider2D[10];
    const string PlayerIdle = "Player_Idle";
    const string PlayerRun = "Player_Running";
    const string PlayerJump = "Player_Jump";
    const string PlayerFall = "Player_Falling";
    const string PlayerFastFall = "Player_FastFalling";
    const string PlayerSideAttack = "Player_SideAttack";
    private bool _isAttackPressed;
    private float _dirX;
    private string _currentState;
    private static readonly int Attacking = Animator.StringToHash("attacking");


    // Start is called before the first frame update
    void Start()
    {
        _currentState = PlayerIdle;
        _rb = GetComponent<Rigidbody2D>();
        _bc = GetComponent<BoxCollider2D>();
        _sr = GetComponent<SpriteRenderer>();
        _anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        _dirX = Input.GetAxisRaw("Horizontal");
        _isAttackPressed = Input.GetKeyDown(KeyCode.E);
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }
        if (_isAttackPressed)
        {
            SideAttack();
        }
        StateUpdate();
    }

    private void FixedUpdate()
    {
        if (IsGrounded())
        {
            if (_rb.velocity.y < 0f)
            {
                _rb.velocity = new Vector2(_rb.velocity.x, 0f);
            }
        }
        Move();
    }

    private void Move()
    {
        _rb.velocity = new Vector2(_dirX * moveSpeed, _rb.velocity.y);
    }

    private void StateUpdate()
    {
        if(_anim.GetBool(Attacking)) return;
        if (_dirX > 0f)
        {
            if (IsGrounded())
            {
                AnimationUpdate(PlayerRun);
            }
            _sr.flipX = false;
        }
        else if (_dirX < 0f)
        {
            if (IsGrounded())
            {
                AnimationUpdate(PlayerRun);
            }
            _sr.flipX = true;
        }
        else
        {
            if (IsGrounded())
            {
                AnimationUpdate(PlayerIdle);
            }
        }
        if ((_rb.velocity.y > 0f) && (!IsGrounded()))
        {
            AnimationUpdate(PlayerJump);
        }
        else if ((_rb.velocity.y < 0f) && (!IsGrounded()))
        {
            AnimationUpdate(PlayerFall);
        }
        else if ((_rb.velocity.y < fastFallSpeed) && (!IsGrounded()))
        {
            AnimationUpdate(PlayerFastFall);
        }
        if (!_isAttackPressed) return;
        AnimationUpdate(PlayerSideAttack);
        _anim.SetBool(Attacking, true);
    }
    
    private void AnimationUpdate(string newState)
    {
        if (_currentState == newState) return;
        _anim.Play(newState);
        _currentState = newState;
    }
    
    public void EndAttack()
    {
        _anim.SetBool(Attacking, false);
        StateUpdate();
    }

    private void Jump()
    {
        if (!IsGrounded()) return;
        _rb.velocity = new Vector2(_rb.velocity.x, jumpHeight);
    }

    private bool IsGrounded()
    {
        var bounds = _bc.bounds;
        return Physics2D.BoxCast(bounds.center, bounds.size, 0f, Vector2.down, .1f, jumpableGround);
    }

    private void SideAttack()
    {
        var temp = Physics2D.OverlapCircleNonAlloc(_sr.flipX ? attackPointLeft.position : attackPointRight.position, attackRadius, _results, enemyLayer);
        for (int i = 0; i < temp; i++)
        {
            Debug.Log("Hit " + _results[i].name);
        }
    }
    
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPointRight.position, attackRadius);
        Gizmos.DrawWireSphere(attackPointLeft.position, attackRadius);
    }
}
