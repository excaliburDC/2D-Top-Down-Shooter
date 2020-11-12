using System;
using System.Collections;
using System.Collections.Generic;
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

    private MobileControls mb;
    private Health playerHealth;
    private FlashColor flashColor;
    private Camera cam;
    private Vector2 movePos;
    private Transform spawnPoint;

    private Vector2 screenBounds;
    private float objWidth;
    private float objHeight;


    // Start is called before the first frame update
    void Awake()
    {
        mb = GetComponent<MobileControls>();
        playerHealth = GetComponent<Health>();
        flashColor = GetComponent<FlashColor>();
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
        FireBullets();
        
    }

    private void LateUpdate()
    {
        BoundSpaceship();
    }



    private void ChoosePlatformForControls()
    {
//#if UNITY_STANDALONE || UNITY_WEBPLAYER
         StandaloneInput();  
//#elif UNITY_IOS || UNITY_ANDROID
//        TouchMovement();
//#endif
    }

    private void StandaloneInput()
    {
        this.transform.Translate(Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime,
           Input.GetAxis("Vertical") * moveSpeed * Time.deltaTime, 0);
    }

    /// <summary>
    /// Function to implement touch movements
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


        transform.position = Vector2.Lerp(transform.position, movePos * 2f, moveSpeed * Time.deltaTime);

        
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
    private void FireBullets()
    {
        if (fireCountdown < 0f)
        {

            PoolManager.Instance.SpawnInWorld("PlayerBullet", spawnPoint.position, spawnPoint.rotation);
            fireCountdown = 1f / fireRate;
        }

        fireCountdown -= Time.deltaTime;

        
    }

    public void DestroyShip(int damage)
    {
        playerHealth.ReduceHealth(damage);

        //AudioManager.Instance.PlaySFX(collisionSound);

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
            gameObject.SetActive(false);
            playerHealth.Value = initialHealth;

        }

    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.tag == "EnemyBullet")
        {
            EnemyBullet bullet = col.GetComponent<EnemyBullet>();

            DestroyShip(bullet.bulletDamage);
        }
    }
}
