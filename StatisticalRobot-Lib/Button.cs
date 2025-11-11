using System.Device.Gpio;

namespace Avans.StatisticalRobot;

public class Button
{
    private readonly int _pin;
    private readonly bool _defHigh;
    private bool _previousState;

    /// <summary>
    /// This is a digital device
    /// 3.3V/5V
    /// </summary>
    /// <param name="pin">Pin number on grove board</param>
    /// <param name="defHigh">button has default behaviour: HIGH</param>
    public Button(int pin, bool defHigh = false)
    {
        Robot.SetDigitalPinMode(pin, PinMode.Input);
        _pin = pin;
        _defHigh = defHigh;
    }
    /// <summary>
    /// returns "Released" when button is up and "Pressed" when button is down
    /// </summary>
    /// <returns>string</returns>
    public string GetState()
    {
        return (Robot.ReadDigitalPin(_pin) == PinValue.High) ? "Released" : "Pressed";
    }

    /// <summary>
    /// returns True when button is up and False when button is down
    /// </summary>
    /// <returns>boolean return type</returns>
    public bool GetStateBool()
    {
        return (Robot.ReadDigitalPin(_pin) == PinValue.High);
    }
    /// <summary>
    /// only returns true on a state change of down to up, must be called every cycle like with an if statement
    /// </summary>
    /// <returns>boolean return type</returns>
    public bool GetButtonUp()
    {
        bool current = GetStateBool();
        bool retVal = false;
        if (!current && _previousState)
        {
            retVal = true;
        }
        
        _previousState = current;
        return retVal;
    }
}