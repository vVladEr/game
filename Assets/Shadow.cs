using Game;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shadow : MonoBehaviour
{
    public bool IsTriggered { get; private set; }

    // Start is called before the first frame update
   // [SerializeField] public Player player; 
    void Start()
    {
        
    }

    public void UpdatePosition(Vector2 directionnVector)
    {
        transform.position = (Vector2)transform.position + directionnVector;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        IsTriggered = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        IsTriggered = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
