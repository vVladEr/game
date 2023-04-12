using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game 
{
    public class Wall : MonoBehaviour
    {
        private void OnCollisionEnter2D(Collision2D collision) 
        {
            Debug.Log("wall spot collision");
        }
    }
}

