using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spear : BasicWeapon
{
    public override void InisialiseBaseInfo()
    {
        Name = "Spear";
        Damage = 2;
    }
    public override void InisialisePlayer()
    {
        InisialiseBaseInfo();
        coll = gameObject.GetComponent<Collider2D>();
        stepLength = gameObject.GetComponent<Character>().stepLength;
        Enemy = gameObject.GetComponent<Character>().Enemy;
    }

    public override List<Collider2D> IsEnemyInDirection(Vector3 dir)
    {
        var enemies = new List<Collider2D>();
        for (var i = 1; i < 3; i++)
        {
            var enemy = Physics2D.BoxCast(coll.bounds.center + i * stepLength * dir, coll.bounds.size, 0f,
                Vector2.right, 0, Enemy).collider;
            if (enemy)
            {
                enemies.Add(enemy);
                if (i != 2 && enemy.GetComponent<EnemyHp>().Hp > Damage)
                    break;
            }
        }
        return enemies;
    }
}
