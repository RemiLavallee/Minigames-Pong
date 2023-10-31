using UnityEngine;

namespace Core.Pong
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private GameObject ball;
        private Vector2 spawnPosition = new Vector2(0, 0);
        
        private void RespawnBall()
        {
            Instantiate(ball, spawnPosition, Quaternion.identity);
        }
        
        public void DestroyAndRespawnBall(GameObject ball)
        {
            Destroy(ball);
            Invoke(nameof(RespawnBall), 2.0f);
        }
    }
}
