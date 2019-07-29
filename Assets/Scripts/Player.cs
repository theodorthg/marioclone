using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

	public Vector2 velocity;
	private bool walk, walk_left, walk_right, jump;
	public LayerMask wallMask = 0; // muss im Player-Inspektor auf Default gesetzt werden.


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

			/* 
			 * Das Attribut scale des Player-Objekts ist hier die Richtung [1 für rechts, -1 für links]
			 * Je nachdem, ob eine Kollision stattfand, ändert sich die Position je nach Laufrichtung oder
			 * sie bleibt an der Stelle, wo die Kollision stattfindet.
			 */
			pos = CheckWallRays (pos, scale.x); // BeBi
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


	Vector3 CheckWallRays(Vector3 pos, float direction) {

		/*
		 * Richtung ist entweder 1 (rechts) oder -1 (links). Die pos.x ist immer in der x-Mitte des Players.
		 * Daher wird die Richtung mit 0,4 multipliziert und zu der aktuellen Position addiert.
		 * Der Player ist eine Einheit breit.
		 * Das Ergebnis für die x-Position liegt noch knapp innerhalb des Spielers, 
		 * weil andernfalls Kollisionen entstünden, wenn gar keine vorhanden sind.
		 * Das gleiche gilt für die y-Position.  Der Player ist 2 Einheiten hoch. Es wird also 
		 * knapp am oberen Rand des Spielers "gemessen". 
		 */

		Vector2 originTop = new Vector2(pos.x + direction * 0.4f, pos.y + 1f - 0.2f);
		Vector2 originMiddle = new Vector2 (pos.x + direction * 0.4f, pos.y);
		/* 
		 * Ähnlich wie originTop, nur das bei y nun knapp 1 Einheit abgezogen werden muss.
		 * y wird nach unten hin abgezogen, noch oben hin addiert (ganz logisch).
		 */
		Vector2 originBottom = new Vector2(pos.x + direction * 0.4f, pos.y - 1f + 0.2f);

		/*
		 * Start ist die berechnete Stelle für Top / Middle und Bottom. Richtung ist die x-Richtung, 
		 * Entfernung richtet sich nach der Geschwindigkeit der Player-Bewegung.
		 * Man hätte das auch in ein Array packen können, um in einer Schleife alle verschiedenen Rays
		 * abfragen können. Aber das ist hier erst mal die einfachste Variante - ohne Optimierung.
		 * 
		 * Bei einer Kollision wird ein sog. Collider-Objekt erzeugt. Andernfalls ist es null.
		 * Es muss dann auch die Player-Position an der vorherigen Stelle bleiben 
		 * [in unserem Fall die Anzahl der Pixel, die sonst in die entsprechende Richtung gelaufen worden
		 * wäre], falls es zu einer Kollision kam.
		 * Das passiert, damit man nicht durch Objekte hindurchläuft.
		 */
		RaycastHit2D wallTop = Physics2D.Raycast (originTop, new Vector2 (direction, 0), velocity.x * Time.deltaTime, wallMask);
		RaycastHit2D wallMiddle = Physics2D.Raycast (originMiddle, new Vector2 (direction, 0), velocity.x * Time.deltaTime, wallMask);
		RaycastHit2D wallBottom = Physics2D.Raycast (originBottom, new Vector2 (direction, 0), velocity.x * Time.deltaTime, wallMask);

		if (wallTop.collider != null || wallMiddle.collider != null || wallBottom.collider != null) {
			pos.x -= velocity.x * Time.deltaTime * direction;
		}

		/* Entweder die ursprünglich übergebene Position wird zurückgegeben (falls keine Kollision 
		 * stattfand). Oder die neu berechnete Position um die entsprechende Anzahl der Pixel korrigiert,
		 * damit man nicht durch Objekte läuft, sondern davor stehen bleibt. 
		 * Die y-Position wird momentan noch nicht geprüft. 
		 */

		return pos;
	}

}
