using Game;
using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Monster : Character
{
    [SerializeField] public Player player;
    private bool isAllowedToUpdate = true;
    private float lastTime = 0f;
    private float activationDistance = 0.64f; // Можно вынести в константу
    private float pauseCoefficient = 1.5f; // Возможность игроку убежать от монстра
    private float mathEps = 0.0001f;
    public const int TurnsDelay = 1;
    private int turnsTimer = TurnsDelay;

    void Start()
    {
        InitialiseCharacter();
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

            if (attackDictionary[directionVector]())
            {
                Hit(attackDictionary[directionVector]());
            }
            else if (movementDictionary[directionVector]())
            {
                transform.position = directionVector + (Vector2)transform.position;
            }
            isAllowedToUpdate = false;
        }
        else 
            turnsTimer--;

    }

    private Vector2 GetDirectionByDifference(Vector2 difference)
    {
        var activationDifference = 0f; // Он может шагнуть на Playe => обработать коллизию или сравнивать с 1.1
                                          // Затем вынести в констнату
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
        if (curTime - lastTime > Player.Eps 
            && (curTime - lastTime + Player.Eps) % (Player.DeltaTime * pauseCoefficient) < 2 * Player.Eps)
        {
            Debug.Log(curTime - lastTime);
            isAllowedToUpdate = true; // А нужна ли? Возможно надо переименовать (и в плеере тоже)
            lastTime = curTime;
        }
        return isAllowedToUpdate;
    }

    private float GetDistanceToPlayer()
        => Mathf.Sqrt((player.Position.x - transform.position.x) * (player.Position.x - transform.position.x) +
            (player.Position.y - transform.position.y) * (player.Position.y - transform.position.y));

}
