using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyByBoundary : MonoBehaviour {

	[SerializeField]
	UIController uiController;

	void OnTriggerExit(Collider other)
	{
		other.GetComponent<Cloud>().Remove();
		uiController.UpdateCloudCount();
		print(GameObject.Find("cloudHolder").transform.childCount);
	}
}
