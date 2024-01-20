using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MainMenuManager : MonoBehaviour
{
    public void Play() {
		SceneManager.LoadScene("Level1");
	}

	public void Quit() {
		Application.Quit();
	}
}
