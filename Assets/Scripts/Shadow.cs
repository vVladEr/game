using Game;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shadow : MonoBehaviour
{
    // Start is called before the first frame update
    public Vector2 PreviousPosition;
    public Vector2 CurrentPosition;
    [SerializeField] public Player player; 
    void Start()
    {
        transform.position = player.InitialPosition;
        PreviousPosition = transform.position;

    }

    // Update is called once per frame
    void Update()
    {
        Vector2 currentPosition = transform.position;
        Vector2 newPosition = currentPosition;
        if (Input.GetKeyDown(KeyCode.D))
        {
            PreviousPosition = currentPosition;
            newPosition = new(currentPosition.x + 1f, currentPosition.y);
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            PreviousPosition = currentPosition;
            newPosition = new(currentPosition.x - 1f, currentPosition.y);
        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            PreviousPosition = currentPosition;
            newPosition = new(currentPosition.x, currentPosition.y + 1f);
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            PreviousPosition = currentPosition;
            newPosition = new(currentPosition.x, currentPosition.y - 1f);
        }
        transform.position = newPosition;
        CurrentPosition = newPosition;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag != "Player")
        {
            transform.position = PreviousPosition;
        }
        else 
        {
            CurrentPosition = transform.position;
        }
    }
}
