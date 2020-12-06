using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class PipesScript : MonoBehaviour
{
    private const float _leftMapEdgeX = -5.1f;
    private const float _spaceBetweenPipes = 1.2f;
    private const float _upperYPipeThreshold = 1.45f;
    private const float _lowerYPipeThreshold = -0.13f;
    private const float _allowedRandomThreshold = 0.5f;
    private const float _speedChangeTime = 5;
    private const float _restartGamePipesThreshold = 2f;

    public Text scoreText;
    public float increaseSpeedAmount;
    public float movementSpeed;
    public bool gameHasStarted;
    public BirdScript birdScript;
    private GameObject[] _pipes;
    private AudioSource _audioSource;
    private int _score = 0;
    private float _initialMovementSpeed;

    public void AttachBirdDeathCallback() => this.birdScript.onDeath += this.OnBirdDeath;

    void Start()
    {
        this._initialMovementSpeed = this.movementSpeed;
        this._audioSource = this.gameObject.GetComponent<AudioSource>();
        this._pipes = GameObject.FindGameObjectsWithTag("Pipe");

        this.birdScript.onDeath += this.OnBirdDeath;

        this.StartCoroutine(this.SpeedUpdate());
    }

    void Update()
    {
        if (!this.gameHasStarted || this.birdScript.IsDead) return;
        this.ResetPassedPipesPositions(_leftMapEdgeX);
    }

    void OnBirdDeath()
    {
        this.ResetPassedPipesPositions(this.birdScript.transform.position.x + _restartGamePipesThreshold);
        this.movementSpeed = this._initialMovementSpeed;
        this._score = 0;
    }

    void ResetPassedPipesPositions(float minPosition)
    {
        foreach (var pipe in this._pipes)
        {
            var pipePos = pipe.transform.position;
            pipe.transform.Translate(Vector2.left * this.movementSpeed * Time.deltaTime);

            if (pipePos.x <= minPosition)
            {
                var isLowerPipe = pipePos.y <= _lowerYPipeThreshold + _allowedRandomThreshold;
                var lastPipeX = this._pipes.OrderByDescending((p) => p.transform.position.x).First().transform.position.x;

                pipePos.x = lastPipeX + _spaceBetweenPipes;

                var threshold = isLowerPipe ? _lowerYPipeThreshold : _upperYPipeThreshold;

                pipePos.y = Random.Range(threshold - _allowedRandomThreshold, threshold + _allowedRandomThreshold);
                pipe.transform.position = pipePos;

                if (this.scoreText.IsActive())
                {
                    this.scoreText.text = $"{++this._score}";
                }

                this._audioSource.Play();
            }
        }
    }

    IEnumerator SpeedUpdate()
    {
        while(true)
        {
            yield return new WaitForSeconds(_speedChangeTime);

            if (this.gameHasStarted)
            {
                this.movementSpeed += this.increaseSpeedAmount;
            }
        }
    }
}