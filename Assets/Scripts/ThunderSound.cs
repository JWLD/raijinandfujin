using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThunderSound : MonoBehaviour
{
	[SerializeField]
	float _minWaitTime = 20f;
	[SerializeField]
	float _maxWaitTime = 40f;
	[SerializeField]
	List<AudioClip> _clips = new List<AudioClip>();

	AudioSource _audioSource;

	// Use this for initialization
	void Start()
	{
		_audioSource = gameObject.AddComponent<AudioSource>();
		StartCoroutine(PlayRandomSoundAndWait());
	}

	// Update is called once per frame
	void Update()
	{
		
	}

	IEnumerator PlayRandomSoundAndWait()
	{
		while(true)
		{
			yield return new WaitForSeconds(Random.Range(_minWaitTime, _maxWaitTime));
			var clip = _clips[Random.Range(0, _clips.Count)];
			_audioSource.clip = clip;
			_audioSource.Play();
		}
	}
}
