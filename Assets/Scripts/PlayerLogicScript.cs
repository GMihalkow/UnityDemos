using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public delegate void PauseGame();

public class PlayerLogicScript : MonoBehaviour
{
    public event PauseGame pauseGame;
    public Slider healthSlider;
    public Text scoreElement;
    int _health;
    int _score;
    bool isAlive = true;

    void Start()
    {
        Health = 100;
    }

    public int Health
    {
        get
        {
            return _health;
        }
        set
        {
            _health = value;
            this.healthSlider.value = value;
        }
    }

    public int Score
    {
        get
        {
            return _score;
        }
        set
        {
            if (isAlive)
            {
                _score = value;
                this.scoreElement.text = _score.ToString();
            }
        }
    }

    public void OnPauseClicked()
    {
        pauseGame.Invoke();
    }

    void DestroyMe()
    {
        if (!isAlive)
        {
            return;
        }
        
        isAlive = false;
    }
}