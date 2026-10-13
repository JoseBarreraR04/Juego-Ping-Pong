using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour {
    [SerializeField] private TMP_Text paddle1ScoreText;
    [SerializeField] private TMP_Text paddle2ScoreText;

    [SerializeField] private Transform paddle1Transform;
    [SerializeField] private Transform paddle2Transform;
    [SerializeField] private Transform ballTransform;

    private int paddleScoreUno;
    private int paddleScoreDos;

    private static GameManager instance;

    public static GameManager Instance {
        get {
            if (instance == null) {
                instance = FindAnyObjectByType<GameManager>();
            }

            return instance;
        }
    }

    public void Paddle1Scored() {
        paddleScoreUno++;
        paddle1ScoreText.text = paddleScoreUno.ToString();
    }

    public void Paddle2Scored() {
        paddleScoreDos++;
        paddle2ScoreText.text = paddleScoreDos.ToString();
    }

    public void Restart() {
        paddle1Transform.position = new Vector2(paddle1Transform.position.x, 0);
        paddle2Transform.position = new Vector2(paddle2Transform.position.x, 0);
        ballTransform.position = new Vector2(0, 0);
    }
}