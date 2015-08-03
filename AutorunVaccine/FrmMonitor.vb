Public Class FrmMonitor
    Dim Path1 As String = "C:\Users\{0}\Appdata\Roaming"
    Dim Path2 As String = "C:\Users\{0}\Appdata\Roaming\Microsoft\Windows\Start Menu\Programs\Startup"
    Dim Path3 As String = "C:\ProgramData\Microsoft\Windows\Start Menu\Programs\Startup"
    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        Me.Hide()
        Timer1.Enabled = False
    End Sub

    Private Sub FrmMonitor_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim usrname = My.User.Name
       For i = 1 To usrname.Length
            If Mid(usrname, i, 1) = "\" Then
                usrname = Mid(usrname, i + 1)
            End If
        Next
        Path1 = String.Format(Path1, usrname)
        Path2 = String.Format(Path2, usrname)
        FileSystemWatcher1.Path = Path1 & "|" & Path2 & "|" & Path3
    End Sub

    Private Sub FileSystemWatcher1_Created(sender As Object, e As IO.FileSystemEventArgs) Handles FileSystemWatcher1.Created
        My.Computer.FileSystem.RenameFile(e.FullPath, Form1.getonlyname(e.FullPath) & ".bak")

        NotifyIcon1.BalloonTipTitle = "바이러스 감지 - 치료 완료"
        NotifyIcon1.BalloonTipText = e.FullPath & "에 감염된 파일을 제거하였습니다."
        NotifyIcon1.BalloonTipIcon = ToolTipIcon.Info
        NotifyIcon1.ShowBalloonTip(1000)

        
    End Sub

    Private Sub Timer2_Tick(sender As Object, e As EventArgs) Handles Timer2.Tick
        Dim wsp As New List(Of Process)
        Dim tmp = Process.GetProcessesByName("wscript.exe")
        For i = 1 To tmp.Count
            wsp.Add(tmp.GetValue(i - 1))
        Next
        For i = 1 To wsp.Count
            On Error Resume Next
            wsp.Item(i - 1).Kill()
        Next
        NotifyIcon1.BalloonTipTitle = "실행중인 바이러스 치료됨"
        NotifyIcon1.BalloonTipText = "실행중인 바이러스를 치료하였습니다."
        NotifyIcon1.BalloonTipIcon = ToolTipIcon.Info
        NotifyIcon1.ShowBalloonTip(1000)

    End Sub

    Private Sub 실시간감시종료ToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles 실시간감시종료ToolStripMenuItem.Click
        End
    End Sub

    Private Sub 치료화면열기OToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles 치료화면열기OToolStripMenuItem.Click
        Form1.Show()
    End Sub

    Private Sub 정보IToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles 정보IToolStripMenuItem.Click
        AboutBox1.ShowDialog()
    End Sub
End Class