using Assets.Classes;
using Assets.Interfaces;
using Classes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttackManager : MonoBehaviour {

    [SerializeField] List<BossAttack> Attacks;
    [SerializeField] float LifePickupCD = 60f;
    [SerializeField] float HealingCD = 120f;
    [SerializeField] float FireballCD = 45f;

    bool LifePickupOnCD = false;
    bool HealingOnCD = false;
    bool FireballOnCD = false;

    private BossAttack ChooseAttack() {
        BossAttack attack = Attacks[Random.Range(0, Attacks.Count)];
        //print("Attack tag = " + attack.tag);
        if (attack.tag == tags.LIFE_PICKUP)
            if (LifePickupOnCD) {
                attack = null;
                ChooseAttack();
            } else StartCoroutine(StartLifePickupCD());
        if (attack && attack.tag == tags.HEALING)
            if (HealingOnCD) {
                attack = null;
                ChooseAttack();
            } else StartCoroutine(StartHealingCD());
        if (attack && attack.tag == tags.FIREBALL)
            if (FireballOnCD) {
                attack = null;
                ChooseAttack();
            } else StartCoroutine(StartFireballCD());
        return attack;
    }

    public void performAttack() {
        if (Attacks != null && Attacks.Count > 0) {
            BossAttack attack = ChooseAttack();
            if(attack) attack.Activate();
            //print("BossAttackManager/performAttack: " + attack.name);
        } else print("BossAttackManager/performAttack: no attacks avaiable");
    }

    IEnumerator StartHealingCD() {
        HealingOnCD = true;
        yield return new WaitForSeconds(HealingCD);
        print("Healing is off CD now");
        HealingOnCD = false;
    }

    IEnumerator StartLifePickupCD() {
        LifePickupOnCD = true;
        yield return new WaitForSeconds(LifePickupCD);
        print("LifePickup is off CD now");
        LifePickupOnCD = false;
    }

    IEnumerator StartFireballCD() {
        FireballOnCD = true;
        yield return new WaitForSeconds(FireballCD);
        print("Fireball is off CD now");
        FireballOnCD = false;
    }
}
