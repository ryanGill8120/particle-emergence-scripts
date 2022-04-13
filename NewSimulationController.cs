using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class NewSimulationController : MonoBehaviour
{

    Model model;
    public TMP_InputField nameInput;
    public TMP_Dropdown numDropdown;

    void Awake(){
        model = Model.instance;
    }

    public void Back(){
        SceneManager.LoadScene(0);
    }

    public void Next(){
        model.currentName = nameInput.text;
        model.numTypes = numDropdown.value + 1;
        SceneManager.LoadScene(2);
    }
}
