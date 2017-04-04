using Microsoft.SPOT;
using System.Threading;

class Program
{
    const int NoteC = 261;
    const int NoteD = 294;
    const int NoteE = 330;
    const int NoteF = 349;
    const int NoteG = 391;

    const int Whole = 1000;
    const int Half = Whole / 2;
    const int QuarterDot = Whole / 3;
    const int Quarter = Whole / 4;
    const int Eighth = Whole / 8;

    // Mélodie
    int[] note = { 
            NoteE, NoteE, NoteF, NoteG, NoteG, NoteF, NoteE,
            NoteD, NoteC, NoteC, NoteD, NoteE, NoteE, NoteD,
            NoteE, NoteD, NoteC, NoteC, NoteD, NoteE, NoteD,
            NoteC, NoteC
        };

    int[] duration = {
            Quarter,  Quarter,  Quarter, Quarter, Quarter, Quarter,
            Quarter,  Quarter,  Quarter, Quarter, Quarter, Quarter, QuarterDot,
            Eighth, Half, Quarter,  Quarter,  Quarter, Quarter, Quarter, Quarter,
            Quarter,  Quarter,  Quarter, Quarter, Quarter, Quarter, QuarterDot,
            Eighth,Whole
        };

    enum Menu
    {
        Bulb, Traffic, Temperature, Light, Accelerometer, Buzzer, Game
    }
    Menu choiceMenu = Menu.Bulb;

    enum progDemo
    {
        Attente, Bulb, Traffic, Temperature, Light, Accelerometer, Buzzer, Game
    }
    progDemo choiceProg = progDemo.Attente;

    enum colorTemp
    {
        blue, green, red
    }
    colorTemp stateTemp = colorTemp.blue;

    bool activeUp = true, activeDown = true;
    bool finTemp = false;
    static bool bouncerOk = false;
    double brightness = 0.0;

    static int r = 5; // rayon de la balle
    static int x = 80, y = 60; // position
    static int dx = 4, dy = 6; // vitesse et direction 
    static double temperature = 0.0;

   
    public void BrainPadSetup()
    {
        // Initialisation des Leds
        BrainPad.TrafficLight.TurnRedLightOff();
        BrainPad.TrafficLight.TurnGreenLightOff();
        BrainPad.TrafficLight.TurnYellowLightOff();
        BrainPad.LightBulb.TurnOff();

        // Initialize brightness
        brightness = 0.0;
        

        // Initialize the menu
        DisplayMenu();
        BrainPad.Display.DrawText(10, 20, "1. Bulb", BrainPad.Color.Yellow);

        // Events
        BrainPad.Button.ButtonChanged += Button_ButtonChanged;

        // Timer for temperature measurement
        Timer timerTemp = new Timer(new TimerCallback(OnTimer), null, 5000, 1000);

    }

