using Assets.Interfaces;
using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MrBBodypart : MonoBehaviour, IPlayList {

    [SerializeField] IBoss Boss;
    [SerializeField] ParticleSystem effect;

    private void Start() {
        Boss = GetComponentInParent<IBoss>();
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
}
