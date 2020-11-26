using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class ButtonsScript : MonoBehaviour
{
    public Text percentage;
    public Button button;
    public Slider slider;

    private bool _isStarted;

    void Start()
    {
        this.button.onClick.AddListener(async () =>
        {
            if (this.slider.value == this.slider.maxValue)
            {
                this.Restart();
            }

            this._isStarted = !this._isStarted;

            for (var index = this.slider.value; index <= this.slider.maxValue; index++)
            {
                if (!this._isStarted) return;
                this.slider.value = index;
                this.percentage.text = $"{index}%";
                await Task.Delay(100);
            }
        });
    }

    void Restart()
    {
        this.percentage.text = $"{this.slider.minValue}%";
        this.slider.value = this.slider.minValue;
        this._isStarted = false;
    }
}