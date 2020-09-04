using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class RayShooter : MonoBehaviour
{
    private Camera _camera;

    // Start is called before the first frame update
    void Start()
    {
        _camera = GetComponent<Camera>();

        //Cursor.lockState = CursorLockMode.Locked;
        //Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && ! EventSystem.current.IsPointerOverGameObject())
        {
            Vector3 point = new Vector3(
                _camera.pixelWidth / 2, _camera.pixelHeight / 2, 0);

            Ray ray = _camera.ScreenPointToRay(point);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                GameObject hitObject = hit.transform.gameObject; //получем объект, в который попал луч
                ReactiveTarget target = hitObject.GetComponent<ReactiveTarget>();
                if (target != null) //проверяем наличие у этого объекта компонента ReactiveTarget
                {
                    // Debug.Log("Target Hit");
                    target.ReactToHit(); //Вызов метода для мишени
                    Messenger.Broadcast(GameEvent.ENEMY_HIT); //рассылка сообщения
                }
                else
                {
                    //Вызов попрограммы для создания сферы в месте попадания
                    StartCoroutine(SphereIndicator(hit.point)); 
                }
            }
        }
    }

    private IEnumerator SphereIndicator(Vector3 pos)
    {
        GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        sphere.transform.position = pos;

        yield return new WaitForSeconds(5);

        Destroy(sphere);
    }

    void OnGUI()
    {
        int size = 20;
        float posX = _camera.pixelHeight / 2 - size / 2;
        float posY = _camera.pixelWidth / 2 - size / 2;
        GUI.Label(new Rect(posX, posY, size, size), "*");
    }
}
