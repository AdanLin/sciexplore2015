<%@ Page Language="VB" AutoEventWireup="false" CodeFile="sciexplore_registration.aspx.vb" Inherits="registration" %>

<!DOCTYPE html>
<html lang="en" xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <title>2015全國科學探究競賽-這樣教我就懂：網路票選</title>
    <link href="css/reset_css.css" rel="stylesheet" type="text/css" />
    <link href="css/main.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="http://code.jquery.com/jquery-latest.min.js"></script>
    <script src="js/portamento.js"></script>
    <script type="text/javascript">
        function MM_swapImgRestore() { //v3.0
            var i, x, a = document.MM_sr; for (i = 0; a && i < a.length && (x = a[i]) && x.oSrc; i++) x.src = x.oSrc;
        }
        function MM_preloadImages() { //v3.0
            var d = document; if (d.images) {
                if (!d.MM_p) d.MM_p = new Array();
                var i, j = d.MM_p.length, a = MM_preloadImages.arguments; for (i = 0; i < a.length; i++)
                    if (a[i].indexOf("#") != 0) { d.MM_p[j] = new Image; d.MM_p[j++].src = a[i]; }
            }
        }

        function MM_findObj(n, d) { //v4.01
            var p, i, x; if (!d) d = document; if ((p = n.indexOf("?")) > 0 && parent.frames.length) {
                d = parent.frames[n.substring(p + 1)].document; n = n.substring(0, p);
            }
            if (!(x = d[n]) && d.all) x = d.all[n]; for (i = 0; !x && i < d.forms.length; i++) x = d.forms[i][n];
            for (i = 0; !x && d.layers && i < d.layers.length; i++) x = MM_findObj(n, d.layers[i].document);
            if (!x && d.getElementById) x = d.getElementById(n); return x;
        }

        function MM_swapImage() { //v3.0
            var i, j = 0, x, a = MM_swapImage.arguments; document.MM_sr = new Array; for (i = 0; i < (a.length - 2) ; i += 3)
                if ((x = MM_findObj(a[i])) != null) { document.MM_sr[j++] = x; if (!x.oSrc) x.oSrc = x.src; x.src = a[i + 2]; }
        }
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
            width:150px; 
            padding:0px; 
            height:400px; 
            position:absolute; 
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
            <nav>
                <a href="sciexplore_Election.aspx">投票說明</a>
                <a href="sciexplore_pop.aspx">最佳人氣獎</a>
                <a href="sciexplore_female.aspx">女性桂冠獎</a>
                <a href="sciexplore_registration.aspx">進行註冊</a>
            </nav>
            <div id="center">
                <h1>投票註冊</h1>
                <table style="border-spacing: 5px; border-collapse: collapse; width: 65%; border: 0; margin-top: 20px; margin-left: auto; margin-right: auto; border: none;">
                    <tr>
                        <td style="width: 25%;">
                            <h2>
                                <label for="textfield1" runat="server">姓名</label></h2>
                        </td>
                        <td style="width: 75%;">
                            <input name="textfield1" type="text" id="textfield1" size="30" maxlength="5" runat="server" />
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ValidationGroup="regist_group" ControlToValidate="textfield1" ErrorMessage="請填寫姓名欄位。"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 25%;">
                            <h2>
                                <label for="textfield2" runat="server">密碼</label></h2>
                        </td>
                        <td style="width: 75%;">
                            <input name="textfield2" type="password" id="textfield2" size="30" maxlength="10" runat="server" />
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ValidationGroup="regist_group" ControlToValidate="textfield2" ErrorMessage="請填寫密碼欄位。"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 25%;">
                            <h2>
                                <label for="textfield3" runat="server">密碼確認</label>
                            </h2>
                        </td>
                        <td style="width: 75%;">
                            <input name="textfield3" type="password" id="textfield3" size="30" maxlength="10" runat="server" />
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ValidationGroup="regist_group" ControlToValidate="textfield3" Display="Dynamic" ErrorMessage="請填寫密碼確認欄位。"></asp:RequiredFieldValidator>
                            <asp:CompareValidator runat="server" ControlToCompare="textfield2" ControlToValidate="textfield3" CssClass="text-danger" Display="Dynamic" ErrorMessage="密碼和密碼確認不相符。" />
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 25%;">
                            <h2>
                                <label for="textfield4" runat="server">E-mail</label>
                            </h2>
                        </td>
                        <td style="width: 75%;">
                            <input name="textfield4" type="text" id="textfield4" size="30" maxlength="40" runat="server" />
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ControlToValidate="textfield4" runat="server" ValidationGroup="regist_group" ErrorMessage="請填寫E-mail欄位。"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 25%;">
                            <h2>
                                <label for="DropDownList1" runat="server">職業</label>
                            </h2>
                        </td>
                        <td style="width: 75%;">
                            <asp:DropDownList ID="DropDownList1" runat="server">
                                <asp:ListItem Selected="True" Value="-1">請選擇</asp:ListItem>
                                <asp:ListItem Value="工">工</asp:ListItem>
                                <asp:ListItem Value="商">商</asp:ListItem>
                                <asp:ListItem Value="家管">家管</asp:ListItem>
                                <asp:ListItem Value="公教">公教</asp:ListItem>
                                <asp:ListItem Value="軍警">軍警</asp:ListItem>
                                <asp:ListItem Value="醫藥">醫藥</asp:ListItem>
                                <asp:ListItem Value="自由業">自由業</asp:ListItem>
                                <asp:ListItem Value="服務業">服務業</asp:ListItem>
                                <asp:ListItem Value="學生">學生</asp:ListItem>
                                <asp:ListItem Value="農林漁牧業">農林漁牧業</asp:ListItem>
                                <asp:ListItem Value="已退休">已退休</asp:ListItem>
                                <asp:ListItem Value="待業中">待業中</asp:ListItem>
                                <asp:ListItem Value="其他">其他</asp:ListItem>
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ValidationGroup="regist_group" ControlToValidate="DropDownList1" ErrorMessage="請選擇職業。" InitialValue="-1"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 25%;">
                            <h2>
                                <label for="DropDownList2" runat="server">所在地</label>
                            </h2>
                        </td>
                        <td style="width: 75%;">
                            <asp:DropDownList ID="DropDownList2" runat="server">
                                <asp:ListItem Selected="True" Value="-1">請選擇</asp:ListItem>
                                <asp:ListItem Value="基隆市">基隆市 (Keelung City)</asp:ListItem>
                                <asp:ListItem Value="台北市">台北市 (Taipei)</asp:ListItem>
                                <asp:ListItem Value="新北市">新北市 (New Taipei City)</asp:ListItem>
                                <asp:ListItem Value="桃園縣">桃園縣 (Taoyuan County)</asp:ListItem>
                                <asp:ListItem Value="新竹市">新竹市 (Hsinchu City)</asp:ListItem>
                                <asp:ListItem Value="新竹縣">新竹縣 (Hsinchu County)</asp:ListItem>
                                <asp:ListItem Value="苗栗縣">苗栗縣 (Miaoli County)</asp:ListItem>
                                <asp:ListItem Value="台中市">台中市 (Taichung City)</asp:ListItem>
                                <asp:ListItem Value="彰化縣">彰化縣 (Changhua County)</asp:ListItem>
                                <asp:ListItem Value="南投縣">南投縣 (Nantou County)</asp:ListItem>
                                <asp:ListItem Value="雲林縣">雲林縣 (Yunlin County)</asp:ListItem>
                                <asp:ListItem Value="嘉義市">嘉義市 (Chiayi City)</asp:ListItem>
                                <asp:ListItem Value="嘉義縣">嘉義縣 (Chiayi County)</asp:ListItem>
                                <asp:ListItem Value="台南市">台南市 (Tainan City)</asp:ListItem>
                                <asp:ListItem Value="高雄市">高雄市 (Kaohsiung City)</asp:ListItem>
                                <asp:ListItem Value="屏東縣">屏東縣 (Pingtung County)</asp:ListItem>
                                <asp:ListItem Value="台東縣">台東縣 (Taitung County)</asp:ListItem>
                                <asp:ListItem Value="花蓮縣">花蓮縣 (Hualien County)</asp:ListItem>
                                <asp:ListItem Value="宜蘭縣">宜蘭縣 (Yilan County)</asp:ListItem>
                                <asp:ListItem Value="澎湖縣">澎湖縣 (Penghu County)</asp:ListItem>
                                <asp:ListItem Value="金門縣">金門縣 (Kinmen County)</asp:ListItem>
                                <asp:ListItem Value="連江縣">連江縣 (Lienchiang County)</asp:ListItem>
                                <asp:ListItem Value="其他">其他 (Other)</asp:ListItem>
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ValidationGroup="regist_group" ControlToValidate="DropDownList2" ErrorMessage="請選擇所在地。" InitialValue="-1"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <h4 style="padding-left: 20px; line-height: 30px;">注意：上述六項資訊為必填，請您務必填妥相關資訊。</h4>
                            <h4 style="line-height: 30px;">．若因資料填寫不齊、造假、誤填等情況，導致導致獎品無法寄送，視同放棄中獎權益，恕不另行補發。</h4>
                            <h4 style="line-height: 30px;">．E-mail填寫請盡量使用Gmail，以免被分類到垃圾信件中。</h4>
                            <h4 style="line-height: 30px;">．本活動所填寫之個人資料，僅作本次抽獎、科技部、財團法人國家實驗研究院國家高速網路與計算中心、高雄市政府教育局宣傳科學活動之用，不作其他用途。</h4>
                            <h4 style="line-height: 30px;">．本活動得獎者不得要求折現或轉換其他獎品。</h4>
                            <h4 style="line-height: 30px;">．如對領獎辦法有任何疑問，歡迎利用電子信箱與我們連絡:<a href="mailto:sciexplore2015@gmail.com">sciexplore2015@gmail.com</a></h4>
                        </td>
                    </tr>
                    <tr>
                        <td style="height: 65px; text-align: center; vertical-align: middle;" colspan="2">
                            <!--<input id="sent1" name="" type="submit" value="送出" runat="server" onserverclick="Register_Click" />-->
                            <asp:Button ID="sent" runat="server" Text="送出" OnClick="Register_Click" UseSubmitBehavior="True" ValidationGroup="regist_group" CausesValidation="true" />
                            <br />
                            <asp:Literal runat="server" ID="ErrorMessage" />
                        </td>
                    </tr>
                </table>

            </div>
            <div id="footer">
                <div id="fb-root"></div>
                <script>
                    (function (d, s, id) {
                        var js, fjs = d.getElementsByTagName(s)[0];
                        if (d.getElementById(id)) return;
                        js = d.createElement(s); js.id = id;
                        js.src = "//connect.facebook.net/zh_TW/sdk.js#xfbml=1&version=v2.3";
                        fjs.parentNode.insertBefore(js, fjs);
                    }(document, 'script', 'facebook-jssdk'));
                </script>
                <div class="fb-like" data-href="https://www.facebook.com/scitechvista" data-layout="button" data-action="like" data-show-faces="false" data-share="true"></div>
                <br />
                <a href="http://scitechvista.most.gov.tw/zh-tw/Home.htm" target="new" onmouseover="MM_swapImage('科技大觀園','','img/logo1.jpg',1)" onmouseout="MM_swapImgRestore()">
                    <img src="img/logo1.jpg" alt="科技大觀園" width="106" height="30" id="科技大觀園" style="padding-right: 10px; margin-top: 30px" />
                </a>
                <a href="http://www.nchc.org.tw/tw/" target="new" onmouseover="MM_swapImage('財團法人國家實驗研究院國家高速網路與計算中心','','img/logo2.jpg',1)" onmouseout="MM_swapImgRestore()">
                    <img style="padding-right: 10px" src="img/logo2.jpg" alt="財團法人國家實驗研究院國家高速網路與計算中心" width="195" height="30" id="財團法人國家實驗研究院國家高速網路與計算中心" />
                </a>
                <a href="http://www.kh.edu.tw/" target="new" onmouseover="MM_swapImage('高雄市政府教育局','','img/kh.gif',1)" onmouseout="MM_swapImgRestore()">
                    <img style="padding-right: 10px" src="img/kh.gif" alt="高雄市政府教育局" width="109" height="30" id="高雄市政府教育局" />
                </a>
                <a href="http://sa.ylib.com/" target="new" onmouseover="MM_swapImage('科學人雜誌','','img/logo4.jpg',1)" onmouseout="MM_swapImgRestore()">
                    <img style="padding-right: 10px" src="img/logo4.jpg" alt="科學人雜誌" width="82" height="30" id="科學人雜誌" />
                </a>
                <a href="http://ys.ylib.com/" target="new" onmouseover="MM_swapImage('科學少年','','img/ysbanner.jpg',1)" onmouseout="MM_swapImgRestore()">
                    <img style="padding-right: 10px" src="img/ysbanner.jpg" alt="科學少年" width="71" height="30" id="科學少年" />
                </a>
                <a href="http://ejournal.stpi.narl.org.tw/NSC_INDEX/Journal/EJ0001/index.html" target="new" onmouseover="MM_swapImage('科學發展月刊','','img/nscmnew.png',1)" onmouseout="MM_swapImgRestore()">
                    <img style="padding-right: 10px" src="img/nscmnew.png" alt="科學發展月刊" width="72" height="30" id="科學發展月刊" />
                </a>
                <a href="http://www.mdnkids.com/" target="new" onmouseover="MM_swapImage('財團法人國語日報社','','img/mankids.jpg',1)" onmouseout="MM_swapImgRestore()">
                    <img src="img/mankids.jpg" alt="財團法人國語日報社" width="135" height="30" id="財團法人國語日報社" />
                </a>
                <br />
                <br />
                主辦單位：財團法人國家實驗研究院 國家高速網路與計算中心、高雄市政府教育局<br />
                協辦單位：各縣市教育局(處)、南臺科技大學資訊傳播系<br />
                媒體贊助：科學發展月刊、科學人雜誌、科學少年雜誌、財團法人國語日報社<br />
                承辦單位：高雄市私立三信高級家事商業職業學校<br />
                獎狀頒發單位：高雄市政府教育局
            </div>
        </div>
        <div id="sidebar">
			<a href="https://www.facebook.com/scitechvista" target="new">
                <img src="img/doctorQ.jpg" height="150" width="150" />
                <h4 style="background: rgba(255,255,255,0.5); font-weight:bold;">現在加入科技大觀園粉絲團，還可以參加粉絲團活動喔!</h4>
			</a>
		</div>
    </form>
</body>
</html>
