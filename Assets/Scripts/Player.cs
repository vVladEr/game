using System;
using System.Collections.Generic;
using UnityEngine;
namespace Game 
{
    public class Player : Character
    {
        public Vector2 InitialPosition;
        public Vector2 PreviousPosition;
        private float lastTime = 0f;
        private bool isAllowedToMove = true;
        public Vector2 Position => transform.position;

        public const float DeltaTime = 1f;
        public const float Eps = 0.1f;
        public const float mathEps = 0.0001f;

        void Start()
        {
            //coll = gameObject.GetComponent<Collider2D>();
            InitialiseCharacter();
            transform.position = InitialPosition;
        }

        void Update()
        {
            IsAllowedToMove();
            if (isAllowedToMove)
                PlayerUpdate();
        }

        private Vector2 GetDirectionVector()
        {
            if (Input.GetKeyDown(KeyCode.D))
                return new Vector2(stepLength, 0);
            if (Input.GetKeyDown(KeyCode.A))
                return new Vector2(-stepLength, 0);
            if (Input.GetKeyDown(KeyCode.W))
                return new Vector2(0, stepLength);
            if (Input.GetKeyDown(KeyCode.S))
                return new Vector2(0, -stepLength);

            return new Vector2(0, 0);
        }

        private void PlayerUpdate() 
        {
            Vector2 currentPosition = transform.position;
            Vector2 newPosition = currentPosition;
            if (Input.anyKeyDown)
            {
                var directionVector = GetDirectionVector();
                if (Math.Abs(directionVector.x - stepLength) < mathEps)
                    GetComponent<SpriteRenderer>().flipX = false;
                else if (Math.Abs(directionVector.x + stepLength) < mathEps)
                    GetComponent<SpriteRenderer>().flipX = true;
                if (directionVector != new Vector2(0, 0) && movementDictionary[directionVector]())
                {
                    PreviousPosition = currentPosition;
                    newPosition = currentPosition + directionVector;
                }
                isAllowedToMove = false;
                transform.position = newPosition;
            }

        }

        private void IsAllowedToMove() 
        {
            var curTime = Time.time;
            if (curTime - lastTime > Eps && (curTime-lastTime+Eps)%DeltaTime < 2*Eps) 
            {
                Debug.Log(curTime - lastTime);
                isAllowedToMove = true;
                lastTime = curTime;
            }
        }


    }
}
