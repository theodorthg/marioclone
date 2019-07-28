using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

	public Vector2 velocity;
	private bool walk, walk_left, walk_right, jump;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		CheckPlayerInput ();
		UpdatePlayerPosition ();
	}

	void UpdatePlayerPosition() {

		Vector3 pos = transform.localPosition;
		Vector3 scale = transform.localScale;

		if (walk) {
			if (walk_left) {
				pos.x -= velocity.x * Time.deltaTime;
				scale.x = -1;
			}

			if (walk_right) {
				pos.x += velocity.x * Time.deltaTime;
				scale.x = 1;
			}
		}

		transform.localPosition = pos;
		transform.localScale = scale;
	}


	void CheckPlayerInput () {

		bool input_left = Input.GetKey (KeyCode.LeftArrow);
		bool input_right = Input.GetKey (KeyCode.RightArrow);
		bool jump_key = Input.GetKeyDown (KeyCode.Space);

		/*
		bool input_left_joy = Input.GetButtonDown ("BUTTON_Y");
		bool input_right_joy = Input.GetButtonDown ("BUTTON_A");
		bool input_jump_joy = Input.GetButtonDown ("BUTTON_X");

		walk = input_left_joy || input_right_joy || input_left || input_right;
		walk_left = (input_left || input_left_joy) && !(input_right || input_right_joy);
		walk_right = (input_right || input_right_joy) && !(input_left || input_left_joy);
		jump = input_jump_joy || jump_key;
		*/
		walk = input_left || input_right;
		walk_left = (input_left) && !(input_right);
		walk_right = (input_right) && !(input_left);
		jump = jump_key;

	}

}
