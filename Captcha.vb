Public Class Captcha
    Private Sub WebBrowser1_DocumentCompleted(sender As Object, e As WebBrowserDocumentCompletedEventArgs) Handles WebBrowser1.DocumentCompleted
        If WebBrowser1.Url.ToString = "https://scratch.mit.edu/" Then
            MsgBox("Thanks. The app will now restart.", MsgBoxStyle.Information)
            Application.Restart()
        End If
    End Sub
End Class