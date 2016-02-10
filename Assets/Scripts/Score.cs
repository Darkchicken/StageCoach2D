using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Score : MonoBehaviour {

    Text scoreText;
    int score = 0;
	// Use this for initialization
	void Start ()
    {
        scoreText = this.GetComponent<Text>();
        scoreText.text = "Score: "+score;
	}
	
	// Update is called once per frame
	public void UpdateScore()
    {
        score++;
        scoreText.text = "Score: " + score;
    }
}
