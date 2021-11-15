using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowController : MonoBehaviour
{
    [SerializeField] private float arrowVelocity = 20f;
    Rigidbody2D arrowRigidbody;

    private void Awake() 
    {
        arrowRigidbody =  GetComponent<Rigidbody2D>();
    }

    public void SetDirection(Vector3 directionVector)
    {
        float angle = Vector3.SignedAngle(new Vector3(1f, 0, 0), directionVector, Vector3.forward);
        transform.Rotate(0f, 0f, angle);

        Vector3 magnitude = new Vector3(arrowVelocity, 0f, 0f);
        Vector3 newVelocity = Vector3.RotateTowards(magnitude, directionVector, 2*Mathf.PI, 0);
        arrowRigidbody.velocity = new Vector2(newVelocity.x, newVelocity.y);
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.gameObject.tag == "Enemy")
        {
            Destroy(other.attachedRigidbody.gameObject);
        }    
        Destroy(gameObject);
    }
}
