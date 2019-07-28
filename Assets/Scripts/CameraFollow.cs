using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

    public Transform target;
    public Transform leftBounds;
    public Transform rightBounds;

	public float smoothDampTime = 0.15f;
	private float smoothDampVelocity = 0.0f; //Vector3.zero; BeBi

    private float camWidth, camHeight, levelMinX, levelMaxX;

	// Use this for initialization
	void Start () {
		camHeight = Camera.main.orthographicSize * 2;
		camWidth = camHeight * Camera.main.aspect;

		float leftBoundsWidth = leftBounds.GetComponentInChildren<SpriteRenderer> ().bounds.size.x;
		float rightBoundsWidth = rightBounds.GetComponentInChildren<SpriteRenderer> ().bounds.size.x;

		levelMinX = leftBounds.position.x + leftBoundsWidth + (camWidth / 2);
		levelMaxX = rightBounds.position.x - rightBoundsWidth - (camWidth / 2);



	}

	// Update is called once per frame
	void Update () {
		if (target) {
			float targetX = Mathf.Max (levelMinX, Mathf.Min (levelMaxX, target.position.x));
			float x = Mathf.SmoothDamp (transform.position.x, targetX, ref smoothDampVelocity, smoothDampTime);

			transform.position = new Vector3 (x, transform.position.y, transform.position.z);
		}

	}
}
