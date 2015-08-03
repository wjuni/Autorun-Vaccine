Public Class Form1
    Public drvlst As New List(Of String)
    Public Probemode_Public As Boolean = False
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        
        Me.Text = String.Format("Autorun Vaccine (버전 {0})", My.Application.Info.Version.ToString)
        initdrv(CheckBox1.Checked)
    End Sub
    Private Sub initdrv(ByVal Showall As Boolean)
        drvlst.Clear()
        ComboBox1.Items.Clear()
        Dim drvdat = My.Computer.FileSystem.Drives
        For i = 1 To drvdat.Count
            'Shell("attrib -s -h " & drvdat.Item(i - 1).RootDirectory.FullName & "autorun.inf", AppWinStyle.Hide)
            Try
                If (My.Computer.FileSystem.FileExists(drvdat.Item(i - 1).RootDirectory.FullName & "autorun.inf") = True Or IsLnkExist(drvdat.Item(i - 1).RootDirectory.FullName) = True Or Showall = True) And drvdat.Item(i - 1).RootDirectory.FullName <> "C:\" Then
                    ComboBox1.Items.Add(drvdat.Item(i - 1).RootDirectory.FullName & " (" & drvdat.Item(i - 1).VolumeLabel & ")")
                    drvlst.Add(drvdat.Item(i - 1).RootDirectory.FullName)
                End If
            Catch ex As Exception
                If My.Computer.Keyboard.AltKeyDown = True And My.Computer.Keyboard.ShiftKeyDown = True And My.Computer.Keyboard.CtrlKeyDown = True Then MsgBox(ex.Message)
            End Try

        Next
        If drvlst.Count = 0 Then
            ComboBox1.Items.Add("감염된 드라이브 없음")
            Button1.Enabled = False
        End If
        If My.Computer.Keyboard.AltKeyDown = True And My.Computer.Keyboard.ShiftKeyDown = True And My.Computer.Keyboard.CtrlKeyDown = True Then
            Dim str As String = ""

            For i = 1 To drvlst.Count
                str = str & drvlst.Item(i - 1) & vbCrLf
            Next
            MsgBox(str)
        End If
    End Sub
    Private Function IsLnkExist(ByVal Drvltr As String)
        Dim lnklst = My.Computer.FileSystem.GetFiles(Drvltr, FileIO.SearchOption.SearchTopLevelOnly, "*.lnk")
        If lnklst.Count > 0 Then
            Return True
        Else
            Return False
        End If

    End Function


    Private Sub CheckBox1_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox1.CheckedChanged

        initdrv(CheckBox1.Checked)
        If CheckBox2.Checked = True Then
            Button1.Enabled = True
        Else
            If (ComboBox1.SelectedItem <> Nothing And drvlst.Count > 0) Then
                Button1.Enabled = True
            Else
                Button1.Enabled = False
            End If
        End If


        If CheckBox2.Checked = True Then
            ComboBox1.Enabled = False
        Else
            ComboBox1.Enabled = True
        End If
    End Sub


    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If CheckBox2.Checked = True Then
            ComboBox1.Enabled = False
            CheckBox1.Enabled = False
            CheckBox2.Enabled = False
            Button1.Enabled = False
            RadioButton1.Enabled = False
            RadioButton2.Enabled = False
            Dim a As New List(Of String)
            a.Add("")
            If RadioButton1.Checked = True Then

                a.Add(1)
            Else

                a.Add(2)
            End If
            If My.Computer.Keyboard.AltKeyDown = True And My.Computer.Keyboard.CtrlKeyDown = True And My.Computer.Keyboard.ShiftKeyDown = True Then a.Add(1)

            BackgroundWorker1.RunWorkerAsync(a)
        Else
            If RadioButton1.Checked = True Then
                If MsgBox("USB 내부에 있는 악성코드 파일을 제거합니다." & vbCrLf & "개인 파일은 보존되나, USB(외장하드) 내부에 프로그램이 설치되어 있는 경우" & vbCrLf & "이 옵션을 사용하지 않는 것을 권장합니다." & vbCrLf & vbCrLf & "계속하려면 [확인]을 누르세요.", MsgBoxStyle.OkCancel, "주의") = MsgBoxResult.Ok Then
                    ComboBox1.Enabled = False
                    CheckBox1.Enabled = False
                    CheckBox2.Enabled = False
                    RadioButton1.Enabled = False
                    RadioButton2.Enabled = False
                    Button1.Enabled = False
                    Dim a As New List(Of String)
                    a.Add(drvlst(ComboBox1.SelectedIndex))
                    a.Add(1)
                    If My.Computer.Keyboard.AltKeyDown = True And My.Computer.Keyboard.CtrlKeyDown = True And My.Computer.Keyboard.ShiftKeyDown = True Then
                        ListBox1.Items.Add(a.Item(0))
                        a.Add(1)
                    End If

                    BackgroundWorker1.RunWorkerAsync(a)
                End If
            Else
                If MsgBox("USB 내부에 있는 악성코드 파일을 검역소에 보관합니다." & vbCrLf & "USB 치료에 실패하거나 시간이 많이 걸리는 경우 [삭제] 옵션을 사용하세요." & vbCrLf & vbCrLf & "계속하려면 [확인]을 누르세요.", MsgBoxStyle.OkCancel, "주의") = MsgBoxResult.Ok Then
                    ComboBox1.Enabled = False
                    CheckBox1.Enabled = False
                    CheckBox2.Enabled = False
                    RadioButton1.Enabled = False
                    RadioButton2.Enabled = False
                    Button1.Enabled = False
                    Dim a As New List(Of String)
                    a.Add(drvlst(ComboBox1.SelectedIndex))
                    a.Add(2)
                    If My.Computer.Keyboard.AltKeyDown = True And My.Computer.Keyboard.CtrlKeyDown = True And My.Computer.Keyboard.ShiftKeyDown = True Then
                        ListBox1.Items.Add(a.Item(0))
                        a.Add(1)
                    End If
                    BackgroundWorker1.RunWorkerAsync(a)
                End If
            End If

        End If
    End Sub
    Public Function getonlyname(ByVal InputStr As String) As String
        For i = 1 To InputStr.Length
            If Mid(InputStr, i, 1) = "\" Then
                InputStr = Mid(InputStr, i + 1)
                i = 1
            End If
        Next
        Return InputStr
    End Function
    Public Function getdirectdir(ByVal InputStr As String) As String
        Dim recentdir As String = ""
        Dim recentindex As Integer = 1
        For i = 1 To InputStr.Length
            If Mid(InputStr, i, 1) = "\" Then
                recentdir = Mid(InputStr, recentindex, i - recentindex)
                recentindex = i + 1
            End If
        Next
        Return recentdir
    End Function

    Private Sub BackgroundWorker1_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles BackgroundWorker1.DoWork
        Dim drvltr As String = e.Argument.item(0)
        Dim mode As Integer = e.Argument.item(1)
        Dim probeMode As Boolean = False
        If e.Argument.count = 3 Then
            probeMode = True
        End If
        Probemode_Public = probeMode

        Dim usrname = My.User.Name

        If probeMode = True Then BackgroundWorker1.ReportProgress(0, "> ProbeMode Started.")
        BackgroundWorker1.ReportProgress(0, "> 치료 시작...")
        For i = 1 To usrname.Length
            If Mid(usrname, i, 1) = "\" Then
                usrname = Mid(usrname, i + 1)
            End If
        Next
        BackgroundWorker1.ReportProgress(1, "> 사용자 이름 : " & usrname)
        If IsXP() = False Then
            BackgroundWorker1.ReportProgress(1, "> 현재 운영체제 : Windows Vista/7/8")
        Else
            BackgroundWorker1.ReportProgress(1, "> 현재 운영체제 : Windows XP")
        End If
        Dim Dir1 As String = "C:\Users\" & usrname & "\Appdata\Roaming"
        Dim Dir2 As String = "C:\Users\" & usrname & "\Appdata\Roaming\Microsoft\Windows\Start Menu\Programs\Startup"
        Dim Dir3 As String = "C:\ProgramData\Microsoft\Windows\Start Menu\Programs\Startup"
        If IsXP() = True Then
            Dir1 = "C:\Documents and Settings\" & usrname & "\Application Data"
            Dir2 = My.Computer.FileSystem.SpecialDirectories.CurrentUserApplicationData
            Dir3 = My.Computer.FileSystem.SpecialDirectories.AllUsersApplicationData
        End If
        BackgroundWorker1.ReportProgress(2, "> 컴퓨터 감염 여부 검사중(1/3)...")
        Shell("taskkill -f -im conhost.exe", AppWinStyle.Hide, True, 1000)
        Shell("taskkill -f -im wscript.exe", AppWinStyle.Hide, True, 1000)
        If probeMode = True Then BackgroundWorker1.ReportProgress(0, "> Force Stop Conhost and Wscript")
        If My.Computer.FileSystem.DirectoryExists(Dir1) = False Then
            BackgroundWorker1.ReportProgress(3, "> 컴퓨터는 감염되지 않았습니다.")
            GoTo cps2
        End If
        Dim flag As Boolean = False

        Dim appdatafile1 = My.Computer.FileSystem.GetDirectories(Dir1, FileIO.SearchOption.SearchTopLevelOnly)

        If probeMode = True Then BackgroundWorker1.ReportProgress(0, "> AppdataFile Successfully Loaded.")

        If probeMode = True Then BackgroundWorker1.ReportProgress(0, "> AppdataDirectorycount = " & appdatafile1.Count)
        For k = 1 To appdatafile1.Count
            If probeMode = True Then BackgroundWorker1.ReportProgress(0, "> For Directory = " & appdatafile1.Item(k - 1))

            Dim appdatafile = My.Computer.FileSystem.GetFiles(appdatafile1.Item(k - 1), FileIO.SearchOption.SearchTopLevelOnly, "*.js")
            If probeMode = True Then BackgroundWorker1.ReportProgress(0, "> Current Directory File count = " & appdatafile.Count)

            For i = 1 To appdatafile.Count

                Dim curname As String = appdatafile.Item(i - 1)

                If probeMode = True Then BackgroundWorker1.ReportProgress(0, "> Appdataindex=" & (i - 1) & ", file=" & curname)
                If (getonlyname(curname).Length <= 7) And (getdirectdir(curname).Length <= 5) Then
                    If probeMode = True Then BackgroundWorker1.ReportProgress(0, "> This Seems to be Virus...")
                    If mode = 1 Then
                        'Shell("cmd /c ""del """ & curname & """", AppWinStyle.Hide, True, 5000)
                        Delete_File(curname, True, 5000)
                        If probeMode = True Then BackgroundWorker1.ReportProgress(0, ">Delete Successful")
                    ElseIf mode = 2 Then
                        'My.Computer.FileSystem.RenameFile(curname, getonlyname(curname) & ".bak")
                        Backup_File(curname, True, 5000)
                        If probeMode = True Then BackgroundWorker1.ReportProgress(0, ">Rename Successful")
                    End If
                    BackgroundWorker1.ReportProgress(3, "> 감염 파일 제거 : " & curname)
                    flag = True
                Else
                    If probeMode = True Then BackgroundWorker1.ReportProgress(0, "> This is not virus")
                End If
                Debug.Print(appdatafile.Item(i - 1))
            Next
        Next

        If flag = False Then
            BackgroundWorker1.ReportProgress(3, "> 컴퓨터는 감염되지 않았습니다.")
        End If
        BackgroundWorker1.ReportProgress(4, " ")
