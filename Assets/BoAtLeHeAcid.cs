using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoAtLeHeAcid : BoAtLeHeGrowl {

    [SerializeField] ParticleSystem castEffect;
    [SerializeField] ParticleSystem acidSpawnEffect;
    [SerializeField] PhysicalBall acidBall;
    [SerializeField] MrBWaypoints spawningPointsContainer;
    [SerializeField] List<GameObject> chosenSpawningPoints;
    [SerializeField] int numberOfSpawningPoints = 5;

    List<GameObject> spawningPoints;

    public override void Start() {
        getSpawningPoints();
        ChooseSpawningPoints();
        base.Start();
    }

    public override IEnumerator DelayGrowl() {
        if (castEffect) castEffect.Play();
        else print("BoAtLeHeAcid/DelayGrowl: no effect found");
        foreach (GameObject SP in chosenSpawningPoints) {
            ParticleSystem effect = Instantiate(acidSpawnEffect, SP.transform.position, Quaternion.identity);
            effect.Play();
        }
        yield return new WaitForSeconds(1.5f);
        if(spawningPoints!=null && spawningPoints.Count > 0) {
            foreach(GameObject SP in chosenSpawningPoints) {
                Instantiate(acidBall, SP.transform.position, Quaternion.identity);
            }
        }
        if (SFXPlayer && AttackSound) SFXPlayer.PlayClipOnce(AttackSound);
        else {
            Boss.PlayGrowl();
            print("BoAtLeHeAcid/DelayGrowl: missing SoundSystem or AttackSound");
        }
        //yield return new WaitForSeconds(0.5f);
        ChooseSpawningPoints();
    }

    private void getSpawningPoints() {
        spawningPoints = spawningPointsContainer.GetWaypoints();
    }

    public void ChooseSpawningPoints() {
        chosenSpawningPoints.Clear();
        for (int i = 0; i < Math.Min(numberOfSpawningPoints, spawningPoints.Count); i++) {
            while (true) {
                int chosenSP = UnityEngine.Random.Range(0, spawningPoints.Count);
                if (!chosenSpawningPoints.Contains(spawningPoints[chosenSP])) {
                    chosenSpawningPoints.Add(spawningPoints[chosenSP]);
                    break;
                }
            }
        }
    }
}
