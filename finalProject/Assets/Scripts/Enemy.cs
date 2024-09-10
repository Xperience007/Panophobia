using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public GameObject EnemyHealthBar;
    private static Image EnemyHealthBarImage;
    public static float health = 100.0f;

    AudioSource myAudio;

    // Start is called before the first frame update
    void Start()
    {
        if(EnemyHealthBar != null) {
            EnemyHealthBarImage = EnemyHealthBar.transform.GetComponent<Image>();
        }

        SetEnemyHealthBarValue(health);
    }

    // Update is called once per frame
    void Update()
    {
        if(PauseMenu.GameIsPaused == false) {
            if(health == 0) {
                StartCoroutine(PlayAndDestroy(myAudio.clip.length));
            }

            SetEnemyHealthBarValue(health/100);
        }
    }

    public static void SetEnemyHealthBarValue(float value) {
        EnemyHealthBarImage.fillAmount = value;
    }

    public static float GetEnemyHealthBarValue() {
        return EnemyHealthBarImage.fillAmount;
    }

    void OnTriggerEnter(Collider other) {
        //Put player magic tags and hits to health here
    }

    private IEnumerator PlayAndDestroy(float waitTime) {
        myAudio.Play();
        yield return new WaitForSeconds(waitTime);
        Destroy(gameObject);
    }
}
