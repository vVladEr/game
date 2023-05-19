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

    public void InitialiseCharacter() 
    {
        coll = gameObject.GetComponent<Collider2D>();
    }

    public bool IsDirectionFree(Vector3 dir) 
    {
        return !Physics2D.BoxCast(coll.bounds.center + stepLength * dir, coll.bounds.size, 0f,
            Vector2.right, 0, CollidebaleAndFriends);
    }
}
