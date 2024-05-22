using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameState : MonoBehaviour
{
    public static bool gameIsPaused;
    [SerializeField] GameObject pauseMenu;
    [SerializeField] GameObject gameOver;
    private bool isGameOver = false;

    void Awake()
    {
        FindObjectsOfType<Note>(true)[0].Appear(0);
    }
    void Start()
    {
        pauseMenu.SetActive(false);
        gameOver.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !isGameOver) LoadMenu();
    }

    public void Resume()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        gameIsPaused = false;
    }

    public void Pause()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        gameIsPaused = true;
    }

    public void LoadMenu()
    {
        if (gameIsPaused)
            Resume();
        else
            Pause();
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void GameOver()
    {
        gameOver.SetActive(true);
        isGameOver = true;
        var players = GameObject.FindGameObjectsWithTag("Player");
        var (player1, player2) = (players[0].GetComponent<PlayerMovement>(), players[1].GetComponent<PlayerMovement>());
        (player1.enabled, player2.enabled) = (false, false);
        var notes = GameObject.FindGameObjectsWithTag("Note");
        if (notes.Length > 0)
            notes[0].SetActive(false);
        
        //GameObject.FindWithTag("Player1").GetComponent<Player>().enabled = false;
        //GameObject.FindWithTag("Player2").GetComponent<Player>().enabled = false;
    }
    
    public void RestartCurrentScene()
    {
        // Получаем имя текущей сцены
        string currentSceneName = SceneManager.GetActiveScene().name;
        // Перезапускаем сцену по имени
        SceneManager.LoadScene(currentSceneName);
    }
}