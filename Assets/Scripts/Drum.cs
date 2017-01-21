using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drum : MonoBehaviour
{
	[SerializeField]
	KeyCode _key;

	[SerializeField]
	List<AudioClip> _sounds = new List<AudioClip>();

	float _forceMultiplier = 5f;
	float _distanceMultiplier = 50f;
	AudioSource _audioSource;

	Cloud[] _clouds;

	// Use this for initialization
	void Start ()
	{
		_clouds = FindObjectsOfType<Cloud>();
		_audioSource = GetComponent<AudioSource>();
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
		var soundIndex = Random.Range(0, _sounds.Count);
		_audioSource.clip = _sounds[soundIndex];
		_audioSource.Play();

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
