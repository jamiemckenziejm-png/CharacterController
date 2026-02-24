using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using Unity.VisualScripting;

public class MenuController : MonoBehaviour
{
    [SerializeField] string playScene = "SampleScene";
    [SerializeField] string mainMenuScene = "StartScene";

    [Tooltip("drag in an options menu panel, if one exists")] // Tooltip for the optionsMenuPanel field in the Unity Inspector
    [SerializeField] GameObject optionsMenuPanel; // Reference to the options menu panel GameObject

    [Tooltip("Drag in a pause menu panel, if one exists")] // Tooltip for the pauseMenuPanel field in the Unity Inspector
    [SerializeField] GameObject pauseMenuPanel; // Reference to the pause menu panel GameObject

    [SerializeField] bool IsPauseMenuAvailable = false; // Flag to indicate if the pause menu is available
    [HideInInspector] public static bool IsGamePaused = false; // Static flag to indicate if the game is currently paused


    PlayerInput playerInput; // Reference to the PlayerInput component
    InputAction escapeAction; // Reference to the InputAction for the Escape key

    private void OnEnable()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player"); // Find the player GameObject by tag
        if (player != null)
        {
            playerInput = player.GetComponent<PlayerInput>(); // Get the PlayerInput component from the player GameObject
            var map = playerInput.currentActionMap; // Get the current action map from the PlayerInput component
            escapeAction = map.FindAction("Escape", true); // Find the "Escape" action in the action map
        }
    }

    private void Update()
    {
        PauseMenu(); // Call the method to handle the pause menu panel
    }

    public void PauseMenu()
    {
        if (IsPauseMenuAvailable)
        {
            if (escapeAction.triggered)
            {
                if (IsGamePaused)
                {
                    Resume();
                }
                else
                {
                    Pause();
                }
            }
        }
    }

    public void OptionsMenuClose()
    {
        optionsMenuPanel.SetActive(false); // Deactivate the options menu panel
    }

    public void OptionsMenuOpen()
    {
        optionsMenuPanel.SetActive(true); // Activate the options menu panel
    }

    public void Pause()
    {
        Cursor.visible = true; // Show the cursor when the game is paused
        pauseMenuPanel.SetActive(true); // Activate the pause menu panel
        Time.timeScale = 0f; // Set the time scale to 0 to pause the game
        IsGamePaused = true; // Set the IsGamePaused flag to true
    }

    public void Resume()
    {
        Cursor.visible = false; // Hide the cursor when resuming the game
        pauseMenuPanel.SetActive(false); // Deactivate the pause menu panel
        Time.timeScale = 1f; // Set the time scale back to 1 to resume the game
        IsGamePaused = false; // Set the IsGamePaused flag to false
    }

    public void ReturnToMainMenu()
    {
        Resume(); // Ensure the game is resumed before returning to the main menu
        Cursor.visible = true; // Show the cursor when returning to the main menu
        SceneManager.LoadScene(mainMenuScene); // Load the specified scene for the main menu
    }

    public void StartGame()
    {
        Cursor.visible = false; // Hide the cursor when starting the game
        SceneManager.LoadScene(playScene); // Load the specified scene for playing the game
    }

    public void QuitGame()
    {
        Debug.Log("Quit Game"); // Log a message to the console when quitting the game
        Application.Quit(); // Quit the application
    }
}
