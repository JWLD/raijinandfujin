using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerSpawner : MonoBehaviour
{
	[SerializeField]
	int _multiplier16x9;
	[SerializeField]
	GameObject _triggerPrefab;
	[SerializeField]
	bool _doSpawning;

	// Use this for initialization
	void Start()
	{
		var numHorizontal = 16 * _multiplier16x9;
		var numVertical = 9 * _multiplier16x9;

		var map = FindObjectOfType<Map>();
		map.Init(numHorizontal, numVertical);

		if(!_doSpawning)
		{
			return;
		}

		var distance = 17.7777777f / numHorizontal;
		var size = 1f / _multiplier16x9;
		var horizontalOffset = 8.888888f - (size / 2f);
		var verticalOffset = 5f - (size / 2f);

		for(var i = 0; i < numHorizontal; i++)
		{
			for(var j = 0; j < numVertical; j++)
			{
				var position = new Vector3((i * distance) - horizontalOffset, (j * distance) - verticalOffset);
				var trigger = Instantiate(_triggerPrefab, position, Quaternion.identity, transform);
				trigger.transform.localScale = new Vector3(size, size, 10f);
				var gridTrigger = trigger.GetComponent<GridTrigger>();
				gridTrigger.x = i;
				gridTrigger.y = j;
			}
		}
	}

	// Update is called once per frame
	void Update()
	{

	}
}
