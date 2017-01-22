using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class ThunderSound : MonoBehaviour
{
	[SerializeField]
	float _minWaitTime = 20f;
	[SerializeField]
	float _maxWaitTime = 40f;
	[SerializeField]
	List<AudioClip> _clips = new List<AudioClip>();
	[SerializeField]
	AudioMixerGroup _audioMixerGroup;

	AudioSource _audioSource;

	// Use this for initialization
	void Start()
	{
		_audioSource = gameObject.AddComponent<AudioSource>();
		_audioSource.outputAudioMixerGroup = _audioMixerGroup;
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