cps2:

        BackgroundWorker1.ReportProgress(2, "> 컴퓨터 감염 여부 검사중(2/3)...")

        If My.Computer.FileSystem.DirectoryExists(Dir2) = False Then
            BackgroundWorker1.ReportProgress(3, "> 컴퓨터는 감염되지 않았습니다.")
            GoTo cps3
        End If
        Dim appdatafile2 = My.Computer.FileSystem.GetFiles(Dir2, FileIO.SearchOption.SearchTopLevelOnly, "*.js")
        If probeMode = True Then BackgroundWorker1.ReportProgress(0, "> Appdatafile2 Loaded Successfully.")

        For i = 1 To appdatafile2.Count

            Dim curname As String = appdatafile2.Item(i - 1)
            If probeMode = True Then BackgroundWorker1.ReportProgress(0, "> Appdatafile = " & curname)
            If mode = 1 Then
                'Shell("cmd /c ""del """ & curname & """", AppWinStyle.Hide, True, 5000)
                Delete_File(curname, True, 5000)
                If probeMode = True Then BackgroundWorker1.ReportProgress(0, ">Delete Successful")
            ElseIf mode = 2 Then
                'My.Computer.FileSystem.RenameFile(curname, getonlyname(curname) & ".bak")
                Backup_File(curname, True, 5000)

                If probeMode = True Then BackgroundWorker1.ReportProgress(0, ">Rename Successful")
            End If
            BackgroundWorker1.ReportProgress(3, "> 감염 파일 제거 : " & curname)
            Debug.Print(appdatafile2.Item(i - 1))
        Next
        If appdatafile2.Count = 0 Then
            BackgroundWorker1.ReportProgress(3, "> 컴퓨터는 감염되지 않았습니다.")
        End If
        BackgroundWorker1.ReportProgress(4, " ")
