using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindSwipe : MonoBehaviour {

    public Texture Tex1;
    public Texture Tex2;
    public Renderer rend;
    public float rate;

    private bool running = false;

    // Use this for initialization
    void Start () {
        rend = GetComponent<Renderer>();
	}
	
	// Update is called once per frame
	void Update () {
        if (!running)
            StartCoroutine(Wait(Random.Range(0.0f, 0.5f)));
	}

    IEnumerator WindStartFade()
    {
        rend.material.SetTexture("_DissolveTexture", Tex1);
        float i = 0;
        while (i<1)
        {
            i += Time.deltaTime * rate;

            rend.material.SetFloat("_DissolveAmount", Mathf.Lerp(0, 1, i));
            yield return new WaitForEndOfFrame();
        }
        StartCoroutine(WindEndFade());
    }

    IEnumerator WindEndFade()
    {
        rend.material.SetTexture("_DissolveTexture", Tex2);
        float i = 0;
        while (i < 1)
        {
            i += Time.deltaTime * rate;

            rend.material.SetFloat("_DissolveAmount", Mathf.Lerp(1, 0, i));

            yield return new WaitForEndOfFrame();
        }
        running = false;
    }

    IEnumerator Wait(float seconds)
    {
        running = true;
        yield return new WaitForSeconds(seconds);
        StartCoroutine(WindStartFade());
    }
}
