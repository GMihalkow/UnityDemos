using UnityEngine;
using System.Collections;

public class EnemyScript : ShipScript
{
    bool isMoving;
    float speed = 0.05f;
    float maxSpeed = 0.05f;
    float minSpeed = 0.025f;
    bool isFiring;
    int _enemyHealth = 10;
    float destroyExplosionTimeout = 1f;
    float outOfScreenValueMax = 3f;
    float outOfScreenValueMin = -12f;
    float playerChaseTimeMin = 0.5f;
    float playerChaseTimeMax = 3.5f;
    float playerChaseTime = 1f;
    float playerChaseSpeed = 0f;
    Transform playerTransform;

    void Start()
    {
        playerTransform = GameObject.FindWithTag("Player").transform;
        isMoving = Random.Range(0, 10) > 5;
        EnemyHealth = 10;
    }

    void OnEnable()
    {
        Start();
    }

    public int EnemyHealth
    {
        get
        {
            return _enemyHealth;
        }
        set
        {
            _enemyHealth = value;
            if (_enemyHealth <= 0)
            {
                DestroyMe();
            }
        }
    }

	void Update ()
    {
        if (isPaused)
        {
            return;
        }

        transform.position -= Vector3.forward * speed;

        if (isMoving && transform.position.z > playerTransform.position.z)
        {
            transform.position = new Vector3(Mathf.SmoothDamp(transform.position.x, playerTransform.position.x, ref playerChaseSpeed, playerChaseTime), transform.position.y, transform.position.z);
        }


        if (transform.position.z > outOfScreenValueMax || transform.position.z < outOfScreenValueMin)
        {
            Destroy(this.gameObject);
        }

        if (isFiring && Time.frameCount % 12 == 0)
        {
            Fire();
        }
	}

    void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Player")
        {
            isFiring = true;
        }
    }

    void OnTriggerExit(Collider col)
    {
        if (col.tag == "Player")
        {
            isFiring = false;
        }
    }

    public void DestroyMe()
    {
        Destroy(this.gameObject);
        playerLogicScript.Score++;
        
        var explosion = Instantiate(Resources.Load("Explosion"), transform.position, Quaternion.Euler(90f, 0f, 0f));
        Destroy(explosion, destroyExplosionTimeout);
    }
}
