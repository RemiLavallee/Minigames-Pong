using System;
using UnityEngine;

namespace Core.Pong
{
    [RequireComponent(typeof(Paddle))]
    public class PaddleInput : MonoBehaviour
    {
        [SerializeField] private KeyCode upKey = KeyCode.W;
        [SerializeField] private KeyCode downKey = KeyCode.S;

        private Vector2 _direction;

        private Paddle _paddle;

        private void Awake()
        {
            _paddle = GetComponent<Paddle>();
        }

        private void Update()
        {
            _direction.y = 0f;
            if (Input.GetKey(upKey))
            {
                _direction.y = 1f;
            }

            if (Input.GetKey(downKey))
            {
                _direction.y = -1f;
            }
        }

        private void FixedUpdate()
        {
            _paddle.Move(_direction);
        }
    }
}