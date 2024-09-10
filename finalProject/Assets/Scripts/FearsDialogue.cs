using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FearsDialogue : MonoBehaviour
{
    void Update() {
        if(GroundEnemy.ghostKills == 10) {
            Dialogue.Instance.ShowDialogue("Do you want to face your fears?", () => {
                //Do nothing on yes
            }, () => {
                SceneManager.LoadScene("Level2");
            });
        }
    }
}
