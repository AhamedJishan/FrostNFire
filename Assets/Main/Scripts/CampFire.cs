using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CampFire : MonoBehaviour
{
    private int campHealth = 5;

    public GameObject hotFire;
    public GameObject coldFire;

    public GameObject enemyPrefab;
    public Transform enemySpawnPoint;
    public float spawnInterval = 10f;

    public bool isHot = false; // Initial state is false

    void Start() {
        UpdateFireState();
        InvokeRepeating("SpawnEnemy", spawnInterval, spawnInterval);
    }

	public void TakeDamage() {
		if (campHealth>0) {
            campHealth--;
		}
		if (campHealth<=0) {
            ToggleFireState();
        }
	}

	void SpawnEnemy() {
        if (!isHot) Instantiate(enemyPrefab, enemySpawnPoint.position, Quaternion.identity);
    }

    public void ToggleFireState() {
        isHot = !isHot;
        UpdateFireState();
        campHealth = 5;
    }

    private void UpdateFireState() {
        hotFire.SetActive(isHot);
        coldFire.SetActive(!isHot);
    }
}
