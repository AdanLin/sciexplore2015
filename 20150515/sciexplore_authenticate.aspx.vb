Imports System
Imports System.Web.Configuration
Imports System.Data
Imports MySql.Data.MySqlClient
Partial Class authenticate
    Inherits System.Web.UI.Page
    Sub auth_init()
        Dim guid As String = Request.QueryString("g")
        If guid Is Nothing Or guid = "" Then
            Response.Redirect("sciexplore_Election.aspx")
        Else
            Dim cs As String = ConfigurationManager.ConnectionStrings("DB").ConnectionString.ToString()
            Dim Conn As New MySqlConnection(cs)
            Dim conn2 As New MySqlConnection(cs)
            Dim dr As MySqlDataReader = Nothing
            Conn.Open()
            '--檢查是否認證過
            Dim search_cmd As MySqlCommand = New MySqlCommand("select * from vote_user where guid = @guid;", Conn)
            search_cmd.Parameters.AddWithValue("guid", guid.ToString)
            dr = search_cmd.ExecuteReader()
            If dr.HasRows() Then
                dr.Read()
                If dr.Item("auth") = "1" Then
                    '--關閉conn
                    If Not (dr Is Nothing) Then
                        dr.Dispose()
                    End If
                    If (Conn.State = ConnectionState.Open) Then
                        Conn.Close()
                        Conn.Dispose()
                    End If
                    If (conn2.State = ConnectionState.Open) Then
                        conn2.Close()
                        conn2.Dispose()
                    End If
                    Me.response_label.Text = "<a href=""sciexplore_login.aspx""><font color = ""#3366cc"">謝謝您，您已經認證過了，請點選連結登入</font></a><br /><p>或五秒後將自動轉至登入頁面</p>"
                    Page.Response.Write(" <script> setTimeout('location.href=""sciexplore_login.aspx""',5000); </script> ")
                Else
                    '--write to db
                    conn2.Open()
                    Dim write_cmd As MySqlCommand = New MySqlCommand("UPDATE vote_user SET auth = '1' where guid = '" & guid.ToString & "';", conn2)
                    'write_cmd.ExecuteNonQuery()
                    
                    Dim RecordsAffected As Integer = write_cmd.ExecuteNonQuery()
                    If RecordsAffected = 0 Then
                        Me.response_label.Text = "<font color = ""#8f0a11"">認證失敗!!</font>"
                    Else
                        '--關閉conn
                        If Not (dr Is Nothing) Then
                            dr.Dispose()
                        End If
                        If (Conn.State = ConnectionState.Open) Then
                            Conn.Close()
                            Conn.Dispose()
                        End If
                        If (conn2.State = ConnectionState.Open) Then
                            conn2.Close()
                            conn2.Dispose()
                        End If
                        Me.response_label.Text = "<a href=""sciexplore_login.aspx""><font color = ""#3366cc"">謝謝您，認證成功!請點選連結登入!</font></a><br /><p>或五秒後將自動轉至登入頁面</p>"
                        Page.Response.Write(" <script> setTimeout('location.href=""sciexplore_login.aspx""',5000); </script> ")
                    End If
                End If
            Else
                Me.response_label.Text = "<font color = ""#8f0a11"">認證失敗!!查無此帳號</font>"
            End If
            search_cmd.Dispose()
            '--關閉conn
            If Not (dr Is Nothing) Then
                dr.Dispose()
            End If
            If (Conn.State = ConnectionState.Open) Then
                Conn.Close()
                Conn.Dispose()
            End If
            If (conn2.State = ConnectionState.Open) Then
                conn2.Close()
                conn2.Dispose()
            End If
        End If
    End Sub
    Protected Sub Page_Init(sender As Object, e As EventArgs) Handles Me.Init
        auth_init()
    End Sub
End Class
