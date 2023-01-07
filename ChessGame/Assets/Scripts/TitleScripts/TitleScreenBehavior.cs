// Caleb Smith
// 01/07/2023
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleScreenBehavior : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadGame()
    {
        SceneManager.LoadScene("ChessGameScene");
    }

    public void LoadStart() 
    {
        SceneManager.LoadScene("TitleScreen");
    }

    public void LoadCredits()
    {
        SceneManager.LoadScene("CreditsScene");
    }

    public void LoadHelp()
    {
        SceneManager.LoadScene("HelpScene");
    }
}
