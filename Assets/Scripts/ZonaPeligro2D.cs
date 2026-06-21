using UnityEngine;

public class ZonaPeligro2D : MonoBehaviour
{
    [Header("Control del juego")]
    // Aquí se pone el GameManager.
    [SerializeField] private ControlJuego2D controlJuego;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Si el jugador toca la zona peligrosa, pierde una vida.
        if (collision.CompareTag("Player"))
        {
            controlJuego.PerderVida();
        }
    }
}