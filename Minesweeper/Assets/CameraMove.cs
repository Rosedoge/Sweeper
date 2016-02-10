using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class CameraMove : MonoBehaviour {
	int theScreenWidth, theScreenHeight;
	int Boundary = 50, speed = 5;
	int maxX, maxY;
	public GameObject Controller;
	Scene curScene;
	// Use this for initialization
	void Start () 
	{
		curScene = SceneManager.GetActiveScene ();
		Controller = GameObject.FindGameObjectWithTag ("GameController");
		theScreenWidth = Screen.width;
		theScreenHeight = Screen.height;
		if (curScene.name == "HardMineSweeper") {
			this.gameObject.transform.position = new Vector3 (Controller.gameObject.GetComponent<HardSweeper> ().width / 2,
				Controller.gameObject.GetComponent<HardSweeper> ().height / 2,
				-10f);
			maxX = Controller.gameObject.GetComponent<HardSweeper> ().width;
			maxY = Controller.gameObject.GetComponent<HardSweeper> ().height;
		}else if (curScene.name == "MineSweeper") {
			this.gameObject.transform.position = new Vector3 (Controller.gameObject.GetComponent<Grid2> ().width / 2,
				Controller.gameObject.GetComponent<Grid2> ().height / 2,
				-10f);
			maxX = Controller.gameObject.GetComponent<Grid2> ().width;
			maxY = Controller.gameObject.GetComponent<Grid2> ().height;
		}
	}
	void Update() 
	{
		if (Input.mousePosition.x > theScreenWidth - Boundary)
		{
			//gameObject.transform.position.x += speed * Time.deltaTime; // move on +X axis
			if(this.gameObject.transform.position.x < maxX)
				gameObject.transform.position = new Vector2(gameObject.transform.position.x + speed * Time.deltaTime,
			                                            	gameObject.transform.position.y);
		}
		if (Input.mousePosition.x < 0 + Boundary)
		{
			if(this.gameObject.transform.position.x > -3)
				gameObject.transform.position = new Vector2(gameObject.transform.position.x - speed * Time.deltaTime,
				                                            gameObject.transform.position.y);
		}
		if (Input.mousePosition.y > theScreenHeight - Boundary)
		{
			if(this.gameObject.transform.position.y < maxY)
			gameObject.transform.position = new Vector2(gameObject.transform.position.x ,
			                                            gameObject.transform.position.y + speed * Time.deltaTime); // move on +Z axis
		}
		if (Input.mousePosition.y < 0 + Boundary)
		{
			if(this.gameObject.transform.position.y > -3)
			gameObject.transform.position = new Vector2(gameObject.transform.position.x ,
			                                            gameObject.transform.position.y - speed * Time.deltaTime);
		}

		gameObject.transform.position = new Vector3(gameObject.transform.position.x ,
		                                            gameObject.transform.position.y, -10f);
	}   

}