using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    private Rigidbody target;
    private GameManager gameManager;
    private float minSpeed = 8;
    private float maxSpeed = 10;
    private float maxTorque = 10;
    private float xRange = 3.75f;
    private float ySpawnPos = -6;

    public int pointValue;

    public ParticleSystem explosionParticle;

    // Start is called before the first frame update
    void Start()
    {

        //gets the rigidbody
        target = GetComponent<Rigidbody>();

        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();


        // throws up 
        target.AddForce(RandomForce(), ForceMode.Impulse);


        //adds torque(rotation)
        target.AddTorque(RandomTorque(), RandomTorque(), RandomTorque(), ForceMode.Impulse);


        // throws anywhere on the scene is the enclosed borders
        transform.position = RandomSpawnPos();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseDown()
    {
        if(gameManager.isGameActive)
        {
            Destroy(gameObject);
            Instantiate(explosionParticle, transform.position, explosionParticle.transform.rotation);
            gameManager.UpdateScore(pointValue);
            
            if (gameObject.CompareTag("Bad"))
            {
                gameManager.GameOver();
            }
        }      
    }

    // this is used for sensor, sensor detects and takes following actions
    private void OnTriggerEnter(Collider other)
    {
        Destroy(gameObject);
        
        if(!gameObject.CompareTag("Bad"))
        {
            gameManager.GameOver();
        }
    }

    Vector3 RandomForce()
    {
        return Vector3.up * Random.Range(minSpeed, maxSpeed);
    }

    float RandomTorque()
    {
        return Random.Range(-maxTorque, maxTorque);
    }

    Vector3 RandomSpawnPos()
    {
        return new Vector3(Random.Range(-xRange, xRange), -ySpawnPos);
    }
}
