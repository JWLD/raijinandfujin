using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drum : MonoBehaviour
{
	[SerializeField]
	KeyCode _key;
	float _forceMultiplier = 5f;
	float _distanceMultiplier = 50f;

	Cloud[] _clouds;

	// Use this for initialization
	void Start ()
	{
		_clouds = FindObjectsOfType<Cloud>();
	}

	// Update is called once per frame
	void Update ()
	{
		if(Input.GetKeyDown(_key))
		{
			PushClouds();
		}
	}

	void PushClouds()
	{
		foreach(var cloud in _clouds)
		{
			var direction = cloud.transform.position - transform.position;

			var distance = direction.magnitude;

			var force = direction.normalized * _forceMultiplier * (_distanceMultiplier / distance);
			cloud.GetComponent<Rigidbody>().AddForce(force);
		}
	}

	void OnMouseDown()
	{
		PushClouds();
	}
}
