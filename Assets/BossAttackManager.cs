using Assets.Classes;
using Assets.Interfaces;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttackManager : MonoBehaviour {

    [SerializeField] List<BossAttack> Attacks;

    private BossAttack ChooseAttack() {
        return Attacks[Random.Range(0, Attacks.Count)];
    }

    public void performAttack() {
        if (Attacks != null && Attacks.Count > 0) {
            BossAttack attack = ChooseAttack();
            attack.Activate();
            print("BossAttackManager/performAttack: " + attack.name);
        } else print("BossAttackManager/performAttack: no attacks avaiable");
    }
}
