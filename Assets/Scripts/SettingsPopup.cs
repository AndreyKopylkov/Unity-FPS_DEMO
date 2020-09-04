using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsPopup : MonoBehaviour
{
    [SerializeField] private Slider speedSlider;

    public void Open()
    {
        gameObject.SetActive(true);
    }

    public void Close()
    {
        gameObject.SetActive(false);
    }

    public void OnSubmitName(string name) //Срабатывает в момент начала ввода данных
    {
        //Debug.Log(name);
    }

    public void OnSpeedValue(float speed) //Срабатывает при изменении ползунка
    {
        //Debug.Log("Speed: " + speed);
        PlayerPrefs.SetFloat("speed", speed); //сохранение скорости в переменной speed
        Messenger<float>.Broadcast(GameEvent.SPEED_CHANGED, speed);
    }

    // Start is called before the first frame update
    void Start()
    {
       // speedSlider.value = PlayerPrefs.GetFloat("speed, 1");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
