using UnityEngine;

namespace Props
{
    public class Smashable : PlayerClickable
    {
        protected override void Start()
        {
            base.Start();
        }
        
        [Tooltip("How many times the prop can be clicked before it dies or is destroyed.")]
        [SerializeField] private int durability = 5;
        protected override void OnMouseDown()
        {
            base.OnMouseDown();
            durability--;
            if (durability == 0) {
                // TODO Swap to a death state instead of destroying the object.
                _statsController.IncrementObjectsSmashed();
                Destroy(this.gameObject);
            }
        }
    }
}
