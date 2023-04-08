using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class control : MonoBehaviour
{
    public float speed = 1.0f;
    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(0, 0, 0);
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 currentPosition = transform.position;
        Vector3 newPosition = currentPosition;
        var dFlag = true;
        if (Input.GetKeyDown(KeyCode.D) && dFlag)
        {
            newPosition = new(currentPosition.x + 1f, currentPosition.y);
        }
        if (Input.GetKeyDown(KeyCode.A))
            newPosition = new(currentPosition.x - 1f, currentPosition.y);
        if (Input.GetKeyDown(KeyCode.W))
            newPosition = new(currentPosition.x, currentPosition.y + 1f);
        if (Input.GetKeyDown(KeyCode.S))
            newPosition = new(currentPosition.x, currentPosition.y - 1f);
        var time = new WaitForSeconds(1);
        transform.position = newPosition;
    }
}
