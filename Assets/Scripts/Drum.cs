using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drum : MonoBehaviour
{
	[SerializeField]
	KeyCode _key;
	[SerializeField]
	float _distanceThreshold;
	[SerializeField]
	float _forceMultiplier;
	[SerializeField]
	float _distanceMultiplier;

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

			if(distance > _distanceThreshold)
			{
				continue;
			}

			var force = direction.normalized * _forceMultiplier * (_distanceMultiplier / distance);
			cloud.GetComponent<Rigidbody>().AddForce(force);
		}
	}
}
