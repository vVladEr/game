using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField] public LayerMask CollidebaleAndFriends;
    [SerializeField] public LayerMask Enemy;
    public Collider2D coll;
    public float stepLength = 0.16f;
    public IReadOnlyDictionary<Vector2, Func<bool>> movementDictionary;
    public IReadOnlyDictionary<Vector2, Func<Collider2D>> attackDictionary;
    public int damage;

    public void InitialiseCharacter() 
    {
        coll = gameObject.GetComponent<Collider2D>();
        movementDictionary = new Dictionary<Vector2, Func<bool>>(){
                {new Vector2(stepLength, 0), () => IsRightFree()},
                {new Vector2(-stepLength, 0),() => IsLeftFree()},
                {new Vector2(0, stepLength), () => IsTopFree()},
                {new Vector2(0, -stepLength),() => IsBottomFree()}
            };

        attackDictionary = new Dictionary<Vector2, Func<Collider2D>>() {
                {new Vector2(stepLength, 0), () => IsEnemyOnRight()},
                {new Vector2(-stepLength, 0),() => IsEnemyOnLeft()},
                {new Vector2(0, stepLength), () => IsEnemyOnTop()},
                {new Vector2(0, -stepLength),() => IsEnemyOnBottom()}
        };
    }

    public bool IsRightFree()
    {
        return !Physics2D.BoxCast(coll.bounds.center + stepLength * Vector3.right, coll.bounds.size, 0f,
            Vector2.right, 0, CollidebaleAndFriends);
    }

    public bool IsLeftFree()
    {
        return !Physics2D.BoxCast(coll.bounds.center + stepLength * Vector3.left, coll.bounds.size, 0f,
            Vector2.left, 0, CollidebaleAndFriends);
    }

    public bool IsTopFree()
    {
        return !Physics2D.BoxCast(coll.bounds.center + stepLength * Vector3.up, coll.bounds.size, 0f,
            Vector2.up, 0, CollidebaleAndFriends);
    }

    public bool IsBottomFree()
    {
        return !Physics2D.BoxCast(coll.bounds.center + stepLength * Vector3.down, coll.bounds.size, 0f,
            Vector2.down, 0, CollidebaleAndFriends);
    }

    public Collider2D IsEnemyOnRight()
    {
        return Physics2D.BoxCast(coll.bounds.center + stepLength * Vector3.right, coll.bounds.size, 0f,
            Vector2.right, 0, Enemy).collider;
    }

    public Collider2D IsEnemyOnLeft()
    {
        return Physics2D.BoxCast(coll.bounds.center + stepLength * Vector3.left, coll.bounds.size, 0f,
            Vector2.left, 0, Enemy).collider;
    }

    public Collider2D IsEnemyOnTop()
    {
        return Physics2D.BoxCast(coll.bounds.center + stepLength * Vector3.up, coll.bounds.size, 0f,
            Vector2.up, 0, Enemy).collider;
    }

    public Collider2D IsEnemyOnBottom()
    {
        return Physics2D.BoxCast(coll.bounds.center + stepLength * Vector3.down, coll.bounds.size, 0f,
            Vector2.down, 0, Enemy).collider;
    }

    public void Hit(Collider2D hit)
    {
        if (hit.GetComponent<EnemyHp>())
        {
            hit.GetComponent<EnemyHp>().TakeHit(damage);
        }
        if (hit.GetComponent<PlayerHp>())
            hit.GetComponent<PlayerHp>().TakeHit(damage);
    }
}
