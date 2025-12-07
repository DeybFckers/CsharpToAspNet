using System;


class Event
{
    static void Main(string[] args)
    {
        #region --Practice--
        Console.WriteLine("Press A to simulate a button click");
        var key = Console.ReadLine();
        if (key == "a")
        {
            KeyPressed();
        }
        #endregion

        #region --CHALLENGE--

        #region -- TASK 1 --
        Console.WriteLine(" ");
        Console.WriteLine("Task 1");
        AlarmStart();
        #endregion

        #region -- TASK 2 --
        Console.WriteLine(" ");
        Console.WriteLine("TASK 2");
        download();
        #endregion

        #region -- TASK 3 --

        Console.WriteLine(" ");
        Console.WriteLine("TASK 3");
        ButtonClicked button = new ButtonClicked();
        Logger logger = new Logger();
        UIHandler ui = new UIHandler();

        button.Clicked += logger.Log;
        button.Clicked += ui.UpdateUI;

        button.Press();

        #endregion

        #region -- TASK 4 --

        Console.WriteLine(" ");
        Console.WriteLine("TASK 4");
        Console.WriteLine("Press p key to unsubscribe");
        var anykey = Console.ReadLine();

        if(anykey == "p")
        {
            button.Clicked -= logger.Log;
        }

        Console.WriteLine("Press u key to subscribe");
        var update = Console.ReadLine();

        if(update == "ui")
        {
            button.Clicked += ui.UpdateUI;
        }
        button.Press();


        #endregion

        #region -- TASK 5 -- 

        TemperatureSensor sensor = new TemperatureSensor();
        Heater heater = new Heater();

        // Subscribe heater to the sensor event
        sensor.TemperatureChanged += heater.OnTemperatureChanged;

        // Simulation
        sensor.Update(16);  // Heater ON
        sensor.Update(20);  // No change
        sensor.Update(28);  // Heater OFF

        #endregion


        #endregion

    }

    #region --PRACTICE--
    static void KeyPressed()
    {
        Button button = new Button();
        button.ClickEvent += (s, args) =>
        {
            Console.WriteLine($"You clicked a button {args.Name}");
        };
        button.OnClick();
         
    }
    #endregion
    static void AlarmStart()
    {
        AlarmClock am = new AlarmClock();
        am.ClickEvent += (s, alarm) =>
        {
            Console.WriteLine("ALARM! WAKE UP!");
        };
        am.TriggerAlarm();
    }
    
    static void download()
    {
        DownloadComplete dc = new DownloadComplete();
        dc.ClickEvent += (s, dc) =>
        {
            Console.WriteLine($"File {dc.FileName} ({dc.SizeMB}MB) finished downloading");
        };
        dc.Done();
    }
    
}

#region -- TASK 1 CLASS --
class AlarmClock
{
    public EventHandler ClickEvent;
    public void TriggerAlarm()
    {
        ClickEvent.Invoke(this, EventArgs.Empty);
    }
}
#endregion
#region -- TASK 2 CLASS --

class DownloadComplete
{
    public EventHandler<files> ClickEvent;
    public void Done()
    {
        files file = new files();
        file.FileName = "report.pdf";
        file.SizeMB = 12;
        ClickEvent.Invoke(this,  file);
    }

}

class files
{
    public String FileName { get; set; }
    public double SizeMB { get; set; }
}

#endregion
#region -- TASK 3/4 CLASS --
class ButtonClicked
{
    public event EventHandler Clicked;

    public void Press()
    {
        Clicked?.Invoke(this, EventArgs.Empty);
    }
}
class Logger
{
    public void Log(object sender, EventArgs e)
    {
        Console.WriteLine($"Button was clicked at {DateTime.Now}");
    }
}

class UIHandler
{
    public void UpdateUI(object sender, EventArgs e)
    {
        Console.WriteLine("UI updated because button clicked");
    }
}
#endregion
#region -- TASK 5 CLASS --
class TemperatureSensor
{
    public event EventHandler<TemperatureArgs> TemperatureChanged;

    public void Update(double newTemp)
    {
        TemperatureArgs args = new TemperatureArgs();
        // Send the temperature to all subscribers
        args.NewTemp = newTemp;
        TemperatureChanged?.Invoke(this, args );
    }
}

class TemperatureArgs : EventArgs
{
    public double NewTemp { get; set; }
}

class Heater
{
    private bool isOn = false;

    public void OnTemperatureChanged(object sender, TemperatureArgs e)
    {
        if (e.NewTemp < 18 && !isOn)
        {
            isOn = true;
            Console.WriteLine("Heater ON");
        }
        else if (e.NewTemp > 25 && isOn)
        {
            isOn = false;
            Console.WriteLine("Heater OFF");
        }
    }
}


#endregion

#region --PRACTICE
class Button
{
    public EventHandler<MyCustomArguments> ClickEvent;
    public void OnClick()
    {
        MyCustomArguments myArgs = new MyCustomArguments();
        myArgs.Name = "Dave";
        ClickEvent.Invoke(this, myArgs);
    }
}

class MyCustomArguments : EventArgs
{
    public String Name { get; set; }
}
#endregion

