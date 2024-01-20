using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    public GameObject projectilePrefab;
    public Transform spawnPoint;
    public float shootingForce = 10f;

    void Update() {
        if (Input.GetMouseButtonDown(0))
        {
            ShootProjectile();
        }
    }

    void ShootProjectile() {
        // Raycast from the camera through the center of the screen
        Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit)) {

            // TODO: if hit enemy, destroy enemy

            Vector3 shootingDirection = (hit.point - spawnPoint.position).normalized;

            GameObject projectile = Instantiate(projectilePrefab, spawnPoint.position, Quaternion.LookRotation(shootingDirection));
            Rigidbody projectileRb = projectile.GetComponent<Rigidbody>();

            if (projectileRb != null) {
                projectileRb.AddForce(shootingDirection * shootingForce, ForceMode.Impulse);
            }
		} else {
            Vector3 shootingDirection = ray.direction.normalized;

            GameObject projectile = Instantiate(projectilePrefab, spawnPoint.position, Quaternion.LookRotation(shootingDirection));
            Rigidbody projectileRb = projectile.GetComponent<Rigidbody>();

            if (projectileRb != null) {
                projectileRb.AddForce(shootingDirection * shootingForce, ForceMode.Impulse);
            }
        }
    }
}
