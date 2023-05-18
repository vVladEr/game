using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Axe : BasicWeapon
{
    public override void Inisialise()
    {
        Name = "Axe";
        Damage = 3;
        coll = gameObject.GetComponent<Collider2D>();
        stepLength = gameObject.GetComponent<Character>().stepLength;
        Enemy = gameObject.GetComponent<Character>().Enemy;
    }

    public override List<Collider2D> IsEnemyInDirection(Vector3 dir)
    {
        var enemies = new List<Collider2D>();
        var horizontalHit = true;
        if (dir.x == 0)
            horizontalHit = false;
        for (var i = -1; i <= 1; i++)
        {
            Collider2D enemy;
            if (horizontalHit)
                enemy = Physics2D.BoxCast(coll.bounds.center + stepLength * (dir + i * Vector3.up), coll.bounds.size, 0f,
                    Vector2.right, 0, Enemy).collider;
            else
                enemy = Physics2D.BoxCast(coll.bounds.center + stepLength * (dir + i * Vector3.left), coll.bounds.size, 0f,
                    Vector2.right, 0, Enemy).collider;

            if (enemy)
            {
                enemies.Add(enemy);
            }
        }
        return enemies;
    }
}
