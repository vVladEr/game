using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BasicMonster : Character
{
    public int TurnsDelay = 1;
    public int turnsTimer;
    public BasicWeapon Weapon;
    public Player player;
    [SerializeField] private bool isStrong;
    [SerializeField] private bool isFast;
    void Start()
    {
        if (isFast)
            TurnsDelay = 0;
        InitialiseCharacter();
        InitialiseMonster();
        player = GameObject.Find("Player").GetComponent<Player>();
        General = GameObject.Find("General").GetComponent<General>();
        if (isStrong)
            Weapon.Damage = 2;
    }
    private void Update()
    {
        MoveSmoothly();
        if (gameObject.GetComponent<EnemyHp>().IsAlive && CanUpdate())
            MonsterUpdate();
    }
    public abstract void InitialiseMonster();

    public abstract bool CanUpdate();

    public abstract void MonsterUpdate();

    public bool IsAllowedToMoveByTick() 
    {
        var curTime = General.Time;
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