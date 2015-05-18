Imports System
Imports System.Web.Configuration
Imports System.Data
Imports MySql.Data.MySqlClient
Partial Class sciexplore_female
    Inherits System.Web.UI.Page
    Sub vote_list()
        '從資料庫讀取各組清單
        Dim cs As String = ConfigurationManager.ConnectionStrings("DB").ConnectionString.ToString()
        Dim Conn As New MySqlConnection(cs)
        Dim ds As MySqlDataReader = Nothing
        Dim cs2 As String = ConfigurationManager.ConnectionStrings("DB").ConnectionString.ToString()
        Dim Conn2 As New MySqlConnection(cs2)
        Dim v_count As Integer = 0
        Dim AL As New ArrayList()
        Dim BL As New ArrayList()
        Dim CL As New ArrayList()
        Dim DL As New ArrayList()
        Dim ListString1 As New StringBuilder

        Try
            Conn.Open()  '==連結DB1
            Conn2.Open() '==連結DB2
            For group_count = 1 To 4  '--共有四個組別
                Dim cmd As MySqlCommand = New MySqlCommand("select * from team where guid not in( select guid from teammate where teammate_gender = 'm' ) and team_leader_gender='f' and stage = '1' and activity_group = '" & group_count & "';", Conn)
                'select *  from team where guid not in( select guid from teammate where teammate_gender = 'm' ) and team_leader_gender='f' and stage = '1' order by activity_group;
                ds = cmd.ExecuteReader() '==取出資料
                While ds.Read
                    AL.Add(ds.Item("team_name"))
                    BL.Add(ds.Item("team_subject"))
                    CL.Add(ds.Item("guid"))
                    DL.Add(ds.Item("activity_group"))
                End While
                For i = 0 To AL.Count - 1
                    '--取得票數
                    Dim cmd2 As MySqlCommand = New MySqlCommand("SELECT COUNT(*) FROM female_vote_count WHERE guid='" & CL(i) & "';", Conn2)
                    v_count = cmd2.ExecuteScalar()
                    cmd2.Dispose()
                    ListString1.Append("<a href=""sciexplore_project.aspx?f=1&g=" & CL(i) & """ target=""_blank"">")
                    ListString1.Append("<div class=""box"" > <img src=""img/1.png"" width=""150"" height=""84"" /><img src=""img/more.jpg"" width=""50"" height=""84"" alt=""我要觀看"" style=""margin-left:15px""/>")
                    ListString1.Append("<h3>主題：" & BL(i) & "</h3><h4>隊名：" & AL(i) & "</h4>")
                    ListString1.Append("<h5>票數：" & v_count & "票</h5></div></a>")
                Next
                Dim Literaltext As Literal = FindControl("Literal_list" & group_count)
                If (Not Literaltext Is Nothing) Then
                    Literaltext.Text &= ListString1.ToString()
                End If
                ListString1.Clear()
                cmd.Dispose()
                ds.Close()
                AL.Clear()
                BL.Clear()
                CL.Clear()
                DL.Clear()
            Next
            Conn2.Close()
            Conn2.Dispose()

        Catch ex As Exception
            Response.Write("Error Message--" & ex.ToString())
        Finally
            If Not (ds Is Nothing) Then
                ds.Close()
            End If
            If (Conn2.State = ConnectionState.Open) Then
                Conn2.Close()
                Conn2.Dispose()
            End If
            If (Conn.State = ConnectionState.Open) Then
                Conn.Close()
                Conn.Dispose()
            End If
        End Try
    End Sub


    Protected Sub Page_Init(sender As Object, e As EventArgs) Handles Me.Init
        If Not Request.Cookies("user_check") Is Nothing Then
            If Request.Cookies("user_check").Value = "OK" Then
                'Response.Write("helo! welceome~您的會員編號是:" & Request.Cookies("sciexplore_user").Value)
                Me.login_td.Style.Item("display") = "none"
            Else

            End If
        End If

        If Not Page.IsPostBack Then

        End If
        vote_list()
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
End Class
