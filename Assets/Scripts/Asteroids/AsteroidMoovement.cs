using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class AsteroidMoovement : MonoBehaviour
{
    private float _speed;
    private float _lifeTime;
    private Rigidbody _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _speed = 100f;
        _lifeTime = 10f;
    }

    private void Update()
    {
        _lifeTime -= Time.deltaTime;
        if (_lifeTime <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void FixedUpdate()
    {
        _rigidbody.velocity = new Vector3( 0, 0, _speed * (-1) );
    }
    
}
