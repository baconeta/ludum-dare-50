using System.Collections;
using System.Collections.Generic;
using Props;
using UnityEngine;

public class DodoEat : MonoBehaviour
{
    [SerializeField] private float _eatSpeedMin;
    [SerializeField] private float _eatSpeedMax;

    private Dodo dodo;
    // Start is called before the first frame update
    void Start()
    {
        dodo = GetComponentInParent<Dodo>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void EatMelon()
    {
        StartCoroutine(waitToEat());
    }

    IEnumerator waitToEat()
    {
        float randomTimeToEat = Random.Range(_eatSpeedMin, _eatSpeedMax);
        yield return new WaitForSeconds(randomTimeToEat);
        dodo.getFocusedObject().GetComponent<DodoBait>().getEaten();
        dodo.setEatingStatus(false);
    }
}
