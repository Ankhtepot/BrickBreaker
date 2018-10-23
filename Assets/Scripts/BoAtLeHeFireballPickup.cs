using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoAtLeHeFireballPickup : BoAtLeHeGrowl {

    [SerializeField] Pickup pickup;
    [SerializeField] ParticleSystem effect;

    public override void Start() {
        base.Start();
        effect = GetComponentInChildren<ParticleSystem>();
    }

    public override IEnumerator DelayGrowl() {
        if (effect) effect.Play();
        else print("BoAtLeHeFireballPickup/DelayFireball: no effect found");
        yield return new WaitForSeconds(1f);
        if (SFXPlayer && AttackSound) SFXPlayer.PlayClipOnce(AttackSound);
        else {
            Boss.PlayGrowl();
            print("BoAtRiHeLifePickup/DelayGrowl: missing SoundSystem or AttackSound");
        }
        yield return new WaitForSeconds(0.5f);
        if (pickup) {
            //Pickup spawnedPickup = 
            Instantiate(pickup, transform.position, Quaternion.identity);
        } else print("BoAtLeHeFireballPickup/Activate: no pickup to instantiate found.");
    }
}
