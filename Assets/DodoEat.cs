using System.Collections;
using System.Collections.Generic;
using Props;
using UnityEngine;

public class DodoEat : MonoBehaviour
{
    [SerializeField] private float _eatSpeedMin;
    [SerializeField] private float _eatSpeedMax;

    private Dodo _dodo;
    private AudioSource[] _eatingSounds;

    // Start is called before the first frame update
    void Start()
    {
        _dodo = GetComponentInParent<Dodo>();
        _eatingSounds = GetComponents<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void EatMelon()
    {
        var sound = _eatingSounds.ChooseRandom();
        StartCoroutine(waitToEat(sound));
    }

    IEnumerator waitToEat(AudioSource sound)
    {
        sound.Play();
        float randomTimeToEat = Random.Range(_eatSpeedMin, _eatSpeedMax);
        yield return new WaitForSeconds(randomTimeToEat);
        _dodo.getFocusedObject().GetComponent<DodoBait>().getEaten();
        _dodo.setEatingStatus(false);
        sound.Stop();
    }
}
