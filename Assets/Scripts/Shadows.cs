using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shadows : MonoBehaviour
{
    [SerializeField] MonoBehaviour entity;
    [SerializeField] private Shadow leftShadow;
    [SerializeField] private Shadow rightShadow;
    [SerializeField] private Shadow topShadow;
    [SerializeField] private Shadow bottomShadow;
    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector2(entity.transform.position.x, entity.transform.position.y);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector2(entity.transform.position.x, entity.transform.position.y);
    }

    public bool IsRightNotCollided() 
    {
        return rightShadow.NotCollided;
    }
    public bool IsLeftNotCollided()
    {
        return leftShadow.NotCollided;
    }
    public bool IsTopNotCollided()
    {
        return topShadow.NotCollided;
    }
    public bool IsBottomNotCollided()
    {
        return bottomShadow.NotCollided;
    }
}
