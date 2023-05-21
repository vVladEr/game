using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    private Vector3 velocity = Vector3.zero;
    
    [Range(0, 1)] public float DampingTime = 0.15f;
    [SerializeField] private Transform target;

    void Update()
    {
        Vector3 targetPosition = new(target.position.x, target.position.y, -10f);
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, DampingTime);
    }
}
