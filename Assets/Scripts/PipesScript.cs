using System.Linq;
using UnityEngine;

public class PipesScript : MonoBehaviour
{
    private const float _leftMapEdgeX = -5.1f;
    private const float _spaceBetweenPipes = 1.2f;
    private const float _upperYPipeThreshold = 1.45f;
    private const float _lowerYPipeThreshold = -0.13f;
    private const float _allowedRandomThreshold = 0.5f;

    public GameObject bird;
    public float movementSpeed;
    private GameObject[] _pipes;
    private BirdScript _birdScript;

    // TODO [GM]: Implement speeding up the pipes movment every 5 seconds
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
            var pipePos = pipe.transform.position;
            pipe.transform.Translate(Vector2.left * this.movementSpeed * Time.deltaTime);

            if (pipePos.x <= _leftMapEdgeX)
            {
                var isLowerPipe = pipePos.y <= _lowerYPipeThreshold + _allowedRandomThreshold;
                var lastPipeX = this._pipes.OrderByDescending((p) => p.transform.position.x).First().transform.position.x;

                pipePos.x = lastPipeX + _spaceBetweenPipes;

                var threshold = isLowerPipe ? _lowerYPipeThreshold : _upperYPipeThreshold;

                pipePos.y = Random.Range(threshold - _allowedRandomThreshold, threshold + _allowedRandomThreshold);
                pipe.transform.position = pipePos;
            }
        }
    }
}