using UnityEngine;
using System.Collections;

namespace Props
{
    public class Smashable : PlayerClickable
    {
        [Tooltip("How many times the prop can be clicked before it dies or is destroyed.")]
        [SerializeField] private int durability = 5;
        private bool shaking = false;

        protected override void Start()
        {
            base.Start();
        }

        protected override void OnMouseDown()
        {
            base.OnMouseDown();
            if (CanPlayerInteract && !IsDodoInteracting)
            {
                durability--;
                if (durability == 0) {
                    // TODO Swap to a death state instead of destroying the object.
                    _statsController.IncrementSabersSlain();
                    Destroy(this.gameObject);
                }
                StartCoroutine(DoShake());
            }
        }

        protected IEnumerator DoShake()
        {
            if (!shaking)
            {
                shaking = true;
                transform.Rotate(0, 0, 8);
                yield return new WaitForSeconds(0.05F);
                transform.Rotate(0, 0, -16);
                yield return new WaitForSeconds(0.0F);
                transform.Rotate(0, 0, 8);
                shaking = false;
            }
        }
    }
}
