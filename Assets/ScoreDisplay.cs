using UnityEngine;
using UnityEngine.UI;

public class ScoreDisplay : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Text myText = GetComponent<Text>();
        myText.text = ScoreKeeper.actualScore.ToString();
        ScoreKeeper.ResetScore();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
