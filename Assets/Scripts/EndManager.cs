using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EndManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText;
    private GameManager _manager;

    private void Awake() 
    {
        _manager = FindObjectOfType<GameManager>();  
        _manager.HideHUD();  
    }

    // Start is called before the first frame update
    void Start()
    {
        scoreText.text = "Your Final Score Was: " + _manager.GetCurrentScore();
    }

    public void PlayAgain()
    {
        _manager.PlayAgain();
    }
}
