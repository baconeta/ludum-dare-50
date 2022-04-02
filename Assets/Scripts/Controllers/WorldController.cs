using UnityEngine;

namespace Controllers
{
    public class WorldController : MonoBehaviour
    {
        private StatsController _statsController;

        [Tooltip("The speed at which the world moves, before any modifiers are applied.")] [SerializeField]
        private float baselineWorldSpeed = 4.0f;

        // Modifying this variable will
        [Tooltip("The initial speed at which the world moves. Set to 1.0 for no effect.")] [SerializeField]
        private float initialRampSpeedModifier = 0.1f;

        public float currentRampSpeedModifier;
        public float environmentalSpeedModifier = 1.0f;

        [Tooltip("How many seconds it takes for the rampSpeedModifier to decay to 1.0.")] [SerializeField]
        private float worldSpeedRampDuration = 60.0f;

        private bool _ramping;

        private Vector3 _moveDirection = new Vector3(1f, -.5f);

        private float _timeElapsed;

        // Start is called before the first frame update
        void Start()
        {
            _statsController = FindObjectOfType<StatsController>();
            currentRampSpeedModifier = initialRampSpeedModifier;
            _ramping = currentRampSpeedModifier < 1.0f;
            _statsController.StartRun();
        }

        // Update is called once per frame
        void Update()
        {
            // Decay ramp modifier to 1 over the course of the ramp duration.
            if (_ramping)
            {
                // Check if we have been ramping for long enough.
                if (worldSpeedRampDuration < _statsController.time)
                {
                    _ramping = false;
                }
                else
                {
                    if (_timeElapsed < worldSpeedRampDuration)
                    {
                        currentRampSpeedModifier = Mathf.Lerp(initialRampSpeedModifier, 1.0f,
                            _timeElapsed / worldSpeedRampDuration);
                        _timeElapsed += Time.deltaTime;
                    }
                    else
                    {
                        currentRampSpeedModifier = 1.0f;
                    }
                }
            }
        }

        public float getWorldSpeed()
        {
            return baselineWorldSpeed * currentRampSpeedModifier * environmentalSpeedModifier;
        }

        public Vector3 getMoveDirection()
        {
            return _moveDirection;
        }
    }
}