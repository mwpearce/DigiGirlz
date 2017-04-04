Class Program

    Dim frequency As Double
    Dim increment As Integer

    Public Sub BrainPadSetup()
        'Put your setup code here. It runs once when the BrainPad starts up.
        frequency = 0
        increment = 0
    End Sub

    Public Sub BrainPadLoop()
        'Put your program code here. It runs repeatedly after the BrainPad starts up.
        If BrainPad.Button.IsUpPressed() Then
            increment = 100
        End If

        If BrainPad.Button.IsDownPressed() Then
            increment = -100
        End If

        If increment <> 0 Then
            frequency = frequency + increment
            increment = 0

            If frequency > 6000 Then
                BrainPad.TrafficLight.TurnRedLightOn()
            Else
                BrainPad.TrafficLight.TurnRedLightOff()
            End If

            BrainPad.Buzzer.PlayFrequency(frequency)
            BrainPad.WriteDebugMessage(frequency)
            BrainPad.Wait.Seconds(0.2)
            BrainPad.Buzzer.Stop()
        End If
    End Sub
End Class
