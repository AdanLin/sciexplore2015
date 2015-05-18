Imports System
Imports System.Web.Configuration
Imports System.Data
Imports MySql.Data.MySqlClient
Partial Class sciexplore_project
    Inherits System.Web.UI.Page
    Sub team_info()
        Dim gender As String = Request.QueryString("f")
        Dim guid As String = Request.QueryString("g")
        Dim cs As String = ConfigurationManager.ConnectionStrings("sci_scipower").ConnectionString.ToString()
        Dim Conn As New MySqlConnection(cs)
        Dim ds As MySqlDataReader = Nothing
        Dim AL As New ArrayList()
        Try
            '--step1 讀隊伍資訊
            Conn.Open()  '==連結DB1
            Dim cmd As MySqlCommand = New MySqlCommand("select * from team where guid = '" & guid & "';", Conn)
            ds = cmd.ExecuteReader() '==取出資料
            If ds.Read Then
                Select Case ds("activity_group")
                    Case 1
                        If gender = 0 Then
                            Me.team_group.Text = "網路人氣獎-國小組"
                        Else
                            Me.team_group.Text = "女性桂冠獎-國小組"
                        End If
                    Case 2
                        If gender = 0 Then
                            Me.team_group.Text = "網路人氣獎-國中組"
                        Else
                            Me.team_group.Text = "女性桂冠獎-國中組"
                        End If
                    Case 3
                        If gender = 0 Then
                            Me.team_group.Text = "網路人氣獎-高中組"
                        Else
                            Me.team_group.Text = "女性桂冠獎-高中組"
                        End If
                    Case 4
                        If gender = 0 Then
                            Me.team_group.Text = "網路人氣獎-教師組"
                        Else
                            Me.team_group.Text = "女性桂冠獎-教師組"
                        End If
                        Me.teacher_tr.Style.Add("display", "none")
                End Select
                Me.team_name.Text = ds("team_name").ToString
                Me.team_school.Text = ds("school_name").ToString
                Me.teammate.Text = ds("team_leader_name").ToString
                Me.team_teacher.Text = ds("teacher_name").ToString & "老師"
                Me.team_subject.Text = ds("team_subject").ToString
                Me.team_abrust.Text = ds("abrust").ToString
            End If
            cmd.Dispose()
            ds.Close()
            '--step2 讀隊員姓名
            Dim cmd2 As MySqlCommand = New MySqlCommand("select * from teammate where guid = '" & guid & "';", Conn)
            ds = cmd2.ExecuteReader() '==取出資料
            While ds.Read
                AL.Add(ds.Item("teammate_name"))
            End While
            If AL.Count <> 0 Then
                Me.teammate.Text &= "、"
                For i = 0 To AL.Count - 1
                    If i = AL.Count - 1 Then
                        Me.teammate.Text &= AL(i).ToString
                    Else
                        Me.teammate.Text &= AL(i).ToString
                        Me.teammate.Text &= "、"
                    End If
                Next
            End If
            cmd2.Dispose()
            ds.Close()
            '--step3 內嵌PDF
            Dim cmd3 As MySqlCommand = New MySqlCommand("select * from file where guid = '" & guid & "';", Conn)
            ds = cmd3.ExecuteReader()
            If ds.Read Then
                Dim pdf_filaname As String = ds("file_name").ToString
                Me.pdf_box.Text = "<iframe src=""http://sciexplore.colife.org.tw/Upload/" & pdf_filaname & """></iframe>"
            End If
            cmd3.Dispose()
            ds.Dispose()
        Catch ex As Exception
            Response.Write("Error Message--" & ex.ToString())
        Finally
            If (Conn.State = ConnectionState.Open) Then
                Conn.Close()
                Conn.Dispose()
            End If
            Response.Write(Conn.State.ToString)
        End Try
    End Sub

    Protected Sub Log_Out(sender As Object, e As EventArgs)
        If Not Request.Cookies("user_check") Is Nothing Then
            If Request.Cookies("user_check").Value = "OK" Then
                Dim myCookie As HttpCookie
                myCookie = New HttpCookie("user_check")
                myCookie.Expires = DateTime.Now.AddDays(-2D)
                Response.Cookies.Add(myCookie)
                Response.Redirect("sciexplore_Election.aspx")
            End If
        Else
            Response.Write("<script>alert('您還沒登入喔!');</script>")
        End If
    End Sub

    Protected Sub voting(sender As Object, e As EventArgs)
        If Not Request.Cookies("user_check") Is Nothing Then
            If Request.Cookies("user_check").Value = "OK" Then
                Dim gender As String = Request.QueryString("f")
                Dim guid As String = Request.QueryString("g")
                Dim ds As MySqlDataReader = Nothing
                Dim cs2 As String = ConfigurationManager.ConnectionStrings("sciexplore").ConnectionString.ToString()
                Dim Conn2 As New MySqlConnection(cs2)
                'Dim Write_Conn As New MySqlConnection(cs2)
                Dim group As New Integer
                If Me.team_group.Text = "網路人氣獎-國小組" Or Me.team_group.Text = "女性桂冠獎-國小組" Then
                    group = 1
                ElseIf Me.team_group.Text = "網路人氣獎-國中組" Or Me.team_group.Text = "女性桂冠獎-國中組" Then
                    group = 2
                ElseIf Me.team_group.Text = "網路人氣獎-高中組" Or Me.team_group.Text = "女性桂冠獎-高中組" Then
                    group = 3
                ElseIf Me.team_group.Text = "網路人氣獎-教師組" Or Me.team_group.Text = "女性桂冠獎-教師組" Then
                    group = 4
                End If
                Try
                    '--判斷投票類別
                    Conn2.Open()
                    If gender = "0" Then
                        '--網路人氣獎
                        Dim cmd As MySqlCommand = New MySqlCommand("select COUNT(*) from pop_vote_count where uid = '" & Request.Cookies("sciexplore_user").Value & "' and activity_group = '" & group & "';", Conn2)
                        Dim vote_count As Integer = cmd.ExecuteScalar()
                        cmd.Dispose()
                        '--判斷總投票數量
                        If vote_count < 5 Then
                            Dim cmd2 As MySqlCommand = New MySqlCommand("select COUNT(*) from pop_vote_count where uid = '" & Request.Cookies("sciexplore_user").Value & "' and guid = '" & guid & "' and activity_group = '" & group & "';", Conn2)
                            Dim vote_repeat As Integer = cmd2.ExecuteScalar()
                            cmd2.Dispose()
                            '--判斷是否重複投票
                            If vote_repeat < 1 Then
                                '--寫入
                                'Write_Conn.Open()
                                Dim write_cmd As MySqlCommand = New MySqlCommand("INSERT INTO pop_vote_count(guid, uid, activity_group) values (@guid, @uid, @group)", Conn2)
                                write_cmd.Parameters.AddWithValue("guid", guid)
                                write_cmd.Parameters.AddWithValue("uid", Request.Cookies("sciexplore_user").Value)
                                write_cmd.Parameters.AddWithValue("group", group)
                                Dim RecordsAffected As Integer = write_cmd.ExecuteNonQuery()
                                If RecordsAffected = 0 Then
                                    write_cmd.Cancel()
                                    If (Conn2.State = ConnectionState.Open) Then
                                        Conn2.Close()
                                        Conn2.Dispose()
                                    End If
                                    Response.Write("<script>alert('抱歉，網頁出現錯誤，請聯絡主辦單位，謝謝!" & Conn2.State.ToString & "');</script>")
                                Else
                                    write_cmd.Cancel()
                                    If (Conn2.State = ConnectionState.Open) Then
                                        Conn2.Close()
                                        Conn2.Dispose()
                                    End If
                                    Response.Write("<script>alert('投票成功!您在" & Me.team_group.Text & "還可以再投" & 4 - vote_count & "票喔!" & Conn2.State.ToString & "');</script>")
                                    Response.Write("<script>window.location.href=window.location.href;</script>")
                                End If
                                'write_cmd.Cancel()
                                'Write_Conn.Dispose()
                            Else
                                If (Conn2.State = ConnectionState.Open) Then
                                    Conn2.Close()
                                    Conn2.Dispose()
                                End If
                                Response.Write("<script>alert('同一個作品只能夠投一票喔!!" & Conn2.State.ToString & "');</script>")
                                Response.Write("<script>window.location.href=window.location.href;</script>")
                            End If
                        Else
                            If (Conn2.State = ConnectionState.Open) Then
                                Conn2.Close()
                                Conn2.Dispose()
                            End If
                            Response.Write("<script>alert('您已經投完五票囉!" & Conn2.State.ToString & "');</script>")
                            Response.Write("<script>window.location.href=window.location.href;</script>")
                        End If
                    ElseIf gender = "1" Then
                        '--女性桂冠獎
                        Dim cmd As MySqlCommand = New MySqlCommand("select COUNT(*) from female_vote_count where uid = '" & Request.Cookies("sciexplore_user").Value & "' and activity_group = '" & group & "';", Conn2)
                        Dim vote_count As Integer = cmd.ExecuteScalar()
                        cmd.Dispose()
                        If vote_count < 5 Then
                            '--判斷總投票數量
                            Dim cmd2 As MySqlCommand = New MySqlCommand("select COUNT(*) from female_vote_count where uid = '" & Request.Cookies("sciexplore_user").Value & "' and guid = '" & guid & "' and activity_group = '" & group & "';", Conn2)
                            Dim vote_repeat As Integer = cmd2.ExecuteScalar()
                            cmd2.Dispose()
                            '--判斷是否重複投票
                            If vote_repeat < 1 Then
                                '--寫入
                                'Write_Conn.Open()
                                Dim write_cmd As MySqlCommand = New MySqlCommand("INSERT INTO female_vote_count(guid, uid, activity_group) values (@guid, @uid, @group)", Conn2)
                                write_cmd.Parameters.AddWithValue("guid", guid)
                                write_cmd.Parameters.AddWithValue("uid", Request.Cookies("sciexplore_user").Value)
                                write_cmd.Parameters.AddWithValue("group", group)
                                Dim RecordsAffected As Integer = write_cmd.ExecuteNonQuery()
                                If RecordsAffected = 0 Then
                                    write_cmd.Cancel()
                                    If (Conn2.State = ConnectionState.Open) Then
                                        Conn2.Close()
                                        Conn2.Dispose()
                                    End If
                                    Response.Write("<script>alert('寫入錯誤!');</script>")
                                Else
                                    write_cmd.Cancel()
                                    If (Conn2.State = ConnectionState.Open) Then
                                        Conn2.Close()
                                        Conn2.Dispose()
                                    End If
                                    Response.Write("<script>alert('投票成功!您在" & Me.team_group.Text & "還可以再投" & 4 - vote_count & "票喔!');</script>")
                                    Response.Write("<script>window.location.href=window.location.href;</script>")
                                End If
                                'write_cmd.Cancel()
                                'Write_Conn.Dispose()
                            Else
                                If (Conn2.State = ConnectionState.Open) Then
                                    Conn2.Close()
                                    Conn2.Dispose()
                                End If
                                Response.Write("<script>alert('同一個作品只能夠投一票喔!!');</script>")
                                Response.Write("<script>window.location.href=window.location.href;</script>")
                            End If
                        Else
                            If (Conn2.State = ConnectionState.Open) Then
                                Conn2.Close()
                                Conn2.Dispose()
                            End If
                            Response.Write("<script>alert('您已經投完五票囉!');</script>")
                            Response.Write("<script>window.location.href=window.location.href;</script>")
                        End If
                    End If

                Catch ex As Exception
                    Response.Write("Error Message--" & ex.ToString())
                Finally
                    If (Conn2.State = ConnectionState.Open) Then
                        Conn2.Close()
                        Conn2.Dispose()
                    End If
                End Try
            Else
                Response.Write("<script>alert('請重新登入!');</script>")
                Response.Write("<script>window.location.href=window.location.href;</script>")
            End If
        Else
            Response.Write("<script>alert('您還沒登入喔!');</script>")
            Response.Write("<script>window.location.href=window.location.href;</script>")
        End If
    End Sub

    Protected Sub Page_Init(sender As Object, e As EventArgs) Handles Me.Init
        If Not Request.Cookies("user_check") Is Nothing Then
            If Request.Cookies("user_check").Value = "OK" Then
                Dim gender As String = Request.QueryString("f")
                Dim guid As String = Request.QueryString("g")
                Dim cs2 As String = ConfigurationManager.ConnectionStrings("sciexplore").ConnectionString.ToString()
                Dim Conn2 As New MySqlConnection(cs2)
                'Response.Write("helo! welceome~您的會員編號是:" & Request.Cookies("sciexplore_user").Value)
                Me.login_td.Style.Item("display") = "none"
                Try
                    Conn2.Open()
                    If gender = "0" Then
                        '--網路人氣獎
                        Dim cmd2 As MySqlCommand = New MySqlCommand("select COUNT(*) from pop_vote_count where uid = '" & Request.Cookies("sciexplore_user").Value & "' and guid = '" & guid & "';", Conn2)
                        Dim vote_repeat As Integer = cmd2.ExecuteScalar()
                        cmd2.Dispose()
                        If vote_repeat >= 1 Then
                            Me.vote.Disabled = False
                            Me.vote.Visible = False
                            Me.Literal_btn.Visible = True
                            Me.Literal_btn.Text = "<div id=""vote"" style=""text-align:center;""><a id=""vote_btn"" runat=""server"" onserverclick=""voting"">已投票</a></div>"
                        End If
                    ElseIf gender = "1" Then
                        '--女性桂冠獎
                        Dim cmd2 As MySqlCommand = New MySqlCommand("select COUNT(*) from female_vote_count where uid = '" & Request.Cookies("sciexplore_user").Value & "' and guid = '" & guid & "';", Conn2)
                        Dim vote_repeat As Integer = cmd2.ExecuteScalar()
                        cmd2.Dispose()
                        If vote_repeat >= 1 Then
                            Me.vote.Disabled = False
                            Me.vote.Visible = False
                            Me.Literal_btn.Visible = True
                            Me.Literal_btn.Text = "<div id=""vote"" style=""text-align:center;""><a id=""vote_btn"" runat=""server"" onserverclick=""voting"">已投票</a></div>"
                        End If
                    End If

                Catch ex As Exception
                    Response.Write("Error Message--" & ex.ToString())
                Finally
                    If (Conn2.State = ConnectionState.Open) Then
                        Conn2.Close()
                        Conn2.Dispose()
                    End If
                    Response.Write(Conn2.State.ToString)
                End Try
            Else

            End If
        End If

        team_info()
    End Sub
End Class
