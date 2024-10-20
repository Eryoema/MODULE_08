using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LAB
{
    public interface ICommand
    {
        void Execute();
        void Undo();
    }

    public class LightOnCommand : ICommand
    {
        private Light _light;
        public LightOnCommand(Light light)
        {
            _light = light;
        }

        public void Execute()
        {
            _light.On();
        }

        public void Undo()
        {
            _light.Off();
        }
    }

    public class LightOffCommand : ICommand
    {
        private Light _light;

        public LightOffCommand(Light light)
        {
            _light = light;
        }

        public void Execute()
        {
            _light.Off();
        }

        public void Undo()
        {
            _light.On();
        }
    }

    public class Light
    {
        public void On()
        {
            Console.WriteLine("Свет включен.");
        }
        public void Off()
        {
            Console.WriteLine("Свет выключен.");
        }
    }

    public class TelevisionOnCommand : ICommand
    {
        private Television _television;
        public TelevisionOnCommand(Television television)
        {
            _television = television;
        }

        public void Execute()
        {
            _television.Off();
        }

        public void Undo()
        {
            _television.On();
        }
    }

    public class TelevisionOffCommand : ICommand
    {
        private Television _television;

        public TelevisionOffCommand(Television television)
        {
            _television = television;
        }

        public void Execute()
        {
            _television.On();
        }

        public void Undo()
        {
            _television.Off();
        }
    }


    public class Television

    {
        public void On()
        {
            Console.WriteLine("Телевизор включен.");
        }
        public void Off()
        {
            Console.WriteLine("Телевизор выключен.");
        }
    }
    public class AirConditionerOnCommand : ICommand
    {
        private AirConditioner _airConditioner;
        public AirConditionerOnCommand(AirConditioner airConditioner)
        {
            _airConditioner = airConditioner;
        }

        public void Execute()
        {
            _airConditioner.Off();
        }

        public void Undo()
        {
            _airConditioner.On();
        }
    }

    public class AirConditionerOffCommand : ICommand
    {
        private AirConditioner _airConditioner;

        public AirConditionerOffCommand(AirConditioner airConditioner)
        {
            _airConditioner = airConditioner;
        }

        public void Execute()
        {
            _airConditioner.On();
        }

        public void Undo()
        {
            _airConditioner.Off();
        }
    }
    public class AirConditioner
    {
        public void On()
        {
            Console.WriteLine("Кондиционер включен.");
        }
        public void Off()
        {
            Console.WriteLine("Кондиционер выключен.");
        }
    }

    public class RemoteControl
    {
        private ICommand _onCommand;
        private ICommand _offCommand;

        public void SetCommands(ICommand onCommand, ICommand offCommand)
        {
            _onCommand = onCommand;
            _offCommand = offCommand;
        }
        public void PressOnButton()
        {
            _onCommand.Execute();
        }
        public void PressOffButton()
        {
            _offCommand.Execute();
        }
        public void PressUndoButton()
        {
            _onCommand.Undo();
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            // Создаем устройства
            Light livingRoomLight = new Light();
            Television tv = new Television();
            AirConditioner con = new AirConditioner();


            // Создаем команды для управления светом
            ICommand lightOn = new LightOnCommand(livingRoomLight);
            ICommand lightOff = new LightOffCommand(livingRoomLight);

            // Создаем команды для управления телевизором
            ICommand tvOn = new TelevisionOnCommand(tv);
            ICommand tvOff = new TelevisionOffCommand(tv);

            ICommand conOn = new AirConditionerOnCommand(con);
            ICommand conOff = new AirConditionerOffCommand(con);

            // Создаем пульт и привязываем команды к кнопкам
            RemoteControl remote = new RemoteControl();

            // Управляем светом
            remote.SetCommands(lightOn, lightOff);
            Console.WriteLine("Управление светом:");
            remote.PressOnButton();
            remote.PressOffButton();
            remote.PressUndoButton();

            // Управляем телевизором
            remote.SetCommands(tvOn, tvOff);
            Console.WriteLine("\nУправление телевизором:");
            remote.PressOnButton();
            remote.PressOffButton();

            remote.SetCommands(conOn, conOff);
            Console.WriteLine("\nУправление кондиционером:");
            remote.PressOnButton();
            remote.PressOffButton();
        }
    }
}