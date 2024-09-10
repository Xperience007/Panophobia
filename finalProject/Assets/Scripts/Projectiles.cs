using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectiles : MonoBehaviour
{
    public Camera cam;
    public GameObject projectile;
    public GameObject projectileF;
    public Transform firePoint;
    public AudioClip fireClip;
    public AudioClip psychicClip;
    public AudioSource sound;
    public float projectileSpeed = 30;
    public float fireRate = 4;
    public float arcRange = 1;

    private Vector3 destination;
    private float timeToFire;
    private float timeToFireF;

    // Update is called once per frame
    void Update()
    {
        if(PauseMenu.GameIsPaused == false) {
            if(Input.GetButton("Fire1") && Time.time >= timeToFire) {
                timeToFire = Time.time + 1/fireRate;
                if(Player.magic >= 10) {
                    Player.magic -= 10;
                    ShootProjectile(projectile);
                    sound.clip = psychicClip;
                    sound.Play();
                }
            }

            if(Input.GetButtonDown("Fire2")) {
                timeToFireF = Time.time + 1/fireRate;
                if(Player.magic >= 25) {
                    Player.magic -= 25;
                    ShootProjectile(projectileF);
                    sound.clip = fireClip;
                    sound.Play();
                }
            }
        }
    }

    void ShootProjectile(GameObject projectile) {
        Ray ray = cam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;

        if(Physics.Raycast(ray, out hit)) {
            destination = hit.point;
        }

        else {
            destination = ray.GetPoint(1000);
        }

        InstantiateProjectile(firePoint, projectile);
    }

    void InstantiateProjectile(Transform fire, GameObject projectile) {
        var projectileObj = Instantiate(projectile, fire.position, Quaternion.identity) as GameObject;
        projectileObj.GetComponent<Rigidbody>().velocity = (destination - fire.position).normalized * projectileSpeed;

        iTween.PunchPosition(projectileObj, new Vector3(Random.Range(-arcRange, arcRange), Random.Range(-arcRange, arcRange), 0), Random.Range(0.5f, 2));
    }
}
