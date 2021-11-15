using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeEnemyController : MonoBehaviour
{
    [SerializeField] float slimeSpeed = 8f;
    Rigidbody2D slimeRigidbody;

    void Awake()
    {
        slimeRigidbody = GetComponent<Rigidbody2D>();
        slimeRigidbody.velocity = new Vector2(slimeSpeed, 0f);
    }

    void Update()
    {

    }

    private void OnTriggerExit2D(Collider2D other) 
    {
        if (other.tag == "Platform")
        {
            slimeRigidbody.velocity = new Vector2(slimeRigidbody.velocity.x * -1, 0f);
            this.transform.localScale = new Vector3(this.transform.localScale.x * -1, 1f, 1f);
        }
    }
}
