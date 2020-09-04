using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WanderingAI : MonoBehaviour
{
    [SerializeField] private GameObject fireballPrefab;
    private GameObject _fireball;

    public const float baseSpeed = 3.0f; //базовая скорость, которая регулируется ползунком

    public float speed = 4.0f;
    public float obstacleRange = 5.0f;

    private bool _alive;

    void Start()
    {
        _alive = true;
    }

    void Update()
    {
        if (_alive)
        {
            transform.Translate(0, 0, speed * Time.deltaTime); //непрерывное движение вперед

            Ray ray = new Ray(transform.position, transform.forward);
            RaycastHit hit;
            if (Physics.SphereCast(ray, 0.8f, out hit))
            {
                GameObject hitObject = hit.transform.gameObject;
                if (hitObject.GetComponent<PlayerCharacter>()) //игрок распознается как и в RayShooter
                {
                    if(_fireball == null)
                    {
                        _fireball = Instantiate(fireballPrefab) as GameObject;
                        _fireball.transform.position = //поместим огненный шар перед врагом в направлении его движения
                        transform.TransformPoint(Vector3.forward * 1.5f);
                        _fireball.transform.rotation = transform.rotation;
                    }
                }

                else if (hit.distance < obstacleRange)
                {
                    float angle = Random.Range(Random.Range(-110, -90), Random.Range(90, 110));
                    transform.Rotate(0, angle, 0);
                }
            }
        }
    }

    private void OnSpeedChanged(float value) //мето, объявленный в подписке для события SPEED_CHANGED
    {
        speed = baseSpeed * value;
    }

    void Awake() //добавление подписки
    {
        Messenger<float>.AddListener(GameEvent.SPEED_CHANGED, OnSpeedChanged);
    }

    void OnDestroy() //удаление подписки
    {
        Messenger<float>.RemoveListener(GameEvent.SPEED_CHANGED, OnSpeedChanged);
    }

    public void SetAlive(bool alive) //Открытый метод, для изменения извне
    {
        _alive = alive;
    }
}
