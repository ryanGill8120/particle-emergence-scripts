using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

using Random=UnityEngine.Random;

public class GameHandler : MonoBehaviour
{
    Model model;

    GameObject o1, o2;
    Particle p1, p2;
    Vector3 v1, v2, c1, c2;
    SimulationPhysics sim = new SimulationPhysics();

    void Awake()
    {
        model = Model.instance;
        
    }

    // Start is called before the first frame update
    void Start() {

        //model.numTypes = 3;
        model.screenWidthUnits = 104f;
        model.particleRadius = 1f;
        model.particleDiameter = 2f;

        /*
        model.relationships[0, 0] = new ParticleAttributes(.5f, 2.5f, 6.0f, 3.0f, .25f);
        model.relationships[0, 1] = new ParticleAttributes(1.5f, 3.5f, 7.5f, 1.0f, 3.5f);
        model.relationships[0, 2] = new ParticleAttributes(1.0f, 3.0f, 4.0f, 2.0f, 1.0f);

        model.relationships[1, 0] = new ParticleAttributes(.7f, .9f, 1.4f, 3.0f, 3.0f);
        model.relationships[1, 1] = new ParticleAttributes(2.0f, 2.25f, 2.5f, .25f, 4.0f);
        model.relationships[1, 2] = new ParticleAttributes(.5f, 1.0f, 1.5f, 2.0f, 2.0f);

        model.relationships[2, 0] = new ParticleAttributes(1.5f, 2.5f, 3.5f, 3.0f, 3.0f);
        model.relationships[2, 1] = new ParticleAttributes(2.0f, 2.5f, 3.0f, 1.5f, 1.5f);
        model.relationships[2, 2] = new ParticleAttributes(1.0f, 2.0f, 3.0f, 2.0f, 4.0f);
        */


        PopulateParticles();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        UpdateParticles();
    }

    public void Back(){
        SceneManager.LoadScene(2);
    }

    void PopulateParticles(){

        for (int i =0; i < model.numTypes; i++){
            model.numTypesArr[i] = 33;
        }

        int sum = 0;
        for(int i =0; i < model.numTypes; i++){
            sum+=model.numTypesArr[i];
        }
        model.numParticles = sum;
        //int count = 0;
        List<Vector3> positions = new List<Vector3>();
        
        for (int i = 0; i < model.numParticles; i++){
            positions.Add(new Vector3(Random.Range(0, model.screenWidthUnits), Random.Range(0, model.screenHeightUnits), 0));
        }


        int myIndex = 0;
        GameObject clone;
        for (int i = 0; i < model.numTypes; i++){
            for (int j = 0; j < model.numTypesArr[i]; j++){

                clone = Instantiate(model.particlePrefab, positions[myIndex], Quaternion.identity);
                Particle p = clone.GetComponent<Particle>();
                SpriteRenderer sr = clone.GetComponent<SpriteRenderer>();
                p.idx = myIndex;
                p.type = i;
                sr.color = model.particleColors[i];
                model.particles.Add(clone);
                myIndex++;
  
            }
        }

        model.nextVectors = new Vector2[model.numParticles];

    }

    void UpdateParticles(){

        float resultX, resultY, resultMagnitude, distance, theta, resultTheta, repulsionMagnifier;
        float repulsionM, repulsionB, decelAttractionM, decelAttractionB, accAttractionM, accAttractionB;
        GameObject applierObj, receiverObj;
        Particle applier, receiver;
        float eventRadius, repulsionRadius, decelRadius;

        for (int i = 0; i < model.particles.Count; i++){

            resultX = 0;
            resultY = 0;
            repulsionMagnifier = 1;
            receiverObj = model.particles[i];
            receiver = receiverObj.GetComponent<Particle>();
            for (int j = 0; j < model.particles.Count; j++){
                if (i != j){

                    applierObj = model.particles[j];
                    applier = applierObj.GetComponent<Particle>();
                    distance = Vector3.Distance(model.particles[i].transform.position, model.particles[j].transform.position);
                    theta = sim.GetTheta(model.particles[i].transform.position, model.particles[j].transform.position);
                    eventRadius = model.relationships[receiver.type, applier.type].eventRadius;

                    if (distance < eventRadius){

                        repulsionRadius = model.relationships[receiver.type, applier.type].repulsionRadius;
                        decelRadius = model.relationships[receiver.type, applier.type].decelRadius;
                        repulsionM = model.relationships[receiver.type, applier.type].repulsionM;
                        repulsionB = model.relationships[receiver.type, applier.type].repulsionB;
                        decelAttractionM = model.relationships[receiver.type, applier.type].decelAttractionM;
                        decelAttractionB = model.relationships[receiver.type, applier.type].decelAttractionB;
                        accAttractionM = model.relationships[receiver.type, applier.type].accAttractionM;
                        accAttractionB = model.relationships[receiver.type, applier.type].accAttractionB;

                        //repulsion
                        if (distance < repulsionRadius){
                            resultTheta = theta;
                            resultMagnitude = repulsionM * distance + repulsionB;
                            repulsionMagnifier += model.magnifierArr[applier.type];
                            resultMagnitude *= repulsionMagnifier;

                        //declerating attractions
                        }else if(distance < decelRadius){
                            resultTheta = (float)((theta + Math.PI) % (2 * Math.PI)); //mirror vector direction
                            resultMagnitude = decelAttractionM * distance + decelAttractionB;

                        //accelerating attraction
                        }else{
                            resultTheta = (float)((theta + Math.PI) % (2 * Math.PI));
                            resultMagnitude = accAttractionM * distance + accAttractionB;
                        }

                        //resultant force
                        resultX += resultMagnitude * (float)Math.Cos(resultTheta);
                        resultY += resultMagnitude * (float)Math.Sin(resultTheta); 

                    }

                }

            }//end n

            //apply friction
            resultX *= model.friction;
            resultY *= model.friction;
            model.nextVectors[receiver.idx] = new Vector2(resultX, resultY);
        }//end n^2

    }

    
}
