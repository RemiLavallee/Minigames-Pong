using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Core.Pong
{
    public class Ball : MonoBehaviour
    {
        private Rigidbody2D _rb;
        [SerializeField] private float initialSpeed = 25;
        private RaycastHit2D[] _raycastHit2Ds = new RaycastHit2D[1];
        private float maxSpeed;

        private void Start()
        {
            _rb = GetComponent<Rigidbody2D>();
            maxSpeed = initialSpeed + 10;
            StartCoroutine(LaunchBallDelay(1.0f));
        }

        private void MoveBall()
        {
            var vector2 = new Vector2(Random.Range(-1f, 1f), Random.Range(-0.5f, 0f)).normalized;
            _rb.velocity = vector2 * initialSpeed;
        }

        private void FixedUpdate()
        {
            if (_rb.Cast(_rb.velocity.normalized, _raycastHit2Ds, _rb.velocity.magnitude * Time.deltaTime) <= 0) return;
            var hit = _raycastHit2Ds[0];

            if (hit.collider.gameObject.CompareTag("Paddle"))
            {
                var paddleRb = hit.collider.gameObject.GetComponent<Rigidbody2D>();
                
                var hitPointRelative = (hit.point - (Vector2)hit.collider.transform.position).y;
                var normalizedRelativePosition = hitPointRelative / (hit.collider.bounds.size.y / 2);
                var speedMultiplier = 1 + Mathf.Abs(normalizedRelativePosition);
                var newVelocity = Vector2.Reflect(_rb.velocity, hit.normal) * speedMultiplier;
                
                newVelocity += paddleRb.velocity * 0.25f;
                _rb.velocity = newVelocity.magnitude > maxSpeed ? newVelocity.normalized * maxSpeed : newVelocity;
            }
        }

        private void OnCollisionEnter2D(Collision2D col)
        {
            if (col.collider.CompareTag("EndZone"))
            {
                var gameManager = FindObjectOfType<GameManager>();
                if (gameManager != null)
                {
                    gameManager.DestroyAndRespawnBall(gameObject);
                }
            }
        }
        
        private IEnumerator LaunchBallDelay(float delay)
        {
            yield return new WaitForSeconds(delay);
            MoveBall();
        }
    }
}