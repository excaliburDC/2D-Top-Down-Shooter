
public class EnemyShipController : Enemy,IPooledObject
{
    
    protected override void OnEnable()
    {
        base.OnEnable();
       
    }



    public void OnObjectSpawner()
    {
        MoveEnemyShip();
        
    }
   
    /// <summary>
    /// Moves the enemy ship towards the screen
    /// </summary>
    private void MoveEnemyShip()
    {
        rb2d.velocity = -transform.up * moveSpeed;
    }

    

    
}
