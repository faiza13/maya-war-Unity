using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class parallaxing : MonoBehaviour {
	public Transform[] layers;
	public float backgroundSize;
	//private float[] parallaxScales;
	public float smoothing = 1f;
	private float viewZone = 10;
	private float lastCameraX;
	private int rightIndex;
	private int leftIndex;
	private Transform cameraTransform;
	private Vector3 previousCamPos;

	// Use this for initialization
	void Start () {
		cameraTransform = Camera.main.transform;
		lastCameraX = cameraTransform.position.x;
		layers = new Transform[transform.childCount];
		for (int i = 0; i < transform.childCount; i++) {
			layers [i] = transform.GetChild (i);

			leftIndex = 0;
			rightIndex = layers.Length - 1;
		}
		/*previousCamPos = cam.position;
        parallaxScales = new float[backgrounds.Length];
        for (int i = 0; i < backgrounds.Length; i++) {
            parallaxScales [i] = backgrounds [i].position.z * -1;
        }*/
	}

	private void ScrollLeft(){
		int lastLeft = rightIndex;
		layers [rightIndex].position = Vector3.right * (layers [leftIndex].position.x - backgroundSize);
		leftIndex = rightIndex;
		rightIndex--;
		if (rightIndex < 0) {
			rightIndex = layers.Length - 1;
		}
	}

	private void ScrollRight(){
		int lastLeft = leftIndex;
		layers [leftIndex].position = Vector3.right * (layers [rightIndex].position.x + backgroundSize);
		rightIndex = leftIndex;
		leftIndex--;
		if (leftIndex == layers.Length) {
			rightIndex = layers.Length - 1;
		}
	}

	// Update is called once per frame
	void Update () {
		float deltaX = cameraTransform.position.x - lastCameraX;
		transform.position += Vector3.right * (deltaX * smoothing);
		lastCameraX = cameraTransform.position.x;

		if (leftIndex==0&&rightIndex==0 && leftIndex <layers.Length && rightIndex <layers.Length) {




			if(cameraTransform.position.x < (layers [leftIndex].position.x + viewZone)){
				ScrollLeft ();
			}
			if(cameraTransform.position.x > (layers [rightIndex].position.x - viewZone)){
				ScrollRight ();
			}
		}

		/*for (int i = 0; i < backgrounds.Length; i++) {
            float parallax = (previousCamPos.x - cam.position.x) * parallaxScales[i];
            float backgroundTargetPosX = backgrounds [i].position.x + parallax;
            Vector3 backgroundTargetPos = new Vector3 (backgroundTargetPosX,backgrounds [i].position.y,backgrounds [i].position.z);
            backgrounds [i].position = Vector3.Lerp (backgrounds [i].position, backgroundTargetPos,smoothing * Time.deltaTime);
        }
        previousCamPos = cam.position;*/
	}
}
