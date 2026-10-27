using System;
using UnityEngine;
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]


public class ControlEnemigoVertical : MonoBehaviour
{
    [Header("Movimiento")]

    public float velocity = 2f;
    private Vector2 posicionInicial;

    public Vector2 posicionFinal;

    private bool movimientoHorizontal = true;

    [SerializeField] private float tolerancia = 0.01f;
    private SpriteRenderer renderizador;
    private Animator animador;






    private Rigidbody2D fisica;


    private void Awake()
    {
        fisica = GetComponent<Rigidbody2D>();
        renderizador = GetComponent<SpriteRenderer>();
        animador = GetComponent<Animator>();



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
        if (velocity <= 0)
        {
            return;
        }

        if (movimientoHorizontal)
        {
            posicionObjetivo = posicionFinal;
        }
        else
        {
            posicionObjetivo = posicionInicial;
        }
        //Vector2 posicionObjetivo = movimientoHaciaArriba ? posicionFinal : posicionInicial  //otra forma de hacer el if

        if (Vector2.Distance(fisica.position, posicionObjetivo) <= tolerancia) //comprobamos que la posicion objetivo es menor o igual que la tolerancia que hemos creado
        {

            movimientoHorizontal = !movimientoHorizontal;
            posicionObjetivo = movimientoHorizontal ? posicionFinal : posicionInicial;
        }

        fisica.MovePosition(Vector2.MoveTowards(fisica.position, posicionObjetivo, velocity * Time.fixedDeltaTime));
        OrientarSprite();
        AnimarJugador();




    }

    private void AnimarJugador()
    {
        if (animador == null) //ponemos que no pueda ser nulo
        {
            return;
        }
        if(posicionInicial.x != posicionFinal.x)
        {
            animador.Play("EnemigoAndando");
        }
    }

    private void OrientarSprite()
    {
        if (renderizador == null)
        {
            return;
        }
            
        if (movimientoHorizontal)
        {
            renderizador.flipX = true;

        }
        else 
        {
            
            renderizador.flipX = false;
        }

    }

}
