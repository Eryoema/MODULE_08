using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DOM
{
    public interface ICommand
    {
        void Execute();
        void Undo();
    }

    public class Light
    {
        public void On() => Console.WriteLine("Свет включен.");
        public void Off() => Console.WriteLine("Свет выключен.");
    }

    public class Door
    {
        public void Open() => Console.WriteLine("Дверь открыта.");
        public void Close() => Console.WriteLine("Дверь закрыта.");
    }

    public class Thermostat
    {
        private int temperature;

        public void SetTemperature(int temp)
        {
            temperature = temp;
            Console.WriteLine($"Температура установлена на {temperature}°C.");
        }
    }

    public class TV
    {
        public void On() => Console.WriteLine("Телевизор включен.");
        public void Off() => Console.WriteLine("Телевизор выключен.");
    }

    public class LightOnCommand : ICommand
    {
        private Light light;

        public LightOnCommand(Light light)
        {
            this.light = light;
        }

        public void Execute() => light.On();
        public void Undo() => light.Off();
    }

    public class LightOffCommand : ICommand
    {
        private Light light;

        public LightOffCommand(Light light)
        {
            this.light = light;
        }

        public void Execute() => light.Off();
        public void Undo() => light.On();
    }

    public class DoorOpenCommand : ICommand
    {
        private Door door;

        public DoorOpenCommand(Door door)
        {
            this.door = door;
        }

        public void Execute() => door.Open();
        public void Undo() => door.Close();
    }

    public class DoorCloseCommand : ICommand
    {
        private Door door;

        public DoorCloseCommand(Door door)
        {
            this.door = door;
        }

        public void Execute() => door.Close();
        public void Undo() => door.Open();
    }

    public class ThermostatSetCommand : ICommand
    {
        private Thermostat thermostat;
        private int temperature;

        public ThermostatSetCommand(Thermostat thermostat, int temperature)
        {
            this.thermostat = thermostat;
            this.temperature = temperature;
        }

        public void Execute() => thermostat.SetTemperature(temperature);

        public void Undo() => thermostat.SetTemperature(20);
    }

    public class TVOnCommand : ICommand
    {
        private TV tv;

        public TVOnCommand(TV tv)
        {
            this.tv = tv;
        }

        public void Execute() => tv.On();
        public void Undo() => tv.Off();
    }

    public class TVOffCommand : ICommand
    {
        private TV tv;

        public TVOffCommand(TV tv)
        {
            this.tv = tv;
        }

        public void Execute() => tv.Off();
        public void Undo() => tv.On();
    }

    public class RemoteControl
    {
        private ICommand[] commands;
        private Stack<ICommand> commandHistory;

        public RemoteControl()
        {
            commands = new ICommand[10];
            commandHistory = new Stack<ICommand>();
        }

        public void SetCommand(int slot, ICommand command)
        {
            commands[slot] = command;
        }

        public void ExecuteCommand(int slot)
        {
            if (commands[slot] != null)
            {
                commands[slot].Execute();
                commandHistory.Push(commands[slot]);
            }
            else
            {
                Console.WriteLine("Команда не задана.");
            }
        }

        public void UndoCommand()
        {
            if (commandHistory.Count > 0)
            {
                ICommand lastCommand = commandHistory.Pop();
                lastCommand.Undo();
            }
            else
            {
                Console.WriteLine("Нет команд для отмены.");
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            RemoteControl remote = new RemoteControl();

            Light livingRoomLight = new Light();
            Door frontDoor = new Door();
            Thermostat thermostat = new Thermostat();
            TV livingRoomTV = new TV();

            ICommand lightOn = new LightOnCommand(livingRoomLight);
            ICommand lightOff = new LightOffCommand(livingRoomLight);
            ICommand doorOpen = new DoorOpenCommand(frontDoor);
            ICommand doorClose = new DoorCloseCommand(frontDoor);
            ICommand setTemp = new ThermostatSetCommand(thermostat, 22);
            ICommand tvOn = new TVOnCommand(livingRoomTV);
            ICommand tvOff = new TVOffCommand(livingRoomTV);

            remote.SetCommand(0, lightOn);
            remote.SetCommand(1, lightOff);
            remote.SetCommand(2, doorOpen);
            remote.SetCommand(3, doorClose);
            remote.SetCommand(4, setTemp);
            remote.SetCommand(5, tvOn);
            remote.SetCommand(6, tvOff);

            remote.ExecuteCommand(0);
            remote.ExecuteCommand(2);
            remote.ExecuteCommand(4); 
            remote.ExecuteCommand(5);

            remote.UndoCommand();
            remote.UndoCommand();
            remote.UndoCommand(); 
            remote.UndoCommand();
            remote.UndoCommand();
        }
    }
}