using UnityEngine;

namespace Controllers
{
    public class WorldController : MonoBehaviour
    {
        private GameController _gameController;
        private StatsController _statsController;

        [Tooltip("The speed at which the world moves, before any modifiers are applied.")] [SerializeField]
        private float baselineWorldSpeed = 4.0f;

        // Modifying this variable will
        [Tooltip("The initial speed at which the world moves. Set to 1.0 for no effect.")] [SerializeField]
        private float initialRampSpeedModifier = 0.1f;

        public float _currentRampSpeedModifier;

        [Tooltip("A value of 0 - 1. All speed is modified by this value. 0 = Standstill, 1 = full speed")] 
        private float percentageSpeedModifier = 1;
        
        [Tooltip("Environmental effect on speed - Such as tar slowing Dodo down")]
        public float environmentalSpeedModifier = 1.0f;

        [Tooltip("How many seconds it takes for the rampSpeedModifier to decay to 1.0.")] [SerializeField]
        private float worldSpeedRampDuration = 60.0f;

        private bool _ramping;

        private Vector3 _moveDirection = new Vector3(1f, -.5f);

        private float _timeElapsed;

        // Start is called before the first frame update
        void Start()
        {
            _gameController = FindObjectOfType<GameController>();
            _statsController = FindObjectOfType<StatsController>();
            _currentRampSpeedModifier = initialRampSpeedModifier;
            _ramping = _currentRampSpeedModifier < 1.0f;
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
                        _currentRampSpeedModifier = Mathf.Lerp(initialRampSpeedModifier, 1.0f,
                            _timeElapsed / worldSpeedRampDuration);
                        _timeElapsed += Time.deltaTime;
                    }
                    else
                    {
                        _currentRampSpeedModifier = 1.0f;
                    }
                }
            }
        }

        public float getWorldSpeed()
        {
            if (_gameController.gameRunning)
            {
                return baselineWorldSpeed * _currentRampSpeedModifier * environmentalSpeedModifier * percentageSpeedModifier;
            }
            else
            {
                return 0.0F;
            }
        }

        public void setBaselineWorldSpeed(float speed)
        {
            baselineWorldSpeed = speed;
        }
        
        public void setWorldSpeedPercentage(float speed)
        {
            percentageSpeedModifier = speed;
        }
        
        public float getWorldSpeedPercentage()
        {
            return percentageSpeedModifier;
        }

        public Vector3 getMoveDirection()
        {
            return _moveDirection;
        }
    }
}