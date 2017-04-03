Class Program
    Dim counter As Integer
    Public Sub BrainPadSetup()
        'Put your setup code here. It runs once when the BrainPad starts up.

        BrainPad.TrafficLight.TurnYellowLightOff()

        counter = 0
    End Sub

    Public Sub BrainPadLoop()
        'Put your program code here. It runs repeatedly after the BrainPad starts up.

        counter = counter + 1
        BrainPad.WriteDebugMessage(counter)
        If counter = 1000 Then
            BrainPad.TrafficLight.TurnYellowLightOn()
        End If
    End Sub
End Class
