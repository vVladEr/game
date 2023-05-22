using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dagger : BasicWeapon
{
    public override void InisialiseBaseInfo()
    {
        Name = "Dagger";
        Damage = 1;
    }
    public override void InisialisePlayer()
    {
        InisialiseBaseInfo();
        coll = gameObject.GetComponent<Collider2D>();
        stepLength = gameObject.GetComponent<Character>().stepLength;
        Enemy = gameObject.GetComponent<Character>().Enemy;
        Barriers = gameObject.GetComponent<Character>().AnyCollidable;
    }

    public override List<Collider2D> GetEnemiesInDirection(Vector3 dir) 
    {
        var res = new List<Collider2D>();
        var enemy = Physics2D.BoxCast(coll.bounds.center + stepLength * dir, coll.bounds.size, 0f,
            Vector2.right, 0, Enemy).collider;
        if (enemy)
            res.Add(enemy);
        return res;
    }

    public override bool IsEnemyInDirection(Vector3 dir)
    {
        var enemy = Physics2D.BoxCast(coll.bounds.center + stepLength * dir, coll.bounds.size, 0f,
            Vector2.right, 0, Enemy);
        if (enemy)
            return true;
        return false;
    }
}
