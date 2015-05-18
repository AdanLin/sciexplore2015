Imports System
Imports System.Web.Configuration
Imports System.Data
Imports MySql.Data.MySqlClient
Imports System.Net.Mail   '-- MailMessage會用到。
Imports System.Reflection
Imports System.Collections.Specialized
Partial Class sciexplore_resend
    Inherits System.Web.UI.Page

    Protected Sub Resend_Click(sender As Object, e As EventArgs)
        If Me.user_name.Value = "" Or Me.user_email.Value = "" Then
            Me.warn_message.Text = "<font color = ""#8f0a11"">請填寫所有必填欄位。</font>"
        Else
            '--查詢註冊資訊
            Dim cs2 As String = ConfigurationManager.ConnectionStrings("sciexplore").ConnectionString.ToString()
            Dim Conn As New MySqlConnection(cs2)
            Dim dr As MySqlDataReader = Nothing
            Conn.Open()
            Dim search_cmd As MySqlCommand = New MySqlCommand("select * from vote_user where email = @email and user_name = @user_name;", Conn)
            search_cmd.Parameters.AddWithValue("email", Me.user_email.Value)
            search_cmd.Parameters.AddWithValue("user_name", Me.user_name.Value)
            dr = search_cmd.ExecuteReader()

            '--判斷是否已有USER
            If dr.HasRows() Then
                dr.Read()
                '--判斷是否已認證
                If dr.Item("auth").ToString = "0" Then
                    '發送Email
                    Dim mail_content As String = "<p>歡迎註冊2015全國科學探究競賽-這樣教我就懂網路票選系統</p><br /><p>" & dr.Item("user_name").ToString & "您好~</p><br />" & "<p>您的帳號為: " & dr.Item("email").ToString & "</p><br /><p>您的密碼為: " & dr.Item("pw").ToString & "</p><br />" _
                    & "<p>請點選連結已完成註冊認證:<a href=""http://sciexplore.colife.org.tw/vote/sciexplore_authenticate.aspx?g=" & dr.Item("guid").ToString & """>認證連結</a></p><br /><p>謝謝您的註冊，如有相關問題請與我們聯絡:<a href=""mailto:sciexplore2015@gmail.com"">sciexplore2015@gmail.com</a>。</p>"
                    Dim mail As New MailMessage
                    mail.From = New MailAddress("sciexplore2015@gmail.com", "sciexplore")
                    mail.To.Add(dr.Item("email").ToString)
                    mail.Priority = MailPriority.Normal
                    mail.Subject = "補發2015全國科學探究競賽帳號認證信"
                    mail.Body = mail_content
                    mail.IsBodyHtml = True
                    Dim MySmtp As New System.Net.Mail.SmtpClient("140.110.17.174", 25)
                    MySmtp.Credentials = New System.Net.NetworkCredential(ConfigurationManager.AppSettings("smtpUser"), ConfigurationManager.AppSettings("smtpPass"))
                    MySmtp.EnableSsl = False
                    MySmtp.Send(mail)
                    MySmtp.Dispose()
                    mail.Dispose()
                    '--show the complete message
                    Me.warn_message.Text = "<font color = ""#5873A3"">已將信件寄出，請至電子信箱查收並完成認證，謝謝。</font>"
                Else
                    '發送Email
                    Dim mail_content As String = "<p>歡迎註冊2015全國科學探究競賽-這樣教我就懂網路票選系統</p><br /><p>" & dr.Item("user_name").ToString & "您好~</p><br />" & "<p>您的帳號為: " & dr.Item("email").ToString & "</p><br /><p>您的密碼為: " & dr.Item("pw").ToString & "</p><br />" _
                    & "<p>謝謝您的註冊，如有相關問題請與我們聯絡:<a href=""mailto:sciexplore2015@gmail.com"">sciexplore2015@gmail.com</a>。</p>"
                    Dim mail As New MailMessage
                    mail.From = New MailAddress("sciexplore2015@gmail.com", "sciexplore")
                    mail.To.Add(dr.Item("email").ToString)
                    mail.Priority = MailPriority.Normal
                    mail.Subject = "補發2015全國科學探究競賽帳號密碼信"
                    mail.Body = mail_content
                    mail.IsBodyHtml = True
                    Dim MySmtp As New System.Net.Mail.SmtpClient("140.110.17.174", 25)
                    MySmtp.Credentials = New System.Net.NetworkCredential(ConfigurationManager.AppSettings("smtpUser"), ConfigurationManager.AppSettings("smtpPass"))
                    MySmtp.EnableSsl = False
                    MySmtp.Send(mail)
                    MySmtp.Dispose()
                    mail.Dispose()
                    '--show the complete message
                    Me.warn_message.Text = "<font color = ""#5873A3"">已將信件寄出，請至電子信箱查收，謝謝。</font>"
                End If
            Else
                Me.warn_message.Text = "<font color = ""#B22900"">查無此帳號，請確認相關資訊有無輸入錯誤，<br />或進行註冊，謝謝。</font>"
            End If
            search_cmd.Cancel()
            '--關閉conn
            If Not (dr Is Nothing) Then

                dr.Close()
            End If
            If (Conn.State = ConnectionState.Open) Then
                Conn.Close()
                Conn.Dispose()
            End If
            Response.Write(Conn.State.ToString)
        End If
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

        If Not Page.IsPostBack Then

        End If
    End Sub
End Class
