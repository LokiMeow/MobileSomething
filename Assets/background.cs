using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class background : MonoBehaviour {
	// Use this for initialization
    float width = 861;
    float height = 1262;
	void Start () {
        float rate = width / height;
        if(Screen.width/Screen.height>rate)
            transform.GetComponent<RectTransform>().sizeDelta = new Vector2(Screen.width, Screen.width*rate);
        else
            transform.GetComponent<RectTransform>().sizeDelta = new Vector2(Screen.height*rate, Screen.height);
	}
	
	// Update is called once per frame
	void Update () {
	}
}
