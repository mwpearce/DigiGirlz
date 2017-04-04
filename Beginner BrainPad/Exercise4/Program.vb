Class Program
    Public Sub BrainPadSetup()
        'Put your setup code here. It runs once when the BrainPad starts up.

        BrainPad.TrafficLight.TurnRedLightOff()
        BrainPad.TrafficLight.TurnYellowLightOff()
        BrainPad.TrafficLight.TurnGreenLightOff()
    End Sub

    Public Sub BrainPadLoop()
        'Put your program code here. It runs repeatedly after the BrainPad starts up.

        If BrainPad.Button.IsUpPressed() Then
            BrainPad.TrafficLight.TurnGreenLightOn()
        End If
        If BrainPad.Button.IsDownPressed() Then
            BrainPad.TrafficLight.TurnGreenLightOff()
        End If
        If BrainPad.Button.IsLeftPressed() Then
            BrainPad.TrafficLight.TurnGreenLightOn()
            BrainPad.TrafficLight.TurnRedLightOn()
        End If
    End Sub
End Class
