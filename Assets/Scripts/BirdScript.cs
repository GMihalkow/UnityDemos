using UnityEngine;

public class BirdScript : MonoBehaviour
{
    private const float _gravityScaleAfterDeath = 3f;

    public float jumpForce;
    private bool _isDead;
    private Rigidbody2D _rigidbody;

    public bool IsDead => this._isDead;

    void Start() => this._rigidbody = this.gameObject.GetComponent<Rigidbody2D>();

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !this._isDead)
        {
            this._rigidbody.AddRelativeForce(Vector2.up * this.jumpForce, ForceMode2D.Impulse);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        this._isDead = true;
        this._rigidbody.gravityScale = _gravityScaleAfterDeath;
    }
}