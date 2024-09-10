using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;


public class GroundEnemy : BaseEnemy_1
{
    private State currentState;

    public GameObject EnemyHealthBar;
    private static Image EnemyHealthBarImage;
    public float health = 100.0f;

    public AudioSource myA;
    public AudioClip dead;

    public GameObject ghost;
    public static float ghostKills;
    public float speed = 5.0f;
    public float chaseDistance = 10.0f;

    public override void Start()
    {
        base.Start();

        SetState(new PatrolState(this));

        if (EnemyHealthBar != null)
        {
            EnemyHealthBarImage = EnemyHealthBar.transform.GetComponent<Image>();
        }

        SetEnemyHealthBarValue(health);

        myA = GetComponent<AudioSource>();
    }
    void Update()
    {
        if (PauseMenu.GameIsPaused == false)
        {
           if (health == 0)
            {
                StartCoroutine(PlayAndDestroy(myA.clip.length));
            }

            SetEnemyHealthBarValue(health / 100);

            HandleUpdate();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Psychic"))
        {
            if (health > 25)
             {
                health -= 25;
             }
             else
             {
                 if(gameObject != null) {
                    health -= health;
                    ghostKills++;
                    StartCoroutine(PlayAndDestroy(myA.clip.length));
                 }
             }
            //myA.clip = dead;
        }
        else if (other.gameObject.CompareTag("Fire"))
        {
            if (health > 50)
            {
                health -= 50.0f;
            }
            else
            {
                if(gameObject != null) {
                    health -= health;
                    ghostKills++;
                    StartCoroutine(PlayAndDestroy(myA.clip.length));
                }
            }

            myA.clip = dead;
        }
        
    }
    
    public static void SetEnemyHealthBarValue(float value)
    {
        EnemyHealthBarImage.fillAmount = value;
    }

    public static float GetEnemyHealthBarValue()
    {
        
        return EnemyHealthBarImage.fillAmount;
    }
    
    public override void MoveToPosition(Vector3 position)
    {
        base.Start();
        SetState(new PatrolState(this));
    }

    public override void HandleUpdate()
    {
        currentState.HandleUpdate();
    }

    public void SetState(State newState)
    {
        currentState?.OnExit();
        currentState = newState;
        currentState.OnEnter();
    }


}