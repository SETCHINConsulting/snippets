Public Class InputBoxValidated
    Public Function InputBoxValidated _
        (
        prompt As String,
        title As String,
        Optional stringValidator As IStringValidator = Nothing,
        Optional validationMessage As String = ""
        ) As String
        Dim value As String _
            = Microsoft.VisualBasic.Interaction.InputBox(prompt, title)

        ' If the cancel button wasn't pressed 
        ' And IStringValidator is passed with validation message
        If Not value = String.Empty AndAlso stringValidator IsNot Nothing _
            AndAlso Not String.IsNullOrEmpty(validationMessage) Then
            If Not stringValidator.Validate(value) Then
                MessageBox.Show(validationMessage, Application.ProductName)
                value = InputBoxValidated(
                    prompt, title, stringValidator, validationMessage)
            End If
        End If

        Return value
    End Function
End Class
