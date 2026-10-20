using UnityEngine;
using UnityEngine.InputSystem;
[RequireComponent(typeof(Rigidbody2D))]
public class MovimientoJugador : MonoBehaviour
{

    [Header("Movimiento")]
    public float velocity;

    [SerializeField]private InputActionReference actionMover;

    [Header("Salto")]

    [SerializeField] private InputActionReference actionSaltar;
    public float fuerzaImpulso = 8f; // fuerza aplicada al saltar
    [SerializeField] private LayerMask capaSuelo;




    private Rigidbody2D fisica; //Accedo a rigidbody y se si se ha pulsado una tecla 

    private Vector2 entradaX;
    private bool pendienteSalto;
    private bool estaTocandoSuelo;
    public float longitudRaycast = 1.2f;

    private void Awake() //se ejecuta antes que el start y se ejecuta aunque no este activo
    {
        //Detengo el componente Rigitbody del player para modificar posteriormente su linearvelocity
        fisica = GetComponent<Rigidbody2D>();
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        entradaX = actionMover.action.ReadValue<Vector2>();

        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, longitudRaycast, capaSuelo);

         estaTocandoSuelo = hit.collider != null;

        Color colorRayo = estaTocandoSuelo ? Color.green : Color.red; //Si esta tocando suelo es verde y si no toca suelo es rojo

        Debug.DrawRay(transform.position, Vector2.down * longitudRaycast, colorRayo);

        if (actionSaltar.action.WasPressedThisFrame() && estaTocandoSuelo) //devuelve un true si se pulsa el boton espacio
        {
            pendienteSalto = true;
        }
        
    }

    private void OnEnable()
    {
        //MOVER
        if ((actionMover == null) || actionMover.action == null)
        {
            Debug.LogError("Asigna Mapa de acción Jugador/Mover en AccionMover");
            enabled = false;
            return;
        }
        actionMover.action.Enable();

        //SALTAR
        if ((actionSaltar == null) || actionSaltar.action == null)
        {
            Debug.LogError("Asigna Mapa de acción Jugador/Saltar en AccionSaltar");
            enabled = false;
            return;
        }
        actionSaltar.action.Enable();



    }

    private void OnDisable()
    {


        //MOVER
        if (actionMover != null && actionMover.action != null)
        {
            actionMover.action.Disable();
        }


        //SALTAR
        if (actionSaltar != null && actionSaltar.action != null)
        {
            actionSaltar.action.Disable();
        }
    }

    private void FixedUpdate()
    {
        fisica.linearVelocity = new Vector2(entradaX.x * velocity, fisica.linearVelocity.y); // lo movemos en esta direccion a una determinada velocidad

        if (pendienteSalto)
        {
            fisica.AddForce(Vector2.up * fuerzaImpulso, ForceMode2D.Impulse); //multiplicamos el vector2.up (es automaticamente (0,1) por la fuerza de impulso
        }

        pendienteSalto = false;

    }
}
