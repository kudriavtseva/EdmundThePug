using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MenuController : MonoBehaviour {

	public static MenuController current;

	void Awake() {
		current = this;
	}

	public static bool isMusicOn() {
		return GameObject.Find ("PUG1").GetComponent<AudioSource> ().volume > 0;
	}

    public void StartMenuScene()
    {
        SceneManager.LoadScene("Level1");
    }

	public void ToggleMusic()
	{
		AudioSource menu_audio = GameObject.Find ("PUG1").GetComponent<AudioSource> ();
		menu_audio.volume = isMusicOn() ? 0 : 100;
	}
}
