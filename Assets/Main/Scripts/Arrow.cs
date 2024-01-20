using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    public AudioClip collisionSound;
    public AudioClip campCollisionSound;
    public Rigidbody rb;
    public GameObject impactEffect;

    bool isMoving = true;

    void Start() {
        rb = GetComponent<Rigidbody>();
        if (rb == null) {
            Debug.LogError("Rigidbody component not found on the GameObject.");
        }
    }

	void FixedUpdate() {
        if (rb != null && isMoving && rb.velocity.sqrMagnitude!=0f) {
            this.transform.rotation = Quaternion.LookRotation(rb.velocity);
		}
    }

    void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.CompareTag("Camp")) {
            CampFire camp = collision.gameObject.GetComponent<CampFire>();
            if (!camp.isHot) {
                camp.ToggleFireState();
                AudioSource.PlayClipAtPoint(campCollisionSound, transform.position);
            }
		}
        isMoving = false;
        Instantiate(impactEffect,this.transform.position, Quaternion.identity);
        Destroy(this.gameObject);
    }
}
