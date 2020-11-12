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


    /// <summary>
    /// Updates the score 
    /// </summary>
    /// <param name="points">Score amount</param>
    public void UpdatePointsValue(int points)
    {
        CurrentScore += points;
        hUD.UpdateScore(CurrentScore);
    }

    /// <summary>
    /// Called when player loses life.
    /// Game is over when player loses all life
    /// </summary>
    public void OnLifeLost()
    {
        --Lives;
        if(Lives>=0)
        {
            hUD.UpdateLives(Lives);
        }

        else
        {
            
            IsDead = true;
            hUD.gameObject.SetActive(false);
            MenuManager.Instance.GameOver(CurrentScore);
        }
        
    }

    /// <summary>
    /// Resets the Game
    /// </summary>
    void ResetGame()
    {
        hUD.UpdateHiScore();
        Lives = startingLives;
        isDead = false;
    }

}
