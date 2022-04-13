using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Model : MonoBehaviour
{
    #region Singleton
    public static Model instance;

    public void Awake()
    {
       
        instance = this;
        for(int i = 0; i < maxTypes; i++){
            for(int j = 0; j < maxTypes; j++){
                relationships[i, j] = new ParticleAttributes(0.0f, 0.0f, 0.0f, 0.0f, 0.0f);
            }
        }
       
    }
    #endregion


    public float screenHeightUnits = 50f, 
                  screenWidthUnits = 104f,
                    particleRadius = 1.0f,
                  particleDiameter = 2.0f, 
                           friction = .8f;

    public GameObject particlePrefab;

    private static int maxTypes = 5;
    public int numTypes = 5;

    public int numParticles;

    public Color[] particleColors = {Color.red, Color.blue, Color.green, Color.yellow, Color.white};
    public string[] particleNames = {"Red", "Blue", "Green", "Yellow", "White"}; 
    public int[] numTypesArr = new int[maxTypes];
    public float[] magnifierArr = new float[maxTypes];
    public ParticleAttributes[,] relationships = new ParticleAttributes[maxTypes, maxTypes];
    public List<GameObject> particles = new List<GameObject>();

    public Vector2[] nextVectors;
    public string currentName = "test string";



}
