using UnityEngine;

public class BirdScript : MonoBehaviour
{
    private const float _deathTimeout = 2f;
    private const float _gravityScaleAfterDeath = 3f;

    public AudioClip hitSound;
    public AudioClip deathSound;
    public float jumpForce;
    private bool _isDead;
    private Rigidbody2D _rigidbody;
    private Animator _animator;
    private AudioSource _audioSource;

    public bool IsDead => this._isDead;

    void Start()
    {
        this._audioSource = this.gameObject.GetComponent<AudioSource>();
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
        if (!this._audioSource.isPlaying)
        {
            this._audioSource.clip = this.deathSound;

            this._audioSource.Play();
            this._audioSource.PlayOneShot(this.hitSound);
        }

        this._isDead = true;
        this._rigidbody.gravityScale = _gravityScaleAfterDeath;
        this._animator.SetBool("CanFly", false);
        Destroy(this.gameObject, _deathTimeout);
    }
}