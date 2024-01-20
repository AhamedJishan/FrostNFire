using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    GameObject[] camps;
    private int numberOfCamps;
    private int campsCold = 0;
    private int campsHot = 0;

    public GameObject GameOverUI;
    public TextMeshProUGUI gameOverText;

    bool isGameOver = false;
    void Start()
    {
        GameOverUI.SetActive(false);
        camps = GameObject.FindGameObjectsWithTag("Camp");
        numberOfCamps = camps.Length;
    }

    // Update is called once per frame
    void Update()
    {

		if (isGameOver) GameOver();

        campsCold = 0;
        campsHot = 0;
        foreach (GameObject camp in camps) {
			if (camp.GetComponent<CampFire>().isHot) campsHot += 1;
			else campsCold += 1;
		}

		if (campsHot == numberOfCamps) {
            gameOverText.text = "You WON!";
            isGameOver = true;
		}
		if (campsCold == numberOfCamps) {
            gameOverText.text = "You Lost!";
            isGameOver = true;
        }
    }

    void GameOver() {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject enemy in enemies) {
            EnemyAI enemyAI = enemy.GetComponent<EnemyAI>();
            if (enemyAI != null) enemyAI.enabled = false;
        }
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        player.GetComponent<FirstPersonController>().enabled = false;
        player.GetComponent<Shoot>().enabled = false;

        Cursor.lockState=CursorLockMode.None;
        GameOverUI.SetActive(true);
    }

    public void RestartPressed() {
        SceneManager.LoadScene("Level1");
	}
    public void MainMenuPressed() {
        SceneManager.LoadScene("MainMenu");
    }
}
