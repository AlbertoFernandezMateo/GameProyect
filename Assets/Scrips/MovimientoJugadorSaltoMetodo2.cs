using UnityEngine;
using UnityEngine.InputSystem;
[RequireComponent(typeof(Rigidbody2D))]

public class MovimientoJugadorSaltoMetodo2 : MonoBehaviour
{

    [Header("Movimiento")]
    public float velocity;

    [SerializeField] private InputActionReference actionMover;

    [Header("Salto")]

    [SerializeField] private InputActionReference actionSaltar;
    public float fuerzaImpulso = 8f; // fuerza aplicada al saltar

    [Header("Chequeo de Suelo")]

    [SerializeField] private float distanciaSuelo = 0.2f; //el radio de la esfera que tendra bola

    [SerializeField] private Transform bola; //objeto vacio en los pies

    [SerializeField] private LayerMask capaSuelo;


    private Rigidbody2D fisica;
    private Vector2 entradaMovimiento;
    private bool estaTocandoSuelo;
    private bool pendienteSalto;




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
        entradaMovimiento= actionMover.action.ReadValue<Vector2>();

        estaTocandoSuelo = Physics2D.OverlapCircle(bola.position, distanciaSuelo, capaSuelo);

        if (actionSaltar.action.WasPressedThisFrame() && estaTocandoSuelo) //devuelve un true si se pulsa el boton espacio
        {
            pendienteSalto = true;
        }

    }
    private void FixedUpdate()
    {
        fisica.linearVelocity = new Vector2(entradaMovimiento.x * velocity, fisica.linearVelocity.y); // lo movemos en esta direccion a una determinada velocidad


        if (pendienteSalto)
        {
            fisica.AddForce(Vector2.up * fuerzaImpulso, ForceMode2D.Impulse); //multiplicamos el vector2.up (es automaticamente (0,1) por la fuerza de impulso
        }

        pendienteSalto = false;

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


    private void OnDrawGizmosSelected()
    {
        if(bola != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawSphere(bola.position, distanciaSuelo);
        }
    }
}
