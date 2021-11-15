using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    [SerializeField] int playerLives = 3;
    [SerializeField] float deathDelay = 1f;
    [SerializeField] float loadNextDelaySeconds = 0.5f;
    [SerializeField] AudioClip coinPickupSFX;

    [Header("HUD")]
    [SerializeField] Canvas hud;
    [SerializeField] TextMeshProUGUI livesText;
    [SerializeField] TextMeshProUGUI scoreText;

    private int coinsThisLevel = 0;
    private int playerScore = 0;

    void Awake()
    {
        int numInstances = FindObjectsOfType<GameManager>().Length;
        if (numInstances > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    private void Start() 
    {
        ResetUI();
    }

    public void TallyDeath()
    {
        playerLives--;
        livesText.text = "Lives: " + playerLives;

        if(playerLives == 0)
        {
            StartCoroutine(ResetGameSession());
        }
        else
        {
            StartCoroutine(ReloadCurrentLevel());
        }
    }

    public int GetCurrentScore()
    {
        return playerScore;
    }

    public void CoinCollected()
    {
        AudioSource.PlayClipAtPoint(coinPickupSFX, Camera.main.transform.position);
        coinsThisLevel++;
        scoreText.text = "Coins: " + (playerScore + coinsThisLevel);
    }

    public void ShowHUD()
    {
        hud.gameObject.SetActive(true);
    }

    public void HideHUD()
    {
        hud.gameObject.SetActive(false);
    }

    private void ResetUI()
    {
        livesText.text = "Lives: " + playerLives;
        scoreText.text = "Coins: " + playerScore;
    }

    public IEnumerator LevelCompleted()
    {
        yield return new WaitForSecondsRealtime(loadNextDelaySeconds);
        if (playerLives != 0)
        {
            playerScore += coinsThisLevel;
            int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
            SceneManager.LoadScene(currentSceneIndex + 1);
        }

    }

    public void PlayAgain()
    {
        SceneManager.LoadScene(0);
        Destroy(gameObject);
    }

    private IEnumerator ResetGameSession()
    {
        yield return new WaitForSecondsRealtime(deathDelay);
    }

    private IEnumerator ReloadCurrentLevel()
    {
        coinsThisLevel = 0;
        ResetUI();
        yield return new WaitForSecondsRealtime(deathDelay);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
