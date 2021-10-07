using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSettings : MonoBehaviour
{
    [SerializeField] private float gameSpeed = 1f;
    public static float GameSpeed => Instance.gameSpeed;
    
    #region singleton
    public static GameSettings Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType(typeof(GameSettings)) as GameSettings;
            }
            return instance;
        }
        set
        {
            instance = value;
        }
    }
    
    private static GameSettings instance;
    #endregion
}
