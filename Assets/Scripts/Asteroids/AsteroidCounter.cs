using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class AsteroidCounter : MonoBehaviour
{
    public int AsteroidCount => _asteroidCount;
    public UnityEvent AsteroidCountChange;

    private int _asteroidCount;
    private float _collizionCD;
    private bool _canCount;

    private void Start()
    {
        _canCount = true;
        _collizionCD = 1f;   //CD of asterod spawn / 2;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<AsteroidMoovement>())
        {
            if (_canCount)
            {
                _asteroidCount++;
                AsteroidCountChange.Invoke();
                StartCoroutine(CollizionCount());
            } 
        }
    }

    IEnumerator CollizionCount()
    {
        _canCount = false;
        yield return new WaitForSeconds(_collizionCD);
        _canCount = true;
    }
}
