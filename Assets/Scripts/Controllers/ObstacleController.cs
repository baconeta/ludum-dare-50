using UnityEngine;

namespace Controllers
{
    public class ObstacleController : MonoBehaviour
    {
        [SerializeField] private GameObject[] obstacleTypes;

        public void SpawnObstacle(GameObject parent, float width)
        {
            var obstacleType = GetRandomObstacleType();
            var obstacle = Instantiate(obstacleType, parent.transform);

            var location = GetRandomOffset(width);
            obstacle.transform.position += location;
        }

        private GameObject GetRandomObstacleType()
        {
            return obstacleTypes[Random.Range(0, obstacleTypes.Length)];
        }

        private static Vector3 GetRandomOffset(float width)
        {
            var maxOffset = width * 0.8f / 2f;
            var offset = Random.Range(-maxOffset, maxOffset);
            return new Vector3(offset, 0, 0);
        }
    }
}
