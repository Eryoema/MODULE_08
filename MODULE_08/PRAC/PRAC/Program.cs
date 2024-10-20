using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRAC
{
    public interface ICommand
    {
        void Execute();
        void Undo();
    }
    public class Light
    {
        public void On() => Console.WriteLine("Свет включен");
        public void Off() => Console.WriteLine("Свет выключен");
    }

    public class AirConditioner
    {
        public void TurnOn() => Console.WriteLine("Кондиционер включен");
        public void TurnOff() => Console.WriteLine("Кондиционер выключен");
        public void SetTemperature(int temperature) => Console.WriteLine($"Температура кондиционера установлена на {temperature}°C");
    }
    public class TV
    {
        public void TurnOn() => Console.WriteLine("Телевизор включен");
        public void TurnOff() => Console.WriteLine("Телевизор выключен");
        public void SetChannel(int channel) => Console.WriteLine($"Канал телевизора установлен на {channel}");
    }
    public class LightOnCommand : ICommand
    {
        private Light _light;

        public LightOnCommand(Light light) => _light = light;

        public void Execute() => _light.On();

        public void Undo() => _light.Off();
    }

    public class LightOffCommand : ICommand
    {
        private Light _light;

        public LightOffCommand(Light light) => _light = light;

        public void Execute() => _light.Off();

        public void Undo() => _light.On();
    }

    public class AirConditionerOnCommand : ICommand
    {
        private AirConditioner _ac;

        public AirConditionerOnCommand(AirConditioner ac) => _ac = ac;

        public void Execute() => _ac.TurnOn();

        public void Undo() => _ac.TurnOff();
    }
    public class AirConditionerOffCommand : ICommand
    {
        private AirConditioner _ac;

        public AirConditionerOffCommand(AirConditioner ac) => _ac = ac;

        public void Execute() => _ac.TurnOff();

        public void Undo() => _ac.TurnOn();
    }

    public class TVOnCommand : ICommand
    {
        private TV _tv;

        public TVOnCommand(TV tv) => _tv = tv;

        public void Execute() => _tv.TurnOn();

        public void Undo() => _tv.TurnOff();
    }

    public class TVOffCommand : ICommand
    {
        private TV _tv;

        public TVOffCommand(TV tv) => _tv = tv;

        public void Execute() => _tv.TurnOff();

        public void Undo() => _tv.TurnOn();
    }

    public class MacroCommand : ICommand
    {
        private ICommand[] _commands;

        public MacroCommand(ICommand[] commands) => _commands = commands;

        public void Execute()
        {
            foreach (var command in _commands)
            {
                command.Execute();
            }
        }

        public void Undo()
        {
            foreach (var command in _commands)
            {
                command.Undo();
            }
        }
    }

    public class RemoteControl
    {
        private ICommand[] _onCommands;
        private ICommand[] _offCommands;
        private ICommand _lastCommand;

        public RemoteControl()
        {
            _onCommands = new ICommand[5];
            _offCommands = new ICommand[5];
        }

        public void SetCommand(int slot, ICommand onCommand, ICommand offCommand)
        {
            _onCommands[slot] = onCommand;
            _offCommands[slot] = offCommand;
        }

        public void OnButtonWasPressed(int slot)
        {
            if (_onCommands[slot] != null)
            {
                _onCommands[slot].Execute();
                _lastCommand = _onCommands[slot];
            }
            else
            {
                Console.WriteLine("Команда не назначена для этого слота.");
            }
        }

        public void OffButtonWasPressed(int slot)
        {
            if (_offCommands[slot] != null)
            {
                _offCommands[slot].Execute();
                _lastCommand = _offCommands[slot];
            }
            else
            {
                Console.WriteLine("Команда не назначена для этого слота.");
            }
        }

        public void UndoButtonWasPressed()
        {
            if (_lastCommand != null)
            {
                _lastCommand.Undo();
            }
            else
            {
                Console.WriteLine("Нет команды для отмены.");
            }
        }
    }

    public class Program
    {
        public static void Main(string[] args)
        {
            RemoteControl remote = new RemoteControl();

            Light livingRoomLight = new Light();
            AirConditioner airConditioner = new AirConditioner();
            TV tv = new TV();

            LightOnCommand lightOn = new LightOnCommand(livingRoomLight);
            LightOffCommand lightOff = new LightOffCommand(livingRoomLight);

            AirConditionerOnCommand acOn = new AirConditionerOnCommand(airConditioner);
            AirConditionerOffCommand acOff = new AirConditionerOffCommand(airConditioner);

            TVOnCommand tvOn = new TVOnCommand(tv);
            TVOffCommand tvOff = new TVOffCommand(tv);

            remote.SetCommand(0, lightOn, lightOff);
            remote.SetCommand(1, acOn, acOff);
            remote.SetCommand(2, tvOn, tvOff);

            remote.OnButtonWasPressed(0); 
            remote.OffButtonWasPressed(0);
            remote.UndoButtonWasPressed();

            remote.OnButtonWasPressed(1);
            remote.OffButtonWasPressed(1);
            remote.UndoButtonWasPressed();

            ICommand[] partyMode = { lightOn, acOn, tvOn };
            MacroCommand partyMacro = new MacroCommand(partyMode);
            partyMacro.Execute();
        }
    }
}