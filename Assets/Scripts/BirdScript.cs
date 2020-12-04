using UnityEngine;

public class BirdScript : MonoBehaviour
{
    private const float _deathTimeout = 2f;
    private const float _gravityScaleAfterDeath = 3f;

    public float jumpForce;
    private bool _isDead;
    private Rigidbody2D _rigidbody;
    private Animator _animator;
    private AudioSource _deathAudioSource;
    //private AudioSource _hitAudioSource;

    public bool IsDead => this._isDead;

    void Start()
    {
        var audioSources = this.gameObject.GetComponents<AudioSource>();

        //this._hitAudioSource = this.gameObject.GetComponent<AudioSource>();
        this._deathAudioSource = this.gameObject.GetComponent<AudioSource>();
        this._animator = this.gameObject.GetComponent<Animator>();
        this._rigidbody = this.gameObject.GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !this._isDead)
        {
            this._rigidbody.AddRelativeForce(Vector2.up * this.jumpForce, ForceMode2D.Impulse);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (!this._deathAudioSource.isPlaying)
        {
            this._deathAudioSource.Play();
        }

        this._isDead = true;
        this._rigidbody.gravityScale = _gravityScaleAfterDeath;
        this._animator.SetBool("CanFly", false);
        Destroy(this.gameObject, _deathTimeout);
    }
}