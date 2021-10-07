using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moving : MonoBehaviour
{
    private bool moving;

    private void Awake()
    {
        Move();
        GameManager.OnGameOver += Stop;
        GameManager.OnGameRestart += Move;
    }

    public void Move()
    {
        moving = true;
    }

    public void Stop()
    {
        moving = false;
    }

    private void Update()
    {
        if (moving)
        {
            transform.position += Vector3.left * GameSettings.GameSpeed * Time.deltaTime;
        }
    }
}
