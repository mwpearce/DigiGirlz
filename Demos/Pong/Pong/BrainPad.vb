'
'Copyright GHI Electronics, LLC 2015
'
'Licensed under the Apache License, Version 2.0 (the "License");
'you may not use this file except in compliance with the License.
'You may obtain a copy of the License at
'
'http://www.apache.org/licenses/LICENSE-2.0
'
'Unless required by applicable law or agreed to in writing, software
'distributed under the License is distributed on an "AS IS" BASIS,
'WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
'See the License for the specific language governing permissions and
'limitations under the License.
'


Imports GHI.Pins
Imports Microsoft.SPOT.Hardware
Imports System
Imports System.Threading

''' <summary>
''' The BrainPad class used with GHI Electronics's BrainPad.
''' </summary>
Public NotInheritable Class BrainPad
    Private Sub New()
    End Sub
    ''' <summary>
    ''' A constant value that is always true for endless looping.
    ''' </summary>
    Public Const Looping As Boolean = True

    ''' <summary>
    ''' Writes a message to the output window.
    ''' </summary>
    ''' <param name="message">The message to write.</param>
    Public Shared Sub WriteDebugMessage(message As String)
        Microsoft.SPOT.Debug.Print(message)
    End Sub

    ''' <summary>
    ''' Writes a message to the output window.
    ''' </summary>
    ''' <param name="message">The message to write.</param>
    Public Shared Sub WriteDebugMessage(message As Integer)
        WriteDebugMessage(message.ToString())
    End Sub

    ''' <summary>
    ''' Writes a message to the output window.
    ''' </summary>
    ''' <param name="message">The message to write.</param>
    Public Shared Sub WriteDebugMessage(message As Double)
        WriteDebugMessage(message.ToString())
    End Sub

    ''' <summary>
    ''' Represents a color made up of red, green, and blue.
    ''' </summary>
    Public Class Color
        ''' <summary>
        ''' The amount of red.
        ''' </summary>
        Public Property R() As Byte
            Get
                Return m_R
            End Get
            Set(value As Byte)
                m_R = value
            End Set
        End Property
        Private m_R As Byte

        ''' <summary>
        ''' The amount of green.
        ''' </summary>
        Public Property G() As Byte
            Get
                Return m_G
            End Get
            Set(value As Byte)
                m_G = value
            End Set
        End Property
        Private m_G As Byte

        ''' <summary>
        ''' The amount of blue.
        ''' </summary>
        Public Property B() As Byte
            Get
                Return m_B
            End Get
            Set(value As Byte)
                m_B = value
            End Set
        End Property
        Private m_B As Byte

        ''' <summary>
        ''' The color in 565 format.
        ''' </summary>
        Public ReadOnly Property As565() As UShort
            Get
                Return CUShort(((Me.R And &H1F) * 2048) Or ((Me.G And &H3F) * 32) Or (Me.B And &H1F))
            End Get
        End Property

        ''' <summary>
        ''' Constructs a new instance with the given levels.
        ''' </summary>
        Public Sub New()

            Me.New(0, 0, 0)
        End Sub

        ''' <summary>
        ''' Constructs a new instance with the given levels.
        ''' </summary>
        ''' <param name="red">The amount of red.</param>
        ''' <param name="green">The amount of green.</param>
        ''' <param name="blue">The amount of blue.</param>
        Public Sub New(red As Byte, green As Byte, blue As Byte)
            Me.R = red
            Me.G = green
            Me.B = blue
        End Sub

        ''' <summary>
        ''' A predefined color for black.
        ''' </summary>
        Public Shared Black As New Color(0, 0, 0)
        ''' <summary>
        ''' A predefined color for white.
        ''' </summary>
        Public Shared White As New Color(255, 255, 255)
        ''' <summary>
        ''' A predefined color for red.
        ''' </summary>
        Public Shared Red As New Color(255, 0, 0)
        ''' <summary>
        ''' A predefined color for green.
        ''' </summary>
        Public Shared Green As New Color(0, 255, 0)
        ''' <summary>
        ''' A predefined color for blue.
        ''' </summary>
        Public Shared Blue As New Color(0, 0, 255)
        ''' <summary>
        ''' A predefined color for yellow.
        ''' </summary>
        Public Shared Yellow As New Color(255, 255, 0)
        ''' <summary>
        ''' A predefined color for cyan.
        ''' </summary>
        Public Shared Cyan As New Color(0, 255, 255)
        ''' <summary>
        ''' A predefined color for magneta.
        ''' </summary>
        Public Shared Magneta As New Color(255, 0, 255)
    End Class

    ''' <summary>
    ''' Represents an image that can be reused.
    ''' </summary>
    Public Class Image
        Public Property Width() As Integer
            Get
                Return m_Width
            End Get
            Private Set(value As Integer)
                m_Width = value
            End Set
        End Property
        Private m_Width As Integer
        Public Property Height() As Integer
            Get
                Return m_Height
            End Get
            Private Set(value As Integer)
                m_Height = value
            End Set
        End Property
        Private m_Height As Integer
        Public Property Pixels() As Byte()
            Get
                Return m_Pixels
            End Get
            Private Set(value As Byte())
                m_Pixels = value
            End Set
        End Property
        Private m_Pixels As Byte()

        ''' <summary>
        ''' Constructs a new image with the given dimensions.
        ''' </summary>
        ''' <param name="width">The image width.</param>
        ''' <param name="height">The image height.</param>
        Public Sub New(width As Integer, height As Integer)
            If width < 0 Then
                Throw New ArgumentOutOfRangeException("width", "width must not be negative.")
            End If
            If height < 0 Then
                Throw New ArgumentOutOfRangeException("height", "height must not be negative.")
            End If

            Me.Width = width
            Me.Height = height
            Me.Pixels = New Byte(width * height * 2 - 1) {}
        End Sub

        ''' <summary>
        ''' Sets the given pixel to the given color.
        ''' </summary>
        ''' <param name="x">The x coordinate of the pixel to set.</param>
        ''' <param name="y">The y coordinate of the pixel to set.</param>
        ''' <param name="color">The color to set the pixel to.</param>
        Public Sub SetPixel(x As Integer, y As Integer, color As Color)
            If x < 0 Then
                Throw New ArgumentOutOfRangeException("x", "x must not be negative.")
            End If
            If y < 0 Then
                Throw New ArgumentOutOfRangeException("y", "y must not be negative.")
            End If
            If x > Me.Width Then
                Throw New ArgumentOutOfRangeException("x", "x must not exceed Width.")
            End If
            If y > Me.Height Then
                Throw New ArgumentOutOfRangeException("y", "y must not exceed Height.")
            End If

            Me.Pixels((x * Width + y) * 2 + 0) = CByte(color.As565 \ 256)
            Me.Pixels((x * Width + y) * 2 + 1) = CByte(color.As565)
        End Sub
    End Class

    ''' <summary>
    ''' Provices access to the DC motor on the Brainpad.
    ''' </summary>
    Public NotInheritable Class DcMotor
        Private Sub New()
        End Sub
        Private Shared output As PWM
        Private Shared started As Boolean

        Shared Sub New()
            started = False
            output = New PWM(Peripherals.DcMotor, 2175, 0, False)
        End Sub

        ''' <summary>
        ''' Sets the speed of the DC Motor.
        ''' </summary>
        ''' <param name="speed">A value between -1 (full speed backward) and 1 (full speed forward).</param>
        Public Shared Sub SetSpeed(speed As Double)
            If speed > 1 Or speed < -1 Then
                Throw New ArgumentOutOfRangeException("speed", "speed must be between -1 and 1.")
            End If

            If speed = 1.0 Then
                speed = 0.99
            End If

            If speed = -1.0 Then
                speed = -0.99
            End If

            If started Then
                output.[Stop]()
            End If

            output.DutyCycle = speed
            output.Start()

            started = True
        End Sub

        Public Shared Sub [Stop]()
            If started Then
                output.[Stop]()

                started = False
            End If
        End Sub
    End Class

    ''' <summary>
    ''' Provides access to the servo motor on the BrainPad.
    ''' </summary>
    Public NotInheritable Class ServoMotor
        Private Sub New()
        End Sub
        Private Shared output As PWM
        Private Shared started As Boolean

        Shared Sub New()
            output = New PWM(Peripherals.ServoMotor, 20000, 1250, PWM.ScaleFactor.Microseconds, False)
            started = False
        End Sub

        ''' <summary>
        ''' Sets the position of the Servo Motor.
        ''' </summary>
        ''' <param name="position">The position of the servo between 0 and 180 degrees.</param>
        Public Shared Sub SetPosition(position As Integer)
            If position < 0 OrElse position > 180 Then
                Throw New ArgumentOutOfRangeException("degrees", "degrees must be between 0 and 180.")
            End If

            [Stop]()

            output.Period = 20000
            output.Duration = CUInt(1000.0 + ((position / 180.0) * 1000.0))

            Start()
        End Sub

        ''' <summary>
        ''' Stops the servo motor.
        ''' </summary>
        Public Shared Sub [Stop]()
            If started Then
                output.[Stop]()

                started = False
            End If
        End Sub

        ''' <summary>
        ''' Starts the servo motor.
        ''' </summary>
        Public Shared Sub Start()
            If Not started Then
                output.Start()

                started = True
            End If
        End Sub
    End Class

    ''' <summary>
    ''' Provides access to the light bulb on the BrainPad.
    ''' </summary>
    Public NotInheritable Class LightBulb
        Private Sub New()
        End Sub
        Private Shared red As PWM
        Private Shared green As PWM
        Private Shared blue As PWM
        Private Shared started As Boolean

        Shared Sub New()
            started = False
            red = New PWM(Peripherals.LightBulb.Red, 10000, 1, False)
            green = New PWM(Peripherals.LightBulb.Green, 10000, 1, False)
            blue = New PWM(Peripherals.LightBulb.Blue, 10000, 1, False)
        End Sub

        ''' <summary>
        ''' Sets the color of the light bulb.
        ''' </summary>
        ''' <param name="color">The color to set the light bulb to.</param>
        Public Shared Sub SetColor(color As Color)
            SetColor(color.R / 255.0, color.G / 255.0, color.B / 255.0)
        End Sub

        ''' <summary>
        ''' Sets the color of the light bulb.
        ''' </summary>
        ''' <param name="r">The red value of the color between 0 (fully off) and 1 (fully on).</param>
        ''' <param name="g">The green value of the color between 0 (fully off) and 1 (fully on).</param>
        ''' <param name="blue">The blue value of the color between 0 (fully off) and 1 (fully on).</param>
        Public Shared Sub SetColor(r As Double, g As Double, b As Double)
            If r < 0 OrElse r > 1 Then
                Throw New ArgumentOutOfRangeException("red", "red must be between zero and one.")
            End If
            If g < 0 OrElse g > 1 Then
                Throw New ArgumentOutOfRangeException("green", "green must be between zero and one.")
            End If
            If b < 0 OrElse b > 1 Then
                Throw New ArgumentOutOfRangeException("blue", "blue must be between zero and one.")
            End If

            If started Then
                TurnOff()
            End If

            red.DutyCycle = r
            green.DutyCycle = g
            blue.DutyCycle = b

            TurnOn()
        End Sub

        ''' <summary>
        ''' Turns on the light bulb.
        ''' </summary>
        Public Shared Sub TurnOn()
            If Not started Then
                red.Start()
                green.Start()
                blue.Start()

                started = True
            End If
        End Sub

        ''' <summary>
        ''' Turns off the light bulb.
        ''' </summary>
        Public Shared Sub TurnOff()
            If started Then
                red.[Stop]()
                green.[Stop]()
                blue.[Stop]()

                started = False
            End If
        End Sub
    End Class

    ''' <summary>
    ''' Provides access to the buzzer on the BrainPad.
    ''' </summary>
    Public NotInheritable Class Buzzer
        Private Sub New()
        End Sub
        Private Shared output As PWM
        Private Shared volumeControl As OutputPort
        Private Shared started As Boolean

        Shared Sub New()
            started = False
            output = New PWM(Peripherals.Buzzer, 500, 0.5, False)
            volumeControl = New OutputPort(Peripherals.BuzzerVolumeController, False)
        End Sub

        ''' <summary>
        ''' Notes the Buzzer can play.
        ''' </summary>
        Public Enum Note
            A = 880
            ASharp = 932
            B = 988
            C = 1047
            CSharp = 1109
            D = 1175
            DSharp = 1244
            E = 1319
            F = 1397
            FSharp = 1480
            G = 1568
            GSharp = 1661
        End Enum

        ''' <summary>
        ''' Volumes the Buzzer can play at.
        ''' </summary>
        Public Enum Volume
            ''' <summary>
            ''' The default louder volume.
            ''' </summary>
            Loud
            ''' <summary>
            ''' A quieter volume.
            ''' </summary>
            Quiet
        End Enum

        ''' <summary>
        ''' Sets the volume..
        ''' </summary>
        ''' <param name="volume">The volume to play at.</param>
        Public Shared Sub SetVolume(volume As Volume)
            volumeControl.Write(volume = Volume.Quiet)
        End Sub

        ''' <summary>
        ''' Plays the given note.
        ''' </summary>
        ''' <param name="note">The note to play.</param>
        Public Shared Sub PlayNote(note As Note)
            PlayFrequency(CInt(note))
        End Sub

        ''' <summary>
        ''' Plays a given frequency.
        ''' </summary>
        ''' <param name="frequency">The frequency to play.</param>
        Public Shared Sub PlayFrequency(frequency As Integer)
            [Stop]()

            If frequency > 0 Then
                output.Frequency = frequency
                output.Start()

                started = True
            End If
        End Sub

        ''' <summary>
        ''' Stops any note or frequency currently playing.
        ''' </summary>
        Public Shared Sub [Stop]()
            If started Then
                output.[Stop]()

                started = False
            End If
        End Sub
    End Class

    ''' <summary>
    ''' Provices access to the buttons on the BrainPad.
    ''' </summary>
    Public NotInheritable Class Button
        Private Sub New()
        End Sub
        Private Shared ports As InterruptPort()

        ''' <summary>
        ''' The avilable buttons.
        ''' </summary>
        Public Enum DPad
            Up = 0
            Down
            Left
            Right
        End Enum

        ''' <summary>
        ''' The button state.
        ''' </summary>
        Public Enum State
            Pressed
            NotPressed
        End Enum

        Shared Sub New()
            ports = New InterruptPort() {New InterruptPort(Peripherals.Button.Up, True, Port.ResistorMode.PullUp, Port.InterruptMode.InterruptEdgeBoth), New InterruptPort(Peripherals.Button.Down, True, Port.ResistorMode.PullUp, Port.InterruptMode.InterruptEdgeBoth), New InterruptPort(Peripherals.Button.Left, True, Port.ResistorMode.PullUp, Port.InterruptMode.InterruptEdgeBoth), New InterruptPort(Peripherals.Button.Right, True, Port.ResistorMode.PullUp, Port.InterruptMode.InterruptEdgeBoth)}

            For Each p In ports
                AddHandler p.OnInterrupt, Sub(a, b, c) OnInterrupt(p, b <> 0)
            Next
        End Sub

        ''' <summary>
        ''' Is the down button pressed.
        ''' </summary>
        ''' <returns>Whether or not it is pressed.</returns>
        Public Shared Function IsDownPressed() As Boolean
            Return IsPressed(DPad.Down)
        End Function

        ''' <summary>
        ''' Is the up button pressed.
        ''' </summary>
        ''' <returns>Whether or not it is pressed.</returns>
        Public Shared Function IsUpPressed() As Boolean
            Return IsPressed(DPad.Up)
        End Function

        ''' <summary>
        ''' Is the left button pressed.
        ''' </summary>
        ''' <returns>Whether or not it is pressed.</returns>
        Public Shared Function IsLeftPressed() As Boolean
            Return IsPressed(DPad.Left)
        End Function

        ''' <summary>
        ''' Is the right button pressed.
        ''' </summary>
        ''' <returns>Whether or not it is pressed.</returns>
        Public Shared Function IsRightPressed() As Boolean
            Return IsPressed(DPad.Right)
        End Function

        ''' <summary>
        ''' The signature of all button events.
        ''' </summary>
        ''' <param name="button">The button in question.</param>
        ''' <param name="state">The new button state.</param>
        Public Delegate Sub ButtonEventHandler(button As DPad, state As State)

        ''' <summary>
        ''' The event raised when a button is pressed.
        ''' </summary>
        Public Shared Event ButtonPressed As ButtonEventHandler

        ''' <summary>
        ''' The event raised when a button is released.
        ''' </summary>
        Public Shared Event ButtonReleased As ButtonEventHandler

        ''' <summary>
        ''' The event raised when a button changes state.
        ''' </summary>
        Public Shared Event ButtonChanged As ButtonEventHandler

        ''' <summary>
        ''' Checks if a button is pressed.
        ''' </summary>
        ''' <param name="button">The button to check.</param>
        ''' <returns>Whether or not it was pressed.</returns>
        Public Shared Function IsPressed(button As DPad) As Boolean
            Return Not ports(CInt(button)).Read()
        End Function

        Private Shared Sub OnInterrupt(port As InterruptPort, buttonState As Boolean)
            Dim button = CType(-1, DPad)

            For i = 0 To ports.Length - 1
                If ports(i) Is port Then
                    button = CType(i, DPad)
                End If
            Next

            If Not buttonState Then
                RaiseEvent ButtonPressed(button, State.Pressed)
            ElseIf buttonState Then
                RaiseEvent ButtonReleased(button, State.NotPressed)
            End If

            RaiseEvent ButtonChanged(button, If(buttonState, State.NotPressed, State.Pressed))
        End Sub
    End Class

    ''' <summary>
    ''' Provides access to the traffic light on the BrainPad.
    ''' </summary>
    Public NotInheritable Class TrafficLight
        Private Sub New()
        End Sub
        Private Shared red As PWM
        Private Shared yellow As PWM
        Private Shared green As PWM

        Shared Sub New()
            red = New PWM(Peripherals.TrafficLight.Red, 200, 1.0, False)
            yellow = New PWM(Peripherals.TrafficLight.Yellow, 200, 1.0, False)
            green = New PWM(Peripherals.TrafficLight.Green, 200, 1.0, False)
        End Sub

        ''' <summary>
        ''' Turns the red light on.
        ''' </summary>
        Public Shared Sub TurnRedLightOn()
            TurnColorOn(Color.Red)
        End Sub

        ''' <summary>
        ''' Turns the red light off.
        ''' </summary>
        Public Shared Sub TurnRedLightOff()
            TurnColorOff(Color.Red)
        End Sub

        ''' <summary>
        ''' Turns the yellow light on.
        ''' </summary>
        Public Shared Sub TurnYellowLightOn()
            TurnColorOn(Color.Yellow)
        End Sub

        ''' <summary>
        ''' Turns the yellow light off.
        ''' </summary>
        Public Shared Sub TurnYellowLightOff()
            TurnColorOff(Color.Yellow)
        End Sub

        ''' <summary>
        ''' Turns the green light on.
        ''' </summary>
        Public Shared Sub TurnGreenLightOn()
            TurnColorOn(Color.Green)
        End Sub

        ''' <summary>
        ''' Turns the green light off.
        ''' </summary>
        Public Shared Sub TurnGreenLightOff()
            TurnColorOff(Color.Green)
        End Sub

        ''' <summary>
        ''' Turns off all three lights.
        ''' </summary>
        Public Shared Sub TurnOffAllLights()
            TurnColorOff(Color.Green)
            TurnColorOff(Color.Red)
            TurnColorOff(Color.Yellow)
        End Sub

        ''' <summary>
        ''' Turns on a color.
        ''' </summary>
        ''' <param name="color">The color to turn on.</param>
        Public Shared Sub TurnColorOn(color As BrainPad.Color)
            TurnColor(color, 1.0)
        End Sub

        ''' <summary>
        ''' Turns off a color.
        ''' </summary>
        ''' <param name="color">The color to turn off.</param>
        Public Shared Sub TurnColorOff(color As BrainPad.Color)
            TurnColor(color, 0.0)
        End Sub

        Private Shared Sub TurnColor(color As BrainPad.Color, level As Double)
            If color.R <> 0 AndAlso color.G = 0 AndAlso color.B = 0 Then
                red.[Stop]()
                red.DutyCycle = level
                red.Start()
            ElseIf color.R <> 0 AndAlso color.G <> 0 AndAlso color.B = 0 Then
                yellow.[Stop]()
                yellow.DutyCycle = level
                yellow.Start()
            ElseIf color.R = 0 AndAlso color.G <> 0 AndAlso color.B = 0 Then
                green.[Stop]()
                green.DutyCycle = level
                green.Start()
            Else
                Throw New ArgumentException("This color isn't valid.", "color")
            End If
        End Sub
    End Class

    ''' <summary>
    ''' Provides access to the light sensor on the BrainPad.
    ''' </summary>
    Public NotInheritable Class LightSensor
        Private Sub New()
        End Sub
        Private Shared input As AnalogInput

        Shared Sub New()
            input = New AnalogInput(Peripherals.LightSensor)
        End Sub

        ''' <summary>
        ''' Reads the light level.
        ''' </summary>
        ''' <returns>The light level.</returns>
        Public Shared Function ReadLightLevel() As Double
            Return input.Read()
        End Function
    End Class

    ''' <summary>
    ''' Provides access to the temperature sensor on the BrainPad.
    ''' </summary>
    Public NotInheritable Class TemperatureSensor
        Private Sub New()
        End Sub
        Private Shared input As AnalogInput

        Shared Sub New()
            input = New AnalogInput(Peripherals.TemperatureSensor)
        End Sub

        ''' <summary>
        ''' Reads the temperature.
        ''' </summary>
        ''' <returns>The temperature in celsius.</returns>
        Public Shared Function ReadTemperature() As Double
            Dim sum As Double = 0

            For i = 0 To 9
                sum += input.Read()
            Next

            sum /= 10.0

            Return (sum * 3300.0 - 450.0) / 19.5
        End Function
    End Class

    ''' <summary>
    ''' Provices access to the accelerometer on the BrainPad.
    ''' </summary>
    Public NotInheritable Class Accelerometer
        Private Sub New()
        End Sub
        Private Shared device As I2C
        Private Shared buffer As Byte()

        Shared Sub New()
            buffer = New Byte(1) {}

            device = New I2C(&H1C)
            device.WriteRegister(&H2A, &H1)
        End Sub

        Private Shared Function ReadAxis(register As Byte) As Double
            device.ReadRegisters(register, buffer)

            Dim value = CDbl(buffer(0) * 4 Or buffer(1) \ 64)

            If (value > 511.0) Then
                value -= 1024.0
            End If

            Return value / 256.0
        End Function

        ''' <summary>
        ''' Reads the acceleration on the x axis.
        ''' </summary>
        ''' <returns>The acceleration.</returns>
        Public Shared Function ReadX() As Double
            Return ReadAxis(&H1)
        End Function

        ''' <summary>
        ''' Reads the acceleration on the y axis.
        ''' </summary>
        ''' <returns>The acceleration.</returns>
        Public Shared Function ReadY() As Double
            Return ReadAxis(&H3)
        End Function

        ''' <summary>
        ''' Reads the acceleration on the z axis.
        ''' </summary>
        ''' <returns>The acceleration.</returns>
        Public Shared Function ReadZ() As Double
            Return ReadAxis(&H5)
        End Function
    End Class

    ''' <summary> 
    ''' Controls the display on the BrainPad.
    ''' </summary>
    Public NotInheritable Class Display
        Private Sub New()
        End Sub
        Private Shared spi As SPI
        Private Shared controlPin As OutputPort
        Private Shared resetPin As OutputPort
        Private Shared backlightPin As OutputPort

        Private Shared buffer1 As Byte()
        Private Shared buffer2 As Byte()
        Private Shared buffer4 As Byte()
        Private Shared clearBuffer As Byte()
        Private Shared characterBuffer1 As Byte()
        Private Shared characterBuffer2 As Byte()
        Private Shared characterBuffer4 As Byte()

        Private Const ST7735_MADCTL As Byte = &H36
        Private Const MADCTL_MY As Byte = &H80
        Private Const MADCTL_MX As Byte = &H40
        Private Const MADCTL_MV As Byte = &H20
        Private Const MADCTL_BGR As Byte = &H8

        ''' <summary>
        ''' The width of the display in pixels.
        ''' </summary>
        Public Const Width As Integer = 160

        ''' <summary>
        ''' The height of the display in pixels.
        ''' </summary>
        Public Const Height As Integer = 128

        Shared Sub New()
            buffer1 = New Byte(0) {}
            buffer2 = New Byte(1) {}
            buffer4 = New Byte(3) {}
            clearBuffer = New Byte(160 * 2 * 16 - 1) {}
            characterBuffer1 = New Byte(79) {}
            characterBuffer2 = New Byte(319) {}
            characterBuffer4 = New Byte(1279) {}

            controlPin = New OutputPort(Peripherals.Display.Control, False)
            resetPin = New OutputPort(Peripherals.Display.Reset, False)
            backlightPin = New OutputPort(Peripherals.Display.Backlight, True)
            spi = New SPI(New SPI.Configuration(Peripherals.Display.ChipSelect, False, 0, 0, False, True, 12000, Peripherals.Display.SpiModule))

            Reset()

            WriteCommand(&H11)
            'Sleep exit 
            Thread.Sleep(120)

            ' ST7735R Frame Rate
            WriteCommand(&HB1)
            WriteData(&H1)
            WriteData(&H2C)
            WriteData(&H2D)
            WriteCommand(&HB2)
            WriteData(&H1)
            WriteData(&H2C)
            WriteData(&H2D)
            WriteCommand(&HB3)
            WriteData(&H1)
            WriteData(&H2C)
            WriteData(&H2D)
            WriteData(&H1)
            WriteData(&H2C)
            WriteData(&H2D)

            WriteCommand(&HB4)
            ' Column inversion 
            WriteData(&H7)

            ' ST7735R Power Sequence
            WriteCommand(&HC0)
            WriteData(&HA2)
            WriteData(&H2)
            WriteData(&H84)
            WriteCommand(&HC1)
            WriteData(&HC5)
            WriteCommand(&HC2)
            WriteData(&HA)
            WriteData(&H0)
            WriteCommand(&HC3)
            WriteData(&H8A)
            WriteData(&H2A)
            WriteCommand(&HC4)
            WriteData(&H8A)
            WriteData(&HEE)

            WriteCommand(&HC5)
            ' VCOM 
            WriteData(&HE)

            WriteCommand(&H36)
            ' MX, MY, RGB mode
            WriteData(MADCTL_MX Or MADCTL_MY Or MADCTL_BGR)

            ' ST7735R Gamma Sequence
            WriteCommand(&HE0)
            WriteData(&HF)
            WriteData(&H1A)
            WriteData(&HF)
            WriteData(&H18)
            WriteData(&H2F)
            WriteData(&H28)
            WriteData(&H20)
            WriteData(&H22)
            WriteData(&H1F)
            WriteData(&H1B)
            WriteData(&H23)
            WriteData(&H37)
            WriteData(&H0)

            WriteData(&H7)
            WriteData(&H2)
            WriteData(&H10)
            WriteCommand(&HE1)
            WriteData(&HF)
            WriteData(&H1B)
            WriteData(&HF)
            WriteData(&H17)
            WriteData(&H33)
            WriteData(&H2C)
            WriteData(&H29)
            WriteData(&H2E)
            WriteData(&H30)
            WriteData(&H30)
            WriteData(&H39)
            WriteData(&H3F)
            WriteData(&H0)
            WriteData(&H7)
            WriteData(&H3)
            WriteData(&H10)

            WriteCommand(&H2A)
            WriteData(&H0)
            WriteData(&H0)
            WriteData(&H0)
            WriteData(&H7F)
            WriteCommand(&H2B)
            WriteData(&H0)
            WriteData(&H0)
            WriteData(&H0)
            WriteData(&H9F)

            WriteCommand(&HF0)
            'Enable test command  
            WriteData(&H1)
            WriteCommand(&HF6)
            'Disable ram power save mode 
            WriteData(&H0)

            WriteCommand(&H3A)
            '65k mode 
            WriteData(&H5)

            ' Rotate
            WriteCommand(ST7735_MADCTL)
            WriteData(MADCTL_MV Or MADCTL_MY)

            WriteCommand(&H29)
            'Display on
            Thread.Sleep(50)

            Clear()
        End Sub

        Private Shared Sub WriteData(data As Byte())
            controlPin.Write(True)
            spi.Write(data)
        End Sub

        Private Shared Sub WriteCommand(command As Byte)
            buffer1(0) = command
            controlPin.Write(False)
            spi.Write(buffer1)
        End Sub

        Private Shared Sub WriteData(data As Byte)
            buffer1(0) = data
            controlPin.Write(True)
            spi.Write(buffer1)
        End Sub

        Private Shared Sub Reset()
            resetPin.Write(False)
            Thread.Sleep(300)
            resetPin.Write(True)
            Thread.Sleep(1000)
        End Sub

        Private Shared Sub SetClip(x As Integer, y As Integer, width As Integer, height As Integer)
            WriteCommand(&H2A)

            controlPin.Write(True)
            buffer4(1) = CByte(x)
            buffer4(3) = CByte(x + width - 1)
            spi.Write(buffer4)

            WriteCommand(&H2B)
            controlPin.Write(True)
            buffer4(1) = CByte(y)
            buffer4(3) = CByte(y + height - 1)
            spi.Write(buffer4)
        End Sub

        ''' <summary>
        ''' Clears the Display.
        ''' </summary>
        Public Shared Sub Clear()
            SetClip(0, 0, 160, 128)
            WriteCommand(&H2C)

            For i = 0 To 128 / 16 - 1
                WriteData(clearBuffer)
            Next
        End Sub

        ''' <summary>
        ''' Draws an image.
        ''' </summary>
        ''' <param name="data">The image as a byte array.</param>
        Public Shared Sub DrawImage(data As Byte())
            If data Is Nothing Then
                Throw New ArgumentNullException("data")
            End If
            If data.Length = 0 Then
                Throw New ArgumentException("data.Length must not be zero.", "data")
            End If

            WriteCommand(&H2C)
            WriteData(data)
        End Sub

        ''' <summary>
        ''' Draws an image at the given location.
        ''' </summary>
        ''' <param name="x">The x coordinate to draw at.</param>
        ''' <param name="y">The y coordinate to draw at.</param>
        ''' <param name="image">The image to draw.</param>
        Public Shared Sub DrawImage(x As Integer, y As Integer, image As Image)
            If image Is Nothing Then
                Throw New ArgumentNullException("image")
            End If
            If x < 0 Then
                Throw New ArgumentOutOfRangeException("x", "x must not be negative.")
            End If
            If y < 0 Then
                Throw New ArgumentOutOfRangeException("y", "y must not be negative.")
            End If

            SetClip(x, y, image.Width, image.Height)
            DrawImage(image.Pixels)
        End Sub

        ''' <summary>
        ''' Draws a filled rectangle.
        ''' </summary>
        ''' <param name="x">The x coordinate to draw at.</param>
        ''' <param name="y">The y coordinate to draw at.</param>
        ''' <param name="width">The width of the rectangle.</param>
        ''' <param name="height">The height of the rectangle.</param>
        ''' <param name="color">The color to draw.</param>
        Public Shared Sub DrawFilledRectangle(x As Integer, y As Integer, width As Integer, height As Integer, color As Color)
            If x < 0 Then
                Throw New ArgumentOutOfRangeException("x", "x must not be negative.")
            End If
            If y < 0 Then
                Throw New ArgumentOutOfRangeException("y", "y must not be negative.")
            End If
            If width < 0 Then
                Throw New ArgumentOutOfRangeException("width", "width must not be negative.")
            End If
            If height < 0 Then
                Throw New ArgumentOutOfRangeException("height", "height must not be negative.")
            End If

            SetClip(x, y, width, height)

            Dim data = New Byte(width * height * 2 - 1) {}
            For i = 0 To data.Length - 1 Step 2
                data(i) = CByte((color.As565 \ 256) And &HFF)
                data(i + 1) = CByte((color.As565) And &HFF)
            Next

            DrawImage(data)
        End Sub

        ''' <summary>
        ''' Turns the backlight on.
        ''' </summary>
        Public Shared Sub TurnOn()
            backlightPin.Write(True)
        End Sub

        ''' <summary>
        ''' Turns the backlight off.
        ''' </summary>
        Public Shared Sub TurnOff()
            backlightPin.Write(False)
        End Sub

        ''' <summary>
        ''' Draws a pixel.
        ''' </summary>
        ''' <param name="x">The x coordinate to draw at.</param>
        ''' <param name="y">The y coordinate to draw at.</param>
        ''' <param name="color">The color to draw.</param>
        Public Shared Sub SetPixel(x As Integer, y As Integer, color As Color)
            If x < 0 Then
                Throw New ArgumentOutOfRangeException("x", "x must not be negative.")
            End If
            If y < 0 Then
                Throw New ArgumentOutOfRangeException("y", "y must not be negative.")
            End If

            SetClip(x, y, 1, 1)

            buffer2(0) = CByte(color.As565 \ 256)
            buffer2(1) = CByte(color.As565)

            DrawImage(buffer2)
        End Sub

        ''' <summary>
        ''' Draws a line.
        ''' </summary>
        ''' <param name="x">The x coordinate to start drawing at.</param>
        ''' <param name="y">The y coordinate to start drawing at.</param>
        ''' <param name="x1">The ending x coordinate.</param>
        ''' <param name="y1">The ending y coordinate.</param>
        ''' <param name="color">The color to draw.</param>
        Public Shared Sub DrawLine(x0 As Integer, y0 As Integer, x1 As Integer, y1 As Integer, color As Color)
            If x0 < 0 Then
                Throw New ArgumentOutOfRangeException("x0", "x0 must not be negative.")
            End If
            If y0 < 0 Then
                Throw New ArgumentOutOfRangeException("y0", "y0 must not be negative.")
            End If
            If x1 < 0 Then
                Throw New ArgumentOutOfRangeException("x1", "x1 must not be negative.")
            End If
            If y1 < 0 Then
                Throw New ArgumentOutOfRangeException("y1", "y1 must not be negative.")
            End If

            Dim steep = Math.Abs(y1 - y0) > Math.Abs(x1 - x0)
            Dim t As Integer, dX As Integer, dY As Integer, yStep As Integer, err As Integer

            If steep Then
                t = x0
                x0 = y0
                y0 = t
                t = x1
                x1 = y1
                y1 = t
            End If

            If x0 > x1 Then
                t = x0
                x0 = x1
                x1 = t

                t = y0
                y0 = y1
                y1 = t
            End If

            dX = x1 - x0
            dY = System.Math.Abs(y1 - y0)

            err = (dX \ 2)

            If y0 < y1 Then
                yStep = 1
            Else
                yStep = -1
            End If

            While x0 < x1
                If steep Then
                    SetPixel(y0, x0, color)
                Else
                    SetPixel(x0, y0, color)
                End If

                err -= dY

                If err < 0 Then
                    y0 += CByte(yStep)
                    err += dX
                End If
                x0 += 1
            End While
        End Sub

        ''' <summary>
        ''' Draws a circle.
        ''' </summary>
        ''' <param name="x">The x coordinate to draw at.</param>
        ''' <param name="y">The y coordinate to draw at.</param>
        ''' <param name="r">The radius of the circle.</param>
        ''' <param name="color">The color to draw.</param>
        Public Shared Sub DrawCircle(x As Integer, y As Integer, r As Integer, color As Color)
            If x < 0 Then
                Throw New ArgumentOutOfRangeException("x", "x must not be negative.")
            End If
            If y < 0 Then
                Throw New ArgumentOutOfRangeException("y", "y must not be negative.")
            End If
            If r <= 0 Then
                Throw New ArgumentOutOfRangeException("radius", "radius must be positive.")
            End If

            Dim f As Integer = 1 - r
            Dim ddFX As Integer = 1
            Dim ddFY As Integer = -2 * r
            Dim dX As Integer = 0
            Dim dY As Integer = r

            SetPixel(x, y + r, color)
            SetPixel(x, y - r, color)
            SetPixel(x + r, y, color)
            SetPixel(x - r, y, color)

            While dX < dY
                If f >= 0 Then
                    dY -= 1
                    ddFY += 2
                    f += ddFY
                End If

                dX += 1
                ddFX += 2
                f += ddFX

                SetPixel(x + dX, y + dY, color)
                SetPixel(x - dX, y + dY, color)
                SetPixel(x + dX, y - dY, color)
                SetPixel(x - dX, y - dY, color)

                SetPixel(x + dY, y + dX, color)
                SetPixel(x - dY, y + dX, color)
                SetPixel(x + dY, y - dX, color)
                SetPixel(x - dY, y - dX, color)
            End While
        End Sub

        ''' <summary>
        ''' Draws a rectangle.
        ''' </summary>
        ''' <param name="x">The x coordinate to draw at.</param>
        ''' <param name="y">The y coordinate to draw at.</param>
        ''' <param name="width">The width of the rectangle.</param>
        ''' <param name="height">The height of the rectangle.</param>
        ''' <param name="color">The color to use.</param>
        Public Shared Sub DrawRectangle(x As Integer, y As Integer, width As Integer, height As Integer, color As Color)
            If x < 0 Then
                Throw New ArgumentOutOfRangeException("x", "x must not be negative.")
            End If
            If y < 0 Then
                Throw New ArgumentOutOfRangeException("y", "y must not be negative.")
            End If
            If width < 0 Then
                Throw New ArgumentOutOfRangeException("width", "width must not be negative.")
            End If
            If height < 0 Then
                Throw New ArgumentOutOfRangeException("height", "height must not be negative.")
            End If

            For i = x To x + (width - 1)
                SetPixel(i, y, color)
                SetPixel(i, y + height - 1, color)
            Next

            For i = y To y + (height - 1)
                SetPixel(x, i, color)
                SetPixel(x + width - 1, i, color)
            Next
        End Sub

        ' Space	0x20 
        ' ! 
        ' " 
        ' # 
        ' $ 
        ' % 
        ' & 
        ' ' 
        ' ( 
        ' ) 
        ' // 
        ' + 
        ' , 
        ' - 
        ' . 
        ' / 
        ' 0		0x30 
        ' 1 
        ' 2 
        ' 3 
        ' 4 
        ' 5 
        ' 6 
        ' 7 
        ' 8 
        ' 9 
        ' : 
        ' ; 
        ' < 
        ' = 
        ' > 
        ' ? 
        ' @		0x40 
        ' A 
        ' B 
        ' C 
        ' D 
        ' E 
        ' F 
        ' G 
        ' H 
        ' I 
        ' J 
        ' K 
        ' L 
        ' M 
        ' N 
        ' O 
        ' P		0x50 
        ' Q 
        ' R 
        ' S 
        ' T 
        ' U 
        ' V 
        ' W 
        ' X 
        ' Y 
        ' Z 
        ' [ 
        ' \ 
        ' ] 
        ' ^ 
        ' _ 
        ' `		0x60 
        ' a 
        ' b 
        ' c 
        ' d 
        ' e 
        ' f 
        ' g 
        ' h 
        ' i 
        ' j 
        ' k 
        ' l 
        ' m 
        ' n 
        ' o 
        ' p		0x70 
        ' q 
        ' r 
        ' s 
        ' t 
        ' u 
        ' v 
        ' w 
        ' x 
        ' y 
        ' z 
        ' { 
        ' | 
        ' } 
        ' ~ 
		Shared font As Byte() = New Byte(95 * 5 - 1) {&H0, &H0, &H0, &H0, &H0, &H0, _
			&H0, &H4F, &H0, &H0, &H0, &H7, _
			&H0, &H7, &H0, &H14, &H7F, &H14, _
			&H7F, &H14, &H24, &H2A, &H7F, &H2A, _
			&H12, &H23, &H13, &H8, &H64, &H62, _
			&H36, &H49, &H55, &H22, &H20, &H0, _
			&H5, &H3, &H0, &H0, &H0, &H1C, _
			&H22, &H41, &H0, &H0, &H41, &H22, _
			&H1C, &H0, &H14, &H8, &H3E, &H8, _
			&H14, &H8, &H8, &H3E, &H8, &H8, _
			&H50, &H30, &H0, &H0, &H0, &H8, _
			&H8, &H8, &H8, &H8, &H0, &H60, _
			&H60, &H0, &H0, &H20, &H10, &H8, _
			&H4, &H2, &H3E, &H51, &H49, &H45, _
			&H3E, &H0, &H42, &H7F, &H40, &H0, _
			&H42, &H61, &H51, &H49, &H46, &H21, _
			&H41, &H45, &H4B, &H31, &H18, &H14, _
			&H12, &H7F, &H10, &H27, &H45, &H45, _
			&H45, &H39, &H3C, &H4A, &H49, &H49, _
			&H30, &H1, &H71, &H9, &H5, &H3, _
			&H36, &H49, &H49, &H49, &H36, &H6, _
			&H49, &H49, &H29, &H1E, &H0, &H36, _
			&H36, &H0, &H0, &H0, &H56, &H36, _
			&H0, &H0, &H8, &H14, &H22, &H41, _
			&H0, &H14, &H14, &H14, &H14, &H14, _
			&H0, &H41, &H22, &H14, &H8, &H2, _
			&H1, &H51, &H9, &H6, &H3E, &H41, _
			&H5D, &H55, &H1E, &H7E, &H11, &H11, _
			&H11, &H7E, &H7F, &H49, &H49, &H49, _
			&H36, &H3E, &H41, &H41, &H41, &H22, _
			&H7F, &H41, &H41, &H22, &H1C, &H7F, _
			&H49, &H49, &H49, &H41, &H7F, &H9, _
			&H9, &H9, &H1, &H3E, &H41, &H49, _
			&H49, &H7A, &H7F, &H8, &H8, &H8, _
			&H7F, &H0, &H41, &H7F, &H41, &H0, _
			&H20, &H40, &H41, &H3F, &H1, &H7F, _
			&H8, &H14, &H22, &H41, &H7F, &H40, _
			&H40, &H40, &H40, &H7F, &H2, &HC, _
			&H2, &H7F, &H7F, &H4, &H8, &H10, _
			&H7F, &H3E, &H41, &H41, &H41, &H3E, _
			&H7F, &H9, &H9, &H9, &H6, &H3E, _
			&H41, &H51, &H21, &H5E, &H7F, &H9, _
			&H19, &H29, &H46, &H26, &H49, &H49, _
			&H49, &H32, &H1, &H1, &H7F, &H1, _
			&H1, &H3F, &H40, &H40, &H40, &H3F, _
			&H1F, &H20, &H40, &H20, &H1F, &H3F, _
			&H40, &H38, &H40, &H3F, &H63, &H14, _
			&H8, &H14, &H63, &H7, &H8, &H70, _
			&H8, &H7, &H61, &H51, &H49, &H45, _
			&H43, &H0, &H7F, &H41, &H41, &H0, _
			&H2, &H4, &H8, &H10, &H20, &H0, _
			&H41, &H41, &H7F, &H0, &H4, &H2, _
			&H1, &H2, &H4, &H40, &H40, &H40, _
			&H40, &H40, &H0, &H0, &H3, &H5, _
			&H0, &H20, &H54, &H54, &H54, &H78, _
			&H7F, &H44, &H44, &H44, &H38, &H38, _
			&H44, &H44, &H44, &H44, &H38, &H44, _
			&H44, &H44, &H7F, &H38, &H54, &H54, _
			&H54, &H18, &H4, &H4, &H7E, &H5, _
			&H5, &H8, &H54, &H54, &H54, &H3C, _
			&H7F, &H8, &H4, &H4, &H78, &H0, _
			&H44, &H7D, &H40, &H0, &H20, &H40, _
			&H44, &H3D, &H0, &H7F, &H10, &H28, _
			&H44, &H0, &H0, &H41, &H7F, &H40, _
			&H0, &H7C, &H4, &H7C, &H4, &H78, _
			&H7C, &H8, &H4, &H4, &H78, &H38, _
			&H44, &H44, &H44, &H38, &H7C, &H14, _
			&H14, &H14, &H8, &H8, &H14, &H14, _
			&H14, &H7C, &H7C, &H8, &H4, &H4, _
			&H0, &H48, &H54, &H54, &H54, &H24, _
			&H4, &H4, &H3F, &H44, &H44, &H3C, _
			&H40, &H40, &H20, &H7C, &H1C, &H20, _
			&H40, &H20, &H1C, &H3C, &H40, &H30, _
			&H40, &H3C, &H44, &H28, &H10, &H28, _
			&H44, &HC, &H50, &H50, &H50, &H3C, _
			&H44, &H64, &H54, &H4C, &H44, &H8, _
			&H36, &H41, &H41, &H0, &H0, &H0, _
			&H77, &H0, &H0, &H0, &H41, &H41, _
			&H36, &H8, &H8, &H8, &H2A, &H1C, _
			&H8}

        Private Shared Sub DrawLetter(x As Integer, y As Integer, letter As Char, color As Color, scaleFactor As Integer)
            Dim index = 5 * (Strings.AscW(letter) - 32)
            Dim upper = CByte(color.As565 \ 256)
            Dim lower = CByte(color.As565)
            Dim characterBuffer = If(scaleFactor = 1, characterBuffer1, (If(scaleFactor = 2, characterBuffer2, characterBuffer4)))

            Dim i = 0

            Dim j = 1
            While j <= 64
                For k = 0 To scaleFactor - 1
                    For l = 0 To 4
                        For m = 0 To scaleFactor - 1
                            Dim show = (font(index + l) And j) <> 0

                            characterBuffer(i) = If(show, upper, CByte(&H0))
                            characterBuffer(i + 1) = If(show, lower, CByte(&H0))

                            i += 2
                        Next
                    Next
                Next
                j *= 2
            End While

            SetClip(x, y, 5 * scaleFactor, 8 * scaleFactor)
            DrawImage(characterBuffer)
        End Sub

        ''' <summary>
        ''' Draws a letter at the given location.
        ''' </summary>
        ''' <param name="x">The x coordinate to draw at.</param>
        ''' <param name="y">The y coordinate to draw at.</param>
        ''' <param name="letter">The letter to draw.</param>
        ''' <param name="color">The color to use.</param>
        Public Shared Sub DrawLetter(x As Integer, y As Integer, letter As Char, color As Color)
            If Strings.AscW(letter) > 126 OrElse Strings.AscW(letter) < 32 Then
                Throw New ArgumentOutOfRangeException("letter", "This letter cannot be drawn.")
            End If
            If x < 0 Then
                Throw New ArgumentOutOfRangeException("x", "x must not be negative.")
            End If
            If y < 0 Then
                Throw New ArgumentOutOfRangeException("y", "y must not be negative.")
            End If

            DrawLetter(x, y, letter, color, 1)
        End Sub

        ''' <summary>
        ''' Draws a large letter at the given location.
        ''' </summary>
        ''' <param name="x">The x coordinate to draw at.</param>
        ''' <param name="y">The y coordinate to draw at.</param>
        ''' <param name="letter">The letter to draw.</param>
        ''' <param name="color">The color to use.</param>
        Public Shared Sub DrawLargeLetter(x As Integer, y As Integer, letter As Char, color As Color)
            If Strings.AscW(letter) > 126 OrElse Strings.AscW(letter) < 32 Then
                Throw New ArgumentOutOfRangeException("letter", "This letter cannot be drawn.")
            End If
            If x < 0 Then
                Throw New ArgumentOutOfRangeException("x", "x must not be negative.")
            End If
            If y < 0 Then
                Throw New ArgumentOutOfRangeException("y", "y must not be negative.")
            End If

            DrawLetter(x, y, letter, color, 2)
        End Sub

        ''' <summary>
        ''' Draws an extra large letter at the given location.
        ''' </summary>
        ''' <param name="x">The x coordinate to draw at.</param>
        ''' <param name="y">The y coordinate to draw at.</param>
        ''' <param name="letter">The letter to draw.</param>
        ''' <param name="color">The color to use.</param>
        Public Shared Sub DrawExtraLargeLetter(x As Integer, y As Integer, letter As Char, color As Color)
            If Strings.AscW(letter) > 126 OrElse Strings.AscW(letter) < 32 Then
                Throw New ArgumentOutOfRangeException("letter", "This letter cannot be drawn.")
            End If
            If x < 0 Then
                Throw New ArgumentOutOfRangeException("x", "x must not be negative.")
            End If
            If y < 0 Then
                Throw New ArgumentOutOfRangeException("y", "y must not be negative.")
            End If

            DrawLetter(x, y, letter, color, 4)
        End Sub

        ''' <summary>
        ''' Draws text at the given location.
        ''' </summary>
        ''' <param name="x">The x coordinate to draw at.</param>
        ''' <param name="y">The y coordinate to draw at.</param>
        ''' <param name="text">The text to draw.</param>
        ''' <param name="color">The color to use.</param>
        Public Shared Sub DrawText(x As Integer, y As Integer, text As String, color As Color)
            If x < 0 Then
                Throw New ArgumentOutOfRangeException("x", "x must not be negative.")
            End If
            If y < 0 Then
                Throw New ArgumentOutOfRangeException("y", "y must not be negative.")
            End If
            If text Is Nothing Then
                Throw New ArgumentNullException("text")
            End If

            For i = 0 To text.Length - 1
                DrawLetter(x + i * 6, y, text(i), color, 1)
            Next
        End Sub

        ''' <summary>
        ''' Draws large text at the given location.
        ''' </summary>
        ''' <param name="x">The x coordinate to draw at.</param>
        ''' <param name="y">The y coordinate to draw at.</param>
        ''' <param name="text">The text to draw.</param>
        ''' <param name="color">The color to use.</param>
        Public Shared Sub DrawLargeText(x As Integer, y As Integer, text As String, color As Color)
            If x < 0 Then
                Throw New ArgumentOutOfRangeException("x", "x must not be negative.")
            End If
            If y < 0 Then
                Throw New ArgumentOutOfRangeException("y", "y must not be negative.")
            End If
            If text Is Nothing Then
                Throw New ArgumentNullException("text")
            End If

            For i = 0 To text.Length - 1
                DrawLetter(x + i * 7 * 2, y, text(i), color, 2)
            Next
        End Sub

        ''' <summary>
        ''' Draws extra large text at the given location.
        ''' </summary>
        ''' <param name="x">The x coordinate to draw at.</param>
        ''' <param name="y">The y coordinate to draw at.</param>
        ''' <param name="text">The text to draw.</param>
        ''' <param name="color">The color to use.</param>
        Public Shared Sub DrawExtraLargeText(x As Integer, y As Integer, text As String, color As Color)
            If x < 0 Then
                Throw New ArgumentOutOfRangeException("x", "x must not be negative.")
            End If
            If y < 0 Then
                Throw New ArgumentOutOfRangeException("y", "y must not be negative.")
            End If
            If text Is Nothing Then
                Throw New ArgumentNullException("text")
            End If

            For i = 0 To text.Length - 1
                DrawLetter(x + i * 7 * 4, y, text(i), color, 4)
            Next
        End Sub

        ''' <summary>
        ''' Draws a number at the given location.
        ''' </summary>
        ''' <param name="x">The x coordinate to draw at.</param>
        ''' <param name="y">The y coordinate to draw at.</param>
        ''' <param name="number">The number to draw.</param>
        ''' <param name="color">The color to use.</param>
        Public Shared Sub DrawNumber(x As Integer, y As Integer, number As Double, color As Color)
            If x < 0 Then
                Throw New ArgumentOutOfRangeException("x", "x must not be negative.")
            End If
            If y < 0 Then
                Throw New ArgumentOutOfRangeException("y", "y must not be negative.")
            End If

            DrawText(x, y, number.ToString("N2"), color)
        End Sub

        ''' <summary>
        ''' Draws a large number at the given location.
        ''' </summary>
        ''' <param name="x">The x coordinate to draw at.</param>
        ''' <param name="y">The y coordinate to draw at.</param>
        ''' <param name="number">The number to draw.</param>
        ''' <param name="color">The color to use.</param>
        Public Shared Sub DrawLargeNumber(x As Integer, y As Integer, number As Double, color As Color)
            If x < 0 Then
                Throw New ArgumentOutOfRangeException("x", "x must not be negative.")
            End If
            If y < 0 Then
                Throw New ArgumentOutOfRangeException("y", "y must not be negative.")
            End If

            DrawLargeText(x, y, number.ToString("N2"), color)
        End Sub

        ''' <summary>
        ''' Draws an extra large number at the given location.
        ''' </summary>
        ''' <param name="x">The x coordinate to draw at.</param>
        ''' <param name="y">The y coordinate to draw at.</param>
        ''' <param name="number">The number to draw.</param>
        ''' <param name="color">The color to use.</param>
        Public Shared Sub DrawExtraLargeNumber(x As Integer, y As Integer, number As Double, color As Color)
            If x < 0 Then
                Throw New ArgumentOutOfRangeException("x", "x must not be negative.")
            End If
            If y < 0 Then
                Throw New ArgumentOutOfRangeException("y", "y must not be negative.")
            End If

            DrawExtraLargeText(x, y, number.ToString("N2"), color)
        End Sub

        ''' <summary>
        ''' Draws a number at the given location.
        ''' </summary>
        ''' <param name="x">The x coordinate to draw at.</param>
        ''' <param name="y">The y coordinate to draw at.</param>
        ''' <param name="number">The number to draw.</param>
        ''' <param name="color">The color to use.</param>
        Public Shared Sub DrawNumber(x As Integer, y As Integer, number As Long, color As Color)
            If x < 0 Then
                Throw New ArgumentOutOfRangeException("x", "x must not be negative.")
            End If
            If y < 0 Then
                Throw New ArgumentOutOfRangeException("y", "y must not be negative.")
            End If

            DrawText(x, y, number.ToString("N0"), color)
        End Sub

        ''' <summary>
        ''' Draws a large number at the given location.
        ''' </summary>
        ''' <param name="x">The x coordinate to draw at.</param>
        ''' <param name="y">The y coordinate to draw at.</param>
        ''' <param name="number">The number to draw.</param>
        ''' <param name="color">The color to use.</param>
        Public Shared Sub DrawLargeNumber(x As Integer, y As Integer, number As Long, color As Color)
            If x < 0 Then
                Throw New ArgumentOutOfRangeException("x", "x must not be negative.")
            End If
            If y < 0 Then
                Throw New ArgumentOutOfRangeException("y", "y must not be negative.")
            End If

            DrawLargeText(x, y, number.ToString("N0"), color)
        End Sub

        ''' <summary>
        ''' Draws an extra large number at the given location.
        ''' </summary>
        ''' <param name="x">The x coordinate to draw at.</param>
        ''' <param name="y">The y coordinate to draw at.</param>
        ''' <param name="number">The number to draw.</param>
        ''' <param name="color">The color to use.</param>
        Public Shared Sub DrawExtraLargeNumber(x As Integer, y As Integer, number As Long, color As Color)
            If x < 0 Then
                Throw New ArgumentOutOfRangeException("x", "x must not be negative.")
            End If
            If y < 0 Then
                Throw New ArgumentOutOfRangeException("y", "y must not be negative.")
            End If

            DrawExtraLargeText(x, y, number.ToString("N0"), color)
        End Sub
    End Class

    ''' <summary>
    ''' Tells the BrainPad to wait.
    ''' </summary>
    Public NotInheritable Class Wait
        Private Sub New()
        End Sub
        ''' <summary>
        ''' Tells the BrainPad to wait for the given number of seconds.
        ''' </summary>
        ''' <param name="seconds">The number of seconds to wait.</param>
        Public Shared Sub Seconds(seconds__1 As Double)
            If seconds__1 < 0.0 Then
                Throw New ArgumentOutOfRangeException("seconds", "seconds must not be negative.")
            End If

            Thread.Sleep(CInt(seconds__1 * 1000))
        End Sub

        ''' <summary>
        ''' Tells the BrainPad to wait for the given number of milliseconds.
        ''' </summary>
        ''' <param name="milliseconds">The number of milliseconds to wait.</param>
        Public Shared Sub Milliseconds(milliseconds__1 As Double)
            If milliseconds__1 < 0.0 AndAlso milliseconds__1 <> -1.0 Then
                Throw New ArgumentOutOfRangeException("milliseconds", "milliseconds must not be negative.")
            End If

            Thread.Sleep(CInt(milliseconds__1))
        End Sub
    End Class

    ''' <summary>
    ''' Board definition for the BrainPad expansion.
    ''' </summary>
    Public NotInheritable Class Expansion
        Private Sub New()
        End Sub
        ''' <summary>GPIO definitions.</summary>
        Public NotInheritable Class Gpio
            Private Sub New()
            End Sub
            ''' <summary>GPIO pin.</summary>
            Public Const PA7 As Cpu.Pin = G30.Gpio.PA7
            ''' <summary>GPIO pin.</summary>
            Public Const PA6 As Cpu.Pin = G30.Gpio.PA6
            ''' <summary>GPIO pin.</summary>
            Public Const PC3 As Cpu.Pin = G30.Gpio.PC3
            ''' <summary>GPIO pin.</summary>
            Public Const PB3 As Cpu.Pin = G30.Gpio.PB3
            ''' <summary>GPIO pin.</summary>
            Public Const PB4 As Cpu.Pin = G30.Gpio.PB4
            ''' <summary>GPIO pin.</summary>
            Public Const PB5 As Cpu.Pin = G30.Gpio.PB5
            ''' <summary>GPIO pin.</summary>
            Public Const PA3 As Cpu.Pin = G30.Gpio.PA3
            ''' <summary>GPIO pin.</summary>
            Public Const PA2 As Cpu.Pin = G30.Gpio.PA2
            ''' <summary>GPIO pin.</summary>
            Public Const PA10 As Cpu.Pin = G30.Gpio.PA10
            ''' <summary>GPIO pin.</summary>
            Public Const PA9 As Cpu.Pin = G30.Gpio.PA9
        End Class

        ''' <summary>Analog input definitions.</summary>
        Public NotInheritable Class AnalogInput
            Private Sub New()
            End Sub
            ''' <summary>Analog channel.</summary>
            Public Const PA7 As Cpu.AnalogChannel = G30.AnalogInput.PA7
            ''' <summary>Analog channel.</summary>
            Public Const PA6 As Cpu.AnalogChannel = G30.AnalogInput.PA6
            ''' <summary>Analog channel.</summary>
            Public Const PC3 As Cpu.AnalogChannel = G30.AnalogInput.PC3
            ''' <summary>Analog channel.</summary>
            Public Const PA3 As Cpu.AnalogChannel = G30.AnalogInput.PA3
            ''' <summary>Analog channel.</summary>
            Public Const PA2 As Cpu.AnalogChannel = G30.AnalogInput.PA2
        End Class

        ''' <summary>PWM output definitions.</summary>
        Public NotInheritable Class PwmOutput
            Private Sub New()
            End Sub
            ''' <summary>PWM channel.</summary>
            Public Const PA3 As Cpu.PWMChannel = G30.PwmOutput.PA3
            ''' <summary>PWM channel.</summary>
            Public Const PA2 As Cpu.PWMChannel = G30.PwmOutput.PA2
            ''' <summary>PWM channel.</summary>
            Public Const PA10 As Cpu.PWMChannel = G30.PwmOutput.PA10
            ''' <summary>PWM channel.</summary>
            Public Const PA9 As Cpu.PWMChannel = G30.PwmOutput.PA9
        End Class
    End Class

    ''' <summary>
    ''' Board definition for the BrainPad peripherals.
    ''' </summary>
    Public NotInheritable Class Peripherals
        Private Sub New()
        End Sub
        ''' <summary>
        ''' The onboard display.
        ''' </summary>
        Public NotInheritable Class Display
            Private Sub New()
            End Sub
            ''' <summary>The SPI chip select.</summary>
            Public Const ChipSelect As Cpu.Pin = G30.Gpio.PB12
            ''' <summary>The control pin.</summary>
            Public Const Control As Cpu.Pin = G30.Gpio.PC5
            ''' <summary>The reset pin.</summary>
            Public Const Reset As Cpu.Pin = G30.Gpio.PC4
            ''' <summary>The backlight pin.</summary>
            Public Const Backlight As Cpu.Pin = G30.Gpio.PA4
            ''' <summary>The spi module.</summary>
            Public Const SpiModule As SPI.SPI_module = G30.SpiBus.Spi2
        End Class

        ''' <summary>
        ''' The onboard traffic light LEDs.
        ''' </summary>
        Public NotInheritable Class TrafficLight
            Private Sub New()
            End Sub
            ''' <summary>
            ''' The red LED.
            ''' </summary>
            Public Const Red As Cpu.PWMChannel = G30.PwmOutput.PA1
            ''' <summary>
            ''' The yellow LED.
            ''' </summary>
            Public Const Yellow As Cpu.PWMChannel = G30.PwmOutput.PC6
            ''' <summary>
            ''' The green LED.
            ''' </summary>
            Public Const Green As Cpu.PWMChannel = G30.PwmOutput.PB9
        End Class

        ''' <summary>
        ''' The onboard buttons.
        ''' </summary>
        Public NotInheritable Class Button
            Private Sub New()
            End Sub
            ''' <summary>
            ''' The up button.
            ''' </summary>
            Public Const Up As Cpu.Pin = G30.Gpio.PA15
            ''' <summary>
            ''' The down button.
            ''' </summary>
            Public Const Down As Cpu.Pin = G30.Gpio.PC13
            ''' <summary>
            ''' The left button.
            ''' </summary>
            Public Const Left As Cpu.Pin = G30.Gpio.PB10
            ''' <summary>
            ''' The right button.
            ''' </summary>
            Public Const Right As Cpu.Pin = G30.Gpio.PA5
        End Class

        ''' <summary>
        ''' The onboard touch pads.
        ''' </summary>
        Public NotInheritable Class TouchPad
            Private Sub New()
            End Sub
            ''' <summary>
            ''' The left pad.
            ''' </summary>
            Public Const Left As Cpu.Pin = G30.Gpio.PC0
            ''' <summary>
            ''' The middle pad.
            ''' </summary>
            Public Const Middle As Cpu.Pin = G30.Gpio.PC1
            ''' <summary>
            ''' The right pad.
            ''' </summary>
            Public Const Right As Cpu.Pin = G30.Gpio.PC2
        End Class

        ''' <summary>
        ''' The onboard light bulb LEDs.
        ''' </summary>
        Public NotInheritable Class LightBulb
            Private Sub New()
            End Sub
            ''' <summary>
            ''' The red LED.
            ''' </summary>
            Public Const Red As Cpu.PWMChannel = G30.PwmOutput.PC9
            ''' <summary>
            ''' The green LED.
            ''' </summary>
            Public Const Green As Cpu.PWMChannel = G30.PwmOutput.PC8
            ''' <summary>
            ''' The blue LED.
            ''' </summary>
            Public Const Blue As Cpu.PWMChannel = G30.PwmOutput.PC7
        End Class

        ''' <summary>
        ''' The temperature sensor analog input.
        ''' </summary>
        Public Const TemperatureSensor As Cpu.AnalogChannel = G30.AnalogInput.PB0

        ''' <summary>
        ''' The light sensor analog input.
        ''' </summary>
        Public Const LightSensor As Cpu.AnalogChannel = G30.AnalogInput.PB1

        ''' <summary>
        ''' The buzzer pwm output.
        ''' </summary>
        Public Const Buzzer As Cpu.PWMChannel = G30.PwmOutput.PB8

        ''' <summary>
        ''' The buzzer volume control output.
        ''' </summary>
        Public Const BuzzerVolumeController As Cpu.Pin = G30.Gpio.PC12

        ''' <summary>
        ''' The DC motor pwm output.
        ''' </summary>
        Public Const DcMotor As Cpu.PWMChannel = G30.PwmOutput.PA0

        ''' <summary>
        ''' The servo motor pwm output.
        ''' </summary>
        Public Const ServoMotor As Cpu.PWMChannel = G30.PwmOutput.PA8
    End Class

    Private Class I2C
        Private device As I2CDevice
        Private buffer2 As Byte()
        Private buffer1 As Byte()

        Public Sub New(address As Byte)
            If address > &H7F Then
                Throw New ArgumentOutOfRangeException("address")
            End If

            Me.device = New I2CDevice(New I2CDevice.Configuration(address, 400))
            Me.buffer1 = New Byte(0) {}
            Me.buffer2 = New Byte(1) {}
        End Sub

        Public Sub WriteRegister(register As Byte, value As Byte)
            Me.buffer2(0) = register
            Me.buffer2(1) = value

            Me.device.Execute(New I2CDevice.I2CTransaction() {I2CDevice.CreateWriteTransaction(Me.buffer2)}, 1000)
        End Sub

        Public Function ReadRegister(register As Byte) As Byte
            Me.buffer1(0) = register

            Me.device.Execute(New I2CDevice.I2CTransaction() {I2CDevice.CreateWriteTransaction(Me.buffer1), I2CDevice.CreateReadTransaction(Me.buffer1)}, 1000)

            Return Me.buffer1(0)
        End Function

        Public Function ReadRegisters(register As Byte, count As Integer) As Byte()
            If count < 0 Then
                Throw New ArgumentOutOfRangeException("count")
            End If

            Dim buffer = New Byte(count - 1) {}

            Me.ReadRegisters(register, buffer)

            Return buffer
        End Function

        Public Sub ReadRegisters(register As Byte, buffer As Byte())
            Me.buffer1(0) = register

            Me.device.Execute(New I2CDevice.I2CTransaction() {I2CDevice.CreateWriteTransaction(Me.buffer1), I2CDevice.CreateReadTransaction(buffer)}, 1000)
        End Sub
    End Class

    ''' <summary>
    ''' Legacy drivers for older hardware.
    ''' </summary>
    Public NotInheritable Class Legacy
        ''' <summary>
        ''' Provides access to the touch pads on the BrainPad. The contents of this class are commented out. To use this class, uncomment the below code and add a reference to GHI.Hardware.
        ''' </summary>
        Public NotInheritable Class TouchPad
            'Private Sub New()
            'End Sub
            'Private Shared pins As PulseFeedback()
            'Private Shared thresholds As Long()
            '
            'Shared Sub New()
            '	thresholds = New Long() {130, 130, 130}
            '	pins = New PulseFeedback() {New PulseFeedback(PulseFeedback.Mode.DrainDuration, True, 10, Peripherals.TouchPad.Left), New PulseFeedback(PulseFeedback.Mode.DrainDuration, True, 10, Peripherals.TouchPad.Middle), New PulseFeedback(PulseFeedback.Mode.DrainDuration, True, 10, Peripherals.TouchPad.Right)}
            'End Sub
            '
            '''' <summary>
            '''' The available touch pads.
            '''' </summary>
            'Public Enum Pad
            '	''' <summary>
            '	''' The left pad.
            '	''' </summary>
            '	Left
            '	''' <summary>
            '	''' The middle pad.
            '	''' </summary>
            '	Middle
            '	''' <summary>
            '	''' The right pad.
            '	''' </summary>
            '	Right
            'End Enum
            '
            '''' <summary>
            '''' Determines whether or not the left pad is touched.
            '''' </summary>
            '''' <returns>Whether or not the pad is touched.</returns>
            'Public Shared Function IsLeftTouched() As Boolean
            '	Return IsTouched(Pad.Left)
            'End Function
            '
            '''' <summary>
            '''' Determines whether or not the middle pad is touched.
            '''' </summary>
            '''' <returns>Whether or not the pad is touched.</returns>
            'Public Shared Function IsMiddleTouched() As Boolean
            '	Return IsTouched(Pad.Middle)
            'End Function
            '
            '''' <summary>
            '''' Determines whether or not the right pad is touched.
            '''' </summary>
            '''' <returns>Whether or not the pad is touched.</returns>
            'Public Shared Function IsRightTouched() As Boolean
            '	Return IsTouched(Pad.Right)
            'End Function
            '
            '''' <summary>
            '''' Determines whether or not the given pad is touched.
            '''' </summary>
            '''' <param name="pad">The pad to check.</param>
            '''' <returns>Whether or not the pad is touched.</returns>
            'Public Shared Function IsTouched(pad As Pad) As Boolean
            '	Return pins(CInt(pad)).Read() > thresholds(CInt(pad))
            'End Function
            '
            '''' <summary>
            '''' Sets the threshold beyond which a touch should be detected.
            '''' </summary>
            '''' <param name="pad">The pad to set the threshold for.</param>
            '''' <param name="threshold">The threshold value to set.</param>
            'Public Shared Sub SetThreshold(pad As Pad, threshold As Long)
            '	If threshold <= 0 Then
            '		Throw New ArgumentOutOfRangeException("threshold", "threshold must be positive.")
            '	End If
            '
            '	thresholds(CInt(pad)) = threshold
            'End Sub
        End Class

        ''' <summary>
        ''' Board definition for the BrainPad expansion.
        ''' </summary>
        Public NotInheritable Class Expansion
            Private Sub New()
            End Sub
            ''' <summary>GPIO definitions.</summary>
            Public NotInheritable Class Gpio
                Private Sub New()
                End Sub
                ''' <summary>GPIO pin.</summary>
                Public Const E1 As Cpu.Pin = G30.Gpio.PA7
                ''' <summary>GPIO pin.</summary>
                Public Const E2 As Cpu.Pin = G30.Gpio.PA6
                ''' <summary>GPIO pin.</summary>
                Public Const E3 As Cpu.Pin = G30.Gpio.PC3
                ''' <summary>GPIO pin.</summary>
                Public Const E4 As Cpu.Pin = G30.Gpio.PB3
                ''' <summary>GPIO pin.</summary>
                Public Const E5 As Cpu.Pin = G30.Gpio.PB4
                ''' <summary>GPIO pin.</summary>
                Public Const E6 As Cpu.Pin = G30.Gpio.PB5
                ''' <summary>GPIO pin.</summary>
                Public Const E9 As Cpu.Pin = G30.Gpio.PA3
                ''' <summary>GPIO pin.</summary>
                Public Const E10 As Cpu.Pin = G30.Gpio.PA2
                ''' <summary>GPIO pin.</summary>
                Public Const E11 As Cpu.Pin = G30.Gpio.PA10
                ''' <summary>GPIO pin.</summary>
                Public Const E12 As Cpu.Pin = G30.Gpio.PA9
            End Class

            ''' <summary>Analog input definitions.</summary>
            Public NotInheritable Class AnalogInput
                Private Sub New()
                End Sub
                ''' <summary>Analog channel.</summary>
                Public Const E1 As Cpu.AnalogChannel = G30.AnalogInput.PA7
                ''' <summary>Analog channel.</summary>
                Public Const E2 As Cpu.AnalogChannel = G30.AnalogInput.PA6
                ''' <summary>Analog channel.</summary>
                Public Const E3 As Cpu.AnalogChannel = G30.AnalogInput.PC3
                ''' <summary>Analog channel.</summary>
                Public Const E9 As Cpu.AnalogChannel = G30.AnalogInput.PA3
                ''' <summary>Analog channel.</summary>
                Public Const E10 As Cpu.AnalogChannel = G30.AnalogInput.PA2
            End Class

            ''' <summary>PWM output definitions.</summary>
            Public NotInheritable Class PwmOutput
                Private Sub New()
                End Sub
                ''' <summary>PWM channel.</summary>
                Public Const E9 As Cpu.PWMChannel = G30.PwmOutput.PA3
                ''' <summary>PWM channel.</summary>
                Public Const E10 As Cpu.PWMChannel = G30.PwmOutput.PA2
                ''' <summary>PWM channel.</summary>
                Public Const E11 As Cpu.PWMChannel = G30.PwmOutput.PA10
                ''' <summary>PWM channel.</summary>
                Public Const E12 As Cpu.PWMChannel = G30.PwmOutput.PA9
            End Class
        End Class
    End Class
End Class