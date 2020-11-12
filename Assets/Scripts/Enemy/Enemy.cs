
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]

public class Enemy : MonoBehaviour
{
    public GameObject explosionPrefab;

    [SerializeField] protected float moveSpeed;
    [SerializeField] private int pointsValue; //how many points the enemy is worth
    [SerializeField] private int damage; //damage that will be dealt to player



    public int Damage { get => damage;  private set => damage = value; }
    public int PointsValue { get => pointsValue; private set => pointsValue = value; }

    protected Rigidbody2D rb2d;
     

    
    protected virtual void OnEnable()
    {
        rb2d = GetComponent<Rigidbody2D>();
        
    }

    
    


}
