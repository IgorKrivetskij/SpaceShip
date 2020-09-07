using UnityEngine;
using UnityEngine.Events;

public class SpaceShipCollizions : MonoBehaviour
{
    public UnityEvent GameOver;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<AsteroidMoovement>())
        {
           Destroy(collision.gameObject);
            GameOver.Invoke();
        }
    }
}
