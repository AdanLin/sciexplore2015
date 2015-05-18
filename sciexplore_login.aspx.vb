Imports System
Imports System.Web.Configuration
Imports System.Data
Imports MySql.Data.MySqlClient
Partial Class sciexplore_login
    Inherits System.Web.UI.Page
    Protected Sub Login_Click(sender As Object, e As EventArgs)
        If Me.user_email.Value = "" Or Me.user_pw.Value = "" Then
            Me.warn_message.Text = "<font color = ""#8f0a11"">請填寫Email與密碼!</font>"
        Else
            '--檢查帳號密碼
            Dim cs As String = ConfigurationManager.ConnectionStrings("sciexplore").ConnectionString.ToString()
            Dim Conn As New MySqlConnection(cs)
            Dim dr As MySqlDataReader = Nothing
            Conn.Open()
            Dim search_cmd As MySqlCommand = New MySqlCommand("select * from vote_user where email = '" & Me.user_email.Value & "' and pw = '" & Me.user_pw.Value & "';", Conn)
            'search_cmd.Parameters.AddWithValue("email", Me.user_email.Value)
            'search_cmd.Parameters.AddWithValue("password", Me.user_pw.Value)
            dr = search_cmd.ExecuteReader()
            If dr.HasRows() Then
                dr.Read()
                If dr("auth").ToString = 0 Then
                    Me.warn_message.Text = "<font color = ""#8f0a11"">您所輸入的帳號尚未完成認證，<br />請至信箱收取並完成認證</font>"
                Else
                    Dim user_id As String = dr("uid").ToString
                    If Not (dr Is Nothing) Then
                        dr.Dispose()
                        search_cmd.Dispose()
                    End If
                    If (Conn.State = ConnectionState.Open) Then
                        Conn.Close()
                        Conn.Dispose()
                    End If
                    Response.Cookies("sciexplore_user").Value = user_id
                    Response.Cookies("user_check").Value = "OK"
                    Response.Cookies("user_check").Expires = DateTime.Now.AddDays(2)
                    Response.Redirect("sciexplore_Election.aspx")
                    Response.Write(Conn.State.ToString)
                End If
            Else
                Me.warn_message.Text = "<font color = ""#8f0a11"">Email或密碼有誤!</font>"
            End If
            If Not (dr Is Nothing) Then
                dr.Dispose()
                search_cmd.Dispose()
            End If
            If (Conn.State = ConnectionState.Open) Then
                Conn.Close()
                Conn.Dispose()
            End If
        End If
    End Sub
End Class
