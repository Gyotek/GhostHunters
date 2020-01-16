using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Uduino;

public class ArduinoEvents : MonoBehaviour
{
    public void LED_OnOffWithButton(string _controllerNb, int _LEDNumber)
    {
        if (Input.GetAxis(_controllerNb) != 0.0f)
        {
            UduinoManager.Instance.digitalWrite(_LEDNumber, State.HIGH);
            Debug.Log("LED " + _LEDNumber + " is On with button n°" + _controllerNb);
        }
        else
        {
            UduinoManager.Instance.digitalWrite(_LEDNumber, State.LOW);
            Debug.Log("LED " + _LEDNumber + " is Off with button n°" + _controllerNb);
        }
    }

    public void LED_TurnOn(int _LEDNumber)
    {
        UduinoManager.Instance.digitalWrite(_LEDNumber, State.HIGH);
        Debug.Log("LED " + _LEDNumber + " is On");
    }

    public void LED_TurnOff(int _LEDNumber)
    {
        UduinoManager.Instance.digitalWrite(_LEDNumber, State.LOW);
        Debug.Log("LED " + _LEDNumber + " is Off");
    }

    public void LEDBlink(int _LEDNumber, float _blinkTime, bool _blink)
    {
        bool blinking = false;

        if (_blink == true && blinking == false)
        {
            StartCoroutine(Blink());
        }

        if (_blink == false)
        {
            StopCoroutine(Blink());
        }

        IEnumerator Blink()
        {
            blinking = true;
            UduinoManager.Instance.digitalWrite(_LEDNumber, State.HIGH);
            yield return new WaitForSeconds(_blinkTime);
            UduinoManager.Instance.digitalWrite(_LEDNumber, State.LOW);
            yield return new WaitForSeconds(_blinkTime);
            blinking = false;
        }
    }

    public void LEDBlink(int _LEDNumber, bool _blink)
    {
        bool blinking = false;
        int _blinkTime = 1;

        if (_blink == true && blinking == false)
        {
            StartCoroutine(Blink());
        }

        if (_blink == false)
        {
            StopCoroutine(Blink());
        }

        IEnumerator Blink()
        {
            blinking = true;
            UduinoManager.Instance.digitalWrite(_LEDNumber, State.HIGH);
            yield return new WaitForSeconds(_blinkTime);
            UduinoManager.Instance.digitalWrite(_LEDNumber, State.LOW);
            yield return new WaitForSeconds(_blinkTime);
            blinking = false;
        }
    }
}