    void Button_ButtonChanged(BrainPad.Button.DPad button, BrainPad.Button.State state)
    {
        switch (choiceMenu)
        {
            case Menu.Bulb:
                if ((button == BrainPad.Button.DPad.Down) && (state == BrainPad.Button.State.Pressed) && activeDown)
                {
                    choiceMenu = Menu.Traffic;
                    BrainPad.Display.DrawText(10, 20, "1. Bulb", BrainPad.Color.White);
                    BrainPad.Display.DrawText(10, 30, "2. Traffic", BrainPad.Color.Yellow);
                }
                else if ((button == BrainPad.Button.DPad.Up) && (state == BrainPad.Button.State.Pressed) && activeUp)
                {
                    choiceMenu = Menu.Game;
                    BrainPad.Display.DrawText(10, 20, "1. Bulb", BrainPad.Color.White);
                    BrainPad.Display.DrawText(10, 80, "7. Game", BrainPad.Color.Yellow);
                }
                else if ((button == BrainPad.Button.DPad.Right) && (state == BrainPad.Button.State.Pressed))
                {
                    activeUp = false; activeDown = false;
                    choiceProg = progDemo.Bulb;

                }
                else if ((button == BrainPad.Button.DPad.Left) && (state == BrainPad.Button.State.Pressed) && (choiceProg!=progDemo.Attente))
                {
                    choiceProg = progDemo.Attente;
                    activeUp = true; activeDown = true;
                }
                break;

            case Menu.Traffic:
                if ((button == BrainPad.Button.DPad.Down) && (state == BrainPad.Button.State.Pressed) && activeDown)
                {
                    choiceMenu = Menu.Temperature;
                    BrainPad.Display.DrawText(10, 30, "2. Traffic", BrainPad.Color.White);
                    BrainPad.Display.DrawText(10, 40, "3. Temperature Sensor", BrainPad.Color.Yellow);
                }
                else if ((button == BrainPad.Button.DPad.Up) && (state == BrainPad.Button.State.Pressed) && activeUp)
                {
                    choiceMenu = Menu.Bulb;
                    BrainPad.Display.DrawText(10, 30, "2. Traffic", BrainPad.Color.White);
                    BrainPad.Display.DrawText(10, 20, "1. Bulb", BrainPad.Color.Yellow);
                }
                else if ((button == BrainPad.Button.DPad.Right) && (state == BrainPad.Button.State.Pressed))
                {
                    activeUp = false; activeDown = false;
                    choiceProg = progDemo.Traffic;
                }
                else if ((button == BrainPad.Button.DPad.Left) && (state == BrainPad.Button.State.Pressed) && (choiceProg != progDemo.Attente))
                {
                    choiceProg = progDemo.Attente;
                    activeUp = true; activeDown = true;
                }
                break;

            case Menu.Temperature:
                if ((button == BrainPad.Button.DPad.Down) && (state == BrainPad.Button.State.Pressed) && activeDown)
                {
                    choiceMenu = Menu.Light;
                    BrainPad.Display.DrawText(10, 40, "3. Temperature Sensor", BrainPad.Color.White);
                    BrainPad.Display.DrawText(10, 50, "4. Light Sensor", BrainPad.Color.Yellow);
                }
                else if ((button == BrainPad.Button.DPad.Up) && (state == BrainPad.Button.State.Pressed) && activeUp)
                {
                    choiceMenu = Menu.Traffic;
                    BrainPad.Display.DrawText(10, 40, "3. Temperature Sensor", BrainPad.Color.White);
                    BrainPad.Display.DrawText(10, 30, "2. Traffic", BrainPad.Color.Yellow);
                }
                else if ((button == BrainPad.Button.DPad.Right) && (state == BrainPad.Button.State.Pressed))
                {
                    activeUp = false; activeDown = false;
                    BrainPad.Display.Clear();
                    BrainPad.Display.DrawText(40, 0, "Temperature demo", BrainPad.Color.White);
                    choiceProg = progDemo.Temperature;
                }
                else if ((button == BrainPad.Button.DPad.Left) && (state == BrainPad.Button.State.Pressed) && (choiceProg != progDemo.Attente))
                {
                    choiceProg = progDemo.Attente;
                    BrainPad.Display.DrawText(10, 100, "INFO: End, please wait", BrainPad.Color.White);
                    while (!finTemp) ;
                    BrainPad.Display.Clear();
                    DisplayMenu();
                    BrainPad.Display.DrawText(10, 40, "3. Temperature Sensor", BrainPad.Color.Yellow);
                    activeUp = true; activeDown = true;
                }
                break;

            case Menu.Light:
                if ((button == BrainPad.Button.DPad.Down) && (state == BrainPad.Button.State.Pressed) && activeDown)
                {
                    choiceMenu = Menu.Accelerometer;
                    BrainPad.Display.DrawText(10, 50, "4. Light Sensor", BrainPad.Color.White);
                    BrainPad.Display.DrawText(10, 60, "5. Accelerometer", BrainPad.Color.Yellow);
                }
                else if ((button == BrainPad.Button.DPad.Up) && (state == BrainPad.Button.State.Pressed) && activeUp)
                {
                    choiceMenu = Menu.Temperature;
                    BrainPad.Display.DrawText(10, 50, "4. Light Sensor", BrainPad.Color.White);
                    BrainPad.Display.DrawText(10, 40, "3. Temperature Sensor", BrainPad.Color.Yellow);
                }
                else if ((button == BrainPad.Button.DPad.Right) && (state == BrainPad.Button.State.Pressed))
                {
                    activeUp = false; activeDown = false;
                    BrainPad.Display.Clear();
                    InfoLight();
                    BrainPad.LightBulb.SetColor(BrainPad.Color.White);
                    choiceProg = progDemo.Light;
                }
                else if ((button == BrainPad.Button.DPad.Left) && (state == BrainPad.Button.State.Pressed) && (choiceProg != progDemo.Attente))
                {
                    choiceProg = progDemo.Attente;
                    BrainPad.Display.Clear();
                    DisplayMenu();
                    BrainPad.Display.DrawText(10, 50, "4. Light Sensor", BrainPad.Color.Yellow);
                    activeUp = true; activeDown = true;
                }
                break;

            case Menu.Accelerometer:
                if ((button == BrainPad.Button.DPad.Down) && (state == BrainPad.Button.State.Pressed) && activeDown)
                {
                    choiceMenu = Menu.Buzzer;
                    BrainPad.Display.DrawText(10, 60, "5. Accelerometer", BrainPad.Color.White);
                    BrainPad.Display.DrawText(10, 70, "6. Buzzer", BrainPad.Color.Yellow);
                }
                else if ((button == BrainPad.Button.DPad.Up) && (state == BrainPad.Button.State.Pressed) && activeUp)
                {
                    choiceMenu = Menu.Light;
                    BrainPad.Display.DrawText(10, 60, "5. Accelerometer", BrainPad.Color.White);
                    BrainPad.Display.DrawText(10, 50, "4. Light Sensor", BrainPad.Color.Yellow);
                }
                else if ((button == BrainPad.Button.DPad.Right) && (state == BrainPad.Button.State.Pressed))
                {
                    activeUp = false; activeDown = false;
                    BrainPad.Display.Clear();
                    BrainPad.Display.DrawText(35, 0, "Accelerometer demo", BrainPad.Color.White);
                    choiceProg = progDemo.Accelerometer;
                }
                else if ((button == BrainPad.Button.DPad.Left) && (state == BrainPad.Button.State.Pressed) && (choiceProg != progDemo.Attente))
                {
                    choiceProg = progDemo.Attente;
                    Thread.Sleep(150);
                    BrainPad.Display.Clear();
                    DisplayMenu();
                    BrainPad.Display.DrawText(10, 60, "5. Accelerometer", BrainPad.Color.Yellow);
                    activeUp = true; activeDown = true;
                }
                break;

            case Menu.Buzzer:
                if ((button == BrainPad.Button.DPad.Down) && (state == BrainPad.Button.State.Pressed) && activeDown)
                {
                    choiceMenu = Menu.Game;
                    BrainPad.Display.DrawText(10, 70, "6. Buzzer", BrainPad.Color.White);
                    BrainPad.Display.DrawText(10, 80, "7. Game", BrainPad.Color.Yellow);
                }
                else if ((button == BrainPad.Button.DPad.Up) && (state == BrainPad.Button.State.Pressed) && activeUp)
                {
                    choiceMenu = Menu.Accelerometer;
                    BrainPad.Display.DrawText(10, 70, "6. Buzzer", BrainPad.Color.White);
                    BrainPad.Display.DrawText(10, 60, "5. Accelerometer", BrainPad.Color.Yellow);
                }
                else if ((button == BrainPad.Button.DPad.Right) && (state == BrainPad.Button.State.Pressed))
                {
                    activeUp = false; activeDown = false;
                    BrainPad.Display.Clear();
                    InfoBuzzer();
                    choiceProg = progDemo.Buzzer;
                }
                else if ((button == BrainPad.Button.DPad.Left) && (state == BrainPad.Button.State.Pressed) && (choiceProg != progDemo.Attente))
                {
                    choiceProg = progDemo.Attente;
                    BrainPad.Display.Clear();
                    DisplayMenu();
                    BrainPad.Display.DrawText(10, 70, "6. Buzzer", BrainPad.Color.Yellow);
                    activeUp = true; activeDown = true;
                }
                break;

            case Menu.Game:
                if ((button == BrainPad.Button.DPad.Down) && (state == BrainPad.Button.State.Pressed) && activeDown)
                {
                    choiceMenu = Menu.Bulb;
                    BrainPad.Display.DrawText(10, 80, "7. Game", BrainPad.Color.White);
                    BrainPad.Display.DrawText(10, 20, "1. Bulb", BrainPad.Color.Yellow);
                }
                else if ((button == BrainPad.Button.DPad.Up) && (state == BrainPad.Button.State.Pressed) && activeUp)
                {
                    choiceMenu = Menu.Buzzer;
                    BrainPad.Display.DrawText(10, 80, "7. Game", BrainPad.Color.White);
                    BrainPad.Display.DrawText(10, 70, "6. Buzzer", BrainPad.Color.Yellow);
                }
                else if ((button == BrainPad.Button.DPad.Right) && (state == BrainPad.Button.State.Pressed))
                {
                    activeUp = false; activeDown = false;
                    BrainPad.Display.Clear();
                    choiceProg = progDemo.Game;
                    x = 50; y = 50; // position
                    dx = 4; dy = 6; // vitesse et direction 
                    bouncerOk = true;
                }
                else if ((button == BrainPad.Button.DPad.Left) && (state == BrainPad.Button.State.Pressed) && (choiceProg != progDemo.Attente))
                {
                    bouncerOk = false;
                    choiceProg = progDemo.Attente;
                    BrainPad.Display.Clear();
                    DisplayMenu();
                    BrainPad.Display.DrawText(10, 80, "7. Game", BrainPad.Color.Yellow);
                    activeUp = true; activeDown = true;
                }
                break;

            default:

                break;
        }
    }

