Class Program
    Public Sub BrainPadSetup()
        'Put your setup code here. It runs once when the BrainPad starts up.
        While BrainPad.Looping
            BrainPad.TrafficLight.TurnGreenLightOn()
            BrainPad.Wait.Seconds(0.5)
            BrainPad.TrafficLight.TurnGreenLightOff()
            BrainPad.Wait.Seconds(0.5)
        End While
    End Sub

    Public Sub BrainPadLoop()
        'Put your program code here. It runs repeatedly after the BrainPad starts up.
    End Sub
End Class
