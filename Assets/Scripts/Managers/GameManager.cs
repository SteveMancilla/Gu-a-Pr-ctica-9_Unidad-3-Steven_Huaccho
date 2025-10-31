using System.Collections; //para Corrutinas
using UnityEngine;

/// <summary>
/// Gestiona el estado principal del juego, como jugar, ganar o perder
/// Implementa el patrón Singleton para un acceso global único
/// </summary>
public class GameManager : MonoBehaviour
{
    // Singleton Pattern
    public static GameManager Instance { get; private set; }

    // Estado del Juego
    public enum GameState { Playing, Victory, Loss }
    private GameState _currentState;

    //Configuracion de variables que se usa en: HandleObjectiveActivated
    [Header("Gameplay Settings")]
    [SerializeField] private int _objectivesToWin = 3;
    private int _objectivesCompleted = 0;


    [Header("Timer Settings")]
    [SerializeField] private float _timeLimit = 60f; // tiempo total en segundos
    //private bool _isCountingDown = false;            // controla si la cuenta está activa
    private bool _playerWon = false;                 // bandera para detener el conteo si se gana


    private void Awake()
    {
        // Configuración del Singleton
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        // opcional: 
        // DontDestroyOnLoad(gameObject); 
        // si necesitas que persista entre escenas
    }

    // suscripción a eventos
    private void OnEnable()
    {
        GameEvents.OnObjetiveActivated += HandleObjectiveActivated;
    }

    private void OnDisable()
    {
        // importante: para evitar fugas de memoria
        GameEvents.OnObjetiveActivated -= HandleObjectiveActivated;
    }

    private void HandleObjectiveActivated()
    {
        if (_currentState != GameState.Playing) return;
        _objectivesCompleted++;
        Debug.Log($"Objetivo completado. Progreso: {_objectivesCompleted}/{_objectivesToWin}");

        if (_objectivesCompleted >= _objectivesToWin)
        {
            _playerWon = true; // detener el temporizador
            UIManager.Instance?.ShowTimer(false);
            ChangeState(GameState.Victory);
        }
    }

    private void Start()
    {
        // estado inicial del juego
        ChangeState(GameState.Playing);

        StartCoroutine(CountdownTimer());
    }

    public void ChangeState(GameState newState)
    {
        if (_currentState == newState) return;

        _currentState = newState;
        Debug.Log($"Nuevo estado de juego: {_currentState}");

        switch (_currentState)
        {
            case GameState.Playing:
                // logica para cuando empieza el juego
                break;
            case GameState.Victory:
                // logica para cuando se gana
                StartCoroutine(VictorySequence());
                break;
            case GameState.Loss:
                // logica para cuando se pierde
                StartCoroutine(LossSequence());
                break;
        }
    }

    /// <summary>
    /// Corrutina que gestiona la secuencia de eventos cuando el jugador gana.
    /// </summary>
    
    private IEnumerator CountdownTimer()
    {
        //_isCountingDown = true;
        float remainingTime = _timeLimit;

        // Inicializar visual
        UIManager.Instance?.ShowTimer(true);
        UIManager.Instance?.SetTimer(remainingTime);

        while (remainingTime > 0 && !_playerWon)
        {
            Debug.Log($"Tiempo restante: {remainingTime} segundos");
            yield return new WaitForSeconds(1f);
            remainingTime--;
            UIManager.Instance?.SetTimer(remainingTime);
            // (opcional) puedes actualizar un texto en pantalla desde aquí si luego añades uno
        }

        //_isCountingDown = false;

        if (!_playerWon && remainingTime <= 0)
        {
            Debug.Log("Tiempo agotado. Derrota.");
            UIManager.Instance?.ShowTimer(false);
            ChangeState(GameState.Loss);
        }
    }
    private IEnumerator VictorySequence()
    {
        Debug.Log("SECUENCIA DE VICTORIA INICIADA");

        // desactivar el control del jugador (opcional, pero buena práctica)
        var playerController = FindFirstObjectByType<FirstPersonController>();
        if (playerController != null)
        {
            playerController.enabled = false;
        }

        //las dos lineas de codigo siguiente ya lo hacemos arriba con el if
        // desactivar el control del jugador (opcional, pero buena práctica)
        //FindFirstObjectByType<FirstPersonController>().enabled = false;

        // 1: espera 1 segundo
        yield return new WaitForSeconds(1f);

        // 2: muestra un panel de victoria en la UI
        Debug.Log("mostrando UI de Victoria...");
        // suponiendo que UIManager tiene una referencia a este panel
        if (UIManager.Instance != null)
        {
            UIManager.Instance.ShowVictoryPanel();
        }

        // 3. espera 3 segundos más
        yield return new WaitForSeconds(3f);

        // 4. carga la escena del Menú Principal
        Debug.Log("Volviendo al Menú Principal...");
        //SceneManager.LoadScene("MainMenuScene"); // Si tienes esta escena
        if (UIManager.Instance != null)
        {
            UIManager.Instance.victoryPanel.SetActive(false);
            UIManager.Instance.inGameHudPanel.SetActive(true);
            UIManager.Instance.ChangeState(UIManager.Instance.InGameState);
        }

        // 5: reactivar control del jugador
        if (playerController != null)
            playerController.enabled = true;

        // o cambiar el Estado al MainMenu
        //UIManager.Instance.ChangeState(UIManager.Instance.MainMenuState);
    }

    private IEnumerator LossSequence()
    {
        Debug.Log("SECUENCIA DE DERROTA INICIADA");

        var playerController = FindFirstObjectByType<FirstPersonController>();
        if (playerController != null)
            playerController.enabled = false;

        yield return new WaitForSeconds(1f);

        if (UIManager.Instance != null)
        {
            UIManager.Instance.inGameHudPanel.SetActive(false);
            UIManager.Instance.ShowLossPanel();   // 👈 usa el panel de derrota
        }

        yield return new WaitForSeconds(3f);

        // Opción simple: volver al menú (sin cambiar de escena)
        if (UIManager.Instance != null)
        {
            // Oculta derrota y muestra menú
            if (UIManager.Instance.lossPanel) UIManager.Instance.lossPanel.SetActive(false);
            if (UIManager.Instance.mainMenuPanel) UIManager.Instance.mainMenuPanel.SetActive(true);
            UIManager.Instance.ChangeState(UIManager.Instance.MainMenuState);
        }

        // Reactiva control (si decides seguir en la misma escena)
        if (playerController != null)
            playerController.enabled = true;
    }
}