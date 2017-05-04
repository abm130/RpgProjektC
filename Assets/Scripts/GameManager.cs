using UnityEngine;
using System.Collections;
using System.Collections.Generic;
//Nach dem Hinzufügen der UI Elemente ist das nötig
using UnityEngine.UI; 
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {
	
	public float levelStartDelay = 2f;
	/* GameManager wird zum Singleton gemacht
	Da er nur 1 mal benötigt wird */
	public float turnDelay = 0.1f;
	public int playerFoodPoints = 100;
	
	public static GameManager instance = null;
	
	// HideInInspector macht dass die im Editor nicht angezeigt wird
	[HideInInspector] public bool playersTurn = true;
	
	private Text levelText;
	private GameObject levelImage;
	private BoardManager boardScript;
	//Durch Problem 1 durch 0 ersetzen
	private int level = 0;
	private List<Enemy> enemies;
	private bool enemiesMoving;
	private bool doingSetup;
	
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
		enemies = new List<Enemy>();
		boardScript = GetComponent<BoardManager>();
		//InitGame();
	}
	/*Diese Methode gibts nicht mehr in neueren Unity Versionen
	void OnLevelWasLoaded(int index) {
		level++;
		InitGame();
	}*/
	
	void OnLevelFinishedLoading(Scene scene, LoadSceneMode mode) { 
		level++; 
		InitGame(); 
	}
	
	void OnEnable() { 
		SceneManager.sceneLoaded += OnLevelFinishedLoading; 
	}  
	
	void OnDisable() { 
		SceneManager.sceneLoaded -= OnLevelFinishedLoading; 
	} 
	
	
	void InitGame() {
		doingSetup = true;
		levelImage = GameObject.Find("LevelImage");
		levelText = GameObject.Find("LevelText").GetComponent<Text>();
		levelText.text = "Day " + level;
		levelImage.SetActive(true);
		Invoke("HideLevelImage", levelStartDelay);
		
		enemies.Clear();
		boardScript.SetupScene(level);
	}
	//private 
	void HideLevelImage() {
		levelImage.SetActive(false);
		doingSetup = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (playersTurn || enemiesMoving || doingSetup) {
			return;
		}
		StartCoroutine(MoveEnemies());
	}
	
	//Für den GameManager um die Bewegungsreihenfolge zu bestimmen
	public void AddEnemyToList(Enemy script) {
		enemies.Add(script);
	}
	
	public void GameOver() {
		levelText.text = "After " + level + " days, you starved.";
		levelImage.SetActive(true);
		enabled = false;
	}
	
	IEnumerator MoveEnemies() {
		enemiesMoving = true;
		yield return new WaitForSeconds(turnDelay);
		if (enemies.Count == 0) {
			yield return new WaitForSeconds(turnDelay);
		}
		
		for(int i = 0; i < enemies.Count; i++) {
			enemies[i].MoveEnemy();
			//Wartezeit einbauen
			yield return new WaitForSeconds(enemies[i].moveTime);
		}
		
		playersTurn = true;
		enemiesMoving = false;
	}
	
}
