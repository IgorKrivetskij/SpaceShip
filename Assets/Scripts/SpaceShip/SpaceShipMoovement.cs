using UnityEngine;

[RequireComponent (typeof(Rigidbody)) ]
public class SpaceShipMoovement : MonoBehaviour
{   
    private Rigidbody _rigidbody;
    private Vector3 _rotateVector;
    private float _speed;
    private float _maxRotateAngle;
    private float _rotationDamping;
    private int _rightIndex;
    private int _leftIndex;
    private int _idleIndex;
    private bool _isFastFly;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _speed = 30f;
        _maxRotateAngle = 30f;
        _rotationDamping = 30f;
        _rotateVector = new Vector3( 0, 0, _maxRotateAngle);
        _rightIndex = 1;
        _leftIndex = -1;
        _idleIndex = 0;
    }   

    private void FixedUpdate()
    {
        if (Input.GetKeyDown( KeyCode.D ) && transform.position.x < 13f) {
            MooveShip( _rightIndex);
            RotateShip( _rightIndex);
        } else if( Input.GetKeyDown( KeyCode.A ) && transform.position.x > -13f)
        {            
            MooveShip( _leftIndex);
            RotateShip( _leftIndex);
        }
        else if (Input.GetKeyUp( KeyCode.D ) || Input.GetKeyUp( KeyCode.A ) || 
            transform.position.x < -13 || transform.position.x > 13)
        {
            MooveShip( _idleIndex);
            RotateShip( _idleIndex);
        }
    }

    private void MooveShip(float direction)
    {        
        _rigidbody.velocity = new Vector3( direction * _speed, _rigidbody.velocity.y, _rigidbody.velocity.z);        
    }

    private void RotateShip( float index )
    {
        if (_isFastFly)
        {
            return;
        }
        transform.eulerAngles = ( _rotateVector * (-index) * _rotationDamping * Time.deltaTime );
    }

    public void SpeedChange()
    {
        _isFastFly = !_isFastFly;
    }
}
