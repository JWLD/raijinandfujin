using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
	[SerializeField]
	float _threshold;

	Map _map;

	// Use this for initialization
	void Start()
	{
		_map = FindObjectOfType<Map>();
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

		var thresholdNumber = _map.Width * _map.Height * _threshold;
		if(fieldsAboveThreshold >= thresholdNumber)
		{
			Debug.Log("YOU WIN!");
		}
		if(townsAboveThreshold >= thresholdNumber)
		{
			Debug.Log("YOU LOSE!");
		}
	}
}
