using UnityEngine;
using UnityEngine.UI;

public class ScoreKeeper : MonoBehaviour {

    public Text score;
    public static int actualScore = 0;

    private void Start()
    {
        ResetScore();
        score.text = actualScore.ToString();
    }

    public void Score(int points)
    {
        actualScore += points;
        score.text = actualScore.ToString();

    }

    public static void ResetScore()
    {
        actualScore = 0;
        //score.text = actualScore.ToString();
    }


}
