Public Class bigwarning
    Dim okaytoclose = True
    Private Sub Label1_Click(sender As Object, e As EventArgs) Handles Label1.Click

    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        okaytoclose = False
        Close()
    End Sub

    Private Sub Button2_DoubleClick(sender As Object, e As EventArgs) Handles Button2.DoubleClick

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        End
    End Sub

    Private Sub bigwarning_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Private Sub bigwarning_FormClosed(sender As Object, e As FormClosedEventArgs) Handles Me.FormClosed

    End Sub

    Private Sub bigwarning_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        e.Cancel = okaytoclose
        If okaytoclose = True Then
            End
        End If
    End Sub
End Class