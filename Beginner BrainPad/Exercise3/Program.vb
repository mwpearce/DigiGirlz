Class Program
    Public Sub BrainPadSetup()
        'Put your setup code here. It runs once when the BrainPad starts up.
    End Sub

    Public Sub BrainPadLoop()
        'Put your program code here. It runs repeatedly after the BrainPad starts up.
        BrainPad.TrafficLight.TurnRedLightOff()
        BrainPad.TrafficLight.TurnYellowLightOff()
        BrainPad.TrafficLight.TurnGreenLightOn()
        BrainPad.Wait.Seconds(5)
        BrainPad.TrafficLight.TurnGreenLightOff()
        BrainPad.TrafficLight.TurnYellowLightOn()
        BrainPad.Wait.Seconds(1)
        BrainPad.TrafficLight.TurnYellowLightOff()
        BrainPad.TrafficLight.TurnRedLightOn()
        BrainPad.Wait.Seconds(5)
    End Sub
End Class
