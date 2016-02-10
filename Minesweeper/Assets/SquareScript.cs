using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class SquareScript : MonoBehaviour {
	Scene curScene;
	GameObject GameController;
	public Vector2 Loc;
	public bool trigger = false;
	public bool Mine = false;
	public enum Status { Plain, Mine, Question, Flag};
	public Sprite[] Visual ;
	public Status curStatus = Status.Plain;
	public bool Picked = false; // False by default
	//int looked = 0;
	int near = 0; //near blocks that are mines
	// Use this for initialization
	void Start () {
		curScene = SceneManager.GetActiveScene ();
		GameController = GameObject.FindGameObjectWithTag ("GameController");

		//gameObject .GetComponent<SpriteRenderer> ().sprite = Visual [0]; //sets it as base Sprite, unclicked
	}
	
	// Update is called once per frame
	void Update () {
		if (trigger == false) {
			if (!Picked) {
				if (curStatus == Status.Plain)
					gameObject.GetComponent<SpriteRenderer> ().sprite = Visual [0];
				else if (curStatus == Status.Flag)
					gameObject.GetComponent<SpriteRenderer> ().sprite = Visual [11];
				else if (curStatus == Status.Question)
					gameObject.GetComponent<SpriteRenderer> ().sprite = Visual [12];
			
			}

//			if (Mine == true) {
//				gameObject.GetComponent<SpriteRenderer> ().sprite = Visual [1];
//
//			}
		}
	}

	void OnMouseOver () {
		if(Input.GetMouseButtonDown(0)){
			Click();
		}
		if(Input.GetMouseButtonDown(1)){
			Debug.Log("Right Click!");
			//Set square to Flag then set to ? Then Back to Plain
			if(!Picked){
				if(curStatus == Status.Plain)
					curStatus = Status.Flag;
				else if(curStatus == Status.Flag)
					curStatus = Status.Question;
				else
					curStatus = Status.Plain;

			}
		}
	}


//	void OnMouseDown() 
//	{
//		Click ();
//	}

	public int CheckMine(){

		if (Mine == true)
			return 1;
		else 
			return 0;



	}


//	void OnCollisionStay2D(Collision2D col){
//		//Debug.Log ("There are Squares around me");
//		if (Picked == true && looked <= 2) {
//
//			if(col.gameObject.GetComponent<SquareScript>().Picked == false){
//
//				if(col.gameObject.GetComponent<SquareScript>().CheckMine()){
//					near++; //adds 1 mine to the near count;
//					Debug.Log("There's a mine or two" + col.gameObject.name);
//				}
//			}else{
//
//
//			}
//			looked++;
//		}
//
//		//Debug.Log ("Near " + near + "    Looked at: " + looked);
//	}

	public void Click(){
		if (Mine == true) {
			//Game's gotta end
			curStatus = Status.Mine;
			gameObject.GetComponent<SpriteRenderer>().sprite = Visual[1];
			if(curScene.name == "HardMineSweeper")
				GameController.gameObject.GetComponent<HardSweeper>().GameLoss();
			else if(curScene.name == "MineSweeper")
				near = GameController.gameObject.GetComponent<Grid2>().BombCheck(Loc);
		}
		else if(Picked == false && (curStatus == Status.Plain || curStatus == Status.Question)){
			//Can be Selected
			Picked = true;
			if(curScene.name == "HardMineSweeper")
				near = GameController.gameObject.GetComponent<HardSweeper>().BombCheck(Loc);
			else if(curScene.name == "MineSweeper")
				near = GameController.gameObject.GetComponent<Grid2>().BombCheck(Loc);
			//Debug.Log(near + " This many close bombs");
			//Debug.Log ("You clicked " + gameObject.tag.ToString());
			if(near>=1){
				gameObject.GetComponent<SpriteRenderer>().sprite = Visual[near+1];
			}else{
				gameObject.GetComponent<SpriteRenderer>().sprite = Visual[10];
			
			
			
			}
		}


	}



}
