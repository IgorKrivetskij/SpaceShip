using UnityEngine;

public class SmoothFollow : MonoBehaviour
{
    [SerializeField] private Transform _target;
    private float _distance = 10.0f;
    private float _height = 3.0f;
    private float _heightDamping = 0.5f;
    private float _rotationDamping = 3.0f;
    private bool _isMooveFast;
    
    private void LateUpdate()
    {
        // Early out if we don't have a target
        if (!_target)
        {
            return;
        }
        if (!_isMooveFast)
        {
            CameraMove();
        }       
    }

    private void CameraMove()
    {
        // Calculate the current rotation angles
        float wantedRotationAngle = _target.eulerAngles.y;
        float wantedHeight = _target.position.y + _height;

        float currentRotationAngle = transform.eulerAngles.y;
        float currentHeight = transform.position.y;

        // Damp the rotation around the y-axis
        currentRotationAngle = Mathf.LerpAngle(currentRotationAngle, wantedRotationAngle, _rotationDamping * Time.deltaTime);

        // Damp the height
        currentHeight = Mathf.Lerp(currentHeight, wantedHeight, _heightDamping * Time.deltaTime);

        // Convert the angle into a rotation
        Quaternion currentRotation = Quaternion.Euler(0, currentRotationAngle, 0);

        // Set the position of the camera on the x-z plane to:
        // distance meters behind the target
        var pos = transform.position;
        pos = _target.position - currentRotation * Vector3.forward * _distance;
        pos.y = currentHeight;
        transform.position = pos;

        // Always look at the target
        transform.LookAt(_target);
    }

    public void MooveAtSpaceShip()
    {
        _isMooveFast = !_isMooveFast;
        transform.position = _target.position;
        transform.parent = _target;
        transform.position += new Vector3( 0, 1f, -2f);
    }

    public void MooveAlone()
    {
        _isMooveFast = !_isMooveFast;
        transform.parent = null;
    }
}