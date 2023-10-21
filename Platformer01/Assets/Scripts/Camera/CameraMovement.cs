using System;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    private BoxCollider2D _topAndBottomEdges;
    public Transform target;
    public float schmoveSpeed = 12.5f;
    public Vector3 offset;

    private void Start()
    {
        _topAndBottomEdges = GetComponent<BoxCollider2D>();
    }

    private void FixedUpdate()
    {
        MoveCamera();
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (transform.position.y < target.position.y)
            {
                Vector3 desiredPosition = target.position + offset;
                if (desiredPosition.y > transform.position.y)
                {
                   Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, schmoveSpeed * Time.fixedDeltaTime);
                    transform.position = smoothedPosition;
                }
            } 
        }
    }

    private void MoveCamera()
    {
        Vector3 position = target.position;
        Vector3 desiredPosition = new Vector3(position.x, 0, position.z) + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, schmoveSpeed * Time.fixedDeltaTime);
        transform.position = smoothedPosition;
    }
}
