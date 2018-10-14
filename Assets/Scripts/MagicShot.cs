using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicShot : MonoBehaviour {

    [SerializeField] float speed = 0.15f;
    [SerializeField] AudioClip hitSound;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        transform.position = new Vector3(transform.position.x, transform.position.y + speed, 0);
	}

    private void onCollisionEnter2D(Collision2D collision) {
        DestroyThis();
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        //print("MagicShot met trigger");
        DestroyThis();
    }

    private void DestroyThis() {
        SoundSystem SFXPlayer = FindObjectOfType<SoundSystem>();
        if (SFXPlayer) SFXPlayer.PlayClipOnce(hitSound);
        Destroy(gameObject);
    }
}
