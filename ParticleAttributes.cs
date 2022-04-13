using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleAttributes 
{
    // Start is called before the first frame update

        SimulationPhysics sim = new SimulationPhysics();
        public float repulsionRadius;
        public float decelRadius;
        public float eventRadius;
        public float maxAttraction;
        public float maxRepulsion;
        public float repulsionM;
        public float repulsionB;
        public float decelAttractionM;
        public float decelAttractionB;
        public float accAttractionM;
        public float accAttractionB;
        
        public ParticleAttributes(float repulsionRadius, float decelRadius, float eventRadius, float maxAttraction, float maxRepulsion){

            this.repulsionRadius = repulsionRadius;
            this.decelRadius = decelRadius;
            this.eventRadius = eventRadius;
            this.maxAttraction = maxAttraction;
            this.maxRepulsion = maxRepulsion;
            UpdateValues();

        }

        public void UpdateValues(){
            repulsionM = sim.GetSlope(0, maxRepulsion, repulsionRadius, 0);
            repulsionB = this.maxRepulsion;
            decelAttractionM = sim.GetSlope(repulsionRadius, 0, decelRadius, maxAttraction);
            decelAttractionB = sim.GetYIntercept(decelRadius, maxAttraction, decelAttractionM);
            accAttractionM = sim.GetSlope(decelRadius, maxAttraction, eventRadius, 0);
            accAttractionB = sim.GetYIntercept(eventRadius, 0,  accAttractionM);
        }

}
