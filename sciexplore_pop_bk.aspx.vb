Imports System
Imports System.Web.Configuration
Imports System.Data
Imports MySql.Data.MySqlClient
Partial Class sciexplore_pop
    Inherits System.Web.UI.Page
    Sub vote_list()
        '從資料庫讀取國小組清單
        Dim Conn As New MySqlConnection("server=140.110.20.188;user=sci;password=sci@nchc;database=sci_scipower;pooling=false")
        Dim ds As MySqlDataReader = Nothing
        'Dim cmd As MySqlCommand = New MySqlCommand("select * from team where activity_group = '1';", Conn)
        Dim AL As New ArrayList()
        Dim BL As New ArrayList()
        Dim CL As New ArrayList()
        Dim DL As New ArrayList()
        Dim ListString1 As New StringBuilder
        Dim ListString2 As New StringBuilder
        Dim ListString3 As New StringBuilder
        Dim ListString4 As New StringBuilder
        'Me.Literal_list1.Text &= "<div class=""TabbedPanelsContentGroup"">"
        Dim cmd As MySqlCommand = New MySqlCommand("select * from team where activity_group = '1';", Conn)
        Try
            Conn.Open()  '==連結DB
            ds = cmd.ExecuteReader() '==取出資料
            While ds.Read

                AL.Add(ds.Item("team_name"))
                BL.Add(ds.Item("team_subject"))
                CL.Add(ds.Item("guid"))
                DL.Add(ds.Item("activity_group"))
            End While
            ListString1.Append("<div class=""TabbedPanelsContent"">")
            Dim Conn2 As New MySqlConnection("server=140.110.96.34;user=aedan;password=aedan@nchc;database=sciexplore;pooling=false")
            Dim ds2 As MySqlDataReader = Nothing
            Dim v_count As Integer = 0
            Conn2.Open() '==連結DB
            For i = 0 To AL.Count - 1
                '--取得票數
                Dim cmd2 As MySqlCommand = New MySqlCommand("SELECT COUNT(*) FROM vote_count WHERE guid='" & CL(i) & "';", Conn2)
                v_count = cmd2.ExecuteScalar()
                cmd2.Dispose()
                ListString1.Append("<a href=""sciexplore_project.html?g=" & CL(i) & """ target=""new"">")
                ListString1.Append("<div class=""box"" > <img src=""img/1.png"" width=""150"" height=""84"" /><img src=""img/more.jpg"" width=""50"" height=""84"" alt=""我要觀看"" style=""margin-left:15px""/>")
                ListString1.Append("<h3>隊名：" & AL(i) & "</h3><h4>主題：" & BL(i) & "</h4>")
                ListString1.Append("<h5>票數：" & v_count & "票</h5></div></a>")

            Next
            Conn2.Close()
            Conn2.Dispose()
            ListString1.Append("</div>")
            Me.Literal_list1.Text &= ListString1.ToString()

        Catch ex As Exception
            Response.Write("Error Message--" & ex.ToString())
        Finally
            If Not (ds Is Nothing) Then
                ds.Close()
            End If
            AL.Clear()
            BL.Clear()
            CL.Clear()
            cmd.Dispose()
            If (Conn.State = ConnectionState.Open) Then
                Conn.Close()
                Conn.Dispose()
            End If
        End Try

        'Me.Literal_list1.Text &= "</div>"
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
        'Me.Literal_list1.Text = "<div class=""TabbedPanelsContentGroup""><div class=""TabbedPanelsContent""><a href=""sciexplore_project_st1.html"" target=""new""><div class=""box"" > <img src=""img/1.png"" width=""150"" height=""84"" /><img src=""img/more.jpg"" width=""50"" height=""84"" alt=""我要觀看"" style=""margin-left:15px""/><h3>隊名：福康小兵</h3><h4>主題：表面張力</h4><h5>票數：10票</h5></div></a></div><div class=""TabbedPanelsContent""><a href=""sciexplore_project_st1.html"" target=""new""><div class=""box"" > <img src=""img/1.png"" width=""150"" height=""84"" /><img src=""img/more.jpg"" width=""50"" height=""84"" alt=""我要觀看"" style=""margin-left:15px""/><h3>隊名：福康小兵</h3><h4>主題：表面張力</h4><h5>票數：10票</h5></div></a></div><div class=""TabbedPanelsContent""><a href=""sciexplore_project_st1.html"" target=""new""><div class=""box"" > <img src=""img/1.png"" width=""150"" height=""84"" /><img src=""img/more.jpg"" width=""50"" height=""84"" alt=""我要觀看"" style=""margin-left:15px""/><h3>隊名：福康小兵</h3><h4>主題：表面張力</h4><h5>票數：10票</h5></div></a></div><div class=""TabbedPanelsContent""><a href=""sciexplore_project_st1.html"" target=""new""><div class=""box"" > <img src=""img/1.png"" width=""150"" height=""84"" /><img src=""img/more.jpg"" width=""50"" height=""84"" alt=""我要觀看"" style=""margin-left:15px""/><h3>隊名：福康小兵</h3><h4>主題：表面張力</h4><h5>票數：10票</h5></div></a></div><div class=""TabbedPanelsContent""><a href=""sciexplore_project_st1.html"" target=""new""><div class=""box"" > <img src=""img/1.png"" width=""150"" height=""84"" /><img src=""img/more.jpg"" width=""50"" height=""84"" alt=""我要觀看"" style=""margin-left:15px""/><h3>隊名：福康小兵</h3><h4>主題：表面張力</h4><h5>票數：10票</h5></div></a></div></div>"
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
