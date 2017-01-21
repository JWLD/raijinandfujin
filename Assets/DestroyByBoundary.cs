using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyByBoundary : MonoBehaviour {

	[SerializeField]
	UIController uiController;

	void OnTriggerExit(Collider other)
	{
		Destroy(other.gameObject);
		uiController.UpdateCloudCount();
		print(GameObject.Find("cloudHolder").transform.childCount);
	}
}
