
using UnityEngine;

public class SingletonManager<T> : MonoBehaviour where T: MonoBehaviour
{
    private static T instance;

    public static T Instance
    {
        get 
        {
            if(instance==null)
            {
                instance = GameObject.FindObjectOfType<T>();
                
            }

            return instance;
        }
    }

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        
    }

}
