using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class OptionsMenuScript : MonoBehaviour
{
    public RawImage bg;
    public Button backBtn;
    public Slider alphaSlider;

    void Start()
    {
        this.backBtn
            .onClick
            .AddListener(() => SceneManager.LoadSceneAsync("MainMenuScene", LoadSceneMode.Single));

        this.alphaSlider
            .onValueChanged
            .AddListener((val) => this.bg.color = new Color(this.bg.color.r, this.bg.color.g, this.bg.color.b, val));
    }
}