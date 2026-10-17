using UnityEngine;

public class Ball : MonoBehaviour {

    [SerializeField] private float initialVelocity = 4f;
    [SerializeField] private float velocityMultiplier = 1.2f;
    
    // Referencia al prefab de tu explosión de partículas
    [SerializeField] private GameObject explosionPrefab; 

    private Rigidbody2D ballRb;

    void Start() {
        ballRb = GetComponent<Rigidbody2D>();
        Launch();
    }

    public void Launch() { // Cambiar 'private' por 'public'
    float xVelocity = Random.Range(0, 2) == 0 ? 1 : -1;
    float yVelocity = Random.Range(0, 2) == 0 ? 1 : -1;
    ballRb.linearVelocity = new Vector2(xVelocity, yVelocity) * initialVelocity;
}

    private void OnCollisionEnter2D(Collision2D collision) {
        if(collision.gameObject.CompareTag("Paddle")) {
            ballRb.linearVelocity *= velocityMultiplier;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.CompareTag("Gol1")) {
            // Pasamos la posición del "arco" o de la colisión para asegurar el lugar exacto
            CrearExplosion(collision.transform.position); 
            GameManager.Instance.Paddle2Scored();
            GameManager.Instance.Restart();
            Launch();
        } else if (collision.gameObject.CompareTag("Gol2")) {
            CrearExplosion(collision.transform.position); 
            GameManager.Instance.Paddle1Scored();
            GameManager.Instance.Restart();
            Launch();
        }
    }

    // Modificado para recibir la ubicación exacta del gol
    private void CrearExplosion(Vector3 posicionGol) {
        if (explosionPrefab != null) {
            // Creamos la explosión en la portería, no en la pelota que va a teletransportarse
            GameObject explosion = Instantiate(explosionPrefab, posicionGol, Quaternion.identity);
            
            // Destruimos el objeto de partículas de la escena después de 1.5 segundos
            Destroy(explosion, 1.5f);
        }
    }
}