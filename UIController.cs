using UnityEngine;
using UnityEngine.InputSystem;

public class UIController : MonoBehaviour
{
    [Header("Ustawienia Kamery")]
    public GameObject freeLookCamera; 

    private bool isUIMode = false;
    private MonoBehaviour cameraInput;

    void Start()
    {
        if (freeLookCamera != null)
        {
            cameraInput = freeLookCamera.GetComponent("CinemachineInputProvider") as MonoBehaviour;
        }

        isUIMode = false;
        ApplyInterfaceState();
    }

    void Update()
    {
        if (Keyboard.current == null) return;

        if (Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            isUIMode = !isUIMode;
            ApplyInterfaceState();
        }

        // Jeśli jesteśmy w trybie UI, pilnujemy żeby system nie próbował 
        // w żaden sposób narzucać pozycji kursorowi
        if (isUIMode)
        {
            if (Cursor.lockState != CursorLockMode.None)
            {
                Cursor.lockState = CursorLockMode.None;
            }
        }
    }

    void ApplyInterfaceState()
    {
        if (isUIMode)
        {
            // ODBLOKOWANIE KURSORA DLA SYSTEMU
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

            // CAŁKOWITE ODCIĘCIE CINEMACHINE OD MYSZKI
            // Wyłączamy cały komponent dostarczający dane wejściowe,
            // co natychmiast przerywa pętlę centrowania kursora w silniku.
            if (cameraInput != null)
            {
                cameraInput.enabled = false;
            }
        }
        else
        {
            // TRYB GRY
            // Najpierw pozwalamy aparatowi przejąć kontrolę, a potem blokujemy
            if (cameraInput != null)
            {
                cameraInput.enabled = true;
            }

            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }
}