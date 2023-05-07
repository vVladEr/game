using Game;
using Unity.VisualScripting;
using UnityEngine;

public class Monster : MonoBehaviour
{
    [SerializeField] public Player player;
    private bool isAllowedToUpdate = true;
    private float lastTime = 0f;
    private float activationDistance = 4f; // Можно вынести в константу
    private float pauseCoefficient = 1.5f; // Возможность игроку убежать от монстра

    void Start()
    {
        
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
        if (directionVector.x == 1)
            GetComponent<SpriteRenderer>().flipX = false;
        else if (directionVector.x == -1)
            GetComponent<SpriteRenderer>().flipX = true;
        transform.position = directionVector + (Vector2)transform.position;
        isAllowedToUpdate = false;
    }

    private Vector2 GetDirectionByDifference(Vector2 difference)
    {
        var activationDifference = 0f; // Он может шагнуть на Playe => обработать коллизию или сравнивать с 1.1
                                          // Затем вынести в констнату
        if (difference.x > activationDifference) return new Vector2(1, 0);
        if (difference.x < activationDifference) return new Vector2(-1, 0);
        if (difference.y > activationDifference) return new Vector2(0, 1);
        if (difference.y < activationDifference) return new Vector2(0, -1);
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
