using UnityEngine;

namespace Core.Pong
{
    public class Ball : MonoBehaviour
    {
        private Rigidbody2D _rb;
        [SerializeField] private float speed = 25;
        private RaycastHit2D[] _raycastHit2Ds = new RaycastHit2D[1];

        private void Start()
        {
            _rb = GetComponent<Rigidbody2D>();
            MoveBall();
        }

        private void MoveBall()
        {
            var vector2 = new Vector2(Random.Range(-1f, 1f), Random.Range(-0.5f, 0f)).normalized;
            _rb.velocity = vector2 * speed;
        }

        private void FixedUpdate()
        {
            if (_rb.Cast(_rb.velocity.normalized, _raycastHit2Ds, _rb.velocity.magnitude * Time.deltaTime) <= 0) return;
            var hit = _raycastHit2Ds[0];

            if (hit.collider.gameObject.CompareTag("Paddle"))
            {
                _rb.MovePosition(hit.point);
                _rb.velocity = Vector2.Reflect(_rb.velocity, hit.normal);
            }
        }
    }
}