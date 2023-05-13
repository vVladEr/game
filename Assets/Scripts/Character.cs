using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField] public LayerMask walls;
    public Collider2D coll;
    public float stepLength = 0.16f;
    public IReadOnlyDictionary<Vector2, Func<bool>> movementDictionary;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void InitialiseCharacter() 
    {
        coll = gameObject.GetComponent<Collider2D>();
        movementDictionary = new Dictionary<Vector2, Func<bool>>(){
                {new Vector2(stepLength, 0), () => IsRightFree()},
                {new Vector2(-stepLength, 0),() => IsLeftFree()},
                {new Vector2(0, stepLength), () => IsTopFree()},
                {new Vector2(0, -stepLength),() => IsBottomFree()}
            };
    }

    public bool IsRightFree()
    {
        return !Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f,
            Vector2.right, stepLength, walls);
    }

    public bool IsLeftFree()
    {
        return !Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f,
            Vector2.left, stepLength, walls);
    }

    public bool IsTopFree()
    {
        return !Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f,
            Vector2.up, stepLength, walls);
    }

    public bool IsBottomFree()
    {
        return !Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f,
            Vector2.down, stepLength, walls);
    }
}
