using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game 
{
    public class Wall : MonoBehaviour
    {
        private void OnCollisionEnter2D(Collision2D collision) 
        {
            if(collision.gameObject.tag == "Shadow")
                Debug.Log("wall spot collision");
        }
    }
}

