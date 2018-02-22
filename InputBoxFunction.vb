    Public Function InputBoxValidated(prompt As String, title As String, Optional stringValidator As IStringValidator = Nothing, Optional validationMessage As String = "") As String
        Dim value As String = Microsoft.VisualBasic.Interaction.InputBox(Prompt, Title)
    
        'If the cancel button wasn't pressed and IStringValidator is passed with validation message
        If Not value = string.Empty AndAlso stringValidator IsNot Nothing AndAlso Not string.IsNullOrEmpty(validationMessage) Then
            If Not stringValidator.Validate(value) Then
                MessageBox.Show(validationMessage, Application.ProductName)
                value = InputBox(prompt, title, stringValidator, validationMessage)
            End If
        End If

        Return value
    End Function

    Public Interface IStringValidator 
        Function Validate(value As String) As Boolean
    End Interface

    Public Class StringValidator 
        Implements  IStringValidator

        Public Sub New(maxLength As Integer, invalidCharacters As String)
            Me.MaxLength = maxLength
            Me.InvalidCharacters = invalidCharacters
        End Sub

        Public Property MaxLength As Integer
        Public Property InvalidCharacters As String
        Public Function Validate(value As String) As Boolean Implements IStringValidator.Validate
            Dim valid As Boolean = True

            Try
                Dim stringValidator As New System.Configuration.StringValidator(0, MaxLength, InvalidCharacters)                
                if stringValidator.CanValidate(value.GetType()) Then
                    stringValidator.Validate(value)
                Else 
                    valid = false
                End If
            Catch ex As ArgumentException
                valid = False
            End Try

            return valid
        End Function
    End Class