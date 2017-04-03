Imports System

Class Program

    'Create some global variables to keep track of the position of the ball and paddle
    Dim centerX As Integer
    Dim centerY As Integer
    Dim deltaX As Integer
    Dim deltaY As Integer
    Dim paddleY As Integer

    Const paddleHeight = 20
    Const ballRadius = 2

    Public Sub BrainPadSetup()
        centerX = 10
        centerY = 10
        deltaX = 3
        deltaY = 3
        paddleY = 55
        BrainPad.Buzzer.SetVolume(BrainPad.Buzzer.Volume.Quiet)

        DrawPaddle(BrainPad.Color.White)

        Dim playGame As Boolean = True
        While playGame
            'Move the paddle if necessary
            If (BrainPad.Button.IsLeftPressed) Then
                playGame = False
            ElseIf (BrainPad.Button.IsUpPressed) Then
                If (paddleY > 4) Then
                    ErasePaddle()
                    paddleY = paddleY - 4
                    DrawPaddle(BrainPad.Color.White)
                End If
            ElseIf (BrainPad.Button.IsDownPressed) Then
                If (paddleY < (126 - paddleHeight)) Then
                    ErasePaddle()
                    paddleY = paddleY + 4
                    DrawPaddle(BrainPad.Color.White)
                End If
            End If

            'Erase the old ball
            BrainPad.Display.DrawCircle(centerX, centerY, ballRadius, BrainPad.Color.Black)
            centerX = centerX + deltaX
            centerY = centerY + deltaY
            ' If the ball is traveling left, check to see if it's hit the paddle
            If (centerX - ballRadius <= 5 And deltaX < 0) Then
                If (centerY >= paddleY And centerY <= paddleY + paddleHeight) Then
                    'Hit the paddle, so reverse direction
                    deltaX = deltaX * -1
                    BrainPad.Buzzer.PlayFrequency(880)
                    BrainPad.Wait.Milliseconds(10)
                    BrainPad.Buzzer.Stop()
                Else
                    BrainPad.Buzzer.PlayFrequency(220)
                    BrainPad.Wait.Seconds(0.5)
                    BrainPad.Buzzer.Stop()
                    centerX = 10
                    centerY = 10
                    deltaX = 3
                    deltaY = 3
                End If
            Else
                If (centerX > (157 - ballRadius)) Then
                    deltaX = deltaX * -1
                    BrainPad.Buzzer.PlayFrequency(880)
                    BrainPad.Wait.Milliseconds(10)
                    BrainPad.Buzzer.Stop()
                End If
                If (centerY > (125 - ballRadius) Or centerY < 10) Then
                    deltaY = deltaY * -1
                    BrainPad.Buzzer.PlayFrequency(880)
                    BrainPad.Wait.Milliseconds(10)
                    BrainPad.Buzzer.Stop()
                End If
            End If

            BrainPad.Display.DrawCircle(centerX, centerY, ballRadius, BrainPad.Color.Green)
            BrainPad.Wait.Milliseconds(10)
        End While

        BrainPad.Display.Clear()
        BrainPad.Display.TurnOff()
        BrainPad.Buzzer.Stop()

    End Sub

    Public Sub BrainPadLoop()

    End Sub

    Sub ErasePaddle()
        DrawPaddle(BrainPad.Color.Black)
    End Sub

    Sub DrawPaddle(ByVal color As BrainPad.Color)
        BrainPad.Display.DrawFilledRectangle(0, paddleY, 5, paddleHeight, color)
    End Sub
End Class
