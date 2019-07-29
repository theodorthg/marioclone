using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

    public Transform target;
    public Transform leftBounds;
    public Transform rightBounds;

	// Führt die Kamera nicht ruckartig zur neuen Position, sondern innerhalb einer angegebenen Zeit.
	public float smoothDampTime = 0.15f;
	private Vector3 smoothDampVelocity = Vector3.zero; // BeBi

    private float camWidth, camHeight, levelMinX, levelMaxX;

	// Use this for initialization
	void Start () {
		
		camHeight = Camera.main.orthographicSize * 2;
		camWidth = camHeight * Camera.main.aspect;

		/* Sprites sind für Mario 32 Pixel breit. Das könnte man fest setzen. 
		 * Für einen allgemeineren Ansatz tun wir dies nicht.
		 * 
		 * leftBounds und rightBounds werden und müssen auch unbedingt an die Kamera gebunden werden,
		 * in dem sie aus der Projekt-Hirarchie auf die entsprechenden Felder der Main-Kamera im Inspektor 
		 * gezogen werden. Anfangs sind sie mit Bounds und Bounds(1) benannt. Sie müssen 
		 * (passiert später im Tutorial 3) noch umbenannt werden.
		 * 
		 * Aber für andere Breiten muss herausgefunden werden, wie breit diese sind. 
		 * Die x-Position (und y-Position) ist immer in der Mitte des Spielers. 
		 * Daher muss durch 2 geteilt werden.
		 */
		float leftBoundsWidth = leftBounds.GetComponentInChildren<SpriteRenderer> ().bounds.size.x / 2;
		float rightBoundsWidth = rightBounds.GetComponentInChildren<SpriteRenderer> ().bounds.size.x / 2;

		// Die Kamera soll sich so auf den Spieler fokussieren, dass dieser immer rechts und links gleich
		// viel Platz hat, sie also in der "Mitte des Levels" steht und dem Spieler sanft folgt.
		levelMinX = leftBounds.position.x + leftBoundsWidth + (camWidth / 2);
		levelMaxX = rightBounds.position.x - rightBoundsWidth - (camWidth / 2);

	}

	// Update is called once per frame
	void Update () {

		// Wenn die Instance (also das Exemplar) von target existiert, also erzeugt wurde.
		if (target) {
			/* Hier wird sicher gestellt, dass die Grenzen die Linke und rechte Kante der Kamera sind
			 * Es wird das Maximum berechnet aus der linken Grenze und dem 
			 * Ergebnis aus dem Minimum aus rechter Grenze und aktueller Position.
			 * Ist die aktuelle Position über der rechten Grenze, wird die Kamera "smooth" nachgezogen.
			 * Das passiert mit SmoothDamp.Wirkt wie ein Gimbal.
			 * Ebenso, wenn der Spieler die linke Grenze der Kamera-Box überschritten hat.
			 */

			float targetX = Mathf.Max (levelMinX, Mathf.Min (levelMaxX, target.position.x));
			float x = Mathf.SmoothDamp (transform.position.x, targetX, ref smoothDampVelocity.x, smoothDampTime);

			// Hier wird die neue Position der Kamera tatsächlich angewandt
			transform.position = new Vector3(x, transform.position.y, transform.position.z);

		}

	}
}
