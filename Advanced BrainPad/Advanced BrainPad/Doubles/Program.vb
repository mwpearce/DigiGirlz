Class Program

    Dim delay As Double

    Public Sub BrainPadSetup()
        'Put your setup code here. It runs once when the BrainPad starts up.
        delay = 0.2
    End Sub

    Public Sub BrainPadLoop()
        'Put your program code here. It runs repeatedly after the BrainPad starts up.
        If BrainPad.Button.IsPressed(BrainPad.Button.DPad.Up) Then
            delay = delay + 0.2
        End If
        If BrainPad.Button.IsPressed(BrainPad.Button.DPad.Down) Then
            delay = delay - 0.2
        End If

        BrainPad.WriteDebugMessage(delay)

        BrainPad.TrafficLight.TurnGreenLightOn()
        BrainPad.Wait.Seconds(delay)
        BrainPad.TrafficLight.TurnGreenLightOff()
        BrainPad.Wait.Seconds(delay)
    End Sub
End Class
