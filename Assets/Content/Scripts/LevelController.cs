using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class LevelController : MonoBehaviour {

	public static LevelController current;
	public int bones_count = 0;
	
	void Awake() {
		current = this;

	}

	Vector3 startingPosition;
	public void setStartPosition(Vector3 pos) {
		this.startingPosition = pos;
	}
	public void onPugDeath(HeroPug pug) {
		//При смерті мопса повертаємо на початкову позицію
		pug.transform.position = this.startingPosition;
	}
	
	public void addBones (int bones) {
		bones_count += bones;
	}

    public void StartMenuScene()
    {
       SceneManager.LoadScene("Menu");
    }
    
}
