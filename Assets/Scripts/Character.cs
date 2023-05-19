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
        Debug.Log(delta);
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
