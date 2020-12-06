using UnityEngine;
using System.Collections;

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
            col.gameObject.GetComponent<EnemyScript>().DestroyMe();
        }
    }

}
