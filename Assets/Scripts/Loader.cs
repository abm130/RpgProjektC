using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Dafür da um zu checken ob ein GameManager instantiiert wurde
public class Loader : MonoBehaviour {

	public GameObject gameManager;
	
	// Use this for initialization
	void Awake () {
		if(GameManager.instance == null){
			Instantiate(gameManager);
		}
			
	}
	
}
