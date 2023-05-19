using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BasicWeapon : MonoBehaviour
{
    public LayerMask Enemy;
    public string Name;
    public int Damage;
    public bool AttackSucc = false;
    public Collider2D coll { get; set;}
    public float stepLength { get; set;}

    private void Start()
    {
        InisialiseBaseInfo();
    }
    public abstract void InisialiseBaseInfo();
    public abstract void InisialisePlayer();

    public void Attack(Vector3 attackDirection)
    {
        AttackSucc = false;
        var enemies = IsEnemyInDirection(attackDirection.normalized);
        foreach (var enemy in enemies)
        {
            Hit(enemy);
            AttackSucc = true;
        }
    }


    public abstract List<Collider2D> IsEnemyInDirection(Vector3 dir);

    public void Hit(Collider2D hit)
    {
        if (hit.GetComponent<EnemyHp>())
        {
            hit.GetComponent<EnemyHp>().TakeHit(Damage);
        }
        if (hit.GetComponent<PlayerHp>())
            hit.GetComponent<PlayerHp>().TakeHit(Damage);
    }

}
