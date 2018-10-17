using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoAtLeHeFireballPickup : BoAtLeHeGrowl {

    [SerializeField] Pickup pickup;

    public override IEnumerator DelayGrowl() {
        yield return new WaitForSeconds(1.5f);
        if (SFXPlayer && AttackSound) SFXPlayer.PlayClipOnce(AttackSound);
        else {
            Boss.PlayGrowl();
            print("BoAtRiHeFireballPickup/DelayGrowl: missing SoundSystem or AttackSound");
        }
        if (pickup) {
            Pickup spawnedPickup = Instantiate(pickup, transform.position, Quaternion.identity);
        } else print("BoAtHeFireballPickup/Activate: no pickup to instantiate found.");
    }
}
