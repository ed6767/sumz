Public Class ViewsPls
    Private Sub ViewsPls_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Private Sub TrackBar1_Scroll(sender As Object, e As EventArgs) Handles TrackBar1.Scroll
        Label1.Text = "I want " & TrackBar1.Value & " views."
    End Sub

    Private Sub TextBox2_TextChanged(sender As Object, e As EventArgs) Handles TextBox2.TextChanged
        Try
            Timer1.Interval = TextBox2.Text
            Button1.Enabled = True
        Catch ex As Exception
            Button1.Enabled = False
        End Try
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        SetupGroupBox.Enabled = False
        ProgressBar1.Maximum = TrackBar1.Value
        Timer1.Start()
    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        Try
            Timer1.Stop()
            ProgressBar1.Value += 1
            Label4.Text = "Giving view " & ProgressBar1.Value & " of " & ProgressBar1.Maximum
            WebBrowser1.Navigate("http://scratch.mit.edu/projects/embed/" & TextBox1.Text & "/?autostart=true")

            If ProgressBar1.Value = ProgressBar1.Maximum Then
                MsgBox("View bot complete. Enjoy feeling good with yourself and earn real views.", MsgBoxStyle.Information)
                Close()
            End If
            Timer1.Start()
        Catch ex As Exception
            MsgBox("Error: " & ex.Message, MsgBoxStyle.Critical)
            Close()
        End Try
    End Sub
End Class