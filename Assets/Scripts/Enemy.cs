using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class Enemy : MonoBehaviour
{
  private Waypoints[] navPoints;
  private Transform target;
  private Vector3 direction;
  public float amplify = 1;
  private int index = 0;
  private bool move = true;
  private Purse purse;
  public int currentHealth = 100;
  private int startingHealth;
  public int cashPoints = 100;
  private HealthBar healthBar;

  public UnityEvent DeathEvent;

  // Start is called before the first frame update
  public void StartEnemy(Waypoints[] navigationalPath)
  {
        /*
        things = GameObject.Find("EnemyManager").GetComponent<EnemyManager>();
        foreach (Group group in things.enemyWave.enemyGroups)
        {
            enemyFullCounter += group.amountOfEnemies;
        }*/
        //Debug.Log(enemyFullCounter);

        navPoints = navigationalPath;
    purse = GameObject.FindGameObjectWithTag("Purse").GetComponent<Purse>();
    healthBar = GetComponentInChildren<HealthBar>();
    startingHealth = currentHealth;
    //Place our enemy at the start point
    transform.position = navPoints[index].transform.position;
    NextWaypoint();
    
    //Move towards the next waypoint
    //Retarget to the following waypoint when we reach our current waypoint
    //Repeat through all of the waypoints until you reach the end
  }

  // Update is called once per frame
  void Update()
  {
    if (move)
    {
      transform.Translate(direction.normalized * Time.deltaTime * amplify);

      if ((transform.position - target.position).magnitude < .1f)
      {
        NextWaypoint();
      }
    }

  }

  private void NextWaypoint()
  {
    if (index < navPoints.Length - 1)
    {
      index += 1;
      target = navPoints[index].transform;
      direction = target.position - transform.position;
    }
    else
    {
      move = false;
    }
  }
    // Insert enemyDecrementer here
  public void TakeDamage(int amountDamage)
  {
    currentHealth -= amountDamage;
    if (currentHealth <= 0)
    {
            purse.enemyDecrementer();
      purse.AddCash(cashPoints); //add cash to purse
      DeathEvent.Invoke();    ///notify towers that I am killed
      Destroy(this.gameObject); //Get rid of object
            //enemyFullCounter--;
            //Debug.Log(enemyFullCounter);
        //if (enemyFullCounter <= 0)
           // {
            //    SceneManager.LoadScene("Restart");
           // }
    }
    else
    {
      healthBar.Damage(currentHealth, startingHealth);
    }
  }



}
