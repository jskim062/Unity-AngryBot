﻿using UnityEngine;
using System.Collections;

public class RotateAround : MonoBehaviour {
	public Transform Target;
	public float Speed = 5f;

	// Update is called once per frame
	void Update () {
		transform.RotateAround(Target.position, Vector3.up, -Speed * Time.deltaTime);
	}
}
