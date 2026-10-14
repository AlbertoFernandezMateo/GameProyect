using UnityEngine;

public class ControlJugadorInputSystemAll : MonoBehaviour
{
    public float velocidad;

    private Rigidbody2D fisicas;

    private float entradaX;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //Obtengo el componente Rigidbody del player para modificar LinearVelocity
        fisicas = GetComponent<Rigidbody2D>();


        
    }

    // Update is called once per frame
    //    void Update()
    //    {
    //        Vector2 position = transform.position;

    //        position.x = position.x + 0.1f;

    //        transform.position = position;



    //    }

    private void FixedUpdate()
    {
        entradaX = Input.GetAxis("Horizontal"); // devuelve 1 si pulsa derecha y -1 si pulsa izquierda
        if (fisicas != null)
        {
            fisicas.linearVelocity = new Vector2(entradaX * velocidad, fisicas.linearVelocity.y);
        }

        fisicas.linearVelocity = new Vector2(entradaX * velocidad,fisicas.linearVelocity.y); // lo movemos en esta direccion a una determinada velocidad


    }
}
