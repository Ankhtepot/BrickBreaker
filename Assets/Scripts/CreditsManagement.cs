using Assets.Classes;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreditsManagement : MonoBehaviour {
    //[System.Serializable]
    public enum FieldToWorkWith {
        ShowGraphics, ShowAudio, ShowRest
    }
    //Caches
    [SerializeField] Animator animator;

    private void Start() {
        animator = GetComponentInParent<Animator>();
    }

    public void ExpandGraphics() {
        print("Showing graphics");
        if (animator) animator.SetBool(triggers.SHOW_GRAPHICS, true);
        else print("CreditsManagement: animator not found");
    }

    public void CollapseGraphics() {
        print("Collapsing graphics");
        if (animator) animator.SetBool(triggers.SHOW_GRAPHICS, false);
        else print("CreditsManagement: animator not found");
    }

    public void ExpandRest() {
        print("Showing rest");
        if (animator) animator.SetBool(triggers.SHOW_REST, true);
        else print("CreditsManagement: animator not found");
    }

    public void CollapseRest() {
        print("Collapsing rest");
        if (animator) animator.SetBool(triggers.SHOW_REST, false);
        else print("CreditsManagement: animator not found");
    }

    public void ExpandAudio() {
        print("Showing audio");
        if (animator) animator.SetBool(triggers.SHOW_AUDIO, true);
        else print("CreditsManagement: animator not found");
    }

    public void CollapseAudio() {
        print("Collapsing audio");
        if (animator) animator.SetBool(triggers.SHOW_AUDIO, false);
        else print("CreditsManagement: animator not found");
    }

}