    public void BrainPadLoop()
    {
        switch (choiceProg)
        {
            case progDemo.Attente:
                Thread.Sleep(10);
                break;

            case progDemo.Bulb:
                BrainPad.LightBulb.SetColor(BrainPad.Color.Red);
                Flash();
                BrainPad.LightBulb.SetColor(BrainPad.Color.Green);
                Flash();
                BrainPad.LightBulb.SetColor(BrainPad.Color.Blue);
                Flash();
                BrainPad.LightBulb.SetColor(BrainPad.Color.Yellow);
                Flash();
                break;

            case progDemo.Traffic:
                BrainPad.TrafficLight.TurnRedLightOn();
                BrainPad.Wait.Seconds(1);
                BrainPad.TrafficLight.TurnRedLightOff();
                BrainPad.TrafficLight.TurnGreenLightOn();
                BrainPad.Wait.Seconds(1);
                BrainPad.TrafficLight.TurnGreenLightOff();
                BrainPad.TrafficLight.TurnYellowLightOn();
                BrainPad.Wait.Seconds(0.5);
                BrainPad.TrafficLight.TurnYellowLightOff();
                break;

            case progDemo.Temperature:
                finTemp = false;

                if (temperature >= 77)
                {
                    stateTemp = colorTemp.green;
                }
                else if (temperature >= 76)
                {
                    stateTemp = colorTemp.blue;
                }
                else
                {
                    stateTemp = colorTemp.red;
                }

                switch (stateTemp)
                {  
                    case colorTemp.blue:
                        for (int i = 0; i < 3; i++)
                        {
                            BrainPad.Display.DrawLine(4, 37 + i, 155, 37 + i, BrainPad.Color.Blue);
                            BrainPad.Display.DrawLine(4, 87 + i, 155, 87 + i, BrainPad.Color.Blue);
                        }
                        BrainPad.Display.DrawExtraLargeText(5, 50, temperature.ToString("F1") + " F", BrainPad.Color.Blue);
                        break;

                    case colorTemp.green:
                        for (int i = 0; i < 3; i++)
                        {
                            BrainPad.Display.DrawLine(4, 37 + i, 155, 37 + i, BrainPad.Color.Green);
                            BrainPad.Display.DrawLine(4, 87 + i, 155, 87 + i, BrainPad.Color.Green);
                        }
                        BrainPad.Display.DrawExtraLargeText(5, 50, temperature.ToString("F1") + " F", BrainPad.Color.Green);
                        break;

                    case colorTemp.red:
                        for (int i = 0; i < 3; i++)
                        {
                            BrainPad.Display.DrawLine(4, 37 + i, 155, 37 + i, BrainPad.Color.Red);
                            BrainPad.Display.DrawLine(4, 87 + i, 155, 87 + i, BrainPad.Color.Red);
                        }
                        BrainPad.Display.DrawExtraLargeText(5, 50, temperature.ToString("F1") + " F", BrainPad.Color.Red);
                        break;

                    default:
                        break;
                }
                finTemp = true;
                Thread.Sleep(10);
                break;

            case progDemo.Light:
                brightness = BrainPad.LightSensor.ReadLightLevel();

                if (brightness < 0.5)
                    BrainPad.LightBulb.TurnOn();
                else
                    BrainPad.LightBulb.TurnOff();
                break;

            case progDemo.Accelerometer:
                BrainPad.Display.DrawText(5, 20, "X : " + BrainPad.Accelerometer.ReadX().ToString("F3"), BrainPad.Color.White);
                BrainPad.Display.DrawText(5, 30, "Y : " + BrainPad.Accelerometer.ReadY().ToString("F3"), BrainPad.Color.White);
                BrainPad.Display.DrawText(5, 40, "Z : " + BrainPad.Accelerometer.ReadZ().ToString("F3"), BrainPad.Color.White);
                Thread.Sleep(100);
                break;

            case progDemo.Buzzer:
                for (int i = 0; i < note.Length; i++)
                {
                    BrainPad.Buzzer.Stop();
                    BrainPad.Buzzer.PlayFrequency(note[i]);
                    Thread.Sleep(duration[i]);
                }
                BrainPad.Buzzer.Stop();

                break;

            case progDemo.Game:
                BrainPad.Display.DrawRectangle(8, 8, 86, 86, BrainPad.Color.Yellow);

                while (bouncerOk)
                {
                    x += dx; y += dy;
                    BrainPad.Display.DrawCircle(x, y, r, BrainPad.Color.Yellow);
                    if (y > 85) BrainPad.Buzzer.PlayNote(BrainPad.Buzzer.Note.A); 
                        else if (y < 15) BrainPad.Buzzer.PlayNote(BrainPad.Buzzer.Note.D); 
                            else BrainPad.Buzzer.Stop();
                    if (x < 15 || x > 85) dx *= -1;
                    if (y < 15 || y > 85) dy *= -1;
                    Thread.Sleep(25);
                    BrainPad.Display.DrawCircle(x, y, r, BrainPad.Color.Black);
                    Thread.Sleep(25);
                }

                break;

            default:
                break;
        }

    }

