using Game;
using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Monster : Character
{
    [SerializeField] public Player player;
    private float activationDistance = 0.64f; // ����� ������� � ���������
    public const float DeltaTime = 0.5f;
    private const float Eps = 0.001f;
    private float mathEps = 0.0001f;
    public const int TurnsDelay = 1;
    private int turnsTimer = TurnsDelay;
    private int currentTick = -1;
    public Dagger Weapon;

    void Start()
    {
        InitialiseCharacter();
        Weapon = GetComponent<Dagger>();
        Weapon.InisialisePlayer();
    }

    void Update()
    {
        MoveSmoothly();
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
            if (!Weapon.AttackSucc && IsDirectionFree(directionVector.normalized))
            {
                newPosition = directionVector + (Vector2)transform.position;
                isMoving = true;
            }
        }
        else 
            turnsTimer--;

    }

    private Vector2 GetDirectionByDifference(Vector2 difference)
    {
        var activationDifference = 0f; // �� ����� ������� �� Playe => ���������� �������� ��� ���������� � 1.1
                                          // ����� ������� � ���������
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
        var tick = (int)(curTime / DeltaTime);
        if (curTime % DeltaTime < Eps && tick != currentTick) 
        {
            currentTick = tick;
            return true;
        }
        return false;
    }

    private float GetDistanceToPlayer()
        => Mathf.Sqrt((player.Position.x - transform.position.x) * (player.Position.x - transform.position.x) +
            (player.Position.y - transform.position.y) * (player.Position.y - transform.position.y));

}
