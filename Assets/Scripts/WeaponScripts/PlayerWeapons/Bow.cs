using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bow : BasicWeapon
{
    public override void InisialiseBaseInfo()
    {
        Name = "Bow";
        Damage = 4;
    }
    public override void InisialisePlayer()
    {
        InisialiseBaseInfo();
        coll = gameObject.GetComponent<Collider2D>();
        stepLength = gameObject.GetComponent<Character>().stepLength;
        Enemy = gameObject.GetComponent<Character>().Enemy;
        Barriers = gameObject.GetComponent<Character>().NotEnemies;
    }

    public override List<Collider2D> GetEnemiesInDirection(Vector3 dir)
    {
        var enemies = new List<Collider2D>();
        for (var i = 2; i < 4; i++)
        {
            var barrier = Physics2D.BoxCast(coll.bounds.center + i * stepLength * dir, coll.bounds.size, 0f,
                Vector2.right, 0, Barriers);
            if (barrier)
            {
                Door door;
                var cond = barrier.collider.TryGetComponent(out door);
                if(!cond || !door.IsAllowedToWalkIn)
                    break;
            }
            var enemy = Physics2D.BoxCast(coll.bounds.center + i * stepLength * dir, coll.bounds.size, 0f,
                Vector2.right, 0, Enemy).collider;
            if (enemy)
            {
                enemies.Add(enemy);
                if (i != 3 && enemy.GetComponent<EnemyHp>().Hp > Damage)
                    break;
            }
        }
        return enemies;
    }

    public override bool IsEnemyInDirection(Vector3 dir)
    {
        for (var i = 2; i < 4; i++)
        {
            var barrier = Physics2D.BoxCast(coll.bounds.center + i * stepLength * dir, coll.bounds.size, 0f,
                Vector2.right, 0, Barriers);
            if (barrier)
                return false;
            var enemy = Physics2D.BoxCast(coll.bounds.center + i * stepLength * dir, coll.bounds.size, 0f,
                Vector2.right, 0, Enemy).collider;
            if (enemy)
            {
                return true;
            }
        }
        return false;
    }
}