    // Functions
    //-------------------------------------------------------------------
    public static void DisplayMenu()
    {
        BrainPad.Display.DrawText(60, 0, "Menu", BrainPad.Color.White);
        BrainPad.Display.DrawText(10, 20, "1. Bulb", BrainPad.Color.White);
        BrainPad.Display.DrawText(10, 30, "2. Traffic", BrainPad.Color.White);
        BrainPad.Display.DrawText(10, 40, "3. Temperature Sensor", BrainPad.Color.White);
        BrainPad.Display.DrawText(10, 50, "4. Light Sensor", BrainPad.Color.White);
        BrainPad.Display.DrawText(10, 60, "5. Accelerometer", BrainPad.Color.White);
        BrainPad.Display.DrawText(10, 70, "6. Buzzer", BrainPad.Color.White);
        BrainPad.Display.DrawText(10, 80, "7. Game", BrainPad.Color.White);
        BrainPad.Display.DrawText(120, 80, "Up", BrainPad.Color.White);
        BrainPad.Display.DrawText(95, 96, "Off", BrainPad.Color.White);
        BrainPad.Display.DrawText(140, 96, "On", BrainPad.Color.White);
        BrainPad.Display.DrawText(114, 112, "Down", BrainPad.Color.White);
       

        // BrainPad.Display.DrawText(10, 100, "UP->Suibant | DOWN->Precedent | RIGHT->On | LEFT->Off", BrainPad.Color.White);
    }
    //-------------------------------------------------------------------
    public static void InfoLight()
    {
        BrainPad.Display.DrawText(55, 0, "Light demo", BrainPad.Color.White);
        BrainPad.Display.DrawText(5, 20, "INFORMATION", BrainPad.Color.Yellow);
        BrainPad.Display.DrawText(5, 30, "-----------", BrainPad.Color.Yellow);
        BrainPad.Display.DrawText(5, 40, "The bulb LED lights up", BrainPad.Color.Yellow);
        BrainPad.Display.DrawText(5, 50, "when the light", BrainPad.Color.Yellow);
        BrainPad.Display.DrawText(5, 60, "sensor is hidden!", BrainPad.Color.Yellow);
    }
    //-------------------------------------------------------------------

