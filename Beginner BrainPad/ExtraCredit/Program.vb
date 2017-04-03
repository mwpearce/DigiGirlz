Class Program
    Public Sub BrainPadSetup()
        'Put your setup code here. It runs once when the BrainPad starts up.

        BrainPad.TrafficLight.TurnGreenLightOff()
        BrainPad.TrafficLight.TurnRedLightOff()
        BrainPad.TrafficLight.TurnYellowLightOff()
        BrainPad.Buzzer.Stop()
    End Sub

    Public Sub BrainPadLoop()
        'Put your program code here. It runs repeatedly after the BrainPad starts up.

        If BrainPad.Button.IsUpPressed() Or BrainPad.Button.IsDownPressed() Then
            If BrainPad.Button.IsUpPressed() And BrainPad.Button.IsDownPressed() Then
                BrainPad.TrafficLight.TurnRedLightOn()
                BrainPad.Buzzer.Stop()
            Else
                BrainPad.Buzzer.PlayFrequency(5000)
                BrainPad.TrafficLight.TurnRedLightOff()
            End If
        Else
            BrainPad.TrafficLight.TurnRedLightOff()
            BrainPad.Buzzer.Stop()
        End If
    End Sub
End Class
