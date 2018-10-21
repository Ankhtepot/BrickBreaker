using Assets.Interfaces;
using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MrBBodypart : MonoBehaviour, IPlayList {

    [SerializeField] MrBrickworm Boss;
    [SerializeField] ParticleSystem effect;

    private void Start() {
        Boss = GetComponentInParent<MrBrickworm>();
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (Boss != null) {
            if (effect) Instantiate(effect, collision.transform.position, Quaternion.identity);
            Boss.OnCollisionEnter2D(collision);
        } else print("No boss found");
    }

    public SoundSystem.PlayListID GetPlayListID() {
        return SoundSystem.PlayListID.Boss;
    }

    private void OnDisable() {
        if (effect && Boss && !Boss.isAlive) {
            Boss.PlayExplosionSquish();
            Instantiate(Boss.PSDisableEffect, transform.position, Quaternion.identity);
        }
    }
}
