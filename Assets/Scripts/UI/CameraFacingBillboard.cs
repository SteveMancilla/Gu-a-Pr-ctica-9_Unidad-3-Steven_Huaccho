using UnityEngine;

/// <summary>
/// Fuerza a este objeto a mirar siempre hacia la camara principal
/// Esencial para UI en World Space (Diegética)
/// </summary>

public class CameraFacingBillboard : MonoBehaviour
{
    private Camera _mainCamera;

    void Start()
    {
        _mainCamera = Camera.main;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        //LateUpdate asegura que la camra ya se haya movido antes de rotar la UI
        //Hacemos que el objeto mire en la misma direccion que la camara (paralelo),
        //
        transform.forward = _mainCamera.transform.forward;
    }
}
