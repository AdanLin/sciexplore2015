Imports System
Imports System.Web.Configuration
Imports System.Data
Imports MySql.Data.MySqlClient
Imports System.Net.Mail   '-- MailMessage會用到。
Imports System.Reflection
Imports System.Collections.Specialized
Partial Class registration
    Inherits System.Web.UI.Page

    Protected Sub Register_Click(sender As Object, e As EventArgs)
        If Me.textfield1.Value = "" Or Me.textfield2.Value = "" Or Me.textfield3.Value = "" Or Me.textfield4.Value = "" Then
            Me.ErrorMessage.Text = "<font color = ""#8f0a11"">請填寫所有必填欄位。</font>"
        Else

            '--查詢email是否註冊過
            Dim cs2 As String = ConfigurationManager.ConnectionStrings("sciexplore").ConnectionString.ToString()
            Dim Conn As New MySqlConnection(cs2)
            Dim Conn2 As New MySqlConnection(cs2)
            Dim dr As MySqlDataReader = Nothing
            Conn.Open()
            Dim search_cmd As MySqlCommand = New MySqlCommand("select * from vote_user where email = @email;", Conn)
            search_cmd.Parameters.AddWithValue("email", Me.textfield4.Value)
            dr = search_cmd.ExecuteReader()
            search_cmd.Cancel()
            If dr.HasRows() Then
                Me.ErrorMessage.Text = "<font color = ""#8f0a11"">此E-mail已註冊過，請登入或以其他E-mail註冊!</font>"
            Else
                '--寫入DB

                Conn2.Open()
                Dim write_cmd As MySqlCommand = New MySqlCommand("INSERT INTO vote_user(user_name, pw, email, occupation, location, guid) values (@user_name, @pw, @email, @occupation, @location, @guid);", Conn2)
                write_cmd.Parameters.AddWithValue("user_name", Me.textfield1.Value)
                write_cmd.Parameters.AddWithValue("pw", Me.textfield2.Value)
                write_cmd.Parameters.AddWithValue("email", Me.textfield4.Value)
                write_cmd.Parameters.AddWithValue("occupation", Me.DropDownList1.SelectedValue.ToString)
                write_cmd.Parameters.AddWithValue("location", Me.DropDownList2.SelectedValue.ToString)
                Dim g As Guid = Guid.NewGuid()
                write_cmd.Parameters.AddWithValue("guid", g.ToString)

                '--後續動作
                Dim RecordsAffected As Integer = write_cmd.ExecuteNonQuery()
                If RecordsAffected = 0 Then
                    Me.ErrorMessage.Text = "<font color = ""#870D2D"">資料新增失敗!!</font>"
                Else
                    If (Conn2.State = ConnectionState.Open) Then
                        Conn2.Close()
                        Conn2.Dispose()
                    End If
                    If (Conn.State = ConnectionState.Open) Then
                        Conn.Close()
                        Conn.Dispose()
                    End If
                    'Page.Response.Redirect("sciexplore_registration_last.aspx?g=" & g.ToString)
                    'Me.ErrorMessage.Text = "<a href=""sciexplore_login""><font color = ""#D12752"">註冊成功!請至信箱收取認證信!</font></a><br /><font color = ""#D12752"">(可能會被歸類在垃圾信件)</font>"

                    ''發送Email
                    'Dim mail_content As String = "<p>歡迎註冊2015全國科學探究競賽-這樣教我就懂網路票選系統</p><br /><p>" & Me.textfield1.Value & "您好~</p><br />" & "<p>您的帳號為: " & Me.textfield1.Value & "</p><br /><p>您的密碼為: " & Me.textfield2.Value & "</p><br />" _
                    '& "<p>請點選連結已完成註冊認證:<a href=""http://140.110.96.34/sciexplore/sciexplore_authenticate.aspx?g=" & g.ToString & """>認證連結</a></p><br /><p>謝謝您的註冊，如有相關問題請與我們聯絡:<a href=""mailto:sciexplore2015@gmail.com"">sciexplore2015@gmail.com</a>。</p>"
                    'Dim mail As New MailMessage
                    'mail.From = New MailAddress("dreaman10040@gmail.com", "sciexplore")
                    'mail.To.Add(Me.textfield4.Value.ToString)
                    'mail.Priority = MailPriority.Normal
                    'mail.Subject = "2015全國科學探究競賽帳號認證信"
                    'mail.Body = mail_content
                    'mail.IsBodyHtml = True
                    'Dim MySmtp As New System.Net.Mail.SmtpClient("smtp.gmail.com", 587)
                    'MySmtp.Credentials = New System.Net.NetworkCredential(ConfigurationManager.AppSettings("smtpUser"), ConfigurationManager.AppSettings("smtpPass"))
                    'MySmtp.EnableSsl = True
                    'MySmtp.Send(mail)
                    'MySmtp.Dispose()
                    'mail.Dispose()
                    Response.Write(Conn.State.ToString)
                    Response.Write(Conn2.State.ToString)
                End If
                'write_cmd.Cancel()
                
            End If

            '--關閉conn

            If (Conn2.State = ConnectionState.Open) Then
                Conn2.Close()
                Conn2.Dispose()
            End If
            If Not (dr Is Nothing) Then
                dr.Close()
            End If
            If (Conn.State = ConnectionState.Open) Then
                Conn.Close()
                Conn.Dispose()
            End If

            
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
    End Sub
End Class
