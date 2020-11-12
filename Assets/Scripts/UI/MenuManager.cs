using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MenuManager : SingletonManager<MenuManager>
{
   

    public Menus m_startMenu;
    public Menus m_Hud;
   

    [SerializeField] private Menus m_pauseMenu;

    [SerializeField] private Menus m_gameOverMenu;

    [SerializeField] private TMP_Text scoreText;
    

    [SerializeField]
    private Component[] menus = new Component[0];


    private Menus previousMenu;
    public Menus PreviousMenu { get { return previousMenu; } }

    private Menus currentMenu;
    public Menus CurrentMenu { get { return currentMenu; } }

    private void OnEnable()
    {

        //the bool parameter decides whether to include inactive gameobjects or not
        menus = GetComponentsInChildren<Menus>(true);

        DontDestroyOnLoad(this.gameObject);
    }

    private void Start()
    {
        if (m_startMenu)
        {
            SwitchMenus(m_startMenu);
        }

    
            

    }


    private void Update()
    {
        //for testing purpose only
        if(currentMenu==m_Hud && Input.GetKeyDown(KeyCode.Escape))
        {
            PauseMenu();
        }

        
       
    }

   
    /// <summary>
    /// Switches to specified menu
    /// </summary>
    /// <param name="menu">Menu to switch to</param>
    public void SwitchMenus(Menus menu)
    {
        if (menu)
        {
            if (currentMenu)
            {
                currentMenu.CloseMenu();
                previousMenu = currentMenu;
            }
            currentMenu = menu;
            currentMenu.gameObject.SetActive(true);
            currentMenu.ActivateMenu();



        }
    }

    
    /// <summary>
    /// Switch back to previous menu
    /// </summary>
    public void SwitchToPreviousMenu()
    {
        if (previousMenu)
        {
            SwitchMenus(previousMenu);
        }
    }

    /// <summary>
    /// Switch to HUD menu
    /// </summary>
    public void SwitchToHUD()
    {
        if(m_Hud)
        {
            currentMenu.gameObject.SetActive(false);
            SwitchMenus(m_Hud);
            Time.timeScale = 1f;
        
        }
    }

    /// <summary>
    /// Function to pause the game and enter Pause menu
    /// </summary>
    public void PauseMenu()
    {
        if(m_pauseMenu)
        {
            Time.timeScale = 0f;
            //AudioManager.Instance.Pause("MenuSound");
            SwitchMenus(m_pauseMenu);
            //currentMenu.gameObject.SetActive(false);
            
        }
       
    }

    /// <summary>
    /// Function to switch back to Main menu
    /// </summary>
    public void GoToMainMenu()
    {
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex-1);
        Destroy(this.gameObject);
        Time.timeScale = 1f;
    }
    
    /// <summary>
    /// Restarts the Game
    /// </summary>
    public void RestartGame()
    {
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex);
        SwitchToHUD();

    }

    /// <summary>
    /// Called when the player loses all lives and checks if the player has a new high score
    /// </summary>
    /// <param name="currentScore">Current score of player</param>
    public void GameOver(int currentScore)
    {
        int hiScore = PlayerPrefs.GetInt("HiScore");


        if (currentScore > hiScore)
        {
            hiScore = currentScore;

            scoreText.text = "NEW HIGH SCORE:" + hiScore.ToString();

            PlayerPrefs.SetInt("HiScore", hiScore);
        }

        else
        {
            scoreText.text = "YOUR SCORE:" + currentScore.ToString();
        }

        if (m_Hud)
        {
            m_Hud.gameObject.SetActive(false);
        }

        if(m_gameOverMenu)
        {
            Time.timeScale = 0f;
            SwitchMenus(m_gameOverMenu);
        
        }
    }

 

    public void QuitGame()
    {
        #if UNITY_EDITOR
            // Application.Quit() does not work in the editor so
            // UnityEditor.EditorApplication.isPlaying need to be set to false to end the game
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }

   /// <summary>
   /// Loads the Game Scene
   /// </summary>
   /// <param name="index">Index of the Scene to load</param>
    public void LoadGame(int index)
    {
        SceneManager.LoadSceneAsync(index);
        if(m_Hud)
        {
            SwitchToHUD();
        }

    }


}
