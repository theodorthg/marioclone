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

		// Hier können die Eingaben mittels GetKey abgefragt werden.
		bool input_left = Input.GetKey (KeyCode.LeftArrow);
		bool input_right = Input.GetKey (KeyCode.RightArrow);

		// Springen schon dann, wenn die Taste noch nicht losgelassen wurde.
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

		/*
		 * OR-Operator wird benutzt, um zu sehen, ob entweder nach rechts ODER links gedrückt wurde.
		 * Funktioniert im Moment (Tutorial 3 / 12) nur mit den Cursor-Tasten. Im Emulator Citra kann man
		 * das passend belegen (auf die Buttons oder das Steuerkreuz).
		 * Später werden hier auch die Buttons und das Steuerkreuz vom 3DS abgefragt.
		 */ 
	
		walk = input_left || input_right;
		// Wenn nach links gedrückt wurde UND NICHT nach rechts 
		walk_left = (input_left) && !(input_right);
		// Wenn nach rechts gedrückt wurde UND NICHT nach links
		walk_right = (input_right) && !(input_left);
		// Wenn jump gedrückt wurde (Space oder später Y)
		jump = jump_key;

	}

}
