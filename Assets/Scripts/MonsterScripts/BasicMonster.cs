using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game;

public abstract class BasicMonster : Character
{
    public float mathEps = 0.0001f;
    public int TurnsDelay = 1;
    public int turnsTimer;
    public BasicWeapon Weapon;
    public Player player;
    void Start()
    {
        InitialiseCharacter();
        InitialiseMonster();
        player = GameObject.Find("Player").GetComponent<Player>();
    }
    private void Update()
    {
        MoveSmoothly();
        if (CanUpdate())
            MonsterUpdate();
    }
    public abstract void InitialiseMonster();

    public abstract bool CanUpdate();

    public abstract void MonsterUpdate();

    public bool IsAllowedToMoveByTick() 
    {
        var curTime = Time.time;
        var tick = (int)(curTime / DeltaTime);
        if (tick != currentTick && (curTime - tick * DeltaTime) > Eps+ player.dampingTime)
        {
            currentTick = tick;
            return true;
        }
        return false;
    }

    public Vector2 GetRandomDirection() 
    {
        var val = Random.value;
        if (val < 0.25 && IsDirectionFree(new Vector2(1, 0)))
            return new Vector2(stepLength, 0);

        if (val < 0.5 && IsDirectionFree(new Vector2(-1, 0)))
            return new Vector2(-stepLength, 0);

        if (val < 0.75  && IsDirectionFree(new Vector2(0, 1)))
            return new Vector2(0, stepLength);

        if (IsDirectionFree(new Vector2(0, -1)))
            return new Vector2(0, -stepLength);

        return new Vector2(0, 0);
    }

}
