using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class MoveOnScript : MonoBehaviour {

	public int col=0, row=0;
	public float dens=0;

	// Use this for initialization
	void Awake () {
		DontDestroyOnLoad(gameObject);


	}
	
	// Update is called once per frame
	void Update () {

		Debug.Log (col + row + dens);

	}
}
