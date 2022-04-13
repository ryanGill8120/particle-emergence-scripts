using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class SimulationPhysics
{

    
    public float GetSlope(float x1, float y1, float x2, float y2){
        
        return (y2 - y1) / (x2 - x1);
    }

    public float GetYIntercept(float x, float y, float m){

        return(-1 * m * x) + y;
    }


    public float GetTheta(Vector3 v1, Vector3 v2) {

        float output = (float)Math.Atan2(v1.y - v2.y, v1.x - v2.x);
        if (output < 0)
            output += (float)(Math.PI *2);
        return output;

    }

}
