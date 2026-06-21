using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuInicioController : MonoBehaviour
{
    // Esta función carga la escena principal del juego.
    public void Jugar()
    {
        SceneManager.LoadScene("PrototipoJugador2D");
    }

    // Esta función sirve para salir del juego.
    // En Unity Editor no se cierra, pero en el juego compilado sí.
    public void Salir()
    {
        Application.Quit();

        // Esto solo sirve para comprobar en consola cuando estamos probando en Unity.
        Debug.Log("Saliendo del juego...");
    }
}