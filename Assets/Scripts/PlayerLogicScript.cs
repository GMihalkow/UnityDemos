using UnityEngine;
using UnityEngine.UI;

public delegate void PauseGame();

public class PlayerLogicScript : MonoBehaviour
{
    private const string _scorePrefKey = "Score";
    private const float _explosionDestroyTimeout = 2f;

    public event PauseGame pauseGame;
    public Slider healthSlider;
    public Text scoreElement;
    public Text deadLabel;
    MeshRenderer _meshRenderer;
    int _health;
    int _score;
    bool isAlive = true;

    void Start()
    {
        this._meshRenderer = this.GetComponent<MeshRenderer>();
        _score = PlayerPrefs.GetInt(_scorePrefKey, 0);
        this.scoreElement.text = _score.ToString();
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

            if (_health <= 0)
            {
                this.DestroyMe();
                PlayerPrefs.SetInt(_scorePrefKey, 0);
            }
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
                PlayerPrefs.SetInt(_scorePrefKey, value);
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
        this._meshRenderer.enabled = false;
        
        var explosion = Instantiate(Resources.Load("Explosion"), transform.position, Quaternion.Euler(90f, 0f, 0f));
        Destroy(explosion, _explosionDestroyTimeout);

        this.deadLabel.gameObject.SetActive(true);
        this.OnPauseClicked();
    }
}