using Game;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shadow : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] public Shadows mainShadow;
    public bool NotCollided = true;
    public float dx;
    public float dy;
    void Start()
    {
        transform.position = new Vector2(mainShadow.transform.position.x +dx, mainShadow.transform.position.y + dy);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector2(mainShadow.transform.position.x + dx, mainShadow.transform.position.y + dy);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Wall")
        {
            Debug.Log("Shadow Collided");
            NotCollided = false;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Wall")
        {
            NotCollided = true;
        }
    }
}
