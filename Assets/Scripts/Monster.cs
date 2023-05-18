using Game;
using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Monster : Character
{
    [SerializeField] public Player player;
    private float activationDistance = 0.64f; // ћожно вынести в константу
    public const float DeltaTime = 1f;
    private const float Eps = 0.001f;
    private float mathEps = 0.0001f;
    public const int TurnsDelay = 1;
    private int turnsTimer = TurnsDelay;
    public Dagger Weapon;

    void Start()
    {
        InitialiseCharacter();
        Weapon = GetComponent<Dagger>();
        Weapon.Inisialise();
    }

    void Update()
    {
        if (CanUpdate())
            MonsterUpdate();
    }

    private void MonsterUpdate()
    {
        if (turnsTimer == 0)
        {
            turnsTimer = TurnsDelay;
            var difference = player.Position - (Vector2)transform.position;
            var directionVector = GetDirectionByDifference(difference);
            if (directionVector.x == stepLength)
                GetComponent<SpriteRenderer>().flipX = false;
            else if (directionVector.x == -stepLength)
                GetComponent<SpriteRenderer>().flipX = true;

            Weapon.Attack(directionVector.normalized);
            if (!Weapon.AttackSucc)
            {
                transform.position = directionVector + (Vector2)transform.position;
            }
        }
        else 
            turnsTimer--;

    }

    private Vector2 GetDirectionByDifference(Vector2 difference)
    {
        var activationDifference = 0f; // ќн может шагнуть на Playe => обработать коллизию или сравнивать с 1.1
                                          // «атем вынести в констнату
        if (difference.x - activationDifference > mathEps) return new Vector2(stepLength, 0);
        if (difference.x - activationDifference < -mathEps) return new Vector2(-stepLength, 0);
        if (difference.y - activationDifference > mathEps) return new Vector2(0, stepLength);
        if (difference.y - activationDifference < -mathEps) return new Vector2(0, -stepLength);
        return new Vector2(0, 0);
    }


    private bool CanUpdate() 
    {
        if (GetDistanceToPlayer() >= activationDistance)
            return false;
        var curTime = Time.time;
        if (curTime % DeltaTime < Eps ||
            curTime % DeltaTime > DeltaTime - Eps)
            return true;
        return false;
    }

    private float GetDistanceToPlayer()
        => Mathf.Sqrt((player.Position.x - transform.position.x) * (player.Position.x - transform.position.x) +
            (player.Position.y - transform.position.y) * (player.Position.y - transform.position.y));

}
