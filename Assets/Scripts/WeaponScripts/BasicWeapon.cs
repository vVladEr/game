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

    public IReadOnlyDictionary<Vector2, Func<Collider2D>> AttackDictionary;

    public abstract void Inisialise();

    public abstract void Attack(Vector2 attackDirection);

    public abstract Collider2D IsEnemyOnRight();
    public abstract Collider2D IsEnemyOnLeft();
    public abstract Collider2D IsEnemyOnTop();
    public abstract Collider2D IsEnemyOnBottom();

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
