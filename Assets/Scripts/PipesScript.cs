﻿using System.Linq;
using System.Timers;
using UnityEngine;
using UnityEngine.UI;

public class PipesScript : MonoBehaviour
{
    private const float _leftMapEdgeX = -5.1f;
    private const float _spaceBetweenPipes = 1.2f;
    private const float _upperYPipeThreshold = 1.45f;
    private const float _lowerYPipeThreshold = -0.13f;
    private const float _allowedRandomThreshold = 0.5f;
    private const int _speedChangeTime = 5000;

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

        var timer = new Timer(_speedChangeTime);
        timer.Elapsed += this.OnFiveSecondsPassed;
        timer.Enabled = true;

        this.birdScript.onDeath += this.OnBirdDeath;
    }

    void Update()
    {
        if (!this.gameHasStarted || this.birdScript.IsDead) return;

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

                if (this.scoreText.IsActive())
                {
                    this.scoreText.text = $"{++this._score}";
                }

                this._audioSource.Play();
            }
        }
    }

    void OnFiveSecondsPassed(object sender, ElapsedEventArgs args) => this.movementSpeed += this.increaseSpeedAmount;

    void OnBirdDeath()
    {
        this.movementSpeed = this._initialMovementSpeed;
        this._score = 0;
    }
}