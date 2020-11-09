using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;

public class Tower : MonoBehaviour
{
  public List<Enemy> currentEnemies;
  public Enemy currentTarget;

  public GameObject turret;
  private LineRenderer lineRenderer;

  void Start()
  {
    lineRenderer = GetComponent<LineRenderer>();
  }

  void OnDrawGizmos()
  {
    Gizmos.DrawWireSphere(transform.position, GetComponent<SphereCollider>().radius);
  }

  void Update()
  {
    if (currentTarget)
    {
      lineRenderer.SetPosition(0, turret.transform.position);
      lineRenderer.SetPosition(1, currentTarget.transform.position);
      // Have tower destroy
      //InvokeRepeating("DestroyEnemy", 1.0f, 1.0f);
        }
  }

  void OnTriggerEnter(Collider collider)
  {
    Enemy newEnemy = collider.GetComponent<Enemy>();
    currentEnemies.Add(newEnemy);
    
    currentEnemies[0].DeathEvent.AddListener(delegate { BookKeeping(newEnemy);});
        
    EvaluateTarget(newEnemy);

    //Debug.Log($"{collider.name} has entered");
  }

  void OnTriggerExit(Collider collider)
  {
    Enemy enemyLeaving = collider.GetComponent<Enemy>();

    //enemyLeaving.DeathEvent.RemoveListener(delegate { BookKeeping(enemyLeaving);}); //unsubscribing to the DeathEvent for this enemy .... don't care anymore :(

    currentEnemies.Remove(enemyLeaving);  // clean up book
    EvaluateTarget(enemyLeaving);

  }

  private void BookKeeping(Enemy enemy)
  {
    currentEnemies.Remove(enemy);
    EvaluateTarget(enemy);

  }


  private void EvaluateTarget(Enemy enemy)
  {
    if (currentTarget == enemy)
    {
      currentTarget = null;
      lineRenderer.enabled = false;
    }


    if (currentTarget == null && currentEnemies.Count > 0)
    {
      currentTarget = currentEnemies[0];
      lineRenderer.enabled = true;
    }
  }

    private void DestroyEnemy()
    {
        //currentTarget.GetComponent<Enemy>().TakeDamage(20);
    }
}
