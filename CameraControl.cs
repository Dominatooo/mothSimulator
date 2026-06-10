using UnityEngine;

public class CameraController : MonoBehaviour
{
    void Start()
    {
        // Blokuje kursor na środku i go chowa
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        // Pozwala odblokować myszkę Escapem, żeby np. użyć sliderów
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.lockState = CursorLockMode.None;
        }
        
        // Jeśli klikniesz w ekran, myszka znowu zniknie
        if (Input.GetMouseButtonDown(0) && Cursor.lockState == CursorLockMode.None)
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
    }
}