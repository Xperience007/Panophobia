using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectilePrefab : MonoBehaviour
{
    private bool collided;
    public GameObject impact;

    void OnCollisionEnter(Collision col) {
        if(col.gameObject.tag != "Psychic" && col.gameObject.tag != "Player" && !collided) {
            collided = true;

            var imp = Instantiate(impact, col.contacts[0].point, Quaternion.identity) as GameObject;

            Destroy(imp, 2);

            Destroy(gameObject);
        }
    }
}
