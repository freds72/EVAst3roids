using MonoBrickFirmware.Movement;
using MonoBrickFirmware.Sensors;
using MonoBrickFirmware.UserInput;
using MonoBrickFirmware.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EVAst3roids
{
    class Gamepad
    {
        Motor _motorA;
        EV3TouchSensor _touch;
        ButtonEvents _buttons = new ButtonEvents();

        int _angle = 0;
        bool _pressed = false;
        bool _rightPressed = false;
        bool _leftPressed = false;  

        public Gamepad()
        {
            if (PlatFormHelper.RunningPlatform == PlatFormHelper.Platform.EV3)
            {
                _motorA = new Motor(MotorPort.OutA);
                _motorA.ResetTacho();
                _touch = new EV3TouchSensor(SensorPort.In1);
            }
            _buttons.RightPressed += () => { _rightPressed = true; };
            _buttons.RightReleased += () => { _rightPressed = false; };
            _buttons.LeftPressed += () => { _leftPressed = true; };
            _buttons.LeftReleased += () => { _leftPressed = false; };
            _buttons.EnterPressed += () => { _pressed = true; };
            _buttons.EnterReleased += () => { _pressed = false; };
        }

        public void Update(int dt)
        {
            if ( _motorA != null )
                _angle = -_motorA.GetTachoCount();
            if ( _touch != null )
                _pressed = _touch.IsPressed();
            if (_rightPressed)
                _angle--;
            if (_leftPressed)
                _angle++;
        }

        public int Angle
        {
            get { return _angle; }
        }
        public bool IsPressed { get { return _pressed; } }
    }
}
