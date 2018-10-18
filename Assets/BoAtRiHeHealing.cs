using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoAtRiHeHealing : BoAtRiHeGrowl {

    [SerializeField] ParticleSystem effect;

    public override void Start() {
        base.Start();
        effect = GetComponentInChildren<ParticleSystem>();
    }

    public override IEnumerator DelayGrowl() {
        if (effect) effect.Play();
        else print("BoAtRiHeHealing/DelayFireball: no effect found");
        yield return new WaitForSeconds(1f);
        if (SFXPlayer && AttackSound) SFXPlayer.PlayClipOnce(AttackSound);
        else {
            Boss.PlayGrowl();
            print("BoAtRiHeHealing/DelayGrowl: missing SoundSystem or AttackSound");
        }
        Boss.AdjustHealthAndHealthPB(10f);
        //yield return new WaitForSeconds(0.5f);
        
    }

}
