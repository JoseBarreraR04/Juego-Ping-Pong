using System.Collections; // Necesario para las corrutinas
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour {
    [SerializeField] private TMP_Text paddle1ScoreText;
    [SerializeField] private TMP_Text paddle2ScoreText;

    [SerializeField] private Transform paddle1Transform;
    [SerializeField] private Transform paddle2Transform;
    [SerializeField] private Transform ballTransform;

    // NUEVO: Tiempo que durará la pausa (1.5 segundos, igual que tu explosión)
    [SerializeField] private float tiempoDeExplosion = 1.5f;

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

    // Modificamos Restart para que inicie la pausa
    public void Restart() {
        StartCoroutine(RutinaRestart());
    }

    // NUEVO: Corrutina que maneja la pausa y el reinicio
    private IEnumerator RutinaRestart() {
        // 1. Detenemos la bola y la ocultamos mientras ocurre la explosión
        Rigidbody2D ballRb = ballTransform.GetComponent<Rigidbody2D>();
        if (ballRb != null) {
            ballRb.linearVelocity = Vector2.zero; // Frenar la bola
        }
        ballTransform.gameObject.SetActive(false); // Hacerla invisible

        // 2. Esperamos el tiempo de la explosión (1.5 segundos)
        yield return new WaitForSeconds(tiempoDeExplosion);

        // 3. Reiniciamos las posiciones de las raquetas y la bola
        paddle1Transform.position = new Vector2(paddle1Transform.position.x, 0);
        paddle2Transform.position = new Vector2(paddle2Transform.position.x, 0);
        ballTransform.position = new Vector2(0, 0);

        // 4. Volvemos a mostrar la bola y la disparamos
        ballTransform.gameObject.SetActive(true);
        ballTransform.GetComponent<Ball>().Launch(); 
    }
}