Option Explicit On
Option Strict On

Imports System.Threading

Namespace Pong
    Module Startup
        Sub Main()
            Dim p = New Program()

            p.BrainPadSetup()
        End Sub
    End Module
End Namespace