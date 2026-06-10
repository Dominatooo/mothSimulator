using UnityEngine;

public class LightSource : MonoBehaviour
{
    public Light unityLight; // Tu przeciągnij Point Light w inspektorze
    public bool isActive = true;

    // TA METODA MUSI BYĆ TUTAJ (wewnątrz klasy)
    public void SetIntensity(float value)
    {
        if (unityLight != null)
            unityLight.intensity = value;
    }

    public float GetIntensity()
    {
        return isActive ? unityLight.intensity : 0f;
    }
}