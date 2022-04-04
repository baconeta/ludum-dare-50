using JetBrains.Annotations;
using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Controllers
{
    public class ObstacleController : MonoBehaviour
    {
        [SerializeField] private GameObject[] standardHazards;
        [SerializeField] private GameObject[] largeHazards;
        [SerializeField] private bool debug;
        private WorldController _worldController;

        private void Start()
        {
            _worldController = FindObjectOfType<WorldController>();
        }

        public void SpawnHazard(GameObject parent, float parentWidth, float parentHeight)
        {
            if (!debug)
            {
                var obstacleType = GetRandomHazardType(parent);
                SpawnObstacle(obstacleType, parent, parentWidth, parentHeight);
                SpawnHazardBypassIfExists(obstacleType, parent, parentWidth, parentHeight);
            }
            else
            {
                //Spawns an object at each of the maximums to check that they're generating within the boundary
                var obstacleType = GetRandomHazardType(parent);
                SpawnEdgeObstacles(obstacleType, parent, parentWidth, parentHeight);
            }
        }

        private GameObject GetRandomHazardType(GameObject parent)
        {
            if (parent.CompareTag("LargeGround"))
            {
                return largeHazards.ChooseRandom();
            }

            return standardHazards.ChooseRandom();
        }

        private void SpawnHazardBypassIfExists(GameObject hazardType, GameObject parent, float parentWidth, float parentHeight)
        {
            var bypassType = GetBypassObjectType(hazardType);
            if (bypassType != null)
            {
                SpawnObstacle(bypassType, parent, parentWidth, parentHeight);
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

        private void SpawnObstacle(GameObject obstacleType, GameObject parent, float parentWidth, float parentHeight)
        {
            var obstacle = Instantiate(obstacleType, parent.transform);
            var location = GetOffset(obstacleType, parentWidth, parentHeight);
            Debug.Log($"Changing obstacle position from {obstacle.transform.position} to {obstacle.transform.position + location}");

            obstacle.transform.position += location;
        }

        private Vector3 GetOffset(GameObject obstacleType, float width, float height)
        {
            if (ShouldBePlacedRandomly(obstacleType))
                return GetRandomOffsetFromCenter(width, height);

            return Vector3.zero;
        }

        private static bool ShouldBePlacedRandomly(GameObject obstacleType)
        {
            return !obstacleType.CompareTag("BypassableHazard");
        }

        private Vector3 GetRandomOffsetFromCenter(float width, float height)
        {
            var newWidth = GetRandomValueFromMidpoint(width);
            var newHeight = GetRandomValueFromMidpoint(height);
            return AdjustLocationForWorldMovement(newWidth, newHeight);
        }

        private Vector3 AdjustLocationForWorldMovement(float newWidth, float newHeight)
        {
            var motion = -_worldController.getMoveDirection();
            var framesMoved = newWidth;
            var adjustedHeight = newHeight - (motion * framesMoved).y;

            if (debug)
                Debug.Log($"Offset for obstacle randomised to ({newWidth}, {newHeight}). Moved {framesMoved} at {motion}, giving a Y movement of {(motion * framesMoved).y} and a final position change of ({newWidth}, {adjustedHeight})");

            return new Vector3(newWidth, adjustedHeight, 0);
        }

        private static float GetRandomValueFromMidpoint(float size, float padding = 0.7f)
        {
            var maxOffset = size * padding / 2f;
            return Random.Range(-maxOffset, maxOffset);
        }

        #region debug

        private void SpawnEdgeObstacles(GameObject obstacleType, GameObject parent, float parentWidth, float parentHeight)
        {
            var obstacle = Instantiate(obstacleType, parent.transform);
            var location = GetMaxMaxOffsetFromCenter(parentWidth, parentHeight);
            obstacle.transform.position += location;

            var obstacle2 = Instantiate(obstacleType, parent.transform);
            var location2 = GetMaxMinOffsetFromCenter(parentWidth, parentHeight);
            obstacle2.transform.position += location2;

            var obstacle3 = Instantiate(obstacleType, parent.transform);
            var location3 = GetMinMaxOffsetFromCenter(parentWidth, parentHeight);
            obstacle3.transform.position += location3;

            var obstacle4 = Instantiate(obstacleType, parent.transform);
            var location4 = GetMinMinOffsetFromCenter(parentWidth, parentHeight);
            obstacle4.transform.position += location4;
        }

        private Vector3 GetMaxMaxOffsetFromCenter(float width, float height)
        {
            var newWidth = GetMaxValueFromMidpoint(width);
            var newHeight = GetMaxValueFromMidpoint(height);
            return AdjustLocationForWorldMovement(newWidth, newHeight);
        }

        private Vector3 GetMaxMinOffsetFromCenter(float width, float height)
        {
            var newWidth = GetMaxValueFromMidpoint(width);
            var newHeight = GetMinValueFromMidpoint(height);
            return AdjustLocationForWorldMovement(newWidth, newHeight);
        }

        private Vector3 GetMinMaxOffsetFromCenter(float width, float height)
        {
            var newWidth = GetMinValueFromMidpoint(width);
            var newHeight = GetMaxValueFromMidpoint(height);
            return AdjustLocationForWorldMovement(newWidth, newHeight);
        }

        private Vector3 GetMinMinOffsetFromCenter(float width, float height)
        {
            var newWidth = GetMinValueFromMidpoint(width);
            var newHeight = GetMinValueFromMidpoint(height);
            return AdjustLocationForWorldMovement(newWidth, newHeight);
        }

        private static float GetMaxValueFromMidpoint(float size, float padding = 0.7f)
        {
            var maxOffset = size * padding / 2f;
            return maxOffset;
        }

        private static float GetMinValueFromMidpoint(float size, float padding = 0.7f)
            => -GetMaxValueFromMidpoint(size, padding);

        #endregion debug
    }
}
