using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackAndForthMonster : BasicMonster
{
    private Vector2[] route;
    private int pointer = 0;
    [SerializeField] private bool IsHorizontal;
    public override void InitialiseMonster()
    {
        turnsTimer = TurnsDelay;
        Weapon = GetComponent<Dagger>();
        Weapon.InisialisePlayer();
        if(IsHorizontal)
            route = new Vector2[] {
                new Vector2 (stepLength, 0),
                new Vector2 (-stepLength, 0)
            };
        else
            route = new Vector2[] {
                new Vector2 (0, stepLength),
                new Vector2 (0, -stepLength)
            };
    }

    public override void MonsterUpdate()
    {
        if (turnsTimer == 0)
        {
            turnsTimer = TurnsDelay;
            var directionVector = route[pointer];
            FlipSprite(directionVector);
            Weapon.Attack(directionVector.normalized);
            if (!Weapon.AttackSucc &&
                !IsPositionCaptured((Vector2)transform.position + directionVector)
                && (IsDirectionFree(directionVector) || IsInterectiveFree(directionVector)))
            {
                pointer = (pointer+1)%route.Length;
                newPosition = directionVector + (Vector2)transform.position;
                General.CapturedPositions.Add(FixPosition(newPosition));
                isMoving = true;
            }
        }
        else
            turnsTimer--;

    }

    public override bool CanUpdate()
    {
        return IsAllowedToMoveByTick();
    }
}
