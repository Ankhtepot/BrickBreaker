using Classes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicShot : MonoBehaviour {

    [SerializeField] float speed = 0.15f;
    [SerializeField] AudioClip hitSound;
    [SerializeField] ParticleSystem impactEffect;

	void Update () {
        transform.position = new Vector3(transform.position.x, transform.position.y + speed, 0);
	}

    private void onCollisionEnter2D(Collision2D collision) {
        if (impactEffect && collision.gameObject.tag != tags.WALL) {
             Instantiate(impactEffect, transform.position, Quaternion.identity);
        } //else if (!impactEffect) print("MagicShot/onCollisionEnter2D: no imapctEffect assigned");
        DestroyThis();
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (impactEffect && collision.gameObject.tag != tags.WALL) {
            //print("MagicShot met trigger: " + collision.tag);
            Instantiate(impactEffect, collision.transform.position, Quaternion.identity);
        } //else if (!impactEffect) print("MagicShot/onTriggerEnter2D: no imapctEffect assigned");
        DestroyThis();
    }

    private void DestroyThis() {
        SoundSystem SFXPlayer = FindObjectOfType<SoundSystem>();
        if (SFXPlayer) SFXPlayer.PlayClipOnce(hitSound);
        Destroy(gameObject);
    }
}
