using System.Linq;
using UnityEngine;

public class PipesScript : MonoBehaviour
{
    public GameObject bird;
    public float movementSpeed;
    private GameObject[] _pipes;
    private BirdScript _birdScript;

    void Start() 
    {
        this._birdScript = this.bird.GetComponent<BirdScript>();
        this._pipes = GameObject.FindGameObjectsWithTag("Pipe");
    }

    void Update()
    {
        if (this._birdScript.IsDead) return;

        foreach (var pipe in this._pipes)
        {
            pipe.transform.Translate(Vector2.left * this.movementSpeed * Time.deltaTime);

            if (pipe.transform.position.x <= -5.1f)
            {
                var lastPipeX = this._pipes.OrderByDescending((p) => p.transform.position.x).First().transform.position.x;

                var pipePos = pipe.transform.position;
                pipePos.x = lastPipeX + 1f;

                pipe.transform.position = pipePos;
            }
        }
    }
}