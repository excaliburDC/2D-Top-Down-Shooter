using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : SingletonManager<GameController>
{

    [SerializeField] private int startingLives = 3;
    [SerializeField] private HUDManager hUD;

    private bool isDead = false;

    public int Lives { get; private set; }


    public int CurrentScore { get; private set; }
    public bool IsDead { get => isDead; set => isDead = value; }

    private int hiScore;

    private void Awake()
    {
        ResetGame();
    }



    public void UpdatePointsValue(int points)
    {
        CurrentScore += points;
        hUD.UpdateScore(CurrentScore);
    }

    public void OnLifeLost()
    {
        --Lives;
        if(Lives>=0)
        {
            hUD.UpdateLives(Lives);
        }

        else
        {
            if(CurrentScore>hiScore)
            {
                hiScore = CurrentScore;
                PlayerPrefs.SetInt("HiScore", hiScore);
            }
            
            IsDead = true;
        }
        
    }

    void ResetGame()
    {
        hUD.UpdateHiScore();
        Lives = startingLives;
        isDead = false;
    }

}
