﻿' <auto-generated/>

Imports System.Reflection


Namespace Global.TestProject
    ''' <summary />
    Friend Partial Class Resources
        Private Sub New
        End Sub
        
        Private Shared s_resourceManager As Global.System.Resources.ResourceManager
        ''' <summary />
        Public Shared ReadOnly Property ResourceManager As Global.System.Resources.ResourceManager
            Get
                If s_resourceManager Is Nothing Then
                    s_resourceManager = New Global.System.Resources.ResourceManager(GetType(Resources))
                End If
                Return s_resourceManager
            End Get
        End Property

        ''' <summary>value</summary>
        Public Shared ReadOnly Property [Name] As String
          Get
            Return GetResourceString("Name")
          End Get
        End Property

    End Class
End Namespace
