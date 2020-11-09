using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Purse : MonoBehaviour
{
    public int currentCash = 1000;
    public int finalTowerHealth = 300;

    public TextMeshProUGUI purseText;
    public TextMeshProUGUI finalHealthText;

    public EnemyManager things;
    public int enemyFullCounter;

    //Sound stuff
    private AudioSource audioSource;
    public AudioClip clipPew;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        SetCash();
        SetHealth();
        // Obtain total enemies available from EnemyManager
        things = GameObject.Find("EnemyManager").GetComponent<EnemyManager>();
        foreach (Group group in things.enemyWave.enemyGroups)
        {
            enemyFullCounter += group.amountOfEnemies;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void SetCash()
    {
      purseText.text = $"${currentCash}";
    }

    public void AddCash(int amountOfCash)
    {
      currentCash += amountOfCash;
      SetCash();
    }

    public bool PlaceTower(int amountOfCashRequired)
    {
      if (currentCash - amountOfCashRequired >= 0)  // Do I have enough cash?
      {
        currentCash -= amountOfCashRequired; //Update Purse Amount
        SetCash();  // Update GUI
        return true;  // Yea!! Tower can be added
      }

      return false;  //Not enough ... we broke
    }
    // Final Tower Health functions 
    public void SetHealth()
    {
        finalHealthText.text = "Health: " + finalTowerHealth.ToString();
    }
    public void updateHealth(int amountOfDamage)
    {
        finalTowerHealth = finalTowerHealth - amountOfDamage;
        SetHealth();
        if (finalTowerHealth <= 0)
        {
            SceneManager.LoadScene("Restart");
        }
    }

    // Decrement total enemy counter
    public void enemyDecrementer()
    {
        PlaySound();
        enemyFullCounter--;
        //Debug.Log(enemyFullCounter);
        if (enemyFullCounter <= 0)
        {
            // Load appropriate scene
            SceneManager.LoadScene("Restart");
        }
    }
    // Play sound
    public void PlaySound()
    {
        audioSource.PlayOneShot(clipPew);
    }
}
