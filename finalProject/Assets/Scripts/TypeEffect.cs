using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TypeEffect : MonoBehaviour
{
    public TMP_Text t;
    string title;

    void Awake() {
        t = GetComponent<TMP_Text> ();
        title = t.text;
        t.text = "";

        StartCoroutine("PlayText");
    }

    IEnumerator PlayText() {
        foreach(char c in title) {
            t.text += c;
            yield return new WaitForSeconds(0.125f);
        }
    }
}
