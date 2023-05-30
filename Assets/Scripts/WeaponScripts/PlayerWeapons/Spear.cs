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
        shouldMoveAfterHit = true;
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
        dir = dir.normalized;
        var enemies = new List<Collider2D>();
        for (var i = 1; i < 3; i++)
        {
            var barrier = Physics2D.BoxCast(coll.bounds.center + i * stepLength * dir, coll.bounds.size, 0f,
                Vector2.right, 0, Barriers);
            if (barrier)
            {
                Debug.Log("Point1");
                Door door;
                var cond = barrier.collider.TryGetComponent(out door);
                Debug.Log(cond);
                if (!cond || !door.IsAllowedToWalkIn)
                    break;
            }
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

    public override bool IsEnemyInDirection(Vector3 dir)
    {
        for (var i = 1; i < 3; i++)
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
