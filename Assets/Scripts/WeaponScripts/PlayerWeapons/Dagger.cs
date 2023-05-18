using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dagger : BasicWeapon
{
    public override void Inisialise()
    {
        Name = "Dagger";
        Damage = 1;
        AttackDictionary =  new Dictionary<Vector2, Func<Collider2D>>() {
                {Vector2.right, () => IsEnemyOnRight()},
                { Vector2.left,() => IsEnemyOnLeft()},
                { Vector2.up, () => IsEnemyOnTop()},
                { Vector2.down,() => IsEnemyOnBottom()}
        };
        coll = gameObject.GetComponent<Collider2D>();
        stepLength = gameObject.GetComponent<Character>().stepLength;
        Enemy = gameObject.GetComponent<Character>().Enemy;
    }

    public override void Attack(Vector2 attackDirection)
    {
        AttackSucc = false;
        var enemy = AttackDictionary[attackDirection.normalized]();
        if (enemy) 
        {
            Hit(enemy);
            AttackSucc = true;
        }
    }

    public override Collider2D IsEnemyOnRight()
    {
        return Physics2D.BoxCast(coll.bounds.center + stepLength * Vector3.right, coll.bounds.size, 0f,
            Vector2.right, 0, Enemy).collider;
    }

    public override Collider2D IsEnemyOnLeft()
    {
        return Physics2D.BoxCast(coll.bounds.center + stepLength * Vector3.left, coll.bounds.size, 0f,
            Vector2.left, 0, Enemy).collider;
    }

    public override Collider2D IsEnemyOnTop()
    {
        return Physics2D.BoxCast(coll.bounds.center + stepLength * Vector3.up, coll.bounds.size, 0f,
            Vector2.up, 0, Enemy).collider;
    }

    public override Collider2D IsEnemyOnBottom()
    {
        return Physics2D.BoxCast(coll.bounds.center + stepLength * Vector3.down, coll.bounds.size, 0f,
            Vector2.down, 0, Enemy).collider;
    }
}
