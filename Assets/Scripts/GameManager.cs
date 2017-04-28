using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
	
	/* GameManager wird zum Singleton gemacht
	Da er nur 1 mal benötigt wird */
	public static GameManager instance = null;
	public BoardManager boardScript;
	public int playerFoodPoints = 100;
	private int level = 3;
	
	
	// Use this for initialization
	void Awake () {
		if (instance == null) {
			instance = this;
		}
		else if (instance != this){
			Destroy(gameObject);
		}
		//Damit bei Szenenwechsel der GameManager nicht terminiert wird
		DontDestroyOnLoad(gameObject);
		boardScript = GetComponent<BoardManager>();
		InitGame();
	}
	
	void InitGame() {
		boardScript.SetupScene(level);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
