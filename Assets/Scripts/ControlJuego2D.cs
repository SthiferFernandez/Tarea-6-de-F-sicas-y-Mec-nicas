using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class ControlJuego2D : MonoBehaviour
{
    [Header("Puntaje")]
    // Aquí se guarda el puntaje actual del jugador.
    private int puntajeActual = 0;

    // Cantidad total de coleccionables que hay en la escena.
    [SerializeField] private int totalColeccionables = 3;

    // Aquí se guarda cuántos coleccionables faltan.
    private int coleccionablesFaltantes;

    [Header("Vidas")]
    // Cantidad de vidas del jugador.
    [SerializeField] private int vidas = 3;

    // Lugar donde el jugador aparece cuando pierde una vida.
    [SerializeField] private Transform puntoRespawn;

    // Aquí se coloca el objeto del jugador.
    [SerializeField] private GameObject jugador;

    [Header("Textos del HUD")]
    // Texto que muestra el puntaje.
    [SerializeField] private TextMeshProUGUI textoPuntaje;

    // Texto que muestra cuántos coleccionables faltan.
    [SerializeField] private TextMeshProUGUI textoFaltantes;

    // Texto que muestra las vidas.
    [SerializeField] private TextMeshProUGUI textoVidas;

    [Header("Textos de resultado")]
    // Texto que aparece cuando el jugador gana.
    [SerializeField] private GameObject textoGanar;

    // Panel que aparece cuando el jugador pierde todas las vidas.
    [SerializeField] private GameObject panelGameOver;

    // Texto que muestra el puntaje final.
    [SerializeField] private TextMeshProUGUI textoPuntajeFinal;

    [Header("Audio")]
    // AudioSource que va a reproducir los sonidos.
    [SerializeField] private AudioSource audioSource;

    // Sonido cuando recoge un coleccionable.
    [SerializeField] private AudioClip sonidoRecoger;

    // Sonido cuando pierde una vida o muere.
    [SerializeField] private AudioClip sonidoMorir;

    // Esto evita que el juego siga funcionando después de ganar o perder.
    private bool juegoTerminado = false;

    private void Start()
    {
        // Al iniciar, los faltantes son iguales al total de coleccionables.
        coleccionablesFaltantes = totalColeccionables;

        // Ocultamos el texto de ganar.
        textoGanar.SetActive(false);

        // Ocultamos el panel de Game Over.
        panelGameOver.SetActive(false);

        // Actualizamos los textos de la pantalla.
        ActualizarHUD();
    }

    public void SumarPunto()
    {
        // Si el juego ya terminó, no suma más puntos.
        if (juegoTerminado) return;

        // Suma 1 punto.
        puntajeActual++;

        // Resta 1 coleccionable faltante.
        coleccionablesFaltantes--;

        // Reproduce el sonido de recoger.
        if (audioSource != null && sonidoRecoger != null)
        {
            audioSource.PlayOneShot(sonidoRecoger);
        }

        // Actualiza el HUD.
        ActualizarHUD();

        // Si ya recogió todos los coleccionables, gana.
        if (coleccionablesFaltantes <= 0)
        {
            juegoTerminado = true;
            StartCoroutine(GanarYReiniciar());
        }
    }

    public void PerderVida()
    {
        // Si el juego ya terminó, no hace nada.
        if (juegoTerminado) return;

        // Reproduce el sonido de morir.
        if (audioSource != null && sonidoMorir != null)
        {
            audioSource.PlayOneShot(sonidoMorir);
        }

        // Resta una vida.
        vidas--;

        // Actualiza el HUD.
        ActualizarHUD();

        // Si todavía quedan vidas, el jugador vuelve al inicio.
        if (vidas > 0)
        {
            RespawnearJugador();
        }
        else
        {
            // Si ya no quedan vidas, aparece Game Over.
            juegoTerminado = true;
            MostrarGameOver();
        }
    }

    private void RespawnearJugador()
    {
        // Mueve el jugador al punto de respawn.
        jugador.transform.position = puntoRespawn.position;

        // Le quita la velocidad para que no siga cayendo o moviéndose raro.
        Rigidbody2D rb = jugador.GetComponent<Rigidbody2D>();
        rb.linearVelocity = Vector2.zero;
    }

    private void ActualizarHUD()
    {
        // Actualiza el texto del puntaje.
        textoPuntaje.text = "Puntaje: " + puntajeActual;

        // Actualiza cuántos faltan.
        textoFaltantes.text = "Faltan: " + coleccionablesFaltantes;

        // Actualiza las vidas.
        textoVidas.text = "Vidas: " + CrearCorazones();
    }

    private string CrearCorazones()
    {
        // Crea corazones dependiendo de cuántas vidas quedan.
        string corazones = "";

        for (int i = 0; i < vidas; i++)
        {
            corazones += "♥";
        }

        return corazones;
    }

    private IEnumerator GanarYReiniciar()
    {
        // Muestra el mensaje de ganar.
        textoGanar.SetActive(true);

        // Espera 2 segundos.
        yield return new WaitForSeconds(2f);

        // Reinicia la escena.
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void MostrarGameOver()
    {
        // Muestra el panel de Game Over.
        panelGameOver.SetActive(true);

        // Muestra el puntaje final.
        textoPuntajeFinal.text = "Puntaje final: " + puntajeActual;
    }

    public void Reintentar()
    {
        // Recarga la escena actual.
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}