using UnityEngine;

public class PhysicsTweaks : MonoBehaviour
{
    Rigidbody2D _rb;
    public float maxFallSpeed = 15f;

    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_rb.velocity.y < (1-maxFallSpeed))
        {
            _rb.velocity = _rb.velocity.normalized * maxFallSpeed;
        }
    }
}
