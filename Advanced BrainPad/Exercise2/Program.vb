Class Program
    Dim level As Double
    Dim threshold As Double

    Public Sub BrainPadSetup()
        'Put your setup code here. It runs once when the BrainPad starts up.

        level = 0
        threshold = 0.5
    End Sub

    Public Sub BrainPadLoop()
        'Put your program code here. It runs repeatedly after the BrainPad starts up.

        If BrainPad.Button.IsUpPressed() Then
            threshold = BrainPad.LightSensor.ReadLightLevel()
        End If

        level = BrainPad.LightSensor.ReadLightLevel()
        BrainPad.Display.DrawText(0, 0, "Level: " + level.ToString(), BrainPad.Color.White)
        BrainPad.Display.DrawText(0, 10, "Threshold: " + threshold.ToString(), BrainPad.Color.White)

        If level > threshold Then
            BrainPad.LightBulb.TurnOn()
        Else
            BrainPad.LightBulb.TurnOff()
        End If
    End Sub
End Class