cps3:

        BackgroundWorker1.ReportProgress(2, "> 컴퓨터 감염 여부 검사중(3/3)...")
        If My.Computer.FileSystem.DirectoryExists(Dir3) = False Then
            BackgroundWorker1.ReportProgress(3, "> 컴퓨터는 감염되지 않았습니다.")
            GoTo cpsx
        End If
        Dim appdatafile3 = My.Computer.FileSystem.GetFiles(Dir3, FileIO.SearchOption.SearchTopLevelOnly, "*.js")
        For i = 1 To appdatafile3.Count
            Dim curname As String = appdatafile3.Item(i - 1)
            If mode = 1 Then
                'Shell("cmd /c ""del """ & curname & """", AppWinStyle.Hide, True, 5000)
                Delete_File(curname, True, 5000)
                If probeMode = True Then BackgroundWorker1.ReportProgress(0, ">Delete Successful")
            ElseIf mode = 2 Then
                'My.Computer.FileSystem.RenameFile(curname, getonlyname(curname) & ".bak")
                Backup_File(curname, True, 5000)

                If probeMode = True Then BackgroundWorker1.ReportProgress(0, ">Rename Successful")
            End If
            Debug.Print(appdatafile3.Item(i - 1))
        Next
        If appdatafile3.Count = 0 Then
            BackgroundWorker1.ReportProgress(3, "> 컴퓨터는 감염되지 않았습니다.")
        End If
        If probeMode = True Then BackgroundWorker1.ReportProgress(5, "DriveLetter : " & drvltr)

