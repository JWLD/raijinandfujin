using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cloud : MonoBehaviour
{
	[SerializeField]
	float _fadeOutTime = 3f;
	[SerializeField]
	float _fadeInTime = 2f;
	[SerializeField]
	GameObject _rain;

	AudioSource _audioSource;

	// Use this for initialization
	void Start()
	{
		_audioSource = GetComponent<AudioSource>();
		_audioSource.volume = 0f;
		StartCoroutine(FadeAudioIn());
	}

	public void ActivateRain()
	{
		_rain.SetActive(true);
	}

	// Update is called once per frame
	void Update()
	{

	}

	IEnumerator FadeAudioIn()
	{
		var timeLeft = _fadeInTime;
		while(timeLeft > 0f)
		{
			_audioSource.volume = Mathf.Lerp(0f, 0.1f, 1f - (timeLeft / _fadeInTime));
			timeLeft -= Time.deltaTime;
			yield return null;
		}
	}

	public void Remove()
	{
		StartCoroutine(FadeThenDestroy());
	}

	IEnumerator FadeThenDestroy()
	{
		var timeLeft = _fadeOutTime;
		while(timeLeft > 0f)
		{
			_audioSource.volume = Mathf.Lerp(0.1f, 0f, 1f - (timeLeft / _fadeOutTime));
			timeLeft -= Time.deltaTime;
			yield return null;
		}
		Destroy(gameObject);
	}
}
