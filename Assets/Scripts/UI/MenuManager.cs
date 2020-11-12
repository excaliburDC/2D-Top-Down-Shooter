using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MenuManager : SingletonManager<MenuManager>
{
   

    public Menus m_startMenu;
    public Menus m_Hud;


    //keeps track of the level we are currently in
    //use it to check which animations to call in which level
    public static int levelIndex;
    public static bool isLevelComplete; //assign it to true when level is complete
   

    [SerializeField] private Menus m_pauseMenu;

    [SerializeField] private Menus m_LevelCompleteMenu;

     
    

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
    }

    private void Start()
    {
        if (m_startMenu)
        {
            SwitchMenus(m_startMenu);
        }

        levelIndex = 0;
            

    }


    private void Update()
    {
        //for testing purpose only
        if(currentMenu==m_Hud && Input.GetKeyDown(KeyCode.Escape))
        {
            
            PauseMenu();
        }

        //test
        if(Input.GetKeyDown(KeyCode.Space))
        {
            isLevelComplete = true;
            LevelComplete();
        }
       
    }

   

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

    

    public void SwitchToPreviousMenu()
    {
        if (previousMenu)
        {
            SwitchMenus(previousMenu);
        }
    }

    public void SwitchToHUD()
    {
        if(m_Hud)
        {
            currentMenu.gameObject.SetActive(false);
            SwitchMenus(m_Hud);
            Time.timeScale = 1f;
        
        }
    }

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




    public void RestartLevel()
    {
        levelIndex = SceneManager.GetActiveScene().buildIndex;
        Debug.Log("Level" + levelIndex);
        isLevelComplete = false;
        SceneManager.LoadSceneAsync(levelIndex);
        SwitchToHUD();

    }

    //call this function after you set isLevelComplete to true
    public void LevelComplete()
    {
        isLevelComplete = true;
       // GameController.isKeyCollected = false;
        if (m_Hud)
        {
            m_Hud.gameObject.SetActive(false);
        }

        if(m_LevelCompleteMenu)
        {
            SwitchMenus(m_LevelCompleteMenu);
        
            
        }
    }

 
    //public void PlayButtonSound()
    //{
    //    AudioManager.Instance.Play("ButtonSound");
    //}

    

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

    //loads from the next level button in level complete menu
    public void LoadNextScene()
    {
        levelIndex = SceneManager.GetActiveScene().buildIndex + 1;
        Debug.Log("Level" + levelIndex);
        isLevelComplete = false;
        currentMenu.gameObject.SetActive(false);
        SceneManager.LoadSceneAsync(levelIndex);
        SwitchMenus(m_Hud);
        

       

    }

    //loads from level menu
    public void LoadLevel(int index)
    {
        levelIndex = index;
        Debug.Log("Level" + levelIndex);
        isLevelComplete = false;
        SceneManager.LoadSceneAsync(index);

      
    }


}
