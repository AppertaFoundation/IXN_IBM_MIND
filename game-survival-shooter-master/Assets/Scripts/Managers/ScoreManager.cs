using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    [SerializeField]
    private Text scoreText;

    public static int score;

    void Awake()
    {
        score = 0;
    }

    void Update()
    {
        scoreText.text = "Score: " + score;
    }
}
