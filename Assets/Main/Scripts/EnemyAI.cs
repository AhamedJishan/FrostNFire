using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public AudioClip blockSound;

    private int health;
    public float moveSpeed = 5f;
    public float rotationSpeed = 3f;
    public float shootingInterval = 2f;
    public float maxDistancefromCamp = 2f;

    private GameObject targetCamp;
    public GameObject projectilePrefab;
    public Transform projectileSpawnPoint;
    public GameObject DestroyVFX;

    private float timeSinceLastShot;

    void Start() {
        timeSinceLastShot = shootingInterval;
        health = 2;
    }

    void Update() {
		if (targetCamp == null) {
            FindClosestCamp();
        }
		else if(targetCamp.GetComponent<CampFire>().isHot){
            ChaseCamp();
		} else {
            FindClosestCamp();
        }

        if (health <= 0) { 
            Die();
        }
    }

    private void FindClosestCamp() {
        GameObject[] campObjects = GameObject.FindGameObjectsWithTag("Camp");

        float closestDistance = Mathf.Infinity;
        GameObject closestCamp = null;

        foreach (GameObject camp in campObjects) {
			if (camp.GetComponent<CampFire>().isHot) {
                float distance = Vector3.Distance(transform.position, camp.transform.position);

                if (distance < closestDistance) {
                    closestDistance = distance;
                    closestCamp = camp;
                }
            }
        }
        if (closestCamp != null) {
            targetCamp = closestCamp;
        }
    }

	private void OnCollisionEnter(Collision collision) {
		if (collision.gameObject.CompareTag("Arrow")) {
            TakeDamage();
            AudioSource.PlayClipAtPoint(blockSound, transform.position, 10f);
		}
	}

	public void TakeDamage() {
        health=health-1;
	}

    void ChaseCamp() {
        Vector3 directionToCamp = targetCamp.transform.position - transform.position;
        directionToCamp.y = 0;
		if (directionToCamp.magnitude>= maxDistancefromCamp) {
            Quaternion rotationToPlayer = Quaternion.LookRotation(directionToCamp);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotationToPlayer, rotationSpeed * Time.deltaTime);

            transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
		} else {
            ShootAtCamp();
		}
    }

    void ShootAtCamp() {
        timeSinceLastShot += Time.deltaTime;

        if (timeSinceLastShot >= shootingInterval) {
            Instantiate(projectilePrefab, projectileSpawnPoint.position, transform.rotation);
            timeSinceLastShot = 0f;
        }
    }

	public void Die() {
        Instantiate(DestroyVFX, this.transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
