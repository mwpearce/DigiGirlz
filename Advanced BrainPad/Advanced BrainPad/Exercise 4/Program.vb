Class Program
    Dim count As Integer
    Public Sub BrainPadSetup()
        'Put your setup code here. It runs once when the BrainPad starts up.

        count = 0
    End Sub

    Public Sub BrainPadLoop()
        'Put your program code here. It runs repeatedly after the BrainPad starts up.

        Dim level As Double

        level = BrainPad.LightSensor.ReadLightLevel()

        If BrainPad.Button.IsUpPressed() Then
            If level > 0.6 Then
                BrainPad.WriteDebugMessage("Count: " + count.ToString())
                count = count + 1
                If count > 10 Then
                    count = 0
                End If
            End If
        End If

    End Sub
End Class
