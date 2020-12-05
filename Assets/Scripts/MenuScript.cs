using UnityEngine;
using UnityEngine.UI;

public class MenuScript : MonoBehaviour
{
    public GameObject bird;
    public GameObject birdPrefab;
    public Text score;
    public PipesScript pipesScript;
    public Button playBtn;
    public Button restartBtn;
    public GameObject gameOverScreen;

    void Start()
    {
        this.playBtn.onClick.AddListener(() =>
        {
            this.bird.SetActive(true);
            this.score.gameObject.SetActive(true);
            Destroy(this.playBtn.gameObject);
            this.pipesScript.gameHasStarted = true;
        });

        this.restartBtn.onClick.AddListener(() =>
        {
            this.bird = Instantiate(this.birdPrefab);

            var script = this.bird.GetComponent<BirdScript>();
            script.onDeath += this.OnBirdDeath;

            this.pipesScript.birdScript = script;

            this.restartBtn.gameObject.SetActive(false);
            this.gameOverScreen.SetActive(false);
            this.score.gameObject.SetActive(true);
            this.score.text = "0";

            this.bird.SetActive(true);
        });

        var birdScript = this.bird.GetComponent<BirdScript>();
        birdScript.onDeath += this.OnBirdDeath;
    }

    // TODO [GM]: fix score synchronization bug
    void OnBirdDeath()
    {
        this.gameOverScreen.SetActive(true);
        this.score.gameObject.SetActive(false);
        this.restartBtn.gameObject.SetActive(true);
    }
}