using UnityEngine;
using UnityEngine.InputSystem;

public class ControlJugadorInputSystemNew : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public InputAction moveAction;
    public float velocity;


    void Start(){
        moveAction.Enable();

        QualitySettings.vSyncCount = 0;

        Application.targetFrameRate = 30; //estamos restringiendolo a unicamente 30 fps por segundo
    }

    // Update is called once per frame
    void Update(){
        Vector2 move = moveAction.ReadValue<Vector2>();

        Vector2 position = (Vector2)transform.position + move * velocity * Time.deltaTime; //hemos casteado position a vector2 ya que position tiene Z

        transform.position = position; //asignamos la nueva posicion 


        
    }
}
