using JetBrains.Annotations;
using UnityEngine;

namespace Controllers
{
    public class ObstacleController : MonoBehaviour
    {
        [SerializeField] private GameObject[] standardHazards;
        [SerializeField] private GameObject[] largeHazards;

        public void SpawnHazard(GameObject parent, float parentWidth)
        {
            var obstacleType = GetRandomHazardType(parent);
            SpawnObstacleRandomly(obstacleType, parent, parentWidth);

            SpawnHazardBypassIfExists(obstacleType, parent, parentWidth);
        }

        private GameObject GetRandomHazardType(GameObject parent)
        {
            if (parent.CompareTag("LargeGround"))
            {
                return largeHazards.ChooseRandom();
            }

            return standardHazards.ChooseRandom();
        }

        private void SpawnHazardBypassIfExists(GameObject hazardType, GameObject parent, float parentWidth)
        {
            var bypassType = GetBypassObjectType(hazardType);
            if (bypassType != null)
            {
                Debug.Log("Spawning bypass object: ", bypassType);
                SpawnObstacleRandomly(bypassType, parent, parentWidth);
            }
        }

        [CanBeNull]
        private GameObject GetBypassObjectType(GameObject hazardType)
        {
            var bypassableHazard =  hazardType.GetComponent<BypassableHazard>();
            if (bypassableHazard == null)
                return null;

            return bypassableHazard.bypassObject;
        }

        private void SpawnObstacleRandomly(GameObject obstacleType, GameObject parent, float parentWidth)
        {
            var obstacle = Instantiate(obstacleType, parent.transform);
            var location = GetRandomOffset(parentWidth);
            obstacle.transform.position += location;
        }

        private static Vector3 GetRandomOffset(float width)
        {
            var maxOffset = width * 0.8f / 2f;
            var offset = Random.Range(-maxOffset, maxOffset);
            return new Vector3(offset, 0, 0);
        }
    }
}
