using UnityEngine;

public class OptionsState : UIState
{
    // A dónde volver: MainMenuState o PauseMenuState (o lo que sea)
    public UIState ReturnState { get; set; }

    public OptionsState(UIManager uiManager) : base(uiManager) { }

    public override void Enter()
    {
        Debug.Log("Entrando al estado de Opciones");
        // Mostrar solo el panel de Opciones
        m_uiManager.mainMenuPanel.SetActive(false);
        m_uiManager.pauseMenuPanel.SetActive(false);
        m_uiManager.inGameHudPanel.SetActive(false);
        m_uiManager.optionsPanel.SetActive(true);

        Time.timeScale = 0f;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public override void Exit()
    {
        Debug.Log("Saliendo del estado de Opciones");
        m_uiManager.optionsPanel.SetActive(false);
    }
}