using UnityEngine;
using System.Collections.Generic;

public class MothController : MonoBehaviour
{
    [Header("Settings")]
    public float speed = 5f;
    public float rotationSpeed = 10f;
    public float spiralAngle = 85f; // Kąt utrzymywany względem światła

    private LightSource targetLight;

    void Update()
    {
        FindStrongestLight();
        if (targetLight != null)
        {
            FlyTowardsLight();
        }
    }

   void FindStrongestLight()
{
    // Najprostsza metoda - pobiera wszystkie żarówki w scenie
    // Ignorujemy ostrzeżenia, przy 5 żarówkach nie ma to znaczenia
    LightSource[] allLights = FindObjectsOfType<LightSource>(); 
    
    float maxInfluence = -1f;
    LightSource bestLight = null;

    foreach (var light in allLights)
    {
        if (!light.isActive) continue;

        float dist = Vector3.Distance(transform.position, light.transform.position);
        
        // Zabezpieczenie przed dzieleniem przez zero
        if (dist < 0.2f) continue; 

        // I = Jasność / Dystans^2
        float influence = light.GetIntensity() / (dist * dist);

        if (influence > maxInfluence)
        {
            maxInfluence = influence;
            bestLight = light;
        }
    }
    targetLight = bestLight;
}

    void Start() {
    speed += Random.Range(-1f, 1f);
    rotationSpeed += Random.Range(-0.5f, 0.5f);
    }

    void FlyTowardsLight()
    {
        Vector3 dirToLight = (targetLight.transform.position - transform.position).normalized;
        
        // Obliczamy wektor prostopadły do światła, aby stworzyć orbitę/spiralę
        // Używamy Vector3.up jako osi obrotu dla uproszczenia w 3D
        Vector3 tangentDir = Vector3.Cross(dirToLight, Vector3.up);

        // Mieszamy wektor prostopadły (orbita) z wektorem "do światła" (zacieśnianie spirali)
        // Kąt spiralAngle steruje tym, jak bardzo ćma "leci w ogień"
        float radAngle = spiralAngle * Mathf.Deg2Rad;
        Vector3 desiredDir = Vector3.RotateTowards(dirToLight, tangentDir, radAngle, 0f);

        // Płynny obrót i ruch
        Quaternion targetRotation = Quaternion.LookRotation(desiredDir);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }
}