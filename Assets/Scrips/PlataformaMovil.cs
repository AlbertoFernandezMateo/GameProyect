using UnityEngine;
[RequireComponent (typeof(Rigidbody2D))]
public class PlataformaMovil : MonoBehaviour
{
    [SerializeField] private Transform puntoA;
    [SerializeField] private Transform puntoB;
    [SerializeField, Min(0f)] private float velocidad = 2f;

    private Rigidbody2D fisica;
    private bool haciaB = true;
    private float tolerancia = 0.001f;

    private void Awake()
    {
        fisica = GetComponent<Rigidbody2D>();
  
    }

    public Vector2 Velocidad
    {
        get; private set;
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if  (puntoA ==  null || puntoB == null)
        {
            Debug.LogError("Asigna el PuntoA y el PuntoB a la plataforma."); //nos aseguramos que la posicion de PuntoA/B no sea nulo
            return;
        }

        fisica.position = (Vector2)puntoA.position; //me aseguro que la plataforma este justo en el puntoA
    }
    private void FixedUpdate()
    {
        Vector2 destino = haciaB ? (Vector2)puntoB.position : (Vector2)puntoA.position;

        if (Vector2.Distance(fisica.position, destino)<= tolerancia)
        {
            haciaB = !haciaB;
            destino = haciaB ? (Vector2)puntoB.position : (Vector2)puntoA.position;
        }

        Vector2 siguiente = Vector2.MoveTowards(fisica.position, destino, velocidad * Time.fixedDeltaTime); //esta linea hace que el desplazamiento de la plataforma sea suave y no un salto

        fisica.MovePosition(siguiente); //realiza el movimiento
    }
    private void OnDrawGizmos()
    {
        if (puntoA == null || puntoB == null)
        {
            return;
            
        }
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(puntoA.position, puntoB.position);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
