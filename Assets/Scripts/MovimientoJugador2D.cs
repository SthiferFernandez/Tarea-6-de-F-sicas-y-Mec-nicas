using UnityEngine;

public class MovimientoJugador2D : MonoBehaviour
{
    [Header("Componentes")]
    // Este es el Rigidbody2D del jugador.
    // Sirve para moverlo usando físicas.
    [SerializeField] private Rigidbody2D rb2d;

    [Header("Movimiento")]
    // Velocidad con la que el jugador se mueve.
    [SerializeField] private float velocidadMovimiento = 6f;

    // Aquí se guarda si el jugador va a la izquierda, derecha o está quieto.
    private float entradaHorizontal;

    [Header("Salto")]
    // Fuerza con la que el jugador salta.
    [SerializeField] private float fuerzaSalto = 12f;

    // Este objeto va en los pies del jugador.
    // Sirve para saber si está tocando el suelo.
    [SerializeField] private Transform controladorSuelo;

    // Tamaño de la cajita invisible que detecta el suelo.
    [SerializeField] private Vector2 dimensionesCaja = new Vector2(0.8f, 0.3f);

    // Aquí se pone la capa del suelo y las plataformas.
    [SerializeField] private LayerMask capasSuelo;

    // Dice si el jugador está tocando el suelo.
    private bool enSuelo;

    // Guarda cuando el jugador presiona espacio para saltar.
    private bool entradaSalto;

    private void Awake()
    {
        // Busca automáticamente el Rigidbody2D del jugador.
        rb2d = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        // Lee A/D o las flechas izquierda/derecha.
        entradaHorizontal = Input.GetAxisRaw("Horizontal");

        // Revisa si el jugador presionó la barra espaciadora.
        if (Input.GetButtonDown("Jump"))
        {
            entradaSalto = true;
        }

        // Revisa si la cajita de los pies está tocando el suelo.
        // Esto evita que el jugador salte infinitamente.
        enSuelo = Physics2D.OverlapBox(
            controladorSuelo.position,
            dimensionesCaja,
            0f,
            capasSuelo
        );
    }

    private void FixedUpdate()
    {
        // Mueve al jugador hacia los lados.
        rb2d.linearVelocity = new Vector2(
            entradaHorizontal * velocidadMovimiento,
            rb2d.linearVelocity.y
        );

        // Hace que el jugador salte solo si está tocando el suelo.
        if (entradaSalto && enSuelo)
        {
            rb2d.linearVelocity = new Vector2(
                rb2d.linearVelocity.x,
                fuerzaSalto
            );
        }

        // Apaga la entrada de salto para que no se repita solo.
        entradaSalto = false;
    }

    private void OnDrawGizmosSelected()
    {
        // Dibuja una cajita amarilla en los pies del jugador.
        // Sirve para ver dónde se está detectando el suelo.
        if (controladorSuelo != null)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireCube(controladorSuelo.position, dimensionesCaja);
        }
    }
}