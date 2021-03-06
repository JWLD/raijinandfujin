using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class GameController : MonoBehaviour
{
	[SerializeField]
	float _threshold;

	Map _map;

	int _numFields, _numTowns;

	[SerializeField]
	Image uiGrainFill;
	[SerializeField]
	Image uiWaterFill;
	[SerializeField]
	Canvas _gameOverCanvas;
	[SerializeField]
	Text _gameOverText;
	[SerializeField]
	AudioClip _winSound;
	[SerializeField]
	AudioClip _loseSound;
	[SerializeField]
	AudioMixerGroup _audioMixerGroup;

	bool _gameOver = false;

	// Use this for initialization
	void Start()
	{
		_map = FindObjectOfType<Map>();

		for(var i = 0; i < _map.Width; i++)
		{
			for(var j = 0; j < _map.Height; j++)
			{
				if(_map.MapData[i, j].Zone == Zone.Field)
				{
					_numFields++;
				}
				else if(_map.MapData[i, j].Zone == Zone.Town)
				{
					_numTowns++;
				}
			}
		}
	}

	// Update is called once per frame
	void Update()
	{
		if(_gameOver)
		{
			return;
		}

		var fieldsAboveThreshold = 0;
		var townsAboveThreshold = 0;

		for(var i = 0; i < _map.Width; i++)
		{
			for(var j = 0; j < _map.Height; j++)
			{
				if(_map.MapData[i, j].Value >= _threshold)
				{
					if(_map.MapData[i, j].Zone == Zone.Field)
					{
						fieldsAboveThreshold++;
					}
					else if(_map.MapData[i, j].Zone == Zone.Town)
					{
						townsAboveThreshold++;
					}
				}
			}
		}

		// update UI elements
		uiGrainFill.fillAmount = fieldsAboveThreshold / (_numFields * _threshold);
		uiWaterFill.fillAmount = townsAboveThreshold / (_numTowns * _threshold);
		Debug.LogFormat("total towns: {0}, townsAbove {1}, fieldsAbove {2}", _numTowns, townsAboveThreshold, fieldsAboveThreshold);

//		Debug.Log(townsAboveThreshold);
		if(fieldsAboveThreshold >= _numFields * _threshold)
		{
			EndGame(true);
		}
		if(townsAboveThreshold >= _numTowns * _threshold)
		{
			EndGame(false);
		}
	}

	void EndGame(bool won)
	{
		var audioSource = gameObject.AddComponent<AudioSource>();
		audioSource.clip = won ? _winSound : _loseSound;
		audioSource.outputAudioMixerGroup = _audioMixerGroup;
		audioSource.Play();

		_gameOverText.text = won ? @"<size=120>Sincere Congratulations</size>
Your villagers will be
well fed for many days" :
@"<size=120>Game Over</size>
Your villagers' navels
have been eaten";
		_gameOverCanvas.gameObject.SetActive(true);
		Time.timeScale = 0.1f;
		_gameOver = true;
	}

	public void Restart()
	{
		Time.timeScale = 1f;
		SceneManager.LoadScene(SceneManager.GetActiveScene().name);
	}
}
