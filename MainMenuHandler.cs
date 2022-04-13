using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuHandler : MonoBehaviour
{
    Model model;

    void Awake(){
        model = Model.instance;
    }

    public void NewSimulation(){

        SceneManager.LoadScene(1);
    }

    public void QuitSimulation(){

        Application.Quit();

    }
    
}
