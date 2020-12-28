using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator StartGame(string levelName)
    {

        yield return new WaitForSeconds(0.1f);
        SceneManager.LoadScene(levelName);
    }

    public void onLevel1ButtonPressed()
    {
        StartCoroutine(StartGame("Poziom1"));
    }

    public void onLevel2ButtonPressed()
    {
        StartCoroutine(StartGame("Poziom2"));
    }

    public void onExitButtonPressed()
    {
        // save any game data here
#if UNITY_EDITOR
         // Application.Quit() does not work in the editor so
         // UnityEditor.EditorApplication.isPlaying need to be set to false to end the game
         UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

}
