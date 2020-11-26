using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuScript : MonoBehaviour
{
    public Button playBtn;
    public Button optionsBtn;
    public Button exitBtn;

    void Start()
    {
        this.exitBtn.onClick.AddListener(() => Application.Quit());
        this.optionsBtn.onClick.AddListener(() => SceneManager.LoadSceneAsync("OptionsMenu", LoadSceneMode.Single));
    }
}