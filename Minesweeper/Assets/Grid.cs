using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class Grid : MonoBehaviour {
	int Height = 10;/// <summary>
	/// The width.
	/// </summary>
	int Width = 10;
	Vector2 temp;
	Vector2 Rolf; 
	GameObject[,] Squares;

	public GameObject squareSprite;
	GameObject clone;

	void Awake(){
		Squares = new GameObject[10,10];


	}
	// Use this for initialization
	void Start () {
		temp = this.gameObject.transform.position;
		//Needs to make several boxes
		float tempx = temp.x;
		float tempy = temp.y;

		for (int x = 0; x<Height; x++) {
			for (int y = 0; y<Width; y++) {
//				Rolf.x = (float)(tempx + (1 * x));
//				Rolf.y = (float)(tempy + (1 * y));
				//Rolf = new Vector2(tempx + (1 * x), tempy + (1 * y));


				Squares[x,y] = (GameObject)Instantiate(squareSprite, new Vector2(tempx + (1 * x), tempy + (1 * y)), this.gameObject.transform.rotation);

				//Squares[x,y] = (GameObject)Instantiate(squareSprite, Rolf, this.gameObject.transform.rotation);
				Squares[x,y].GetComponent<SquareScript>().Loc = new Vector2(x,y);
			}
		}

		for (int x = 0; x<Height; x++) {
			for (int y = 0; y<Width; y++) {
				Debug.Log("Square at " + Squares[x,y] + " " + x + " " + y);
			}

		}
	}
	
	// Update is called once per frame
	void Update () {
		//Debug.Log (Squares [0, 0].gameObject.GetComponent<SquareScript> ().CheckMine ());
	}

	public int BombCheck(Vector2 SquareLoc){

		int found = 0;
		float tempx, tempy;
		tempx = SquareLoc.x;
		tempy = SquareLoc.y;
		Debug.Log (tempx + " " +  (int)tempy + " Picked");
		//needs to do CheckMine in all 8 directions of the Square
		if (SquareLoc.x == 0) {
			if (SquareLoc.y == 0) {

				found += Squares [1, 1].GetComponent<SquareScript> ().CheckMine ();
				found += Squares [1, 0].GetComponent<SquareScript> ().CheckMine ();
				found += Squares [0, 1].GetComponent<SquareScript> ().CheckMine ();
			} else if (SquareLoc.y == 9) {
				found += Squares [1, 8].GetComponent<SquareScript> ().CheckMine ();
				found += Squares [1, 9].GetComponent<SquareScript> ().CheckMine ();
				found += Squares [0, 8].GetComponent<SquareScript> ().CheckMine ();
			} else { //bottom row but not and edge
				found += Squares [(int)tempx + 1, (int)tempy + 1].GetComponent<SquareScript> ().CheckMine ();// Above to Right
				//found += Squares [(int)tempx - 1, (int)tempy + 1].GetComponent<SquareScript> ().CheckMine (); //Above to LEft
				found += Squares [(int)tempx + 1, (int)tempy].GetComponent<SquareScript> ().CheckMine (); //Right
				//found += Squares [(int)tempx - 1, (int)tempy].GetComponent<SquareScript> ().CheckMine (); //left
				//found += Squares[(int)tempx,(int)tempy-1].GetComponent<SquareScript>().CheckMine();//below
				found += Squares [(int)tempx, (int)tempy + 1].GetComponent<SquareScript> ().CheckMine ();//above

				//found += Squares[(int)tempx-1,(int)tempy-1].GetComponent<SquareScript>().CheckMine();//below to left
				//found += Squares[(int)tempx-1,(int)tempy+1].GetComponent<SquareScript>().CheckMine(); //below to right


			}
		


		} else if (SquareLoc.x == 9) {
			if (SquareLoc.y == 0) {
				found += Squares [9, 1].GetComponent<SquareScript> ().CheckMine ();
				found += Squares [8, 0].GetComponent<SquareScript> ().CheckMine ();
				found += Squares [8, 1].GetComponent<SquareScript> ().CheckMine ();
			} else if (SquareLoc.y == 9) {
				found += Squares [8, 8].GetComponent<SquareScript> ().CheckMine ();
				found += Squares [8, 9].GetComponent<SquareScript> ().CheckMine ();
				found += Squares [9, 8].GetComponent<SquareScript> ().CheckMine ();
			} else { //1-8 This block is on the top row but not on the edge
				//found += Squares[(int)tempx+1,(int)tempy+1].GetComponent<SquareScript>().CheckMine();// Above to Right
				found += Squares[(int)tempx-1,(int)tempy+1].GetComponent<SquareScript>().CheckMine(); //Above to LEft
				//found += Squares [(int)tempx + 1, (int)tempy].GetComponent<SquareScript> ().CheckMine (); //Right
				found += Squares [(int)tempx - 1, (int)tempy].GetComponent<SquareScript> ().CheckMine (); //left
				found += Squares [(int)tempx, (int)tempy - 1].GetComponent<SquareScript> ().CheckMine ();//below
				found += Squares[(int)tempx,(int)tempy+1].GetComponent<SquareScript>().CheckMine();//above
				
				found += Squares [(int)tempx - 1, (int)tempy - 1].GetComponent<SquareScript> ().CheckMine ();//below to left
				//found += Squares [(int)tempx  + 1, (int)tempy -1].GetComponent<SquareScript> ().CheckMine (); //below to right

			}

		} else {
			if (SquareLoc.y == 0) {
				found += Squares [(int)tempx + 1, (int)tempy + 1].GetComponent<SquareScript> ().CheckMine ();// Above to Right
				found += Squares[(int)tempx-1,(int)tempy+1].GetComponent<SquareScript>().CheckMine(); //Above to LEft
				found += Squares [(int)tempx + 1, (int)tempy].GetComponent<SquareScript> ().CheckMine (); //Right
				found += Squares [(int)tempx - 1, (int)tempy].GetComponent<SquareScript> ().CheckMine (); //left
				//found += Squares [(int)tempx, (int)tempy - 1].GetComponent<SquareScript> ().CheckMine ();//below
				found += Squares [(int)tempx, (int)tempy + 1].GetComponent<SquareScript> ().CheckMine ();//above
				//found += Squares [(int)tempx - 1, (int)tempy - 1].GetComponent<SquareScript> ().CheckMine ();//below to left
				//found += Squares [(int)tempx  + 1, (int)tempy -1].GetComponent<SquareScript> ().CheckMine (); //below to right

			} else if (SquareLoc.y == 9) {

				//found += Squares [(int)tempx + 1, (int)tempy + 1].GetComponent<SquareScript> ().CheckMine ();// Above to Right
				//found += Squares[(int)tempx-1,(int)tempy+1].GetComponent<SquareScript>().CheckMine(); //Above to LEft
				found += Squares [(int)tempx + 1, (int)tempy].GetComponent<SquareScript> ().CheckMine (); //Right
				found += Squares [(int)tempx - 1, (int)tempy].GetComponent<SquareScript> ().CheckMine (); //left
				found += Squares [(int)tempx, (int)tempy - 1].GetComponent<SquareScript> ().CheckMine ();//below
				//found += Squares [(int)tempx, (int)tempy + 1].GetComponent<SquareScript> ().CheckMine ();//above
				found += Squares [(int)tempx - 1, (int)tempy - 1].GetComponent<SquareScript> ().CheckMine ();//below to left
				found += Squares [(int)tempx  + 1, (int)tempy -1].GetComponent<SquareScript> ().CheckMine (); //below to right

			} else {
				found += Squares [(int)tempx + 1, (int)tempy + 1].GetComponent<SquareScript> ().CheckMine ();// Above to Right
				found += Squares[(int)tempx-1,(int)tempy+1].GetComponent<SquareScript>().CheckMine(); //Above to LEft
				found += Squares [(int)tempx + 1, (int)tempy].GetComponent<SquareScript> ().CheckMine (); //Right
				found += Squares [(int)tempx - 1, (int)tempy].GetComponent<SquareScript> ().CheckMine (); //left
				found += Squares [(int)tempx, (int)tempy - 1].GetComponent<SquareScript> ().CheckMine ();//below
				found += Squares [(int)tempx, (int)tempy + 1].GetComponent<SquareScript> ().CheckMine ();//above
			
				found += Squares [(int)tempx - 1, (int)tempy - 1].GetComponent<SquareScript> ().CheckMine ();//below to left
				found += Squares [(int)tempx  + 1, (int)tempy -1].GetComponent<SquareScript> ().CheckMine (); //below to right
		
			}
		}



		return found;
	}
}
