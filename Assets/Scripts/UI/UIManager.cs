using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement; // Para la carga de escenas
using UnityEngine.UI;
using System.Collections;
using TMPro;

/// <summary>
/// Gestiona los estados de la UI y las transiciones entre ellos
/// Utiliza el Patrón de Diseño State, arquitectura limpia y escalable
/// Implementa un Singleton para un acceso global sencillo
/// </summary>
public class UIManager : MonoBehaviour
{
    // Singleton Pattern
    public static UIManager Instance { get; private set; }

    [Header("UI Panels")]
    public GameObject mainMenuPanel;
    public GameObject pauseMenuPanel;
    public GameObject inGameHudPanel;
    public GameObject optionsPanel;
    public GameObject victoryPanel;
    public GameObject lossPanel;

    [Header("Loading Screen")]
    public GameObject loadingScreenPanel;
    public Slider loadingBar;

    [Header("HUD")]
    public TMP_Text timerText;   // ← arrástrale TimerText desde el Inspector


    // Estados de la UI
    private UIState _currentState;
    public MainMenuState MainMenuState { get; private set; }
    public InGameState InGameState { get; private set; }
    public PauseMenuState PauseMenuState { get; private set; }
    public OptionsState OptionsState { get; private set; }

    private void Awake()
    {
        // Configuración del Singleton
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject); // Para que persista entre escenas

        // Inicialización de los estados
        MainMenuState = new MainMenuState(this);
        InGameState = new InGameState(this);
        PauseMenuState = new PauseMenuState(this);
        OptionsState   = new OptionsState(this);
    }

    private void Start()
    {
        // El estado inicial al arrancar el juego
        ChangeState(MainMenuState);
    }
    
    private void Update()
    {
        // Lógica para pausar el juego
        Keyboard keyboard = Keyboard.current;
        if (keyboard.escapeKey.wasPressedThisFrame)
        {
            if (_currentState == InGameState)
            {
                ChangeState(PauseMenuState);
            }
            else if (_currentState == PauseMenuState)
            {
                ChangeState(InGameState);
            }
        }
    }

    public void ChangeState(UIState newState)
    {
        // Salir del estado actual si existe
        _currentState?.Exit();
        
        // Entrar en el nuevo estado
        _currentState = newState;
        _currentState.Enter();
    }

    // Métodos para los botones de la UI
    public void OnPlayButtonClicked()
    {
        Debug.Log("[UI] Play clicked");
        StartCoroutine(LoadSceneWithLoading("Level_001"));
        ChangeState(InGameState);
    }

    public void OnResumeButtonClicked()
    {
        ChangeState(InGameState);
    }

    public void OnExitButtonClicked()
    {
        Debug.Log("Saliendo del juego...");
        Application.Quit();
    }

    public void OnOptionsButtonClicked()
    {
        // Recordamos a dónde volver
        OptionsState.ReturnState = _currentState;
        ChangeState(OptionsState);
    }

    public void OnOptionsBackButtonClicked()
    {
        if (OptionsState.ReturnState != null)
            ChangeState(OptionsState.ReturnState);
        else
            ChangeState(MainMenuState); // fallback
    }

    public void ShowVictoryPanel()
    {
        Debug.Log("¡Victoria! Mostrando panel de victoria.");
        // Aquí podrías activar un panel de victoria específico
        inGameHudPanel.SetActive(false);
        ShowTimer(false);
        victoryPanel.SetActive(true);
    }

    public void ShowLossPanel()
    {
        Debug.Log("Derrota. Mostrando panel de derrota.");
        // Aquí podrías activar un panel de derrota específico
        inGameHudPanel.SetActive(false);
        ShowTimer(false);
        lossPanel.SetActive(true);
    }

    public void SetTimer(float seconds)
    {
        if (!timerText) return;
        int s = Mathf.Max(0, Mathf.CeilToInt(seconds));
        int m = s / 60;
        int r = s % 60;
        timerText.text = $"{m:00}:{r:00}";
    }

    public void ShowTimer(bool on)
    {
        if (timerText) timerText.gameObject.SetActive(on);
    }

    private IEnumerator LoadSceneWithLoading(string sceneName)
    {
        if (loadingScreenPanel != null)
            loadingScreenPanel.SetActive(false);

        // Mostrar loading
        if (loadingScreenPanel) loadingScreenPanel.SetActive(true);
        if (loadingBar)
        {
            loadingBar.maxValue = 100f;
            loadingBar.value = 0f;
        }

        // Asegura que nada más quede visible
        mainMenuPanel?.SetActive(false);
        pauseMenuPanel?.SetActive(false);
        optionsPanel?.SetActive(false);
        victoryPanel?.SetActive(false);
        inGameHudPanel?.SetActive(false);

        yield return null;

        // Cargar escena asíncrona
        var op = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Single);
        op.allowSceneActivation = false; // nos permite mostrar el 0.9f primero

        float simulatedProgress = 0f;

        while (simulatedProgress < 100f)
        {
            simulatedProgress += 20f;
            if (loadingBar) loadingBar.value = simulatedProgress;
            yield return new WaitForSeconds(0.5f);
        }

        // Esperar un pequeño instante antes de activar la escena
        yield return new WaitForSeconds(0.5f);

        // Activamos la escena
        op.allowSceneActivation = true;

        // Ocultar pantalla de carga
        yield return new WaitForSeconds(0.2f);
        if (loadingScreenPanel) loadingScreenPanel.SetActive(false);

        // Activar HUD y cambiar estado a InGame
        inGameHudPanel?.SetActive(true);
        ChangeState(InGameState);
    }
}