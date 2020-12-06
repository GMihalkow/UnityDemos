using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public delegate void PauseGame();

public class PlayerLogicScript : MonoBehaviour
{
    public event PauseGame pauseGame;
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