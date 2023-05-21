using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public Vector2 Position => transform.position;

    public Vector2 velocity = Vector2.zero;
    public float dampingTime = 0.15f;
    public Vector2 newPosition;
    public bool isMoving = false;
    public Animator animator;

    [SerializeField] public LayerMask CollidebaleAndFriends;
    [SerializeField] public LayerMask Enemy;
    public Collider2D coll;
    public float stepLength = 0.16f;

    public void MoveSmoothly()
    {
        animator.SetFloat("Speed", Math.Abs(velocity.magnitude));
        if (isMoving) 
            transform.position = Vector2.SmoothDamp(transform.position, newPosition, ref velocity, dampingTime);
        if (Math.Abs((Position - newPosition).magnitude) < 0.0001f) isMoving = false;
    }
    
    public void InitialiseCharacter() 
    {
        coll = gameObject.GetComponent<Collider2D>();
        FixPosition();
    }

    private void FixPosition() 
    {
        var fixedPos = new Vector2(FixCoord(transform.position.x),
            FixCoord(transform.position.y));
        transform.position = fixedPos;
    }

    private float FixCoord(float coord) 
    {
        var delta = stepLength / 2;
        float left;
        if (coord >= 0)
            left = coord % stepLength;
        else
         left = stepLength + (coord % stepLength);

        return coord - left + delta;
    }

    public bool IsDirectionFree(Vector3 dir) 
    {
        return !Physics2D.BoxCast(coll.bounds.center + stepLength * dir, coll.bounds.size, 0f,
            Vector2.right, 0, CollidebaleAndFriends);
    }
}
