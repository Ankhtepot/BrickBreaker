#pragma warning disable 0414

using Assets.Classes;
using Assets.Interfaces;
using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoAtLeHeGrowl : BossAttack {

    [SerializeField] public Animator BossAnimator;
    [SerializeField] public MrBrickworm Boss;
    [SerializeField] public SoundSystem SFXPlayer;
    [SerializeField] public AudioClip AttackSound;
    [SerializeField] string nameOfAttack = "BoAtLeHeGrowl";


    public virtual void Start() {
        Boss = FindObjectOfType<MrBrickworm>();
        BossAnimator = FindObjectOfType<AnimationAdapter>().GetComponent<Animator>();
        SFXPlayer = FindObjectOfType<SoundSystem>();
    }

    public override void Activate() {
        if (BossAnimator) {
            BossAnimator.SetTrigger(triggers.ATTACKLEFT);
            StartCoroutine(DelayGrowl());
        } else print("BoAtLeHeGrowl/Activate: Boss not found");
    }

    public virtual IEnumerator DelayGrowl() {
        yield return new WaitForSeconds(1.5f);
        if (SFXPlayer && AttackSound) SFXPlayer.PlayClipOnce(AttackSound);
        else {
            Boss.PlayGrowl();
            print("BoAtLeHeGrowl/DelayGrowl: missing SoundSystem or AttackSound");
        }
    }

}
