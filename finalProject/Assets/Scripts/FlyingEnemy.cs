using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class FlyingEnemy : MonoBehaviour
{
    public Transform player;
    public float detectionRange = 15.0f;
    public float rotationSpeed = 5.0f;
    public float diveSpeed = 20.0f;
    public float resetDelay = 2.0f;
    public Vector3 patrolCenter;
    public float patrolRadius = 10.0f;
    public float patrolSpeed = 2.0f;
    private Vector3 initialPosition;
    private bool playerDetected = false;

    public AudioSource myA3;
    public AudioClip deadBird;

    public GameObject BirdHealthBar;
    private static Image BirdHealthBarImage;
    public float birdHealth = 100.0f;

    public static float birdKills;

    void Start()
    {
        initialPosition = transform.position;  // Store the initial position for reset purposes.

        if (BirdHealthBar != null)
        {
            BirdHealthBarImage = BirdHealthBar.transform.GetComponent<Image>();
        }

        SetEnemyHealthBarValue(birdHealth);

        myA3 = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (PauseMenu.GameIsPaused == false) {
            float distanceToPlayer = Vector3.Distance(player.position, transform.position);

            if (distanceToPlayer <= detectionRange)
            {
                playerDetected = true;
                RotateTowards(player.position);
            }
            else
            {
                playerDetected = false;
            }

            if (!playerDetected)
            {
                Patrol();
            }

            if (birdHealth == 0)
            {
                StartCoroutine(PlayAndDestroy(myA3.clip.length));
            }

            SetEnemyHealthBarValue(birdHealth / 100);
        }
    }

    void RotateTowards(Vector3 target)
    {
        Vector3 direction = (target - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        lookRotation *= Quaternion.Euler(0, 90, 0);

        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * rotationSpeed);

        // After rotating, if the enemy is facing the player, start dive bombing
        if (Quaternion.Angle(transform.rotation, lookRotation) < 5)
        {
            DiveBomb(target);
        }
    }

    void DiveBomb(Vector3 target)
    {
        transform.position = Vector3.MoveTowards(transform.position, target, Time.deltaTime * diveSpeed);
    }

    void OnTriggerEnter(Collider collision)
    {
       if (collision.gameObject.CompareTag("Psychic"))
        {
            if (birdHealth > 25)
             {
                birdHealth -= 25;
             }
             else
             {
                 if(gameObject != null) {
                    birdHealth -= birdHealth;
                    birdKills++;
                    StartCoroutine(PlayAndDestroy(myA3.clip.length));
                 }
             }
            //myA.clip = dead;
        }
        else if (collision.gameObject.CompareTag("Fire"))
        {
            if (birdHealth > 50)
            {
                birdHealth -= 50.0f;
            }
            else
            {
                if(gameObject != null) {
                    birdHealth -= birdHealth;
                    birdKills++;
                    StartCoroutine(PlayAndDestroy(myA3.clip.length));
                }
            }

            myA3.clip = deadBird;
        }
    }

    IEnumerator ResetPosition()
    {
        yield return new WaitForSeconds(resetDelay);
        if (Vector3.Distance(player.position, transform.position) <= detectionRange)
        {
            transform.position = initialPosition;
            playerDetected = true;  // Player is still in range, continue attacking.
        }
        else
        {
            playerDetected = false;  // Player is out of range, stop attacking.
        }
    }

    void Patrol()
    {
        float patrolX = Mathf.Sin(Time.time * patrolSpeed) * patrolRadius;
        float patrolZ = Mathf.Cos(Time.time * patrolSpeed) * patrolRadius;
        transform.position = new Vector3(patrolX, initialPosition.y, patrolZ) + patrolCenter;
    }

    public static void SetEnemyHealthBarValue(float value)
    {
        BirdHealthBarImage.fillAmount = value;
    }

    public static float GetEnemyHealthBarValue()
    {
        return BirdHealthBarImage.fillAmount;
    }

    private IEnumerator PlayAndDestroy(float waitTime) {
        myA3.Play();
        yield return new WaitForSeconds(waitTime);
        Destroy(gameObject);
    }
}

