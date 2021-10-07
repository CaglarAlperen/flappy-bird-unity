using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject tapToPlay;
    [SerializeField] private GameObject gameplay;
    [SerializeField] private GameObject gameOver;
    [SerializeField] private TextMeshProUGUI inGameScoreText;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI bestText;

    private void Awake()
    {
        GameManager.OnGameStart += GameStart;
        GameManager.OnGameOver += GameOver;
        GameManager.OnScoreChanged += UpdateScore;
    }

    private void GameStart()
    {
        tapToPlay.SetActive(false);
        gameplay.SetActive(true);
        inGameScoreText.text = 0.ToString();
    }

    private void GameOver()
    {
        gameOver.SetActive(true);
        gameplay.SetActive(false);
        scoreText.text = GameManager.Instance.Score.ToString();
        bestText.text = GameManager.Instance.Best.ToString();
    }

    private void UpdateScore(int score)
    {
        inGameScoreText.text = score.ToString();
    }

    public void RestartGame()
    {
        gameOver.SetActive(false);
        tapToPlay.SetActive(true);
        GameManager.Instance.Restart();
    }
}
