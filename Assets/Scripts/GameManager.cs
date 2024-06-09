using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public EnemySpawner enemySpawner;
    public EnemyData spaceFighterLeaderData;
    public EnemyData spaceFighterData;
    public EnemyData scoutData;
    public GameObject playerPrefab;
    public GameObject orbPrefab;
    public TextMeshProUGUI currentRoundText;
    public TextMeshProUGUI highestRoundText;
    public TextMeshProUGUI buffMessageText;
    public TextMeshProUGUI playerHealthText;
    public TextMeshProUGUI gameOverText;
    public Button restartButton;
    public int currentRound = 1;
    private bool roundInProgress = false;
    private int highestRound = 0;
    private GameObject playerInstance;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        highestRound = PlayerPrefs.GetInt("HighestRound", 1);
        UpdateRoundUI();
        SpawnPlayer();
        HideGameOverUI();
        restartButton.onClick.AddListener(RestartGame);

        StartCoroutine(StartRound());
    }

    void SpawnPlayer()
    {
        if (playerPrefab != null)
        {
            playerInstance = Instantiate(playerPrefab, Vector3.zero, Quaternion.identity);
            PlayerHealth playerHealth = playerInstance.GetComponent<PlayerHealth>();
            PlayerFiring playerFiring = playerInstance.GetComponent<PlayerFiring>();

            if (playerHealth != null)
            {
                playerHealth.maxHealth = 20;
                playerHealth.currentHealth = 20;
                playerHealth.SetHealthTextUI(playerHealthText);
            }
            if (playerFiring != null)
            {
                playerFiring.fireRate = 1.0f;
                playerFiring.damage = 1;
            }
        }
    }

    IEnumerator StartRound()
    {
        HideRoundUI();
        roundInProgress = true;
        UpdateRoundUI();
        ShowRoundUI();

        yield return new WaitForSeconds(5.0f);

        enemySpawner.SpawnEnemiesForRound(currentRound, spaceFighterLeaderData, spaceFighterData, scoutData);
        yield return new WaitUntil(() => GameObject.FindGameObjectsWithTag("Enemy").Length == 0);

        roundInProgress = false;
        currentRound++;
        StartCoroutine(StartRound());
    }

    public void OnEnemyDestroyed()
    {
        if (!roundInProgress && GameObject.FindGameObjectsWithTag("Enemy").Length == 0)
        {
            StartCoroutine(StartRound());
        }
    }

    private void UpdateRoundUI()
    {
        currentRoundText.text = $"Current Round: {currentRound}";
        if (currentRound > highestRound)
        {
            highestRound = currentRound;
            PlayerPrefs.SetInt("HighestRound", highestRound);
        }
        highestRoundText.text = $"Highest Round: {highestRound}";
    }

    private void ShowRoundUI()
    {
        currentRoundText.gameObject.SetActive(true);
        highestRoundText.gameObject.SetActive(true);
    }

    private void HideRoundUI()
    {
        currentRoundText.gameObject.SetActive(false);
        highestRoundText.gameObject.SetActive(false);
    }

    public void DisplayBuffMessage(string message)
    {
        if (buffMessageText != null)
        {
            buffMessageText.text = message;
            StartCoroutine(ClearBuffMessageAfterDelay());
        }
    }

    IEnumerator ClearBuffMessageAfterDelay()
    {
        yield return new WaitForSeconds(2.0f);
        buffMessageText.text = "";
    }

    public void GameOver()
    {
        StopAllCoroutines();
        DestroyAllEnemies();
        ShowGameOverUI();
    }

    public void RestartGame()
    {
        HideGameOverUI();
        currentRound = 1;
        UpdateRoundUI();
        if (playerInstance != null)
        {
            Destroy(playerInstance);
        }
        DestroyAllEnemies();
        enemySpawner.ResetDiceRollCount();
        SpawnPlayer();
        StartCoroutine(StartRound());
    }

    private void ShowGameOverUI()
    {
        gameOverText.text = $"Game Over\nCurrent Round: {currentRound}\nHighest Round: {highestRound}";
        gameOverText.gameObject.SetActive(true);
        restartButton.gameObject.SetActive(true);
    }

    private void HideGameOverUI()
    {
        gameOverText.gameObject.SetActive(false);
        restartButton.gameObject.SetActive(false);
    }

    private void DestroyAllEnemies()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject enemy in enemies)
        {
            Destroy(enemy);
        }
    }
}
