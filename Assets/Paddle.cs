using UnityEngine;
using UnityEngine.InputSystem;

public class Paddle : MonoBehaviour {
    [SerializeField] private float speed = 7f;
    [SerializeField] private bool isPaddle1;
    [SerializeField] private float yBound = 3.75f;

    void Update() {
        float movement = 0f;

        if (Keyboard.current != null) {
            if (isPaddle1) {
                // Controles para el Jugador 1 (W / S)
                if (Keyboard.current.wKey.isPressed) {
                    movement = 1.5f;
                } else if (Keyboard.current.sKey.isPressed) {
                    movement = -1.5f;
                }
            } else {
                // Controles para el Jugador 2 (Flecha Arriba / Flecha Abajo)
                if (Keyboard.current.upArrowKey.isPressed) {
                    movement = 1.5f;
                } else if (Keyboard.current.downArrowKey.isPressed) {
                    movement = -1.5f;
                }
            }
        }

        Vector2 paddlePosition = transform.position;
        paddlePosition.y = Mathf.Clamp(paddlePosition.y + movement * speed * Time.deltaTime, -yBound, yBound);
        transform.position = paddlePosition;
    }
}