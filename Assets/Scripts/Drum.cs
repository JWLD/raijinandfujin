using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drum : MonoBehaviour
{
	[SerializeField]
	KeyCode _key;

	[SerializeField]
	List<AudioClip> _sounds = new List<AudioClip>();

    public Animator anim;
    public Animation soundwave;

	float _forceMultiplier = 25f;
	float _distanceMultiplier = 2f;
	AudioSource _audioSource;
	CloudSpawner _cloudSpawner;

	// Use this for initialization
	void Start ()
	{
		_cloudSpawner = FindObjectOfType<CloudSpawner>();
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

		anim.Play("Soundwave");

		var clouds = _cloudSpawner.GetAllClouds();

		foreach(var cloud in clouds)
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
