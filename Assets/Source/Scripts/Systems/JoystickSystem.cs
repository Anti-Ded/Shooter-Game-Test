using UnityEngine;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine.UI;

// в GameData надо добавить public Vector3 Joystick - это и будут значения джойстика
public class JoystickSystem : GameSystem
{
    [SerializeField] float minJoystickSens = 25;
    [SerializeField] bool adaptive; // чем ближе палец к центру, тем меньше значение

    [Header("Screen")]

    [SerializeField] float handleMaxOffset = 75;

    bool touched;
    float screenScaler;
    Vector3 touchPos;
    Vector3 output;

    public override void OnStart()
    {
        // скаляр для точного определения координат на экране
        screenScaler = screen.GetComponent<CanvasScaler>().referenceResolution.y / (float)Screen.height;

        screen.joystickBG.gameObject.SetActive(false);

        screen.controllerButton.onClick.AddListener(ControllerSwitch);
        screen.controllerText.text = save.keyboardController ? "WASD" : "FINGER";
    }

    void ControllerSwitch()
    {
        Debug.Log("Controller Changed");
        save.keyboardController = !save.keyboardController;
        screen.controllerText.text = save.keyboardController ? "WASD" : "FINGER";
        screen.joystickBG.gameObject.SetActive(!save.keyboardController);
    }
    public override void OnUpdate()
    {
        if (save.keyboardController) return;

        // при нажатии проверяем не ткнул ли игрок в какую-то UI'ку
        if (Input.GetMouseButtonDown(0) && !game.gameover)
        {
            // палец нажат
            touched = true;

            // обновляем место тыка пальцем
            touchPos = Input.mousePosition;

            // включем UI джойстика
            if (screen.joystickBG)
            {
                screen.joystickBG.gameObject.SetActive(true);
                screen.joystickBG.anchoredPosition = Input.mousePosition * screenScaler;
            }
        }

        // при отпускании сюда можно добавить дополнительные параметры и проверки, чтобы игрок не тапал в какие-то моменты
        if (Input.GetMouseButtonUp(0) || game.gameover)
            touched = false;

        if (touched)
        {
            // Если палец сдвинули слишком мало или после движения решили остановиться не отпуская палец
            if ((Input.mousePosition - touchPos).magnitude < minJoystickSens)
                output = Vector3.zero;
            else
            // отклонение пальца от места нажатия
            {
                output = (Input.mousePosition - touchPos).normalized;
                if (adaptive)
                    output *= Mathf.Min(1, (Input.mousePosition - touchPos).magnitude / handleMaxOffset);
            }

            // Выставляем стик на нужное место
            screen.JoystickHandle.anchoredPosition = output * handleMaxOffset;

            // инвертируем из вертикальной плоскости в горизонтальную
            output.z = output.y;
            output.y = 0;

            // Угол поворота камеры
          //  output = Quaternion.Euler(0, game.cameraController.transform.eulerAngles.y, 0) * output;
            game.joystick = output.normalized;
        }
        else
        {
            // Если палец отпущен, то движение нулевое
            game.joystick = Vector3.zero;
            screen.joystickBG.gameObject.SetActive(false);
        }
    }
}
