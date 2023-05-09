using System;
using System.Collections.Generic;
using UnityEngine;
namespace Game 
{
    public class Player : MonoBehaviour
    {
        [SerializeField] public Shadows Shadows;
        public IReadOnlyDictionary<Vector2, Func<bool>> shadowsDictionary;
        public Vector2 InitialPosition;
        public Vector2 PreviousPosition;
        private float lastTime = 0f;
        private bool isAllowedToMove = true;
        public Vector2 Position => transform.position;

        public const float DeltaTime = 0.1f;
        public const float Eps = 0.1f;
        public const float mathEps = 0.0001f;

        void Start()
        {
            transform.position = InitialPosition;
            shadowsDictionary = new Dictionary<Vector2, Func<bool>>(){
                {new Vector2(0.16f, 0), () => Shadows.IsRightNotCollided()},
                {new Vector2(-0.16f, 0),() => Shadows.IsLeftNotCollided()},
                {new Vector2(0, 0.16f), () => Shadows.IsTopNotCollided()},
                {new Vector2(0, -0.16f),() => Shadows.IsBottomNotCollided()}
            };
        }

        void Update()
        {
            PlayerUpdate();
            //IsAllowedToMove();
            //if (isAllowedToMove)
            //    PlayerUpdate();
        }

        private Vector2 GetDirectionVector()
        {
            if (Input.GetKeyDown(KeyCode.D))
                return new Vector2(0.16f, 0);
            if (Input.GetKeyDown(KeyCode.A))
                return new Vector2(-0.16f, 0);
            if (Input.GetKeyDown(KeyCode.W))
                return new Vector2(0, 0.16f);
            if (Input.GetKeyDown(KeyCode.S))
                return new Vector2(0, -0.16f);

            return new Vector2(0, 0);
        }

        private void PlayerUpdate() 
        {
            Vector2 currentPosition = transform.position;
            Vector2 newPosition = currentPosition;
            if (Input.anyKeyDown)
            {
                var directionVector = GetDirectionVector();
                if (Math.Abs(directionVector.x - 0.16f) < mathEps)
                    GetComponent<SpriteRenderer>().flipX = false;
                else if (Math.Abs(directionVector.x + 0.16f) < mathEps)
                    GetComponent<SpriteRenderer>().flipX = true;
                if (directionVector != new Vector2(0, 0) && shadowsDictionary[directionVector]())
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