using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UIElements;

namespace Game 
{
    public class Player : MonoBehaviour
    {
        public float speed = 1.0f;
        public Vector2 PreviousPosition;
        [SerializeField] public Shadow shadow;
        // Start is called before the first frame update
        void Start()
        {
            transform.position = new Vector2(0.5f, 0.5f);
            PreviousPosition = transform.position;
        }

        // Update is called once per frame
        void Update()
        {
            Vector2 currentPosition = transform.position;
            Vector2 newPosition = currentPosition;
            if (Input.anyKeyDown)
            {
                var directionVector = GetDirectionVector();
                PreviousPosition = currentPosition;
                newPosition = currentPosition + directionVector;
            }
                
            transform.position = newPosition;
        }

        Vector2 GetDirectionVector()
        {
            var vector = new Vector2(0, 0);
            if (Input.GetKeyDown(KeyCode.D))
                vector = new Vector2(1, 0);
            if (Input.GetKeyDown(KeyCode.A))
                vector = new Vector2(-1, 0);
            if (Input.GetKeyDown(KeyCode.W))
                vector = new Vector2(0, 1);
            if (Input.GetKeyDown(KeyCode.S))
                vector = new Vector2(0, -1);

            return vector;
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            Debug.Log("collision detected");
            transform.position = PreviousPosition;
        }
    }
}

