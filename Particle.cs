using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Particle : MonoBehaviour
{

    Model model;
    private float  maxX, minX, maxY, minY, radius, diameter;
    public int type, idx;
    private Rigidbody2D rb;

    Vector3 currentPos;

      void Awake()
    {
        //maps instance variables
        model = Model.instance;
        radius = model.particleRadius;
        diameter = model.particleDiameter;
        maxX = model.screenWidthUnits + radius;
        maxY = model.screenHeightUnits + radius;
        minX = -radius;
        minY = -radius;

        //object components
        rb = this.GetComponent<Rigidbody2D>();

    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x < minX)
        {
            rb.position = new Vector3(transform.position.x + (maxX + radius), transform.position.y, 0);
        }else if (transform.position.x > maxX)
        {
            rb.position = new Vector3(transform.position.x - (maxX + radius), transform.position.y, 0);
        }

        if (transform.position.y < minY)
        {
            rb.position = new Vector3(transform.position.x, transform.position.y + (maxY + radius), 0);
        }else if(transform.position.y > maxY)
        {
            rb.position = new Vector3(transform.position.x, transform.position.y - (maxY + radius), 0);
        }
    }

    void FixedUpdate() {

        rb.AddForce(model.nextVectors[idx]);
        
    }

    public void MoveParticle(Vector3 v){

        float x = transform.position.x + v.x;
        float y = transform.position.y + v.y;
        rb.MovePosition(new Vector3(x, y, 0));

    }
}
