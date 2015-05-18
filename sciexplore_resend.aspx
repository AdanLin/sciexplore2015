<%@ Page Language="VB" AutoEventWireup="false" CodeFile="sciexplore_resend.aspx.vb" Inherits="sciexplore_resend" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>2015全國科學探究競賽-這樣教我就懂：網路票選-查詢與補發</title>
    <link href="css/reset_css.css" rel="stylesheet" type="text/css" />
    <link href="css/main.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="http://code.jquery.com/jquery-latest.min.js"></script>
    <script src="js/portamento.js"></script>
    <script type="text/javascript">
        /*$(document).ready(function () {
            $('#sidebar').portamento({ wrapper: $('#content') });
        });*/
        $(window).load(function () {
            var $win = $(window),
                $ad = $('#sidebar').css('opacity', 0).show(),	// 讓廣告區塊變透明且顯示出來
                _width = $ad.width(),
                _height = $ad.height(),
                _diffY = 20, _diffX = 20,	// 距離右及下方邊距
                _moveSpeed = 800;	// 移動的速度

            // 先把 #abgne_float_ad 移動到定點
            $ad.css({
                top: $(document).height(),
                left: $win.width() - _width - _diffX,
                opacity: 1
            });

            // 幫網頁加上 scroll 及 resize 事件
            $win.bind('scroll resize', function () {
                var $this = $(this);

                // 控制 #abgne_float_ad 的移動
                $ad.stop().animate({
                    top: $this.scrollTop() + $this.height() - _height - _diffY,
                    left: $this.scrollLeft() + $this.width() - _width - _diffX
                }, _moveSpeed);
            }).scroll();	// 觸發一次 scroll()
        });
    </script>
    <style>
        #sidebar {
            width: 150px;
            padding: 0px;
            height: 400px;
            position: absolute;
            display: none;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div id="content">
            <div id="log" style="margin-left: 904px">
                <table border="1" style="width: 100%; height: 36px; text-align: center; border-spacing: 0px; border-collapse: collapse;">
                    <tr>
                        <td id="login_td" style="" runat="server">
                            <a href="sciexplore_login.aspx" target="new" style="margin-left: 0px; padding-left: 5px;">登入</a>
                        </td>
                        <td><a href="#" style="margin-left: 0px; padding-left: 5px" runat="server" onserverclick="Log_Out">登出</a></td>
                    </tr>
                </table>
            </div>
            <div id="head"></div>
            <div id="center">
                <h1>忘記密碼/帳號認證信補發</h1>
                <table style="border-spacing: 5px; border-collapse: collapse; width: 45%; margin-top: 20px; margin-left: auto; margin-right: auto; border: 0;">
                    <tr>
                        <td style="width: 28%;">
                            <h2>姓名</h2>
                        </td>
                        <td style="width: 72%;">
                            <label for="user_name"></label>
                            <input name="textfield" type="text" id="user_name" size="30" maxlength="10" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 28%;">
                            <h2>E-mail</h2>
                        </td>
                        <td style="width: 72%;">
                            <label for="user_email"></label>
                            <input name="textfield" type="text" id="user_email" size="30" maxlength="40" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" style="height: 20px; text-align: center; vertical-align: middle;">
                            <h2>
                                <input id="sent" name="" type="submit" value="確認" validationgroup="check_g1" runat="server" onserverclick="Resend_Click" />
                            </h2>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" style="text-align: center;">
                            <a href="sciexplore_registration.aspx" target="new">
                                <h4 style="text-decoration: underline; color: rgba(0,51,255,1)">進行註冊</h4>
                            </a>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" style="text-align: center;">
                            <a href="sciexplore_resend.aspx" target="new">
                                <h4 style="text-decoration: underline; color: rgba(0,51,255,1)">忘記密碼/補發認證信</h4>
                            </a>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" style="text-align: center;">
                            <asp:Literal ID="warn_message" runat="server"></asp:Literal>
                            <br />
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="user_email" ValidationGroup="check_g1" runat="server" ForeColor="#8f0a11" ErrorMessage="請輸入註冊時填寫的E-mail。"></asp:RequiredFieldValidator>
                            <br />
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="user_name" ValidationGroup="check_g1" runat="server" ForeColor="#8f0a11" ErrorMessage="請輸入註冊時填寫的姓名。"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                </table>
            </div>
        </div>
        <div id="sidebar">
            <a href="https://www.facebook.com/scitechvista" target="new">
                <img src="img/doctorQ.jpg" height="150" width="150" />
                <h4 style="background: rgba(255,255,255,0.5); font-weight: bold;">現在加入科技大觀園粉絲團，還可以參加粉絲團活動喔!</h4>
            </a>
        </div>
    </form>
</body>
</html>
