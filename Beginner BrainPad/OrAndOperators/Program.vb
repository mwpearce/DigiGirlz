Class Program
    Public Sub BrainPadSetup()
        'Put your setup code here. It runs once when the BrainPad starts up.
        BrainPad.Buzzer.Stop()
    End Sub

    Public Sub BrainPadLoop()
        'Put your program code here. It runs repeatedly after the BrainPad starts up.

        If BrainPad.Button.IsUpPressed() And BrainPad.Button.IsDownPressed() Then
            BrainPad.Buzzer.PlayFrequency(5000)
        Else
            BrainPad.Buzzer.Stop()
        End If
    End Sub
End Class