cpsx:
        BackgroundWorker1.ReportProgress(2, "> 제어판 바이러스 제거 중...")
        Dim tmpdir = My.Computer.FileSystem.SpecialDirectories.Temp
        If My.Computer.FileSystem.FileExists(tmpdir & "\tmp.reg") = True Then Delete_File(tmpdir & "\tmp.reg", True, 1000)
        My.Computer.FileSystem.WriteAllText(tmpdir & "\tmp.reg", RichTextBox1.Text, False, System.Text.Encoding.Default)
        Shell("reg import """ & tmpdir & "\tmp.reg""", AppWinStyle.Hide, True, 1000)
        BackgroundWorker1.ReportProgress(2, "> 제어판 바이러스 제거 완료.")

        If drvltr = "" Then GoTo finalline
        If probeMode = True Then BackgroundWorker1.ReportProgress(5, "DriveLetter : " & drvltr)

        BackgroundWorker1.ReportProgress(4, " ")
        BackgroundWorker1.ReportProgress(5, "> USB 바이러스 치료중...(1/4) : 숨김 속성 해제 중... 이 작업은 몇 분 정도 걸립니다.")
        Shell("taskkill -f -im conhost.exe", AppWinStyle.Hide, True, 1000)
        Shell("taskkill -f -im wscript.exe", AppWinStyle.Hide, True, 1000)
        If probeMode = True Then
            BackgroundWorker1.ReportProgress(5, "DriveLetter : " & drvltr)
            BackgroundWorker1.ReportProgress(5, "Shell : cmd /k ""attrib -s -h " & drvltr & "* /s /d""")
            Shell("cmd /k ""attrib -s -h " & drvltr & "* /s /d""", AppWinStyle.NormalFocus, True)
        Else
            Shell("attrib -s -h " & drvltr & "* /s /d", AppWinStyle.Hide, True)
        End If

        BackgroundWorker1.ReportProgress(6, "> USB 바이러스 치료중...(2/4)")
        Shell("taskkill -f -im conhost.exe", AppWinStyle.Hide, True, 1000)
        Shell("taskkill -f -im wscript.exe", AppWinStyle.Hide, True, 1000)

        If My.Computer.FileSystem.FileExists(drvltr & "autorun.inf") = True Then
            'My.Computer.FileSystem.DeleteFile(, FileIO.UIOption.OnlyErrorDialogs, FileIO.RecycleOption.DeletePermanently)
            Delete_File(drvltr & "autorun.inf", True, 1000)
            BackgroundWorker1.ReportProgress(8, "> USB 바이러스 치료중...(2/4) Autorun.inf파일이 제거되었습니다.")
        End If
        BackgroundWorker1.ReportProgress(19, "> USB 바이러스 치료중...(3/4) lnk 파일 제거중...")

        Dim lnklst = My.Computer.FileSystem.GetFiles(drvltr, FileIO.SearchOption.SearchTopLevelOnly, "*.lnk")
        If lnklst.Count > 0 Then
            For i = 1 To lnklst.Count
                'My.Computer.FileSystem.DeleteFile(lnklst.Item(i - 1), FileIO.UIOption.OnlyErrorDialogs, FileIO.RecycleOption.DeletePermanently)
                Delete_File(lnklst.Item(i - 1), True, 5000)

                BackgroundWorker1.ReportProgress(3, "> 감염 파일 제거 : " & lnklst.Item(i - 1))
            Next
        End If
        'Shell("cmd /c ""del " & drvltr & "*.lnk""", AppWinStyle.Hide, True, 5000)
        'Delete_File(drvltr & "*.lnk", True, 5000)
        BackgroundWorker1.ReportProgress(19, "> USB 바이러스 치료중...(4/4) 악성 코드 제거중...")
        'On Error Resume Next

        Dim jslst1 = My.Computer.FileSystem.GetDirectories(drvltr, FileIO.SearchOption.SearchTopLevelOnly)
        For i = 1 To jslst1.Count
            If getonlyname(jslst1.Item(i - 1)) = "System Volume Information" Then
                Continue For
            End If
            Debug.Print(jslst1.Item(i - 1))
            Dim jslst2 = My.Computer.FileSystem.GetFiles(jslst1.Item(i - 1), FileIO.SearchOption.SearchTopLevelOnly, "*.js")
            For j = 1 To jslst2.Count
                If mode = 2 Then
                    'My.Computer.FileSystem.RenameFile(jslst2.Item(j - 1), getonlyname(jslst2.Item(j - 1)) & ".bak")
                    Backup_File(jslst2.Item(j - 1), True, 5000)
                ElseIf mode = 1 Then
                    'Shell("cmd /c ""del " & drvltr & "*.js /F /S"" >> """ & tmpdir & "\log.txt""", AppWinStyle.Hide, True)
                    Delete_File(jslst2.Item(j - 1), True, 5000)
                End If
                BackgroundWorker1.ReportProgress(10, "> 감염 파일 제거 : " & jslst2.Item(j - 1))
            Next
        Next

        'Dim tmpdir = My.Computer.FileSystem.SpecialDirectories.Temp & "\Autorunvaccine"
        'If My.Computer.FileSystem.DirectoryExists(tmpdir) = False Then My.Computer.FileSystem.CreateDirectory(tmpdir)
        'If My.Computer.FileSystem.FileExists(tmpdir & "\log.txt") = True Then My.Computer.FileSystem.DeleteFile(tmpdir & "\log.txt", FileIO.UIOption.OnlyErrorDialogs, FileIO.RecycleOption.DeletePermanently)
        '
        '        Shell("cmd /c ""del " & drvltr & "*.js /F /S"" >> """ & tmpdir & "\log.txt""", AppWinStyle.Hide, True)
        '        If My.Computer.FileSystem.FileExists(tmpdir & "\log.txt") = True Then
        ' Dim rddat As String
        ' rddat = My.Computer.FileSystem.ReadAllText(tmpdir & "\log.txt", System.Text.Encoding.Default)
        ' BackgroundWorker1.ReportProgress(11, "> " & rddat)
        'End If
        'End If

