Imports System
Imports System.Web.Configuration
Imports System.Data
Imports MySql.Data.MySqlClient
Imports System.Net.Mail   '-- MailMessage會用到。
Imports System.Reflection
Imports System.Collections.Specialized
Partial Class sciexplore_registration_last
    Inherits System.Web.UI.Page

    Sub send_mail()
        Dim guid As String = Request.QueryString("g")
        Dim cs2 As String = ConfigurationManager.ConnectionStrings("sciexplore").ConnectionString.ToString()
        Dim Conn As New MySqlConnection(cs2)
        Dim dr As MySqlDataReader = Nothing
        Conn.Open()
        Dim search_cmd As MySqlCommand = New MySqlCommand("select * from vote_user where guid = @guid;", Conn)
        search_cmd.Parameters.AddWithValue("guid", guid)
        dr = search_cmd.ExecuteReader()
        search_cmd.Cancel()
        If dr.HasRows() Then
            dr.Read()
            Dim user As String = dr.Item("user_name").ToString
            Dim pw As String = dr.Item("pw").ToString
            Dim email As String = dr.Item("email").ToString
            Dim mail_content As String = "<p>歡迎註冊2015全國科學探究競賽-這樣教我就懂網路票選系統</p><br /><p>" & user & "您好~</p><br />" & "<p>您的帳號為: " & email & "</p><br /><p>您的密碼為: " & pw & "</p><br />" _
                    & "<p>請點選連結已完成註冊認證:<a href=""http://sciexplore.colife.org.tw/vote/sciexplore_authenticate.aspx?g=" & guid & """>認證連結</a></p><br /><p>謝謝您的註冊，如有相關問題請與我們聯絡:<a href=""mailto:sciexplore2015@gmail.com"">sciexplore2015@gmail.com</a>。</p>"
            Dim mail As New MailMessage
            mail.From = New MailAddress("sciexplore2015@gmail.com", "sciexplore")
            mail.To.Add(email)
            mail.Priority = MailPriority.Normal
            mail.Subject = "2015全國科學探究競賽帳號認證信"
            mail.Body = mail_content
            mail.IsBodyHtml = True
            Dim MySmtp As New System.Net.Mail.SmtpClient("140.110.17.174", 25)
            MySmtp.Credentials = New System.Net.NetworkCredential(ConfigurationManager.AppSettings("smtpUser"), ConfigurationManager.AppSettings("smtpPass"))
            MySmtp.EnableSsl = False
            MySmtp.Send(mail)
            MySmtp.Dispose()
            mail.Dispose()
            Me.result_text.Text = "<h1>註冊成功</h1><h4 style=""color: #D12752;"">註冊成功!請至信箱收取認證信!<br />(可能會被歸類在垃圾信件)</h4>"
        Else
            Me.result_text.Text = "<h1>註冊失敗</h1><h4 style=""color: #D12752;"">請重新註冊或通知本單位，謝謝。<a href=""mailto:sciexplore2015@gmail.com"">sciexplore2015@gmail.com</a></h4>"
        End If

        '--finally
        If Not (dr Is Nothing) Then
            dr.Close()

        End If
        
        If (Conn.State = ConnectionState.Open) Then
            Conn.Close()
            Conn.Dispose()
        End If
        Response.Write(Conn.State.ToString)
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

    Protected Sub Page_Init(sender As Object, e As EventArgs) Handles Me.Init
        If Not Request.Cookies("user_check") Is Nothing Then
            If Request.Cookies("user_check").Value = "OK" Then
                'Response.Write("helo! welceome~您的會員編號是:" & Request.Cookies("sciexplore_user").Value)
                Me.login_td.Style.Item("display") = "none"
            Else

            End If
        End If
        send_mail()
    End Sub
End Class
