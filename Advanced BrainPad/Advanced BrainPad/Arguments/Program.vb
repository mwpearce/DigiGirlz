Class Program
    Public Sub BrainPadSetup()
        'Put your setup code here. It runs once when the BrainPad starts up.

        'This code displays "Hello, world!" on the display.
        BrainPad.Display.DrawText(0, 0, "Hello, world!", BrainPad.Color.White)
    End Sub

    Public Sub BrainPadLoop()
        'Put your program code here. It runs repeatedly after the BrainPad starts up.

        'This code turns the light bulb on and then off.
        BrainPad.LightBulb.TurnOn()
        BrainPad.Wait.Seconds(0.5)
        BrainPad.LightBulb.TurnOff()
        BrainPad.Wait.Seconds(0.5)
    End Sub
End Class
