Class Program
    Public Sub BrainPadSetup()
        For height = 1 To 200
            BrainPad.Display.DrawRectangle(0, 0, 10, height, BrainPad.Color.White)
            BrainPad.Display.DrawText(20, 0, height.ToString(), BrainPad.Color.White)

        Next

    End Sub

    Public Sub BrainPadLoop()

    End Sub
End Class
