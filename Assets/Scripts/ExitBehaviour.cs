using UnityEngine;

public class ExitBehaviour : MonoBehaviour
{
    private GameManager _manager;

    private void Awake() 
    {
        _manager = FindObjectOfType<GameManager>();    
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.tag == "Player")
        {
            StartCoroutine(_manager.LevelCompleted());
        }
    }
}
