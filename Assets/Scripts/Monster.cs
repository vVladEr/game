using Game;
using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Monster : MonoBehaviour
{
    [SerializeField] public Player player;
    [SerializeField] public Shadows mainShadow;
    private float stepLength = 0.16f;
    private bool isAllowedToUpdate = true;
    private float lastTime = 0f;
    private float activationDistance = 0.64f; // Можно вынести в константу
    private float pauseCoefficient = 1.5f; // Возможность игроку убежать от монстра
    public IReadOnlyDictionary<Vector2, Func<bool>> shadowsDictionary;

    void Start()
    {
        shadowsDictionary = new Dictionary<Vector2, Func<bool>>(){
                {new Vector2(stepLength, 0), () => mainShadow.IsRightNotCollided()},
                {new Vector2(-stepLength, 0),() => mainShadow.IsLeftNotCollided()},
                {new Vector2(0, stepLength), () => mainShadow.IsTopNotCollided()},
                {new Vector2(0, -stepLength),() => mainShadow.IsBottomNotCollided()}
            };

    }

    void Update()
    {
        if (CanUpdate())
            MonsterUpdate();
    }

    private void MonsterUpdate()
    {
        var difference = player.Position - (Vector2)transform.position;
        var directionVector = GetDirectionByDifference(difference);
        if (directionVector.x == stepLength)
            GetComponent<SpriteRenderer>().flipX = false;
        else if (directionVector.x == -stepLength)
            GetComponent<SpriteRenderer>().flipX = true;
        if (shadowsDictionary[directionVector]()) 
        {
            transform.position = directionVector + (Vector2)transform.position;
        }
        isAllowedToUpdate = false;
    }

    private Vector2 GetDirectionByDifference(Vector2 difference)
    {
        var activationDifference = 0f; // Он может шагнуть на Playe => обработать коллизию или сравнивать с 1.1
                                          // Затем вынести в констнату
        if (difference.x > activationDifference) return new Vector2(stepLength, 0);
        if (difference.x < activationDifference) return new Vector2(-stepLength, 0);
        if (difference.y > activationDifference) return new Vector2(0, stepLength);
        if (difference.y < activationDifference) return new Vector2(0, -stepLength);
        return new Vector2(0, 0);
    }

    private bool CanUpdate()
    {
        if (GetDistanceToPlayer() > activationDistance) 
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
