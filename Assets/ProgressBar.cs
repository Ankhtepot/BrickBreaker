using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour {

    [SerializeField] Image bar;
    [SerializeField] Image frame;
	
	public void UpdateBar(float value) {
        bar.fillAmount += value/100;
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
