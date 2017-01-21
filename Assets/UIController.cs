using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour {

	[SerializeField]
	Text cloudCountText;

	public void UpdateCloudCount()
	{
		int count = GameObject.Find("cloudHolder").transform.childCount;
		cloudCountText.text = "Clouds: " + count;
	}
}
