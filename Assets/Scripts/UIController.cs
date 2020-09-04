using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; //фреймворк для работы с UI

public class UIController : MonoBehaviour
{
    [SerializeField] private Text scoreLabel; //объект сцены Reference Text для заания свойста Text.

    [SerializeField] private SettingsPopup settingsPopup;

    private int _score;

    private void OnEnemyHit()
    {
        _score += 1; //Увеличиваем очки на 1
        scoreLabel.text = _score.ToString();
    }

    void Awake()
    {
        Messenger.AddListener(GameEvent.ENEMY_HIT, OnEnemyHit);
    }

    void OnDestroy()
    {
        Messenger.RemoveListener(GameEvent.ENEMY_HIT, OnEnemyHit); //при разрушении объекта удаляем подписчика событий
    }

    // Start is called before the first frame update
    void Start()
    {
        _score = 0;
        scoreLabel.text = _score.ToString(); //присваем очкам значение 0

        settingsPopup.Close(); //закрываем окно в момент начал игры
    }

    public void OnOpenSettings() //метод, вызываемый кнопкой настроек
    {
        //Debug.Log("open settings");
        settingsPopup.Open();
    }

    public void OpPointerDown()
    {
        Debug.Log("pointer down");
    }
}
