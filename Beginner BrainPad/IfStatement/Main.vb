Option Explicit On
Option Strict On

Imports System.Threading

Namespace IfStatement
    Module Startup
        Sub Main()
            Dim p = New Program()

            p.BrainPadSetup()

            While True
                p.BrainPadLoop()

                Thread.Sleep(10)
            End While
        End Sub
    End Module
End Namespace