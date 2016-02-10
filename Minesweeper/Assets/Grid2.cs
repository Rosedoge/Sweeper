using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Grid2 : MonoBehaviour {

	List<List<GameObject>> Grid;
	public int width = 10;
	public int height = 10;
	public int numBombs = 8;
	int nonMineTiles, picked = 0;
	List<Vector2> bombLoc;
	List<Vector2> ChecLocs;
	public GameObject tile;
	GameObject options;
	float bDens;


	void Awake(){
		Grid = new List<List<GameObject>>();
		bombLoc = new List<Vector2> ();
		nonMineTiles = (width * height) - numBombs;
		options = GameObject.FindGameObjectWithTag ("MenuMover");
		width = options.GetComponent<MoveOnScript> ().col;
		height = options.GetComponent<MoveOnScript> ().row;
		bDens =  options.GetComponent<MoveOnScript> ().dens;
		//Debug.Log ("I'm here");
	}
	// Use this for initialization
	void Start () {
	
		//width = options.GetComponent<MoveOnScript> ().col;
		for(int x = 0; x < width; x++)
		{
			// Creates a new row at Grid[x]
			Grid.Add(new List<GameObject>());
			
			for(int y = 0; y < height; y++)
			{
				// Adds a GameObject to Grid[x][y]
				Grid[x].Add((GameObject)Instantiate(tile, 
				     new Vector2(this.gameObject.transform.position.x + (1 * x), this.gameObject.transform.position.y + (1 * y)),
				     this.gameObject.transform.rotation));
				Grid[x][y].gameObject.GetComponent<SquareScript>().Loc = new Vector2(x,y);
			}
		}
		BombPlanter ();
	}

	//Bomb Planter
	void BombPlanter(){

		numBombs = (int)((height * width) * (bDens / 100));
		nonMineTiles = (width * height) - numBombs;
		for (int x = 0; x<numBombs; x++) {
		

			Vector2 temp = new Vector2();
			temp = Roll ();
			if(bombLoc.Contains(temp)){
				x--;
			}else{
				bombLoc.Add (temp);
				//Debug.Log(temp.x + " :X " + temp.y + " :y");
			}
		
		}

		foreach (Vector2 vec in bombLoc) { //places the bombs
			Grid[(int)vec.x][(int)vec.y].gameObject.GetComponent<SquareScript>().Mine = true;


		}

	}
	Vector2 Roll(){
		Vector2 loc = new Vector2 ();

		int x, y;
		x = (int)Random.Range (0, width);
		y = (int)Random.Range (0, height);


		loc = new Vector2 (x, y);
		return loc;

	}

	public void GameEnd(){




	}




	// Update is called once per frame
	void FixedUpdate () {
		for (int x = 0; x < width; x++) {
			// Creates a new row at Grid[x]
			for (int y = 0; y < height; y++) {


			}

		}
		if (picked == nonMineTiles) {
			Debug.Log("Game Won!");

		}
	
	}

	public int BombCheck(Vector2 loc){
		ChecLocs = new List<Vector2> ();
		//loc x is the column, y is the row
		int Found = 0;
		int row = (int)loc.y;
		int col = (int)loc.x;


		if (row == 0) { //7
			if(col == 0){ //Bottom and Left
				Found += Grid[col+1][row+1].gameObject.GetComponent<SquareScript>().CheckMine();
				if(Grid[col+1][row+1].gameObject.GetComponent<SquareScript>().CheckMine()==0)
					ChecLocs.Add(new Vector2(col+1,row+1));

				Found += Grid[col][row+1].gameObject.GetComponent<SquareScript>().CheckMine();
				if(Grid[col][row+1].gameObject.GetComponent<SquareScript>().CheckMine()==0)
					ChecLocs.Add(new Vector2(col,row+1));

				Found += Grid[col+1][row].gameObject.GetComponent<SquareScript>().CheckMine();
				if(Grid[col+1][row].gameObject.GetComponent<SquareScript>().CheckMine()==0)
					ChecLocs.Add(new Vector2(col+1,row));

			}else if(col == width-1){ //bottom and Right
				Found += Grid[col-1][row+1].gameObject.GetComponent<SquareScript>().CheckMine();
				if(Grid[col-1][row+1].gameObject.GetComponent<SquareScript>().CheckMine()==0)
					ChecLocs.Add(new Vector2(col-1,row+1));

				Found += Grid[col][row+1].gameObject.GetComponent<SquareScript>().CheckMine();
				if(Grid[col][row+1].gameObject.GetComponent<SquareScript>().CheckMine()==0)
					ChecLocs.Add(new Vector2(col,row+1));

				Found += Grid[col-1][row].gameObject.GetComponent<SquareScript>().CheckMine();
				if(Grid[col-1][row].gameObject.GetComponent<SquareScript>().CheckMine()==0)
					ChecLocs.Add(new Vector2(col-1,row));




			}else{ //Left
				Found += Grid[col][row+1].gameObject.GetComponent<SquareScript>().CheckMine();//Up
				if(Grid[col][row+1].gameObject.GetComponent<SquareScript>().CheckMine()==0)
					ChecLocs.Add(new Vector2(col,row+1));

				//Found += Grid[col][row-1].gameObject.GetComponent<SquareScript>().CheckMine();//Down
				Found += Grid[col-1][row].gameObject.GetComponent<SquareScript>().CheckMine();//Left
				if(Grid[col-1][row].gameObject.GetComponent<SquareScript>().CheckMine()==0)
					ChecLocs.Add(new Vector2(col-1,row));

				Found += Grid[col+1][row].gameObject.GetComponent<SquareScript>().CheckMine();//Right
				if(Grid[col+1][row].gameObject.GetComponent<SquareScript>().CheckMine()==0)
					ChecLocs.Add(new Vector2(col+1,row));

				//Found += Grid[col-1][row-1].gameObject.GetComponent<SquareScript>().CheckMine();//Down and left
				Found += Grid[col-1][row+1].gameObject.GetComponent<SquareScript>().CheckMine();//Up and Left
				if(Grid[col-1][row+1].gameObject.GetComponent<SquareScript>().CheckMine()==0)
					ChecLocs.Add(new Vector2(col-1,row+1));

				//Found += Grid[col+1][row-1].gameObject.GetComponent<SquareScript>().CheckMine();//Down and Right
				Found += Grid[col+1][row+1].gameObject.GetComponent<SquareScript>().CheckMine();//Up and Right
				if(Grid[col+1][row+1].gameObject.GetComponent<SquareScript>().CheckMine()==0)
					ChecLocs.Add(new Vector2(col+1,row+1));


			}
		} else if (row == height-1) {
			if(col == 0){ //Top and Left
				//Found += Grid[col][row+1].gameObject.GetComponent<SquareScript>().CheckMine();//Up
				Found += Grid[col][row-1].gameObject.GetComponent<SquareScript>().CheckMine();//Down
				if(Grid[col][row-1].gameObject.GetComponent<SquareScript>().CheckMine()==0)
					ChecLocs.Add(new Vector2(col,row-1));

				//Found += Grid[col-1][row].gameObject.GetComponent<SquareScript>().CheckMine();//Left
				Found += Grid[col+1][row].gameObject.GetComponent<SquareScript>().CheckMine();//Right
				if(Grid[col+1][row].gameObject.GetComponent<SquareScript>().CheckMine()==0)
					ChecLocs.Add(new Vector2(col+1,row));

				//Found += Grid[col-1][row-1].gameObject.GetComponent<SquareScript>().CheckMine();//Down and left
				//Found += Grid[col-1][row+1].gameObject.GetComponent<SquareScript>().CheckMine();//Up and Left
				Found += Grid[col+1][row-1].gameObject.GetComponent<SquareScript>().CheckMine();//Down and Right
				if(Grid[col+1][row-1].gameObject.GetComponent<SquareScript>().CheckMine()==0)
					ChecLocs.Add(new Vector2(col+1,row-1));

				//Found += Grid[col+1][row+1].gameObject.GetComponent<SquareScript>().CheckMine();//Up and Right

				
				
			}else if(col == width-1){ //Top and Right
				//Found += Grid[col][row+1].gameObject.GetComponent<SquareScript>().CheckMine();//Up
				Found += Grid[col][row-1].gameObject.GetComponent<SquareScript>().CheckMine();//Down
				if(Grid[col][row-1].gameObject.GetComponent<SquareScript>().CheckMine()==0)
					ChecLocs.Add(new Vector2(col,row-1));

				Found += Grid[col-1][row].gameObject.GetComponent<SquareScript>().CheckMine();//Left
				if(Grid[col-1][row].gameObject.GetComponent<SquareScript>().CheckMine()==0)
					ChecLocs.Add(new Vector2(col-1,row));

				//Found += Grid[col+1][row].gameObject.GetComponent<SquareScript>().CheckMine();//Right
				Found += Grid[col-1][row-1].gameObject.GetComponent<SquareScript>().CheckMine();//Down and left
				if(Grid[col-1][row-1].gameObject.GetComponent<SquareScript>().CheckMine()==0)
					ChecLocs.Add(new Vector2(col-1,row-1));

				//Found += Grid[col-1][row+1].gameObject.GetComponent<SquareScript>().CheckMine();//Up and Left
				//Found += Grid[col+1][row-1].gameObject.GetComponent<SquareScript>().CheckMine();//Down and Right
				//Found += Grid[col+1][row+1].gameObject.GetComponent<SquareScript>().CheckMine();//Up and Right

				
				
				
				
			}else{ //Top
				//Found += Grid[col][row+1].gameObject.GetComponent<SquareScript>().CheckMine();//Up
				Found += Grid[col][row-1].gameObject.GetComponent<SquareScript>().CheckMine();//Down
				if(Grid[col][row-1].gameObject.GetComponent<SquareScript>().CheckMine()==0)
					ChecLocs.Add(new Vector2(col,row-1));

				Found += Grid[col-1][row].gameObject.GetComponent<SquareScript>().CheckMine();//Left
				if(Grid[col-1][row].gameObject.GetComponent<SquareScript>().CheckMine()==0)
					ChecLocs.Add(new Vector2(col-1,row));

				Found += Grid[col+1][row].gameObject.GetComponent<SquareScript>().CheckMine();//Right
				if(Grid[col+1][row].gameObject.GetComponent<SquareScript>().CheckMine()==0)
					ChecLocs.Add(new Vector2(col+1,row));

				Found += Grid[col-1][row-1].gameObject.GetComponent<SquareScript>().CheckMine();//Down and left
				if(Grid[col-1][row-1].gameObject.GetComponent<SquareScript>().CheckMine()==0)
					ChecLocs.Add(new Vector2(col-1,row-1));

				//Found += Grid[col-1][row+1].gameObject.GetComponent<SquareScript>().CheckMine();//Up and Left
				Found += Grid[col+1][row-1].gameObject.GetComponent<SquareScript>().CheckMine();//Down and Right
				if(Grid[col+1][row-1].gameObject.GetComponent<SquareScript>().CheckMine()==0)
					ChecLocs.Add(new Vector2(col+1,row-1));

				//Found += Grid[col+1][row+1].gameObject.GetComponent<SquareScript>().CheckMine();//Up and Right

				
				
			}
		} else { //Only LEft and Right no good
			if(col == 0){ //Left
				Found += Grid[col][row+1].gameObject.GetComponent<SquareScript>().CheckMine();//Up
				if(Grid[col][row+1].gameObject.GetComponent<SquareScript>().CheckMine()==0)
					ChecLocs.Add(new Vector2(col,row+1));

				Found += Grid[col][row-1].gameObject.GetComponent<SquareScript>().CheckMine();//Down
				if(Grid[col][row-1].gameObject.GetComponent<SquareScript>().CheckMine()==0)
					ChecLocs.Add(new Vector2(col,row-1));

				//Found += Grid[col-1][row].gameObject.GetComponent<SquareScript>().CheckMine();//Left
				Found += Grid[col+1][row].gameObject.GetComponent<SquareScript>().CheckMine();//Right
				if(Grid[col+1][row].gameObject.GetComponent<SquareScript>().CheckMine()==0)
					ChecLocs.Add(new Vector2(col+1,row));

				//Found += Grid[col-1][row-1].gameObject.GetComponent<SquareScript>().CheckMine();//Down and left
				//Found += Grid[col-1][row+1].gameObject.GetComponent<SquareScript>().CheckMine();//Up and Left
				Found += Grid[col+1][row-1].gameObject.GetComponent<SquareScript>().CheckMine();//Down and Right
				if(Grid[col+1][row-1].gameObject.GetComponent<SquareScript>().CheckMine()==0)
					ChecLocs.Add(new Vector2(col+1,row-1));

				Found += Grid[col+1][row+1].gameObject.GetComponent<SquareScript>().CheckMine();//Up and Right
				if(Grid[col+1][row+1].gameObject.GetComponent<SquareScript>().CheckMine()==0)
					ChecLocs.Add(new Vector2(col+1,row+1));


				
				
			}else if(col == width-1){ //Right
				Found += Grid[col][row+1].gameObject.GetComponent<SquareScript>().CheckMine();//Up
				if(Grid[col][row+1].gameObject.GetComponent<SquareScript>().CheckMine()==0)
					ChecLocs.Add(new Vector2(col,row+1));

				Found += Grid[col][row-1].gameObject.GetComponent<SquareScript>().CheckMine();//Down
				if(Grid[col][row-1].gameObject.GetComponent<SquareScript>().CheckMine()==0)
					ChecLocs.Add(new Vector2(col,row-1));

				Found += Grid[col-1][row].gameObject.GetComponent<SquareScript>().CheckMine();//Left
				if(Grid[col-1][row].gameObject.GetComponent<SquareScript>().CheckMine()==0)
					ChecLocs.Add(new Vector2(col-1,row));

				//Found += Grid[col+1][row].gameObject.GetComponent<SquareScript>().CheckMine();//Right
				Found += Grid[col-1][row-1].gameObject.GetComponent<SquareScript>().CheckMine();//Down and left
				if(Grid[col-1][row-1].gameObject.GetComponent<SquareScript>().CheckMine()==0)
					ChecLocs.Add(new Vector2(col-1,row-1));

				Found += Grid[col-1][row+1].gameObject.GetComponent<SquareScript>().CheckMine();//Up and Left
				if(Grid[col-1][row+1].gameObject.GetComponent<SquareScript>().CheckMine()==0)
					ChecLocs.Add(new Vector2(col-1,row+1));

				//Found += Grid[col+1][row-1].gameObject.GetComponent<SquareScript>().CheckMine();//Down and Right
				//Found += Grid[col+1][row+1].gameObject.GetComponent<SquareScript>().CheckMine();//Up and Right

				
			}else{ //All Good
				Found += Grid[col][row+1].gameObject.GetComponent<SquareScript>().CheckMine();//Up
				if(Grid[col][row+1].gameObject.GetComponent<SquareScript>().CheckMine()==0)
					ChecLocs.Add(new Vector2(col,row+1));

				Found += Grid[col][row-1].gameObject.GetComponent<SquareScript>().CheckMine();//Down
				if(Grid[col][row-1].gameObject.GetComponent<SquareScript>().CheckMine()==0)
					ChecLocs.Add(new Vector2(col,row-1));

				Found += Grid[col-1][row].gameObject.GetComponent<SquareScript>().CheckMine();//Left
				if(Grid[col-1][row].gameObject.GetComponent<SquareScript>().CheckMine()==0)
					ChecLocs.Add(new Vector2(col-1,row));

				Found += Grid[col+1][row].gameObject.GetComponent<SquareScript>().CheckMine();//Right
				if(Grid[col+1][row].gameObject.GetComponent<SquareScript>().CheckMine()==0)
					ChecLocs.Add(new Vector2(col+1,row));

				Found += Grid[col-1][row-1].gameObject.GetComponent<SquareScript>().CheckMine();//Down and left
				if(Grid[col-1][row-1].gameObject.GetComponent<SquareScript>().CheckMine()==0)
					ChecLocs.Add(new Vector2(col-1,row-1));

				Found += Grid[col-1][row+1].gameObject.GetComponent<SquareScript>().CheckMine();//Up and Left
				if(Grid[col-1][row+1].gameObject.GetComponent<SquareScript>().CheckMine()==0)
					ChecLocs.Add(new Vector2(col-1,row+1));

				Found += Grid[col+1][row-1].gameObject.GetComponent<SquareScript>().CheckMine();//Down and Right
				if(Grid[col+1][row-1].gameObject.GetComponent<SquareScript>().CheckMine()==0)
					ChecLocs.Add(new Vector2(col+1,row-1));

				Found += Grid[col+1][row+1].gameObject.GetComponent<SquareScript>().CheckMine();//Up and Right
				if(Grid[col+1][row+1].gameObject.GetComponent<SquareScript>().CheckMine()==0)
					ChecLocs.Add(new Vector2(col+1,row+1));

								
			}



		}
		if (Found == 0) {
			foreach (Vector2 vec in ChecLocs) {
				Grid [(int)vec.x] [(int)vec.y].gameObject.GetComponent<SquareScript> ().Click ();


			}
		}
		ChecLocs.Clear ();
		picked++;
		return Found;


	}
}
