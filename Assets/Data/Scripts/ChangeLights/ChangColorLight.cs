using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;
using UnityEngine.UIElements;

public class ChangColorLight : MonoBehaviour
{

    public Light mainLight = null;
    public Gradient ligthColors = new Gradient();
    public float timeLimit = 0.0f;



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        mainLight = FindObjectOfType<Light>();

        if (mainLight != null && mainLight.type == UnityEngine.LightType.Directional)
        {
            Debug.Log("Se encontró una luz direccional: " + mainLight.name);
        }
        else
        {
            Debug.Log("No se encontró ninguna luz direccional en la escena.");
        }
        //asignar referencia del metodo time
        timeLimit = LevelManager.Instance.CurrentTimePercentage ;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //por cada paso, se actualiza el color
        if (mainLight != null && ligthColors != null)
        {
            //asignar referencia del metodo time
            timeLimit = LevelManager.Instance.CurrentTimePercentage;

            // Cambia el color de la luz basado en el valor del float
            mainLight.color = ligthColors.Evaluate(timeLimit);

            Debug.Log($"Time Limit {timeLimit}");

        }
    }
}
