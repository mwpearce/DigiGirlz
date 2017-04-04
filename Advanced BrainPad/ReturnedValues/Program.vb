Class Program
    Dim level As Double

    Public Sub BrainPadSetup()
        'Put your setup code here. It runs once when the BrainPad starts up.

        level = 0
    End Sub

    Public Sub BrainPadLoop()
        'Put your program code here. It runs repeatedly after the BrainPad starts up.

        level = BrainPad.LightSensor.ReadLightLevel()
        BrainPad.Display.DrawText(0, 0, level.ToString(), BrainPad.Color.White)

        If level > 0.5 Then
            BrainPad.LightBulb.TurnOn()
        Else
            BrainPad.LightBulb.TurnOff()
        End If
    End Sub
End Class
