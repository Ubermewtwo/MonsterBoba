using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

public class ChangColorLight : MonoBehaviour
{

    public Light mainLight = null;
    public Gradient ligthColors = new Gradient();
    public float timeLimit = 0.0f;



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        mainLight = FindObjectOfType<Light>();
        //asignar referencia del metodo time
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //por cada paso, se actualiza el color


    }
}
