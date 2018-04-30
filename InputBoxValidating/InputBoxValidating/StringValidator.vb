Public Class StringValidator
    Implements IStringValidator

    Public Sub New(maxLength As Integer, invalidCharacters As String)
        Me.MaxLength = maxLength
        Me.InvalidCharacters = invalidCharacters
    End Sub

    Public Property MaxLength As Integer
    Public Property InvalidCharacters As String
    Public Function Validate(value As String) _
        As Boolean Implements IStringValidator.Validate
        Dim valid As Boolean = True

        Try
            Dim stringValidator As _
                New System.Configuration.StringValidator _
                    (0, MaxLength, InvalidCharacters)
            If stringValidator.CanValidate(value.GetType()) Then
                stringValidator.Validate(value)
            Else
                valid = False
            End If
        Catch ex As ArgumentException
            valid = False
        End Try

        Return valid
    End Function
End Class