using UnityEngine;

namespace Core.Pong
{
    public class Paddle : MonoBehaviour
    {
        [SerializeField] [Min(0)] private float speed = 1f;
        private Rigidbody2D _rigidbody2D;
        private Vector2 _previousDirection;
        
        private void Start()
        {
            _rigidbody2D = GetComponent<Rigidbody2D>();
        }

        public void Move(Vector2 direction)
        {
            if (direction != Vector2.zero && _previousDirection != direction)
            {
                _rigidbody2D.velocity *= -1;
            }

            _rigidbody2D.AddForce(direction * speed);
            _rigidbody2D.velocity = Vector2.ClampMagnitude(_rigidbody2D.velocity, speed);

            _previousDirection = direction;
        }
    }
}