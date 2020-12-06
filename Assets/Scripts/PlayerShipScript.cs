using UnityEngine;

public class PlayerShipScript : ShipScript 
{
    float speed = 0.1f;
    float minX = -4.3f;
    float maxX = 4.3f;
    float minZ = -10f;
    float maxZ = 1.4f;
    float horizontalAxis;
    float verticalAxis;
    float planeRotateTime = 1f;
    float planeReturnRotationSpeed = 0f;
    float planeReturnDampTime = 0.2f;
    PlayerLogicScript logicScript;

    void Start() => this.logicScript = this.GetComponent<PlayerLogicScript>();

    void Update ()
    {
        if (isPaused)
        {
            return;
        }

        if (Input.GetKey(KeyCode.Space) && Time.frameCount % 6 == 0)
        {
            Fire();
        }

        horizontalAxis = Input.GetAxis("Horizontal");
        verticalAxis = Input.GetAxis("Vertical");

        Move(horizontalAxis, verticalAxis);

        if (horizontalAxis > 0f)
        {
            transform.rotation = Quaternion.Euler(0f, 0f, Mathf.SmoothDampAngle(transform.rotation.eulerAngles.z, -45f, ref planeReturnRotationSpeed, planeRotateTime));
        }
        else if (horizontalAxis < 0f)
        {
            transform.rotation = Quaternion.Euler(0f, 0f, Mathf.SmoothDampAngle(transform.rotation.eulerAngles.z, 45f, ref planeReturnRotationSpeed, planeRotateTime));
        }
        else if (transform.rotation.eulerAngles != Vector3.zero)
        {
            transform.rotation = Quaternion.Euler(0f, 0f, Mathf.SmoothDampAngle(transform.rotation.eulerAngles.z, 0f, ref planeReturnRotationSpeed, planeReturnDampTime));
        }
	}

    void OnCollisionEnter(Collision col)
    {
        if (col.collider.tag == "Enemy")
        {
            logicScript.Health -= 5;
            col.gameObject.GetComponent<EnemyScript>().DestroyMe();
        }
    }

    void Move(float horizontalAxis, float verticalAxis)
    {
        if (horizontalAxis == 0f && verticalAxis == 0f) return;

        var x = horizontalAxis * this.speed;
        var z = verticalAxis * this.speed;
        var futurePos = this.transform.position + new Vector3(x, 0f, z);

        this.transform.position 
            = new Vector3(Mathf.Clamp(futurePos.x, this.minX, this.maxX), 0f, Mathf.Clamp(futurePos.z, this.minZ, this.maxZ));
    }
}