Class Program
    Public Sub BrainPadSetup()
        'Put your setup code here. It runs once when the BrainPad starts up.

        Dim count As Integer
        count = 0
        While count < 10
            count = count + 1
            BrainPad.WriteDebugMessage("Count: " + count.ToString())
        End While
    End Sub

    Public Sub BrainPadLoop()
        'Put your program code here. It runs repeatedly after the BrainPad starts up.

    End Sub
End Class
