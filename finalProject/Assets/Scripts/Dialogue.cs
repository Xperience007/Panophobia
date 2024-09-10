using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Dialogue : MonoBehaviour
{
    public static Dialogue Instance { get; private set; }

    public TMP_Text textMeshPro;
    public Button yes;
    public Button no;

    private void Awake() {
        Instance = this;

        textMeshPro = textMeshPro.GetComponent<TMP_Text>();
        yes = yes.GetComponent<Button>();
        no = no.GetComponent<Button>();

        Hide();
    }

    public void ShowDialogue(string questionText, Action yesAction, Action noAction) {
        gameObject.SetActive(true);
        
        textMeshPro.text = questionText;
        yes.onClick.AddListener(() => {
            Hide();
            yesAction();
        });
        no.onClick.AddListener(() => {
            Hide();
            noAction();
        });
    }

    private void Hide() {
        gameObject.SetActive(false);
    }
}
