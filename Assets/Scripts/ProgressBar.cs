using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour {

    [SerializeField] Image bar;
    [SerializeField] Image frame;
	
	public void UpdateBar(float value) {
        bar.fillAmount += value/100;
        if (bar.fillAmount >= 100) bar.fillAmount = 100;
        if (bar.fillAmount <= 0) bar.fillAmount = 0;
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
