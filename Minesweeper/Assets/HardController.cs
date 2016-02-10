using UnityEngine;
using System.Collections;

public class HardController : MonoBehaviour {
	GameObject Menu;
	int times = 0;
	private GameObject hardGrid;
	// Use this for initialization
	void Start () {
		hardGrid = new GameObject ();
		//hardGrid = gameObject.GetComponent<HardSweeper> ();
		hardGrid = GameObject.FindGameObjectWithTag("GameController");
		Menu = GameObject.FindGameObjectWithTag ("MenuManager");
	}

	public void GameLoss(){



	}
	// Update is called once per frame
	void Update () {
	//If times gets to some #, end game screen
		if (times >= 4) {


		}

	}

	public void newGrid(GameObject oldGrid){
		hardGrid = GameObject.FindGameObjectWithTag("GameController");
		GameObject HardGrid = (GameObject)Instantiate(hardGrid, 
		                                              oldGrid.gameObject.transform.position,
		                                              this.gameObject.transform.rotation);
		oldGrid.gameObject.GetComponent<HardSweeper> ().Clean ();
		Destroy (oldGrid.gameObject);

		times += 1;
	}
}