    public static void InfoBuzzer()
    {
        BrainPad.Display.DrawText(60, 0, "Buzzer demo", BrainPad.Color.White);
        BrainPad.Display.DrawText(5, 20, "INFORMATION", BrainPad.Color.Yellow);
        BrainPad.Display.DrawText(5, 30, "-----------", BrainPad.Color.Yellow);
        BrainPad.Display.DrawText(5, 40, "The buzzer plays a ", BrainPad.Color.Yellow);
        BrainPad.Display.DrawText(5, 50, "melody.", BrainPad.Color.Yellow);
        BrainPad.Display.DrawText(5, 60, "Press LEFT to", BrainPad.Color.Yellow);
        BrainPad.Display.DrawText(5, 70, "stop.", BrainPad.Color.Yellow);
    }
    //-------------------------------------------------------------------
    public static void Flash()
    {
        BrainPad.LightBulb.TurnOn();
        BrainPad.Wait.Seconds(0.5);
        BrainPad.LightBulb.TurnOff();
        BrainPad.Wait.Seconds(0.5);
    }
    //-------------------------------------------------------------------
    public static void OnTimer(object state)
    {
        temperature = BrainPad.TemperatureSensor.ReadTemperature();
        temperature = temperature * (9.0 / 5.0) + 32.0;
    }
}
