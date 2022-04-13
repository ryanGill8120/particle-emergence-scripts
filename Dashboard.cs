using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;


public class Dashboard : MonoBehaviour
{
    Model model;

    public TMP_Text simulationName;
    public TMP_Text errorText;
    public Button errorButton;

    public TMP_InputField frictionInput;
    public TMP_InputField numParticlesInput;
    public TMP_InputField repulsionMagnifierInput;
    public TMP_InputField eventRadiusInput;
    public TMP_InputField decelRadiusInput;
    public TMP_InputField repulsionRadiusInput;
    public TMP_InputField maxAttractionInput;
    public TMP_InputField maxRepulsionInput;

    public TMP_Dropdown applicationParticleDropdown;
    public TMP_Dropdown receiverParticleDropdown;
    public TMP_Dropdown particleSettingsDropdown;

    private int selectedApplier;
    private int selectedReceiver;
    private int selectedSettings;

    public SpriteRenderer applierSprite;
    public SpriteRenderer receiverSprite;

    public RawImage frictionError;
    public RawImage numParticlesError;
    public RawImage repulsionMagError;
    public RawImage eventRadiusError;
    public RawImage decelRadiusError;
    public RawImage repulsionRadiusError;
    public RawImage maxAttractionError;
    public RawImage maxRepulsionError;

    private float frictionF, repulsionMagnifierF, eventRadiusF, decelRadiusF, repulsionRadiusF, maxAttractionF, maxRepulsionF;
    private int numParticlesInt;

    public List<string> particleOptions;
    
    public void Awake()
    {
        model = Model.instance;
        
        simulationName.text = model.currentName;

        errorButton.enabled = false;
        frictionError.enabled = false;
        numParticlesError.enabled = false;
        repulsionMagError.enabled = false;
        eventRadiusError.enabled = false;
        decelRadiusError.enabled = false;
        repulsionRadiusError.enabled = false;
        maxAttractionError.enabled = false;
        maxRepulsionError.enabled = false;

        for(int i = 0; i < model.numTypes; i++){
            particleOptions.Add(model.particleNames[i]);
        }

        applicationParticleDropdown.AddOptions(particleOptions);
        receiverParticleDropdown.AddOptions(particleOptions);
        particleSettingsDropdown.AddOptions(particleOptions);

        PopulateDash();


    }

    private bool ValidateDash(){

        frictionError.enabled = false;
        numParticlesError.enabled = false;
        repulsionMagError.enabled = false;
        eventRadiusError.enabled = false;
        decelRadiusError.enabled = false;
        repulsionRadiusError.enabled = false;
        maxAttractionError.enabled = false;
        maxRepulsionError.enabled = false;
    
        bool validFriction = false, validNumParticles = false, validRepulsionMagnifier = false, validEventRadius = false,
            validDecelRadius = false, validRepulsionRadius = false, validMaxAttraction = false, validMaxRepulsion = false;

        if (Single.TryParse(frictionInput.text, out frictionF)){
            validFriction = true;
        }else{
            frictionError.enabled = true;
        }

        if (Int32.TryParse(numParticlesInput.text, out numParticlesInt)){
            validNumParticles = true;
        }else{
            numParticlesError.enabled = true;
        }

        if (Single.TryParse(repulsionMagnifierInput.text, out repulsionMagnifierF)){
            validRepulsionMagnifier = true;
        }else{
            maxRepulsionError.enabled = true;
        }

        if (Single.TryParse(eventRadiusInput.text, out eventRadiusF)){
            validEventRadius = true; 
        }else{
            eventRadiusError.enabled = true;
        }

        if (Single.TryParse(decelRadiusInput.text, out decelRadiusF)){
            validDecelRadius = true;
        }else{
            decelRadiusError.enabled = true;
        }

        if (Single.TryParse(repulsionRadiusInput.text, out repulsionRadiusF)){
            validRepulsionRadius = true;
        }else{
            repulsionRadiusError.enabled = true;
        }

        if (Single.TryParse(maxAttractionInput.text, out maxAttractionF)){
            validMaxAttraction = true;
        }else{
            maxAttractionError.enabled = true;
        }

        if (Single.TryParse(maxRepulsionInput.text, out maxRepulsionF)){
            validMaxRepulsion = true;
        }else{
            maxRepulsionError.enabled = true;
        }
        
        if(validFriction && validRepulsionMagnifier && validNumParticles && validEventRadius && validDecelRadius &&
            validRepulsionRadius && validMaxAttraction && validMaxRepulsion){
                return true;
            }else{
                return false;
            }
    }

    public void UpdateDash(){
        
        if (ValidateDash()){

            applierSprite.color = model.particleColors[applicationParticleDropdown.value];
            receiverSprite.color = model.particleColors[receiverParticleDropdown.value];
            errorButton.enabled = false;
            errorText.text = "";
            applicationParticleDropdown.enabled = true;
            receiverParticleDropdown.enabled = true;
            particleSettingsDropdown.enabled = true;

            model.friction = frictionF;
            model.magnifierArr[selectedSettings] = repulsionMagnifierF;
            model.numTypesArr[selectedSettings] = numParticlesInt;
            model.relationships[selectedReceiver, selectedApplier].eventRadius = eventRadiusF;
            model.relationships[selectedReceiver, selectedApplier].decelRadius = decelRadiusF;
            model.relationships[selectedReceiver, selectedApplier].repulsionRadius = repulsionRadiusF;
            model.relationships[selectedReceiver, selectedApplier].maxAttraction = maxAttractionF;
            model.relationships[selectedReceiver, selectedApplier].maxRepulsion = maxRepulsionF;
            model.relationships[selectedReceiver, selectedApplier].UpdateValues();


        }else{

            errorButton.enabled = true;
            errorText.text = "Error - Invalid Input";
            applicationParticleDropdown.enabled = false;
            receiverParticleDropdown.enabled = false;
            particleSettingsDropdown.enabled = false;

        }
    }

    public void PopulateDash(){

        selectedApplier = applicationParticleDropdown.value;
        selectedReceiver = receiverParticleDropdown.value;
        selectedSettings = particleSettingsDropdown.value;

        applierSprite.color = model.particleColors[selectedApplier];
        receiverSprite.color = model.particleColors[selectedReceiver];

        frictionInput.text = model.friction.ToString();
        numParticlesInput.text = model.numTypesArr[particleSettingsDropdown.value].ToString();
        repulsionMagnifierInput.text = model.magnifierArr[particleSettingsDropdown.value].ToString();
        eventRadiusInput.text = model.relationships[receiverParticleDropdown.value, applicationParticleDropdown.value].eventRadius.ToString();
        decelRadiusInput.text = model.relationships[receiverParticleDropdown.value, applicationParticleDropdown.value].decelRadius.ToString();
        repulsionRadiusInput.text = model.relationships[receiverParticleDropdown.value, applicationParticleDropdown.value].repulsionRadius.ToString();
        maxAttractionInput.text = model.relationships[receiverParticleDropdown.value, applicationParticleDropdown.value].maxAttraction.ToString();
        maxRepulsionInput.text = model.relationships[receiverParticleDropdown.value, applicationParticleDropdown.value].maxRepulsion.ToString();

    }

    public void TestValue(){
        Debug.Log("Fired!");
    }
    
    public void RunSimulation(){

        if(ValidateDash()){

            SceneManager.LoadScene(3);
        }else{
            errorButton.enabled = true;
            errorText.text = "Error - Invalid Input";
        }
    }

    public void Back(){
        SceneManager.LoadScene(1);
    }
    
}
