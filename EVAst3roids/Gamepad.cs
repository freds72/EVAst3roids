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
        private static readonly int LongPressDuration = 750; // ms

        int _angle = 0;
        bool _wasPressed = false;
        bool _pressed = false;
        bool _pressLong = false;
        bool _rightPressed = false;
        bool _leftPressed = false;
        int _touchDuration = 0;
        bool _enterButtonPressed = false;

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
            _buttons.EnterPressed += () => {
                _enterButtonPressed = true; 
            };
            _buttons.EnterReleased += () => {
                _enterButtonPressed = false; 
            };
        }

        void UpdatePressedState(int dt, bool isPressed)
        {
            if (isPressed) // button pressed
            {
                _touchDuration += dt;
            }
            else if (_wasPressed != isPressed) // button released
            {
                _pressLong = _touchDuration >= LongPressDuration; // ms
                _touchDuration = 0;
                _pressed = true;
            }
            else // nothing...
            {
                _touchDuration = 0;
                _pressed = false;
            }
            _wasPressed = isPressed;
        }

        public void Update(int dt)
        {
            if ( _motorA != null )
                _angle = -_motorA.GetTachoCount();
            if (_touch != null)
                UpdatePressedState(dt, _touch.IsPressed());
            else
                UpdatePressedState(dt, _enterButtonPressed);

            if (_rightPressed)
                _angle--;
            if (_leftPressed)
                _angle++;
        }

        public int Angle
        {
            get { return _angle; }
        }
        // normal/short press
        public bool IsPressed { get { return _pressed; } }
        // long press
        public bool IsPressedLong { get { return _pressLong; } }

        public bool IsBeforePressedLong { get { return _touchDuration >= LongPressDuration; } }
    }
}
