﻿
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Health))]
[RequireComponent(typeof(FlashColor))]
public class PlayerController : MonoBehaviour
{
    [Header("Public variables ")]
    [SerializeField] private float moveSpeed = 2f;
    [SerializeField] private float fireRate = 1f;
    [SerializeField] private GameObject shipExplosionEffect;
    


    
    private float fireCountdown;
    private int initialHealth;

    private Rigidbody2D rb2d;
    private MobileControls mb;
    private Health playerHealth;
    private FlashColor flashColor;
    private PlayerShield playerShield;

    private Camera cam;
    //setting the movePos with a bit of offset so that it doesn't go to center of screen when game starts
    private Vector2 movePos = new Vector2(0f,-3f); 
    private Transform spawnPoint;

    private Vector2 screenBounds;
    private float objWidth;
    private float objHeight;


    // Start is called before the first frame update
    void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
        mb = GetComponent<MobileControls>();
        playerHealth = GetComponent<Health>();
        flashColor = GetComponent<FlashColor>();
        playerShield = GetComponent<PlayerShield>();

        spawnPoint = gameObject.transform.GetChild(0); //Spawnpoint is the first child object of the current gameobject
        cam = Camera.main;
    }

    private void Start()
    {
        screenBounds = cam.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, cam.transform.position.z));
        objWidth = transform.GetComponent<BoxCollider2D>().bounds.extents.x; //extents = size of width / 2
        objHeight = transform.GetComponent<BoxCollider2D>().bounds.extents.y; //extents = size of height / 2

        initialHealth = playerHealth.Value;
    }

    // Update is called once per frame
    void Update()
    {
        ChoosePlatformForControls();

        StartCoroutine(FireBullets());
        
        
        

    }

    private void LateUpdate()
    {
        BoundSpaceship();
    }

    /// <summary>
    /// Fucntion to choose setup controls for different platforms
    /// </summary>
    private void ChoosePlatformForControls()
    {
#if UNITY_STANDALONE || UNITY_WEBPLAYER
         StandaloneInput();  
#elif UNITY_IOS || UNITY_ANDROID
        TouchMovement();
#endif
    }

    /// <summary>
    ///Windows/Linux/Webplayer input
    /// </summary>
    private void StandaloneInput()
    {
        this.transform.Translate(Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime,
           Input.GetAxis("Vertical") * moveSpeed * Time.deltaTime, 0);
    }

    /// <summary>
    /// Function to implement touch movements for IOS and Android
    /// </summary>
    private void TouchMovement()
    {
        if (mb.swipeLeft)
            movePos += Vector2.left;

        if (mb.swipeRight)
            movePos += Vector2.right;

        if (mb.swipeUp)
            movePos += Vector2.up;

        if (mb.swipeDown)
            movePos += Vector2.down;


        transform.position = Vector2.Lerp(transform.position, movePos, moveSpeed * Time.deltaTime);




    }

    /// <summary>
    /// Prevents the spaceship from going offscreen 
    /// </summary>
    private void BoundSpaceship()
    {

        Vector3 viewPos = transform.position;
        viewPos.x = Mathf.Clamp(viewPos.x, screenBounds.x * -1 + objWidth, screenBounds.x - objWidth);
        viewPos.y = Mathf.Clamp(viewPos.y, screenBounds.y * -1 + objHeight, screenBounds.y - objHeight);
        transform.position = viewPos;
    }

    /// <summary>
    /// Function to fire bullets from ship's bullet spawnpoint
    /// </summary>
    private IEnumerator FireBullets()
    {
        yield return new WaitForSeconds(2f);

        if (fireCountdown < 0f)
        {

            PoolManager.Instance.SpawnInWorld("PlayerBullet", spawnPoint.position, spawnPoint.rotation);
            fireCountdown = 1f / fireRate;
        }

        fireCountdown -= Time.deltaTime;

        
    }
    /// <summary>
    /// Function to respawn player after losing life
    /// </summary>
    public void RespawnPlayer()
    {
        gameObject.transform.position = new Vector3(0f, -3f, 0f);
        gameObject.SetActive(true);
    }

    /// <summary>
    /// Function to Deal damage to player and destroy it once it's health reaches 0
    /// </summary>
    public void DestroyShip(int damage)
    {
        if(!playerShield.IsInvincible)
        {
            playerHealth.ReduceHealth(damage);


            // check health
            if (playerHealth.Value > 0)
            {
                flashColor.Flash();
            }
            else
            {
                // particles
                GameObject particles = Instantiate(shipExplosionEffect, transform.position, Quaternion.identity);
                Destroy(particles, 0.5f);
                GameController.Instance.OnLifeLost();
                gameObject.SetActive(false);
                playerHealth.Value = initialHealth;

                if (!GameController.Instance.IsDead)
                {
                    Invoke("RespawnPlayer", 2f);
                }
            }
        }

        else
        {
            Debug.Log("Shield Activated");
        }
        

    }

    /// <summary>
    /// Checks Trigger Collsion
    /// </summary>
    private void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.tag == "EnemyBullet")
        {
            EnemyBullet bullet = col.GetComponent<EnemyBullet>();

            DestroyShip(bullet.bulletDamage);

            col.gameObject.SetActive(false);
        }

        if(col.gameObject.tag == "Shield")
        {
            col.gameObject.SetActive(false);

            playerShield.ActivateShield();
        }


    }
}
