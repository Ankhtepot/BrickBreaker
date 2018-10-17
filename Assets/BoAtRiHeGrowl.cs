#pragma warning disable 0414

using Assets.Classes;
using Assets.Interfaces;
using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoAtRiHeGrowl : BossAttack {

    [SerializeField] public Animator BossAnimator;
    [SerializeField] public MrBrickworm Boss;
    [SerializeField] public SoundSystem SFXPlayer;
    [SerializeField] public AudioClip AttackSound;
    [SerializeField] string nameOfAttack = "BoAtLeHeGrowl";

    private void Start() {
        Boss = FindObjectOfType<MrBrickworm>();
        BossAnimator = FindObjectOfType<AnimationAdapter>().GetComponent<Animator>();
        SFXPlayer = FindObjectOfType<SoundSystem>();
    }

    public override void Activate() {
        if (BossAnimator) {
            BossAnimator.SetTrigger(triggers.ATTACKRIGHT);
            StartCoroutine(DelayGrowl());
        } else print("BoAtRiHeGrowl/Activate: Boss not found");
    }

    IEnumerator DelayGrowl() {
        yield return new WaitForSeconds(1.5f);
        if (SFXPlayer && AttackSound) SFXPlayer.PlayClipOnce(AttackSound);
        else {
            Boss.PlayGrowl();
            print("BoAtRiHeGrowl/DelayGrowl: missing SoundSystem or AttackSound");
        }
    }
}
