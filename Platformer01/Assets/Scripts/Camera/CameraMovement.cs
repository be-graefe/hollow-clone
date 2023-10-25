using System;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public Transform target;
    public float schmoveSpeed = 12.5f;
    public Vector3 offset;

    private void Start()
    {
        
    }

    private void FixedUpdate()
    {
        MoveCamera();
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;
        if (!(transform.position.y < target.position.y)) return;
        Vector3 desiredPosition = target.position + offset;
        if (!(desiredPosition.y > transform.position.y)) return;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, schmoveSpeed * Time.fixedDeltaTime);
        transform.position = smoothedPosition;
    }

    private void MoveCamera()
    {
        Vector3 position = target.position;
        Vector3 desiredPosition = new Vector3(position.x, 0, position.z) + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, schmoveSpeed * Time.fixedDeltaTime);
        transform.position = smoothedPosition;
    }
}
