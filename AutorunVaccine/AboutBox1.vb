Public NotInheritable Class AboutBox1

    Private Sub AboutBox1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        ' 폼의 제목을 설정합니다
        ' Dim ApplicationTitle As String
        'If My.Application.Info.Title <> "" Then
        ' ApplicationTitle = My.Application.Info.Title
        ' Else
        ' ApplicationTitle = System.IO.Path.GetFileNameWithoutExtension(My.Application.Info.AssemblyName)
        ' End If
        'Me.Text = String.Format("정보 {0}", ApplicationTitle)
        ' 정보 상자에 표시되는 모든 텍스트를 초기화합니다.
        ' TODO: "프로젝트" 메뉴에서 선택하여 표시되는 프로젝트 속성 대화 상자의 "응용 프로그램" 창에서 응용 프로그램의 
        '    어셈블리 정보를 사용자 지정합니다.
        Me.LabelProductName.Text = My.Application.Info.ProductName
        Me.LabelVersion.Text = String.Format("버전 {0}", My.Application.Info.Version.ToString)
        Me.LabelCopyright.Text = My.Application.Info.Copyright
        Me.LabelCompanyName.Text = "https://github.com/wjuni/Autorun-Vaccine"
        'Me.TextBoxDescription.Text = My.Application.Info.Description
    End Sub

    Private Sub OKButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OKButton.Click
        Me.Close()
    End Sub

    Private Sub TextBoxDescription_TextChanged(sender As Object, e As EventArgs) Handles TextBoxDescription.TextChanged

    End Sub
End Class
