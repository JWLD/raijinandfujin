using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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

//		Debug.Log(townsAboveThreshold);
		if(fieldsAboveThreshold >= _numFields * _threshold)
		{
			Debug.Log("YOU WIN!");
			_gameOverText.text = @"<size=120>Sincere Congratulation</size>
Your villagers will be
well fed for many days";
			_gameOverCanvas.gameObject.SetActive(true);
		}
		if(townsAboveThreshold >= _numTowns * _threshold)
		{
			Debug.Log("YOU LOSE!");
			_gameOverText.text = @"<size=120>Game Over</size>
Your villagers' navels
have been eaten";
			_gameOverCanvas.gameObject.SetActive(true);
		}
	}

	public void Restart()
	{
		SceneManager.LoadScene(SceneManager.GetActiveScene().name);
	}
}
