using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public enum GameState{
        GS_PAUSEMENU,
        GS_GAME,
        GS_LEVELCOMPLETED,
        GS_GAME_OVER
    }
    public GameState currentGameState;
    public static GameManager instance;
    public Canvas inGameCanvas;
    public Text coinsText;
    private int coins = 0;
    public Image[] keysTab;
    private int keys = 0;
    private float timer = 0;
    public Text timerText;
    public int lives = 3;
    public Text livesText;
    private int minutes;
    private int seconds;
    public Text killCount;
    private int kills;
    public Text winText;
    public Canvas pauseMenuCanvas;
    public Canvas levelCompletedCanvas;
    public int maxKeyNumber = 3;
    public bool keysCompleted = false;
    public Canvas gameOverCanvas;


    void SetGameState (GameState newGameState){
        currentGameState = newGameState;
        inGameCanvas.enabled = (currentGameState == GameState.GS_GAME);
        pauseMenuCanvas.enabled = (currentGameState == GameState.GS_PAUSEMENU);
        levelCompletedCanvas.enabled = (currentGameState == GameState.GS_LEVELCOMPLETED);
        gameOverCanvas.enabled = (currentGameState == GameState.GS_GAME_OVER);


    }

    public void InGame(){
        SetGameState(GameState.GS_GAME);
    }

    public void GameOver(){
        SetGameState(GameState.GS_GAME_OVER);
    }

    public void PauseMenu(){
        SetGameState(GameState.GS_PAUSEMENU);
    }

    public void LevelCompleted(){
        SetGameState(GameState.GS_LEVELCOMPLETED);
    }

    void Awake(){
        instance = this;
        InGame();
        coinsText.text = coins.ToString();
        
        for (int i = 0; i< keysTab.Length; i++)
            keysTab[i].color = new Color(0.5f, 0.5f, 0.5f, 0.5f);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape) && currentGameState == GameState.GS_PAUSEMENU){
            InGame();
            
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && currentGameState == GameState.GS_GAME)
        {
            PauseMenu();
        }
        if (currentGameState == GameState.GS_GAME) timer += Time.deltaTime;

        minutes = (int) timer/60;
        seconds = (int) timer%60;
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);

        if (lives < 0) GameOver();
    }

    public void addCoins(){
        coins++;
        coinsText.text = coins.ToString();
    }

    public void addLives(){
        lives++;
        livesText.text = lives.ToString();
    }

    public void removeLives(){
        lives--;
        livesText.text = lives.ToString();
        if (lives < 0) GameOver();
    }

    public void addKeys(){
        keysTab[keys++].color = Color.yellow;
        if (keys == maxKeyNumber) keysCompleted = true;
    }

    public void addKills(){
        kills++;
        killCount.text = kills.ToString();
    }

    public void win(){
        winText.text = "Winner! Time:  " + timerText.text;
        timerText.enabled = false;
        LevelCompleted();
    }

    public void OnResumeButtonClicked()
    {
        InGame();
    }

    public void OnRestartButtonClicked()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void OnExitButtonClicked()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void OnNextLevelClicked()
    {
        SceneManager.LoadScene("Poziom2");
    }

    
}
