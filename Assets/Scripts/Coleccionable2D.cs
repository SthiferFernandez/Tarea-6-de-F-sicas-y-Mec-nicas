using UnityEngine;

public class Coleccionable2D : MonoBehaviour
{
    [Header("Control del juego")]
    // Aquí guardamos la referencia al GameManager.
    [SerializeField] private ControlJuego2D controlJuego;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Revisamos si el objeto que tocó el coleccionable fue el jugador.
        if (collision.CompareTag("Player"))
        {
            // Sumamos un punto al contador.
            controlJuego.SumarPunto();

            // Destruimos el coleccionable para que desaparezca.
            Destroy(gameObject);
        }
    }
}