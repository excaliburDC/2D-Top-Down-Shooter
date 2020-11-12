
using UnityEngine;

public class Bullet : MonoBehaviour,IPooledObject
{
    public int bulletDamage;
    [SerializeField] private float bulletSpeed = 4f;
    
    private Rigidbody2D rb2d; 

    void OnEnable()
    {
        rb2d = GetComponent<Rigidbody2D>(); 
    }

    public void OnObjectSpawner()
    {

        rb2d.velocity = transform.up * bulletSpeed;

        
    }

   

    
}
