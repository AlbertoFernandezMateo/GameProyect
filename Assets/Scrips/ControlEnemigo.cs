using UnityEngine;
[RequireComponent(typeof(Rigidbody2D))]


public class ControlEnemigo : MonoBehaviour
{
    [Header("Movimiento")]

    public float velocity = 2f;
    private Vector2 posicionInicial;

    public Vector2 posicionFinal;

    private bool movimientoHaciaArriba = true;

    [SerializeField] private float tolerancia = 0.01f;





    private Rigidbody2D fisica;

    private void Awake() 
    {
        fisica = GetComponent<Rigidbody2D>();
    }


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        posicionInicial = fisica.position;
        
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        Vector2 posicionObjetivo;
        if(velocity <= 0)
        {
            return;
        }

        if (movimientoHaciaArriba)
        {
            posicionObjetivo = posicionFinal;
        } else
        {
            posicionObjetivo = posicionInicial;
        }
        //Vector2 posicionObjetivo = movimientoHaciaArriba ? posicionFinal : posicionInicial  //otra forma de hacer el if

        if (Vector2.Distance(fisica.position, posicionObjetivo) <= tolerancia) //comprobamos que la posicion objetivo es menor o igual que la tolerancia que hemos creado
        {

            movimientoHaciaArriba = !movimientoHaciaArriba;
            posicionObjetivo = movimientoHaciaArriba ? posicionFinal : posicionInicial;
        }

        fisica.MovePosition(Vector2.MoveTowards(fisica.position, posicionObjetivo, velocity * Time.fixedDeltaTime));

    }

}
