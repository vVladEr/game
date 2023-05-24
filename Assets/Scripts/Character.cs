using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public Vector2 Position => transform.position;
    public float DeltaTime => 60 / General.BPM;
    public float Eps => General.RightTimeWindow;

    public Vector2 velocity = Vector2.zero;
    public float dampingTime => General.DampingTime;
    public Vector2 newPosition;
    public bool isMoving = false;
    public Animator animator;
    public int currentTick = -1;

    [SerializeField] public LayerMask AnyCollidable;
    [SerializeField] public LayerMask Enemy;
    [SerializeField] public LayerMask NotEnemies;
    [SerializeField] public LayerMask Interactive;
    public Collider2D coll;
    public float stepLength = 0.16f;
    public General General;

    public void MoveSmoothly()
    {
        animator.SetFloat("Speed", Math.Abs(velocity.magnitude));
        if (isMoving) 
            transform.position = Vector2.SmoothDamp(transform.position, newPosition, ref velocity, dampingTime);
        if (Math.Abs((Position - newPosition).magnitude) < 0.00001f) 
        {
            isMoving = false;
            FixPosition();
        } 
    }
    
    public void InitialiseCharacter() 
    {
        coll = gameObject.GetComponent<Collider2D>();
        FixPosition();
    }

    public void FixPosition() 
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
        return !Physics2D.BoxCast(coll.bounds.center + stepLength * dir.normalized, coll.bounds.size, 0f,
            Vector2.right, 0, AnyCollidable);
    }

    public bool IsAfterTheTick() 
    {
        var curTime = General.Time;
        var tick = (int)(curTime / DeltaTime);
        if (tick != currentTick && (curTime - tick * DeltaTime) > Eps + dampingTime)
        {
            currentTick = tick;
            return true;
        }
        return false;
    }


}