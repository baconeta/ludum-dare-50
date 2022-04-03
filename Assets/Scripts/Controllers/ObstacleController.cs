using UnityEngine;

namespace Controllers
{
    public class ObstacleController : MonoBehaviour
    {
        [SerializeField] private GameObject[] standardHazards;
        [SerializeField] private GameObject[] largeHazards;

        public void SpawnHazard(GameObject parent, float width)
        {
            var obstacleType = GetRandomHazardType(parent);
            var obstacle = Instantiate(obstacleType, parent.transform);

            var location = GetRandomOffset(width);
            obstacle.transform.position += location;
        }

        private GameObject GetRandomHazardType(GameObject parent)
        {
            if (parent.CompareTag("LargeGround"))
            {
                return largeHazards.ChooseRandom();
            }

            return standardHazards.ChooseRandom();
        }

        private static Vector3 GetRandomOffset(float width)
        {
            var maxOffset = width * 0.8f / 2f;
            var offset = Random.Range(-maxOffset, maxOffset);
            return new Vector3(offset, 0, 0);
        }
    }
}
