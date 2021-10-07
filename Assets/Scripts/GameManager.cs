using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject birdPrefab;
    [SerializeField] private Vector3 birdPosition;
    public static event Action OnGameStart;
    public static event Action OnGameOver;
    public static event Action OnGameRestart;
    public static event Action<int> OnScoreChanged;

    private int score;
    public int Score => score;
    public int Best
    {
        get
        {
            if (PlayerPrefs.GetInt("Best", -1) <= 0)
                PlayerPrefs.SetInt("Best", 0);
            return PlayerPrefs.GetInt("Best");
        }
        private set
        {
            PlayerPrefs.SetInt("Best", value);
        }
    }

    private void Awake()
    {
        Instantiate(birdPrefab, birdPosition, Quaternion.identity);
    }

    public void AddScore()
    {
        score++;
        OnScoreChanged?.Invoke(score);
    }

    public void Begin()
    {
        score = 0;
        OnGameStart?.Invoke();
    }

    public void Lose()
    {
        if (score > Best)
        {
            Best = score;
        }
        OnGameOver?.Invoke();
    }

    public void Restart()
    {
        DestroyObjects();
        OnGameRestart?.Invoke();
        Instantiate(birdPrefab, birdPosition, Quaternion.identity);
    }

    private void DestroyObjects()
    {
        Moving[] movings = FindObjectsOfType<Moving>();
        foreach (Moving moving in movings)
        {
            if (moving.tag == "Pipe" || moving.tag == "Score")
            {
                moving.gameObject.SetActive(false);
                Destroy(moving.gameObject);
            }
        }
        GameObject bird = FindObjectOfType<Bird>().gameObject;
        bird.SetActive(false);
        Destroy(bird);
    }

    #region singleton
    public static GameManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType(typeof(GameManager)) as GameManager;
            }
            return instance;
        }
        set
        {
            instance = value;
        }
    }

    private static GameManager instance;
    #endregion
}
