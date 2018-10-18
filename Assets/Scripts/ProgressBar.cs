using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour {

    [SerializeField] float fillSpeed = 0.01f;
    [SerializeField] Image bar;
    [SerializeField] Image frame;


    [SerializeField] private float targetValue;

    private void Start() {
        targetValue = bar.fillAmount;
    }

    private void Update() {
        bar.fillAmount += bar.fillAmount < targetValue ? fillSpeed : -fillSpeed;
    }

    public void UpdateBar(float value) {
        targetValue += value/100;
        if (targetValue >= 1) targetValue = 1;
        if (targetValue <= 0) targetValue = 0;        
    }

    public void EnableVisuals() {
        bar.enabled = true;
        frame.enabled = true;
    }

    public void DisableVisuals() {
        bar.enabled = false;
        frame.enabled = false;
    }
}
