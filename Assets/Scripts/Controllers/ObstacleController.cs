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
        private WorldController _worldController;

        private void Start()
        {
            _worldController = FindObjectOfType<WorldController>();
        }

        public void SpawnHazard(GameObject parent, float parentWidth, float parentHeight)
        {
            var obstacleType = GetRandomHazardType(parent);
            SpawnObstacle(obstacleType, parent, parentWidth, parentHeight);
            SpawnHazardBypassIfExists(obstacleType, parent, parentWidth, parentHeight);
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
            var motion = -_worldController.getMoveDirection();
            var framesMoved = Math.Abs(newWidth);
            var adjustedHeight = newHeight + (motion * framesMoved).y;

            Debug.Log($"Offset for chunk of ({width}, {height}), randomised to ({newWidth}, {newHeight}). Moved {framesMoved} at {motion}, giving a Y movement of {(motion * framesMoved).y} and a final position of ({newWidth}, {adjustedHeight})");

            return new Vector3(newWidth, adjustedHeight, 0);
        }

        private static float GetRandomValueFromMidpoint(float size, float padding = 0.6f)
        {
            var maxOffset = size * padding / 2f;
            return Random.Range(-maxOffset, maxOffset);
        }
    }
}
