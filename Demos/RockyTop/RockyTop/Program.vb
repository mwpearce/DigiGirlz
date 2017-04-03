Class Program

    Dim ButtonPressed As Boolean
    Dim PlayingSong As Boolean = False

    ' Set up length of each note in seconds
    Const Whole As Integer = 2000
    Const Half As Integer = CInt(Whole / 2)
    Const HalfDot As Integer = CInt(Half + (Half / 2))
    Const Quarter As Integer = CInt(Whole / 4)
    Const QuarterDot As Integer = CInt(Quarter + (Quarter / 2))
    Const Eighth As Integer = CInt(Whole / 8)

    'A structure to contain each piece of a song - word to show on screen, note to play, and duration
    Structure Play
        Public Sub New(ByVal word As String, ByVal note As BrainPad.Buzzer.Note, ByVal duration As Integer)
            Me.Word = word
            Me.Note = note
            Me.Duration = duration
        End Sub

        Dim Word As String
        Dim Note As BrainPad.Buzzer.Note
        Dim Duration As Integer
    End Structure

    Public Sub BrainPadSetup()

        BrainPad.Display.Clear()
        BrainPad.Buzzer.Stop()

        BrainPad.Display.DrawText(0, 0, "Press any button to play", BrainPad.Color.White)
        BrainPad.Display.DrawText(0, 10, "Rocky Top", BrainPad.Color.White)

        'Add a handler to detect when a button is pressed.
        AddHandler BrainPad.Button.ButtonPressed, AddressOf AnyButtonPressed

    End Sub

    Sub PlaySong()
        ButtonPressed = False
        PlayingSong = True

        BrainPad.Display.Clear()

        BrainPad.Buzzer.SetVolume(BrainPad.Buzzer.Volume.Quiet)

        'Set up the song
        Dim tune As Play() = {
            New Play("Wish", BrainPad.Buzzer.Note.A5, Quarter),
            New Play(" that", BrainPad.Buzzer.Note.A5, Quarter),
            New Play(" I", BrainPad.Buzzer.Note.A5, Quarter),
            New Play(" was", BrainPad.Buzzer.Note.A5, Quarter),
            New Play(" on", BrainPad.Buzzer.Note.B5, Quarter),
            New Play(" ol'", BrainPad.Buzzer.Note.B5, Quarter),
            New Play(" Roc-", BrainPad.Buzzer.Note.A5, Eighth),
            New Play("ky", BrainPad.Buzzer.Note.F5, Eighth),
            New Play(" Top,", BrainPad.Buzzer.Note.D5, Quarter),
            New Play(" down", BrainPad.Buzzer.Note.D5, Quarter),
            New Play(" in", BrainPad.Buzzer.Note.D5, Eighth),
            New Play(" the", BrainPad.Buzzer.Note.D5, Eighth),
            New Play(" Ten-", BrainPad.Buzzer.Note.E5, Eighth),
            New Play("nes-", BrainPad.Buzzer.Note.D5, Eighth),
            New Play("see", BrainPad.Buzzer.Note.E5, Quarter),
            New Play(" hills,", BrainPad.Buzzer.Note.F5, Whole),
            New Play(" Ain't", BrainPad.Buzzer.Note.A5, Quarter),
            New Play(" no", BrainPad.Buzzer.Note.A5, Quarter),
            New Play(" smog", BrainPad.Buzzer.Note.A5, Quarter),
            New Play("-gy", BrainPad.Buzzer.Note.A5, Quarter),
            New Play(" smoke", BrainPad.Buzzer.Note.B5, Quarter),
            New Play(" on", BrainPad.Buzzer.Note.B5, Quarter),
            New Play(" Roc-", BrainPad.Buzzer.Note.A5, Eighth),
            New Play("ky", BrainPad.Buzzer.Note.F5, Eighth),
            New Play(" Top,", BrainPad.Buzzer.Note.D5, Quarter),
            New Play(" Ain't", BrainPad.Buzzer.Note.D5, Quarter),
            New Play(" no", BrainPad.Buzzer.Note.D5, Quarter),
            New Play(" te-", BrainPad.Buzzer.Note.F5, Eighth),
            New Play("le-", BrainPad.Buzzer.Note.F5, Eighth),
            New Play("phone", BrainPad.Buzzer.Note.F5, Eighth),
            New Play("", BrainPad.Buzzer.Note.E5, Eighth),
            New Play(" bills.", BrainPad.Buzzer.Note.D5, Whole),
            New Play("", BrainPad.Buzzer.Note.Rest, Whole),
            New Play(" Once", BrainPad.Buzzer.Note.A5, Quarter),
            New Play(" I", BrainPad.Buzzer.Note.A5, Quarter),
            New Play(" had", BrainPad.Buzzer.Note.A5, Quarter),
            New Play(" a", BrainPad.Buzzer.Note.A5, Quarter),
            New Play(" girl", BrainPad.Buzzer.Note.B5, Quarter),
            New Play(" on'", BrainPad.Buzzer.Note.B5, Quarter),
            New Play(" Roc-", BrainPad.Buzzer.Note.A5, Eighth),
            New Play("ky", BrainPad.Buzzer.Note.F5, Eighth),
            New Play(" Top,", BrainPad.Buzzer.Note.D5, Quarter),
            New Play(" Half", BrainPad.Buzzer.Note.D5, Quarter),
            New Play(" bear,", BrainPad.Buzzer.Note.D5, Quarter),
            New Play(" o-", BrainPad.Buzzer.Note.E5, Eighth),
            New Play("ther", BrainPad.Buzzer.Note.D5, Eighth),
            New Play(" half", BrainPad.Buzzer.Note.E5, Quarter),
            New Play(" cat,", BrainPad.Buzzer.Note.F5, Whole),
            New Play(" Wild", BrainPad.Buzzer.Note.A5, Quarter),
            New Play(" as", BrainPad.Buzzer.Note.A5, Eighth),
            New Play(" a", BrainPad.Buzzer.Note.A5, Eighth),
            New Play(" mink,", BrainPad.Buzzer.Note.A5, Quarter),
            New Play(" but", BrainPad.Buzzer.Note.A5, Quarter),
            New Play(" sweet", BrainPad.Buzzer.Note.B5, Quarter),
            New Play(" as", BrainPad.Buzzer.Note.B5, Quarter),
            New Play(" so-", BrainPad.Buzzer.Note.A5, Eighth),
            New Play("da", BrainPad.Buzzer.Note.F5, Eighth),
            New Play(" pop,", BrainPad.Buzzer.Note.D5, Quarter),
            New Play(" I", BrainPad.Buzzer.Note.D5, Quarter),
            New Play(" still", BrainPad.Buzzer.Note.D5, Quarter),
            New Play(" dream", BrainPad.Buzzer.Note.F5, Eighth),
            New Play(" a-", BrainPad.Buzzer.Note.F5, Eighth),
            New Play("bout", BrainPad.Buzzer.Note.E5, Quarter),
            New Play(" that.", BrainPad.Buzzer.Note.D5, Whole),
            New Play(" Roc-", BrainPad.Buzzer.Note.B5, Eighth),
            New Play("ky", BrainPad.Buzzer.Note.B5, Eighth),
            New Play(" Top", BrainPad.Buzzer.Note.B5, Half),
            New Play(" you'll", BrainPad.Buzzer.Note.B5, Quarter),
            New Play(" al-", BrainPad.Buzzer.Note.A5, Quarter),
            New Play(" ways", BrainPad.Buzzer.Note.A5, Quarter),
            New Play(" be", BrainPad.Buzzer.Note.A5, Half),
            New Play(" home", BrainPad.Buzzer.Note.C6, Quarter),
            New Play(" sweet", BrainPad.Buzzer.Note.C6Sharp, Quarter),
            New Play(" home", BrainPad.Buzzer.Note.C6Sharp, QuarterDot),
            New Play(" to", BrainPad.Buzzer.Note.A5, Eighth),
            New Play(" me", BrainPad.Buzzer.Note.B5, Whole),
            New Play(" Good", BrainPad.Buzzer.Note.B5, Half),
            New Play(" ol'", BrainPad.Buzzer.Note.B5, Half),
            New Play(" Roc-", BrainPad.Buzzer.Note.A5, Eighth),
            New Play("ky", BrainPad.Buzzer.Note.F5, Eighth),
            New Play(" Top.", BrainPad.Buzzer.Note.D5, HalfDot),
            New Play(" Roc-", BrainPad.Buzzer.Note.D5, Eighth),
            New Play("ky", BrainPad.Buzzer.Note.D5, Eighth),
            New Play(" Top", BrainPad.Buzzer.Note.D5, Quarter),
            New Play(" Ten-", BrainPad.Buzzer.Note.E5, Quarter),
            New Play("nes-", BrainPad.Buzzer.Note.E5, Quarter),
            New Play("see", BrainPad.Buzzer.Note.F5, Whole),
            New Play(" Roc-", BrainPad.Buzzer.Note.D5, Eighth),
            New Play("ky", BrainPad.Buzzer.Note.D5, Eighth),
            New Play(" Top", BrainPad.Buzzer.Note.D5, Quarter),
            New Play(" Ten-", BrainPad.Buzzer.Note.E5, Quarter),
            New Play("nes-", BrainPad.Buzzer.Note.E5, Quarter),
            New Play("see", BrainPad.Buzzer.Note.D5, Whole)
        }

        'Start playing Rocky Top, but stop whenever a button is pressed
        Dim count As Integer = tune.Length

        'Set up constants to control how wide and tall each letter is on the screen
        Const letterWidth = 6
        Const letterHeight = 10

        Dim x As Integer = 0
        Dim y As Integer = 0
        'Loop through each 
        Dim keepPlaying As Boolean = True
        Dim index As Integer = 0
        While keepPlaying And index < count
            'See if we need to move to the next line
            If (x + (tune(index).Word.Length * letterWidth) > 160) Then
                y += letterHeight
                'Do we need to start over at the top of the screen?
                If (y + letterHeight > 130) Then
                    BrainPad.Display.Clear()
                    y = 0
                End If
                x = 0
            End If
            If tune(index).Word.Length > 0 Then
                BrainPad.Display.DrawText(x, y, tune(index).Word, BrainPad.Color.White)
            End If
            If (tune(index).Note = BrainPad.Buzzer.Note.Rest) Then
                BrainPad.Buzzer.Stop()
            Else
                BrainPad.Buzzer.PlayNote(tune(index).Note)
            End If

            BrainPad.Wait.Milliseconds(tune(index).Duration)

            x += (tune(index).Word.Length * letterWidth)
            keepPlaying = Not ShouldWeStop()
            index += 1
        End While

        BrainPad.Display.Clear()
        BrainPad.Buzzer.Stop()
        BrainPad.Display.TurnOff()
    End Sub

    Sub AnyButtonPressed(button As BrainPad.Button.DPad, state As BrainPad.Button.State)
        If state = BrainPad.Button.State.Pressed Then
            If (Not PlayingSong) Then
                RemoveHandler BrainPad.Button.ButtonPressed, AddressOf AnyButtonPressed
                PlaySong()
            End If
        End If
    End Sub

    Public Sub BrainPadLoop()
    End Sub

    Function ShouldWeStop() As Boolean
        If (BrainPad.Button.IsDownPressed() Or BrainPad.Button.IsLeftPressed() Or BrainPad.Button.IsUpPressed() Or BrainPad.Button.IsRightPressed()) Then
            Return True
        Else
            Return False
        End If
    End Function

End Class
