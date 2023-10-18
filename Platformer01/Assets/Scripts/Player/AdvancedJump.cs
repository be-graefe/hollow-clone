using UnityEngine;

public class AdvancedJump : MonoBehaviour
{
    public float fallMultiplier = 2.5f;
    public float lowJumpMultiplier = 2f;

    Rigidbody2D _rb2;

    private void Awake()
    {
        _rb2 = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (_rb2.velocity.y < 0)
        {
            _rb2.velocity += Vector2.up * (Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime);
        } else if (_rb2.velocity.y > 0 && !Input.GetButton("Jump")) {
            _rb2.velocity += Vector2.up * (Physics2D.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime);
        }
    }
}