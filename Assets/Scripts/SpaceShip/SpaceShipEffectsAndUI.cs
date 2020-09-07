using UnityEngine.Events;
using UnityEngine;

public class SpaceShipEffectsAndUI : MonoBehaviour
{
    [SerializeField] private ParticleSystem _fastFlyEffect;
    private bool _isTurbo =false;
    
    public UnityEvent TurboFlyStart;
    public UnityEvent TurboFlyStop;
    public bool IsTurbo => _isTurbo;

    private void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            _isTurbo = !_isTurbo;
            if (_isTurbo)
            {
                FastFlyBegan();
                TurboFlyStart.Invoke();
            }
            else
            {
                FastFlyStop();
                TurboFlyStop.Invoke();
            }
        }
    }

    private void FastFlyBegan()
    {
        _fastFlyEffect.Play();
    }

    private void FastFlyStop()
    {
        _fastFlyEffect.Stop();
    }
}
