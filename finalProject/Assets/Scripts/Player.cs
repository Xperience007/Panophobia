using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class Player : MonoBehaviour
{
    //Health bar variables
    public GameObject HealthBar;
    private static Image HealthBarImage;
    public static float health = 100.0f;
    [SerializeField]
    public float enemyContactDamage = 10;

    //Magic bar variables
    public GameObject MagicBar;
    private static Image MagicImage;
    public static float magic = 100.0f;

    //Regen speed
    public float regenSpeed = 1;

    // Start is called before the first frame update
    void Start()
    {
        if (HealthBar != null)
        {
            HealthBarImage = HealthBar.transform.GetComponent<Image>();
        }

        SetHealthBarValue(health);

        if (MagicBar != null)
        {
            MagicImage = MagicBar.transform.GetComponent<Image>();
        }

        SetMagicBarValue(magic);
    }

    // Update is called once per frame
    void Update()
    {
        if (PauseMenu.GameIsPaused == false)
        {
            if (health == 0)
            {
                SceneManager.LoadScene("GameOver");
            }

            if (magic < 100)
            {
                magic += 0.1f * regenSpeed;
            }

            SetHealthBarValue(health / 100);

            SetMagicBarValue(magic / 100);

            if(FlyingEnemy.birdKills == 5) {
                SceneManager.LoadScene("YouWin");
            }
        }
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        health = Mathf.Max(health, 0); // Prevent health from dropping below zero
        SetHealthBarValue(health / 100.0f);
    }

    public static void SetHealthBarValue(float value)
    {
        HealthBarImage.fillAmount = value;
    }

    public static float GetHealthBarValue()
    {
        return HealthBarImage.fillAmount;
    }

    public static void SetMagicBarValue(float value)
    {
        MagicImage.fillAmount = value;
    }

    public static float GetMagicBarValue()
    {
        return MagicImage.fillAmount;
    }

    void OnTriggerEnter(Collider other)
    {
        //Put enemy tags and hits to health here
        if (other.CompareTag("Ghost"))
        {
            TakeDamage(enemyContactDamage); // Assumes enemy contact causes 20 points of damage
        }

        if (other.CompareTag("Enemy"))
        {
            TakeDamage(enemyContactDamage); // Assumes enemy contact causes 20 points of damage
        }

        if (other.CompareTag("death point"))
        {
            health = 0;
        }
    }

    void OnTriggerStay(Collider other)
    {
        //Put enemy tags and hits to health here
        if (other.CompareTag("Ghost"))
        {
            Wait(5);
            TakeDamage(enemyContactDamage); // Assumes enemy contact causes 20 points of damage
        }
        if (other.CompareTag("death point"))
        {
            health = 0;
        }


    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("bounding"))
        {
            health = 0;
        }
    }

    private IEnumerator Wait(int seconds) {
        yield return new WaitForSeconds(seconds);
    }
}
