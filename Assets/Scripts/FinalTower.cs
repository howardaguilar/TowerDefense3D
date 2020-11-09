using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalTower : MonoBehaviour
{
    private Purse myPurse;
    //private FinalTower thisTower;
    public int currentHealth = 300;
    // Start is called before the first frame update
    void Start()
    {
        myPurse = GameObject.FindGameObjectWithTag("Purse").GetComponent<Purse>();
        //thisTower = GameObject.Find("FinalTower").GetComponent<FinalTower>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Decrement total enemies and have Final Tower take damage
    private void OnTriggerEnter(Collider collision)
    {
        //Debug.Log("Hello!");
        //Debug.Log(collision.gameObject.tag);
        if (collision.gameObject.tag == "Enemy")
        {
            myPurse.enemyDecrementer();
            myPurse.updateHealth(100);
            Destroy(collision.gameObject);
            // Destroy self if 0 health
            currentHealth = currentHealth - 100;
            if (currentHealth <= 0)
            {
                Destroy(gameObject);
            }
        }
        else if (collision.gameObject.tag == "HardEnemy")
        {
            myPurse.enemyDecrementer();
            myPurse.updateHealth(40);
            Destroy(collision.gameObject);
        }
    }
    
}
