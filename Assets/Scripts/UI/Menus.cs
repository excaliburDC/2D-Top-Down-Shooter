using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Menus : MonoBehaviour
{
    public Selectable m_startSelectable;

    public delegate void ActivateMenuDelegate();
    public delegate void CloseMenuDelegate();

    public event ActivateMenuDelegate onActivateMenu;
    public event CloseMenuDelegate onCloseMenu;

    private Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    private void Start()
    {
        if (m_startSelectable)
        {
            EventSystem.current.SetSelectedGameObject(m_startSelectable.gameObject);
        }
    }
    /// <summary>
    /// Activates the Menu (This method can be overriden to add more functionality)
    /// </summary>
    public virtual void ActivateMenu()
    {
        if (onActivateMenu != null)
        {
            //fire activate menu event
        }
        gameObject.SetActive(true);
        HandleAnimator("MenuShow");
    }

    /// <summary>
    /// Deactivates the Menu (This method can be overriden to add more functionality)
    /// </summary>
    public virtual void CloseMenu()
    {
        if (onCloseMenu != null)
        {
            //fire close menu event
        }
        gameObject.SetActive(false);
        HandleAnimator("MenuHide");
    }

    /// <summary>
    /// Handles the animation for UI
    /// </summary>
    /// <param name="animTrigger">string value to activate the specified trigger</param>
    private void HandleAnimator(string animTrigger)
    {
        if (anim)
        {
            anim.SetTrigger(animTrigger);
        }
    }

}