finalline:
        MsgBox("바이러스 치료를 완료하기 위해 Explorer을 재시작합니다. 파일 복사 및 이동 작업이 진행중이지 않은지 확인하세요." & vbCrLf & "[확인]버튼을 누르면 재시작합니다.", MsgBoxStyle.Information)
        Shell("taskkill -f -im explorer.exe", AppWinStyle.Hide, True, 5000)
        Shell("explorer.exe")
        BackgroundWorker1.ReportProgress(7, "> 바이러스 치료가 완료되었습니다.")
        BackgroundWorker1.ReportProgress(7, "> 윈도우 타일 앱의 사용이 불가능할 경우 컴퓨터를 재시동하세요.")

    End Sub

    Private Sub BackgroundWorker1_ProgressChanged(sender As Object, e As System.ComponentModel.ProgressChangedEventArgs) Handles BackgroundWorker1.ProgressChanged
        ListBox1.Items.Add(e.UserState)
        ListBox1.SelectedIndex = ListBox1.Items.Count - 1
    End Sub

    Private Sub CheckBox2_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox2.CheckedChanged
        If CheckBox2.Checked = True Then
            Button1.Enabled = True
        Else
            If (ComboBox1.SelectedItem <> Nothing And drvlst.Count > 0) Then
                Button1.Enabled = True
            Else
                Button1.Enabled = False
            End If
        End If


        If CheckBox2.Checked = True Then
            ComboBox1.Enabled = False
        Else
            ComboBox1.Enabled = True
        End If
    End Sub

    Private Sub ComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox1.SelectedIndexChanged
        If CheckBox2.Checked = True Then
            Button1.Enabled = True
        Else
            If (ComboBox1.SelectedItem <> Nothing And drvlst.Count > 0) Then
                Button1.Enabled = True
            Else
                Button1.Enabled = False
            End If
        End If


        If CheckBox2.Checked = True Then
            ComboBox1.Enabled = False
        Else
            ComboBox1.Enabled = True
        End If
    End Sub

    Private Sub LinkLabel1_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel1.LinkClicked
        AboutBox1.ShowDialog()
    End Sub


    Public Sub Delete_File(ByVal FildAddress As String, ByVal Wait As Boolean, Optional ByVal Timeout As Integer = -1, Optional ByVal Debugmode As Boolean = False)
        If Probemode_Public = True Then
            MsgBox("cmd /k del """ & FildAddress & """ /f /s /q")
            Shell("cmd /k del """ & FildAddress & """ /f /s /q", AppWinStyle.NormalFocus, Wait, Timeout)

        Else
            Shell("cmd /c del """ & FildAddress & """ /f /s /q", AppWinStyle.Hide, Wait, Timeout)

        End If
         End Sub

    Public Sub Backup_File(ByVal FildAddress As String, ByVal Wait As Boolean, Optional ByVal Timeout As Integer = -1)
        Shell("cmd /c move /Y """ & FildAddress & """ """ & FildAddress & ".bak""", AppWinStyle.Hide, Wait, Timeout)
    End Sub


    Public Function IsXP() As Boolean
        If Mid(My.Computer.Info.OSVersion, 1, 1) = "6" Then
            Return False
        Else
            Return True
        End If
    End Function

    Private Sub Label1_Click(sender As Object, e As EventArgs) Handles Label1.Click

    End Sub

    Private Sub Label2_Click(sender As Object, e As EventArgs) Handles Label2.Click

    End Sub
End Class
