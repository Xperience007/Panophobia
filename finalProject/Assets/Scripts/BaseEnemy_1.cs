using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;



public abstract class BaseEnemy_1 : MonoBehaviour
{
    //public GameObject EnemyHealthBar;
    //private static Image EnemyHealthBarImage;
    public static float health = 100.0f;
    Animator animator;
    AudioSource myA2;
    public Transform[] waypoints;
    protected int waypointIndex = 0;
    public float waypointTolerance = 1.0f;
    public float patrolSpeed = 5.0f;
    public float chaseSpeed = 5.0f;
    public GameObject player;
    public NavMeshAgent agent;
    public float viewDistance = 20f;
    [SerializeField]
    private float maxChaseDistance = 30f;

    public virtual void Start()
    {
        // EnemyHealthBarImage = EnemyHealthBar.GetComponent<Image>();
        //UpdateHealthBar();
        player = GameObject.FindGameObjectWithTag("Player");
        agent = GetComponent<NavMeshAgent>(); // Assign the NavMeshAgent component once here.
        animator = GetComponent<Animator>();
        myA2 = GetComponent<AudioSource>();
        SetNextDestination();
        /* if (EnemyHealthBar != null)
         {
             EnemyHealthBarImage = EnemyHealthBar.transform.GetComponent<Image>();
         }

         SetEnemyHealthBarValue(health);
        */
    }
    private void Update()
    {
        /*if (health == 0)
        {
            StartCoroutine(PlayAndDestroy(myAudio.clip.length));
        }

       SetEnemyHealthBarValue(health / 100);
       UpdateHealthBar();*/
    }
    public void MoveToNextWaypoint()
    {
        if (waypoints.Length == 0) return;
        Transform targetWaypoint = waypoints[waypointIndex];
        float distance = Vector3.Distance(transform.position, targetWaypoint.position);
        if (distance < waypointTolerance)
        {
            waypointIndex = (waypointIndex + 1) % waypoints.Length;
        }
        agent.SetDestination(waypoints[waypointIndex].position);
    }

    public bool IsPlayerTooFar()
    {
        return Vector3.Distance(transform.position, player.transform.position) > maxChaseDistance;
    }

    /*public void SetNextDestination()
    {
        if (waypoints.Length == 0) return;
        agent.SetDestination(waypoints[waypointIndex].position);
        waypointIndex = (waypointIndex + 1) % waypoints.Length;
    }*/

    public void SetNextDestination()
    {
        if (waypoints == null || waypoints.Length == 0)
        {
            Debug.LogWarning("No waypoints assigned!");
            return;
        }
        agent.SetDestination(waypoints[waypointIndex].position);
        waypointIndex = (waypointIndex + 1) % waypoints.Length;
    }

    public abstract void MoveToPosition(Vector3 position);

    public bool CanSeePlayer()
    {
        return Vector3.Distance(transform.position, player.transform.position) < viewDistance;
    }

    public abstract void HandleUpdate();
    public void ChasePlayer()
    {
        agent.SetDestination(player.transform.position);
    }
    public Vector3 PlayerPosition()
    {
        return player.transform.position;
    }
    public void ResetWaypointIndex()
    {
        waypointIndex = 0;
    }
    /*public void TakeDamage(float damage)
    {
        health -= damage;
        health = Mathf.Max(health, 0); // Prevent health from going below zero
        UpdateHealthBar();
    }

      private void UpdateHealthBar()
    {
        EnemyHealthBarImage.fillAmount = health / 100.0f;
    }

    public static void SetEnemyHealthBarValue(float value)
    {
        EnemyHealthBarImage.fillAmount = value;
    }

     public static float GetEnemyHealthBarValue()
     {
         return EnemyHealthBarImage.fillAmount;
     }*/
    void OnTriggerEnter(Collider other)
    {
        //Put player magic tags and hits to health here

        /*if (other.gameObject.CompareTag("Player"))
        {

            // Disable all Renderers and Colliders
            Renderer[] allRenderers = gameObject.GetComponentsInChildren<Renderer>();
            foreach (Renderer c in allRenderers) c.enabled = false;
            Collider[] allColliders = gameObject.GetComponentsInChildren<Collider>();
            foreach (Collider c in allColliders) c.enabled = false;

            StartCoroutine(PlayAndDestroy(myAudio.clip.length));
        }*/
        /*if (other.gameObject.CompareTag("psychic"))
        {
            TakeDamage(10); // psychic shot damage
        }
        else if (other.gameObject.CompareTag("Fire"))
        {
            TakeDamage(25); // Fireball damage
        }*/
    }


   public IEnumerator PlayAndDestroy(float waitTime)
    {
        myA2.Play();
        yield return new WaitForSeconds(waitTime);
        Destroy(gameObject);
    }
}