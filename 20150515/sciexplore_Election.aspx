<%@ Page Language="VB" AutoEventWireup="false" CodeFile="sciexplore_Election.aspx.vb" Inherits="sciexplore_Election" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>2015全國科學探究競賽-這樣教我就懂：網路票選</title>
    <link href="css/reset_css.css" rel="stylesheet" type="text/css" />
    <link href="css/main.css" rel="stylesheet" type="text/css" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <script type="text/javascript" src="http://code.jquery.com/jquery-latest.min.js"></script>
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
        .footer_btn {
            border: 1px solid rgba(243, 243, 243, 1); 
            color: rgba(102, 102, 102, 1); 
            font-size: 20px; 
            letter-spacing: 5px; 
            margin-right: 10px; 
            padding: 10px 20px; 
            text-align: justify; 
            vertical-align: middle; 
            width: 100px; 
            text-decoration: none;
            
        }
        .footer_btn:hover {
            background-color:rgba(255,51,102,1);
	        color:rgba(255,255,255,1);
        }
    </style>
</head>
<body onload="MM_preloadImages('img/logo1.jpg','img/logo2.jpg','img/kh.gif','img/logo4.jpg','img/ysbanner.jpg','img/nscmnew.png','img/mankids.jpg')">
    <form id="form1" runat="server">
        <div id="content">
            <div id="log" style="margin-left: 904px">
                <table border="1" style="width:100%; height:36px; text-align:center; border-spacing: 0px; border-collapse: collapse;">
                    <tr>
                        <td id="login_td" style="" runat="server">
                            <a href="sciexplore_login.aspx" target="new" style="margin-left: 0px; padding-left: 5px;">登入</a>
                        </td>
                        <td><a href="#" style="margin-left: 0px; padding-left: 5px" runat="server" onserverclick="Log_Out">登出</a></td>
                    </tr>
                </table>
            </div>
            <div id="head"></div>
            <nav><a href="sciexplore_Election.aspx">投票說明</a> <a href="sciexplore_pop.aspx">最佳人氣獎</a> <a href="sciexplore_female.aspx">女性桂冠獎</a> <a href="sciexplore_registration.aspx">進行註冊</a></nav>
            <div id="center">
                <h1>投票說明</h1>
                <h2>步驟1-[註冊]：請先完成註冊，才可進行投票。</h2>
                <h2>步驟2-[選擇投票獎項]：選擇上方選單最佳人氣獎與女性桂冠獎進入各組別中。</h2>
                <h2>步驟3-[選擇投票組別]：有國小組、國中組、高中職組、教師組，共四組。</h2>
                <h2>步驟4-[投票]：點選進入各個組別作品頁面，並完成投票。</h2>
                <h2 style="color: rgba(255,0,0,1)">注意：</h2>
                <h2>．感謝各位對本競賽的支持，主辦單位決定延長活動時間至5/22（五），可多鼓勵身旁的朋友一同來參與。</h2>
                <h2>．投票者進入各組別(國小組、國中組、高中職組、教師組)中，分別選出您心中的最佳人氣獎與女性桂冠獎代表，最多可投下五組最佳人氣獎以及五組女性桂冠獎。</h2>
                <h2>．凡參與最佳人氣獎與女性桂冠獎之民眾即可參加摸獎活動，共可抽出民眾參與獎-特獎5名、普獎15名。</h2>
                <h2>註1.網路投票活動參與獎以掛號方式寄送至得獎者居住地。</h2>
                <h2>註2.參與網路活動票選之民眾，所下載之競賽作品如需做為其他使用用途，需以創用CC之授權條款，標示註明作者姓名出處。</h2>
                <div style="text-align:center;">
                    <h2><a href="sciexplore_registration.aspx" style="" class="footer_btn">立即註冊</a></h2>
                </div>
            </div>
            <div class="center">
                <h1>競賽獎勵</h1>
                <table style="font-family:微軟正黑體; margin-top:10px; width:100%; text-align:center;" border="1">
                    <tr>
                        <th style="width:20%;text-align:center;">項目</th>
                        <th style="width:20%;text-align:center;">獎項名稱</th>
                        <th style="width:5%;text-align:center;">名額</th>
                        <th style="width:55%;text-align:center;">獎品</th>
                    </tr>
                    <tr>
                        <td rowspan="4">網路票選<br />最佳人氣獎</td>
                        <td>最佳人氣獎(國小)</td>
                        <td>1組</td>
                        <td>由《科學少年》雜誌提供該組每名組員《科學少年》雜誌3期</td>
                    </tr>
                    <tr>
                        <td>最佳人氣獎(國中)</td>
                        <td>1組</td>
                        <td>由《科學少年》雜誌提供該組每名組員《科學少年》雜誌3期</td>
                    </tr>
                    <tr>
                        <td>最佳人氣獎(高中職)</td>
                        <td>1組</td>
                        <td>由《科學人》雜誌提供該組每名組員《科學人》雜誌3期</td>
                    </tr>
                    <tr>
                        <td>最佳人氣獎(教師)</td>
                        <td>1組</td>
                        <td>由《科學人》雜誌提供該組每名組員《科學人》雜誌3期</td>
                    </tr>
                </table>
                <table style="font-family:微軟正黑體; margin-top:10px; width:100%; text-align:center;" border="1">
                    <tr>
                        <th style="width:20%;text-align:center;">項目</th>
                        <th style="width:20%;text-align:center;">獎項名稱</th>
                        <th style="width:5%;text-align:center;">名額</th>
                        <th style="width:55%;text-align:center;">獎品</th>
                    </tr>
                    <tr>
                        <td rowspan="4">網路票選<br />女性桂冠獎</td>
                        <td>女性桂冠獎(國小)</td>
                        <td>1組</td>
                        <td>由《科學少年》雜誌提供該組每名組員《科學少年》雜誌3期</td>
                    </tr>
                    <tr>
                        <td>女性桂冠獎(國中)</td>
                        <td>1組</td>
                        <td>由《科學少年》雜誌提供該組每名組員《科學少年》雜誌3期</td>
                    </tr>
                    <tr>
                        <td>女性桂冠獎(高中職)</td>
                        <td>1組</td>
                        <td>由《科學人》雜誌提供該組每名組員《科學人》雜誌3期</td>
                    </tr>
                    <tr>
                        <td>女性桂冠獎(教師)</td>
                        <td>1組</td>
                        <td>由《科學人》雜誌提供該組每名組員《科學人》雜誌3期</td>
                    </tr>
                </table>
                <table style="font-family:微軟正黑體; margin-top:10px; width:100%; text-align:center;" border="1">
                    <tr>
                        <th style="width:20%;text-align:center;">項目</th>
                        <th style="width:20%;text-align:center;">獎項名稱</th>
                        <th style="width:5%;text-align:center;">名額</th>
                        <th style="width:55%;text-align:center;">獎品</th>
                    </tr>
                    <tr>
                        <td rowspan="2">網路票選<br />民眾參與獎</td>
                        <td>民眾參與獎-特獎</td>
                        <td>5名</td>
                        <td>每名可獲得《科學發展》月刊1年期</td>
                    </tr>
                    <tr>
                        <td>民眾參與獎-普獎</td>
                        <td>15名</td>
                        <td>每名可獲得便利超商禮卷200元、USB手電筒一支</td>
                    </tr>
                </table>
                <h2 style="color: rgba(255,0,0,1)">注意：</h2>
                <h2>1.本活動各獎項或獎品以公告於本活動網站上的資料為準，如遇不可抗拒之因素，主辦單位保留更換其他等值獎項或獎品之權利，得獎者不得異議。</h2>
                <h2>2.本活動各獎項或獎品之得獎者，除以電子郵件通知各得獎者外，並於活動網站上公佈。</h2>
                <h2>3.得獎獎狀與獎品於競賽結束後以郵局掛號的方式寄送至得獎學校。</h2>
            </div>
            
            <div id="footer">
                <div id="fb-root"></div>
                <script>(function (d, s, id) {
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
                    <img src="img/mankids.jpg" alt="財團法人國語日報社" name="財團法人國語日報社" width="135" height="30" id="財團法人國語日報社" />
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
