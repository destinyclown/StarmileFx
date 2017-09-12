var tab = Object, redirectLogin, publicInterface = Object; //创建标签接口  
var zTree_Menu = Object;
var DropDownMenuTree = function (trreInfo) {
    if (top !== window) {//阻止外层框架嵌套
        var hrefl = $(window.parent.document).find('#iframe-' + window.name).attr('src');
        $(window.parent.document).find('#iframe-' + window.name).attr({ 'src': hrefl }); //当前ifram重载
        //		window.parent.window.location = window.parent.window.location.href;//整个页面重载
        return false;
    }
    if (trreInfo) {
        redirectLogin = tab.redirectLogin;
        this.pinyin = new tranformToPinyin();
        this.BeyondWrapMove = new blockBeyondWrap({ showBox: '.nav-collection-cont', display: '.bottom-more-list', showBtn: '.bottom-more-btn' });
        this.control = (typeof trreInfo.control == "object" ? trreInfo.control : false);
        this.host = window.location.origin;
        this.data = trreInfo.data;  //菜单数据
        this.cont = trreInfo.container;//菜单容器ID
        this.sysMenu = trreInfo.moreMenu ? trreInfo.moreMenu : '#contentMore'; //触发显示系统名按钮
        this.viweW = "100%"; //菜单可视宽度
        this.viweH = $(window).height();//"100%"; //菜单可视高度
        this.search = (typeof trreInfo.search == "boolean" ? trreInfo.search : false); //是否支持搜索
        this.collection = (typeof trreInfo.collection == "boolean" ? trreInfo.collection : false); //是否支持收藏常用菜单
        this.maxW = (typeof trreInfo.showMaxW == "number" ? trreInfo.showMaxW : 505);//最大宽度
        this.minW = 100; //最小宽度
        this.right = 50; //距离右边的距离
        this.helpUrl = (trreInfo.helpUrl ? this.helpUrl : './help.php?id=');    //帮助的链接
        this.subUrl = (trreInfo.subUrl ? this.subUrl : './sub.php?id=');		//订阅的处理链接
        this.defualtPage = (typeof trreInfo.defualtPage == "object" ? trreInfo.defualtPage : false); //工作台页面
        this.sysIco = (typeof trreInfo.sysIco == "string" ? trreInfo.sysIco : "fa-file-text-o");//默认系统图标
        this.NavIco = (typeof trreInfo.NavIco == "string" ? trreInfo.NavIco : "fa-file-text-o");  //默认菜单图标
        this.labelNav = (typeof trreInfo.labelNav == "string" ? trreInfo.labelNav : false); //导航标签容器
        this.iframes = trreInfo.iframes ? trreInfo.iframes : "#iframeListContainer";  //iframe容器ID
        this.winWidth = (typeof trreInfo.winWidth == "number" ? (trreInfo.winWidth > 300 ? trreInfo.winWidth : 300) : 1348);//屏幕宽度
        this.row = (typeof trreInfo.row == "number" ? trreInfo.row : 20);  //默认行数，超过则增加一列
        this.col = (typeof trreInfo.col == "number" ? trreInfo.col : 3);  //默认最大列数，超过则增加行数
        this.tnumbers = 0;
        this.moveSpeel = 300; //长按触发时间
        this.moveTime = 100; //长按时每次移动间隔时间
        this.moevDistance = 150;//单击移动距离
        this.moevShort = 15;//长按时每次移动距离
        this.sysData = []; this.friData = []; this.secData = []; this.thrData = [];
        this.signStata = [];//系统是否显示
        this.showMun = []; //收起子菜单
        this.searchChinesList = []; //搜索下拉数组
        this.localCacheNum = 50;  //缓存搜索下拉条数
        this.showSearchNum = 6; //搜索下拉最大初始显示
        this.defualtPageClose = false;  //记录工作台页面是否关闭
        this.switchStypeOpen = false;//横竖屏的默认值
        this.colScreen = (typeof trreInfo.colScreen == "boolean" ? trreInfo.colScreen : false);//是否支持菜单横屏切换
        this.comp = trreInfo.completionTips;  //回调
        this.TextMenus;
        //点击iframe时候，如果显示则移除名为ID或者Class的块(不支持跨域)
        this.removeEven = [//点击iframe，如果显示则会移除(不支持跨域)
            { removeDom: '#clickRightMenu', removeClass: ['clicked-nav-right', 'bottom-nav-hover'] }
        ];
        this.removeArr = [
            //{trig:'skin-peeler',show:'background-switch',tN:true}
        ];
        //点击iframe时候，如果显示则会隐藏(不支持跨域)
        this.hideEven = [
            { hid: '.system-menu-cont' },
            { hid: '.background-switch', removeClass: ['user-info-active'] },
            { hid: '.auxiliary-dropdown', removeClass: ['user-info-active'] },
            { hid: '.local-cache-history' }
        ];
        //点击页面时(非点击iframe)，如果显示则会隐藏(不支持跨域)
        this.hideArr = [//trig:触发的class，show显示的class，tN点击本身是否阻止
            //trig:触发类show,show:显示类,removeClass:移除类,removeDom:移除的DOM,noAction:阻止移除removeClass和removeDom的数组
            { trig: 'user-info-ctrl', show: 'auxiliary-dropdown', removeClass: ['user-info-active'], noAction: ['background-switch'] },
            {
                trig: 'content-logo',
                show: 'system-menu-cont',
                tN: true,
                removeClass: ['systemtViweviwezindex', 'systemtViweviweshow'],
                removeDom: ['systemtViweviwe', 'close-system-cont'],
                noAction: ['content-logo'],
                hid: ['system-menu-cont']
            },
            {
                trig: 'skin-peeler',
                show: 'background-switch',
                tN: true,
                noAction: ['introjs-helperLayer', 'introjs-overlay', 'introjs-tooltipReferenceLayer', 'introjs-disableInteraction']
            },
            {
                trig: 'show-select-viwe',
                show: 'showSelectViweviwe',
                removeDom: ['show-viwe-list', 'showSelectViweviwe'],
                noAction: ['show-viwe-list', 'viwe-list-close']
            },
            { trig: 'tiem-search-tiem', show: 'local-cache-history', tN: true }//
        ];
        this.intRemove = ['#showSelectViweviwe', '#show-viwe-list'];
        //右键菜单信息
        this.rightMenu = trreInfo.rightMenu ? trreInfo.rightMenu : [
            { icon: "fa fa-repeat", Text: "刷新", sign: "refreshthis", mywroke: true, list: false },
            { icon: "fa fa-close", Text: "关闭", sign: "closethis", mywroke: true, list: false },
            { icon: "", Text: "关闭其他", sign: "closeother", mywroke: true, list: false },
            { icon: "", Text: "关闭所有", sign: "closeall", mywroke: true, list: false },
            { icon: "fa fa-clone", Text: "新窗口打开", sign: "openother", mywroke: false, list: true },
            this.collection ? { icon: "fa fa-heart", Text: "设为常用", sign: "collection", mywroke: false, list: true } : {},
            { icon: "fa fa-check", Text: "设为默认打开", sign: "defaultopen", mywroke: false, list: true },
            { icon: "fa fa-check", Text: "设为默认首页", sign: "defaulthome", mywroke: false, list: true }
        ];
        //弹出右键菜单时隐藏的块
        this.rightNone = ['.auxiliary-dropdown', '.system-menu-cont'];
        this.tool = this.tools(); //载入工具包 
        tab = this.tool;
        publicInterface = this.tool;
        this.hostName = (this.tool.isContain({ arr: ['localhost', 'content.starmile.com.cn'], str: location.hostname }) ? '' : trreInfo.hostName);
        //滚动条
        this.onesF = new PageScrollMouse({ noNew: false });
        this.twosF = new PageScrollMouse({ noNew: false });
        this.thrsF = new PageScrollMouse({ noNew: false });
        this.Initialization(); //初始化
    }
}
DropDownMenuTree.prototype.Initialization = function () {//初始化
    var self = this;
    self.creaHtml();//生成页面基本html结构
    self.switchBGFun(); //皮肤切换
    self.DownNavMove(); //移动标签
    self.TextMenus = self.TextMenu();//自定义右键菜单
    self.otherCtrl();//其他控制
    var menuLoad = self.tool.uiMessage({ type: 'loading', text: '正在初始化菜单...' });
    var initFunct = {
        menuNav: function () {
            self.boxHideArr(); //点击页面，隐藏或者删除event
            self.locationHref();//菜单点击跳转
            self.containerCtrl();//容器大小控制
            menuLoad ? menuLoad.close() : null;
        },
        isOadata: true,
    }
    if (typeof self.data == "object" ? self.data.important : false) {
        self.LeftTrreSearch(self.data.data);
        initFunct.menuNav();
        initFunct.isOadata = false;
    } else {
        var userSiteContainerMenu = self.tool.getLocalCache({ strKey: "userSiteContainerMenuLocalCache" });//获取
        var oadataA = {};
        var data = [];
        if (userSiteContainerMenu !== "null" && userSiteContainerMenu.length > 0) {
            self.LeftTrreSearch(JSON.parse(userSiteContainerMenu));
            initFunct.menuNav();
            initFunct.isOadata = false;
            //self.tool.removeLocalCache({strKey:"userSiteContainerMenuLocalCache"});//删除
        }
        $.ajax({
            url: self.hostName + '/Account/GetMenuJson',
            dataType: 'jsonp',
            jsonp: 'callback',
            data: { email: $('#userEmail').text() },
            success: function (data) {
                self.LeftTrreSearch(data.Content);
                initFunct.isOadata ? initFunct.menuNav() : null;
                self.tool.saveLocalCache({ strKey: "userSiteContainerMenuLocalCache", strValue: JSON.stringify(data.Content) }); //保存
            },
            error: function (err) {
                console.warn('菜单数据无法获取！');
                self.tool.uiMessage({ type: 'error', text: '无法获取到菜单数据,请检查网络是否连接成功！' });
            }
        })
    }
}
DropDownMenuTree.prototype.JsonReomveHeavy = function (arr1, arr2) {//菜单树合并
    var self = this;
    var f = {
        Rec1: function (json1, json2) {
            var newData = json1;
            $.map(json1, function (a1, i) {
                $.map(json2, function (a2, j) {
                    if (a1.text == a2.text) {
                        if (!a1.href && !a2.href && a2.children && a2.children.length > 0) {
                            json1[i].children = f.Rec1(a1.children, a2.children).str1;
                        }
                        if (a2.href && a2.children && a2.children.length <= 0) {
                            a2.text = '(OA)' + a2.text;
                            json1.push(a2);
                        }
                        json2[j] = '';
                    }
                });
            });
            $.map(json2, function (item, index) {
                if (item && item != '') { json1.push(item) }
            });
            return { str1: json1, str2: json2 }
        }
    }
    var newData = f.Rec1(arr1, arr2);
    return newData.str1;
}
DropDownMenuTree.prototype.JsonTransformation = function (config) {//json转换
    var data = [], self = this;
    $.map(config, function (item, index) {
        data.push({});
        data[index].id = item.Id ? item.Id : '';
        data[index].help = typeof item.IsHasHelp == "boolean" ? item.IsHasHelp : false;
        data[index].icon = item.icon ? item.icon : '';
        data[index].newest = typeof item.IsNewest == "boolean" ? item.IsNewest : false;
        data[index].pid = item.ModuleId ? item.ModuleId : '';
        data[index].show = typeof item.IsShow == "boolean" ? item.IsShow : false;
        data[index].text = item.Title ? item.Title : '';
        data[index].href = item.Module ? self.getHref(item.Module) : '';
        data[index].children = (item.Children && item.Children.length > 0) ? self.JsonTransformation(item.Children) : [];
        data[index].isOa = true;
    });
    return data;
}
DropDownMenuTree.prototype.getHref = function (str) {
    var href = '';
    if (str.indexOf('/Start/RedirectLogin?redirectUrl=') >= 0) {
        href = str.split('/Start/RedirectLogin?redirectUrl=')[1];
    } else {
        //		if(document.location.hostname!="erpv2.banggood.cn"){
        //			href = document.location.protocol+'//erp.banggood.cn'+str;
        //		}
        //		if($('#commuServerUrl').length>0&&$('#commuServerUrl').text().length>0){
        //			href = $('#commuServerUrl').text()+str;
        //		}
        href = '/sso/home/RedirectLoginBG?RedirectURL=' + str;
    }

    return href;
}
DropDownMenuTree.prototype.creaHtml = function () {//生成页面基本html结构
    var self = this;
    var html = '<div class="content-bottom" id="collectionNav">'
        + '<div class="bottom-nav-cont" id="bottomNavContShouC">'
        + '</div>'
        + '<div class="bottom-nav-more" title="收起收藏菜单"><i class="fa fa-long-arrow-right"></i></div>'
        + '</div>';
    self.collection ? $('.content-container .container-right').append(html) : null;
    self.switchStypeOpen = (self.tool.getLocalCache({ strKey: location.pathname.replace(/[^\w]/g, '') + 'defaulCrossScreen' }) == 'true' ? true : false);//获取横屏设置
    if (self.switchStypeOpen && self.colScreen) {
        $('.content-container').addClass('cross-screen-style');
        $('.content-container .container-left').append($('.content-container .heand-user-info'));
    }
    $('.menu-open-btn').hide();
}
DropDownMenuTree.prototype.creaTree = function (creaInfo) {
    var self = this,
        data = creaInfo.data,
        searVal = creaInfo.searVal;
    self.signStata = [],
        systttem = self.tool.getLocalCache({ strKey: location.pathname.replace(/[^\w]/g, '') + 'systemMenuState' });//获取缓存系统
    systttem = systttem.length > 0 ? systttem.indexOf('{') >= 0 ? JSON.parse(systttem) : [] : [];
    for (var i in systttem) { self.signStata[i] = systttem[i] }
    var myFun = {
        getMenuState: function (getState) {//用于获取状态
            var getS = $(getState.par).find(getState.fin);
            for (var i = 0; i < getS.length; i++) {
                if (getS.eq(i).hasClass(getState.has)) {
                    getState.val[getS.eq(i).attr('signind')] = false;
                } else {
                    getState.val[getS.eq(i).attr('signind')] = true;
                }
            }
        },
        getCol: function (cl) { //菜单
            var star = [], end = [], allList = 0, sizes = self.row;
            function getCols(size) {
                for (var s = 0; s < cl.nav.length; s++) {
                    var thrNav = cl.nav[s].children ? (cl.nav[s].children == "" ? false : cl.nav[s].children) : false;//判断三级是否存在
                    allList += (thrNav.length > 0 ? thrNav.length + 1 : 1);
                    if (allList > sizes) {
                        allList = 0;
                        star.push(end[end.length - 1] ? end[end.length - 1] : 0);
                        end.push(s + 1);
                    }
                }
                allList > 0 ? (star.push(end[end.length - 1] ? end[end.length - 1] : 0), end.push(cl.nav.length)) : null;
                star.length > self.col ? (sizes += 2, allList = 0, star = [], end = [], getCols()) : null;
            }
            getCols();
            return { s: star, e: end };
        },
        createMune: function () {
            var sysHtml = '', state = '', showChild = [];
            $(self.cont).addClass('trre-cont-shaow');
            if ($(self.cont).find('.frist-cont-parent').length <= 0) {//判断是否创建sign:"",noShow输入框
                var searchHtml = '<div class="tiem-search-tiem">'
                    + '<input type="text" id="leftTrreMenuSearch" placeholder="输入关键词">'
                    + '<i class="fa fa-search search-ctrl"></i>'
                    + '<div class="local-cache-history"><div class="history-cont-list"></div></div>'
                    + '</div>';
                self.search ? $(self.cont).append(searchHtml) : null;
            }

            myFun.getMenuState({ has: 'system-cont-hover', val: self.showMun, par: self.cont, fin: '.system-cont-box' });
            $(self.cont).find('.frist-cont-parent').length > 0 ? $(self.cont).find('.frist-cont-parent').remove() : null;//判断菜单是否存在，存在则删除

            $(self.cont).append('<div class="frist-cont-parent"></div>');//创建菜单容器

            //系统名列表
            if ($(window.document).find('.system-menu-cont').length > 0) {
                myFun.getMenuState({ has: 'system-menu-state', val: self.signStata, par: '.system-menu-cont', fin: '.system-menu-list' });
                $(window.document).find('.system-menu-cont').remove();
                $(window.document).find('.content-container').append('<div class="system-menu-cont"></div>');
            } else {
                $(window.document).find('.content-container').append('<div class="system-menu-cont"></div>');
            }
            sysHtml += '<div style="color:#fff;font-size:14px; margin:0 0 0 10px; margin-bottom:20px">'
                + '<span>请根据您的需要进行选择，所选的系统同时显示在左侧主菜单中！</span>'
                + '<div style="position:absolute;right:12px; top:0;color:#fff;font-size:12px;">全选：<span class="btn-whether-select btn-whether-uncheck system-all-select" style="margin-left:1px;"></span></div>'
                + '</div>';
            sysHtml += '<div class="system-menu-block">';
            for (var i = 0; i < data.length; i++) {
                state = (typeof self.signStata[data[i].Id] == "boolean" ? (self.signStata[data[i].Id] ? "" : " system-menu-state") : (data[i].State ? " system-menu-state" : ""));
                sysHtml += '<div  class = "system-menu-list' + state + '"  signind="' + data[i].Id + '">';
                sysHtml += '<div class="system-menu-listCont">';
                sysHtml += '<div class="system-menu-ico fa ' + (data[i].Icon ? data[i].Icon : self.sysIco) + '"> </div>';
                sysHtml += '<div class="system-menu-text">' + (data[i].Name ? data[i].Name : "") + '</div>';
                sysHtml += '<div class = "system-menu-sign"><i class="fa fa-check"></i></div>';
                sysHtml += '</div>';
                sysHtml += '</div>';
                self.signStata[data[i].Id] ? null : showChild.push(self.signStata[data[i].Id]);
            }
            sysHtml += '</div>';
            $('.system-menu-cont').append(sysHtml);
            $('.system-menu-block').css({ 'max-height': $(window).height() * 0.8 });
            if (showChild.length == data.length) {
                $('.system-all-select').removeClass('btn-whether-uncheck').addClass('btn-whether-check');
            }
            data ? (typeof data === "object" ? myFun.system() : console.error('菜单数据源不存在或格式不正确！')) : console.error('菜单数据源不存在！');
        },
        system: function () {
            var thisText = '', seachSign = '', seachSign1 = '', navHtml = '', texHtml = '', state = '', showMun = '', iconClass = '', fristNav = Object;
            for (var i = 0; i < data.length; i++) {
                state = (typeof self.signStata[data[i].Id] == "boolean" ? (self.signStata[data[i].Id] ? " this-hidden" : "") : (data[i].State ? "" : " this-hidden"));
                showMun = (self.showMun[data[i].Id] ? " mune-nav-stop" : " system-cont-hover");
                iconClass = (self.showMun[data[i].Id] ? " fa-angle-down" : " fa-angle-up");
                fristNav = data[i].Children ? (data[i].Children == "[]" ? false : data[i].Children) : false;//判断一级是否存在
                thisText = self.getThisText({ val: searVal, texts: (data[i].Name ? data[i].Name : ""), level: "sys", index: [i] });
                texHtml = myFun.friNav({ nav: fristNav, f: i, sysId: data[i].Id });
                thisText.indexOf('select-text-yellow') >= 0 ? seachSign = " search-state" : seachSign = "";
                texHtml.html.indexOf('select-text-yellow') >= 0 ? seachSign1 = " search-state" : seachSign1 = "";
                navHtml = '';
                navHtml += '<div class="system-cont-box' + state + '' + showMun + '" signind="' + data[i].Id + '">';
                navHtml += '<div class="system-name-nav' + (seachSign != "" ? seachSign : seachSign1) + '">';
                navHtml += '<span class="sys-tiem-icon fa ' + (data[i].Icon ? data[i].Icon : self.sysIco) + '"></span>';
                navHtml += '<span class="system-nav-text" title="' + (data[i].Name ? data[i].Name : "") + '">' + thisText + '</span>';
                navHtml += '<span class="remove-system" title="收起菜单"><i class="fa' + iconClass + '"></i></span>'; //
                navHtml += '</div>';
                navHtml += texHtml.html;
                navHtml += '</div>';
                $(self.cont).find('.frist-cont-parent').append(navHtml);//菜单
            }
        },
        friNav: function (info) {
            var navHtml = '', texHtml = '', thisText = '', seachSign = '', seachSign1 = '', secNav = Object;
            if (info.nav) {
                for (var i = 0; i < info.nav.length; i++) {
                    secNav = info.nav[i].Children ? (info.nav[i].Children == "[]" ? false : info.nav[i].Children) : false;//判断二级是否存在
                    thisText = self.getThisText({ val: searVal, texts: (info.nav[i].Name ? info.nav[i].Name : ""), level: "fri", index: [info.f, i] });
                    texHtml = myFun.secNav({ nav: secNav, f: info.f, s: i });

                    thisText.indexOf('select-text-yellow') >= 0 ? seachSign = " search-state" : seachSign = "";
                    texHtml.html.indexOf('select-text-yellow') >= 0 ? seachSign1 = " search-state" : seachSign1 = "";
                    if ((typeof info.nav[i].State == "boolean" ? info.nav[i].State : true)) {
                        navHtml += '<div class="frist-cont-list' + (seachSign != "" ? seachSign : seachSign1) + (info.nav[i].Newest ? ' fri-cont-listnew' : '') + '">';
                        navHtml += '<div class="nav-trre-click trre-text-info" signend="' + info.f + '-' + i + '"' +
                            (info.nav[i].Name ? 'title="' + info.nav[i].Name + '"' : '') +
                            //(info.nav[i].href?'hrefl="'+(info.nav[i].href.indexOf(self.host)>=0?info.nav[i].href.split(self.host)[1]:info.nav[i].href)+'"':'')+
                            (info.nav[i].Url ? 'hrefl="' + info.nav[i].Url + '"' : '') +
                            (info.nav[i].Id ? 'mid="' + info.nav[i].Id + '"' : '') +
                            (info.nav[i].blank ? 'blank="' + info.nav[i].blank + '"' : '') +
                            '>';
                        navHtml += '<span class="nav-tiem-icon"> <i class="fa ' + (info.nav[i].Icon ? info.nav[i].Icon : self.NavIco) + '">' + '</i> </span>';
                        navHtml += '<span class="reture-nav-text nav-tiem-text">' + thisText + '</span>';
                        navHtml += '<span class="nav-tiem-nextIcon"> <i class="fa fa-angle-right' + (secNav ? "" : " next-right-opa") + ' "></i> </span>';
                        navHtml += '</div>';
                        navHtml += '<div class="sec-cont-parent' + (secNav ? " sec-cont-active" : "") + '">';
                        navHtml += '<div class="show-system-name"> <span class="system-sign-text">'
                            + (self.getThisText({ val: searVal, texts: (data[info.f].Name ? data[info.f].Name : ""), level: "sys", index: [info.f] }))
                            + '</span> <i class="fa fa-angle-right  sign-text-ico"></i> <span class="frinav-sign-text">' + thisText + '</span></div>';
                        navHtml += '<div class="mune-cont-move-c">' + texHtml.html + '</div>';
                        navHtml += '</div>';
                        navHtml += '</div>';
                    }
                }
            }
            return { html: navHtml };
        },
        secNav: function (info) {
            var navHtml = '', thisText = '', thrNav = Object;
            if (info.nav) {
                var cols = myFun.getCol({ nav: info.nav });//获取每列的段落
                for (var c = 0; c < cols.s.length; c++) {
                    navHtml += '<div class="cont-list-col">';
                    for (var i = cols.s[c]; i < cols.e[c]; i++) {
                        //for(var i=0;i<info.nav.length;i++){
                        thrNav = info.nav[i].Children ? (info.nav[i].Children == "[]" ? false : info.nav[i].Children) : false;//判断三级是否存在
                        thisText = self.getThisText({ val: searVal, texts: (info.nav[i].Name ? info.nav[i].Name : ""), level: "sec", index: [info.f, info.s, i] });

                        thisText.indexOf('select-text-yellow') >= 0 ? seachSign = " search-state" : seachSign = "";
                        texHtml = myFun.thrNav({ nav: thrNav, f: info.f, s: info.s, t: i });
                        if ((typeof info.nav[i].State == "boolean" ? info.nav[i].State : true)) {
                            navHtml += '<div class="sec-cont-list' + seachSign + (info.nav[i].Newest ? ' sec-cont-listnew' : '') + '">';
                            navHtml += '<div class="nav-trre-click sec-cont-title' + (info.nav[i].Url ? " sec-cont-titleHerf" : "") + '" signend="' + info.f + '-' + info.s + '-' + i + '"';
                            //navHtml += (info.nav[i].href?'hrefl="'+(info.nav[i].href.indexOf(self.host)>=0?info.nav[i].href.split(self.host)[1]:info.nav[i].href)+'"': '');
                            navHtml += (info.nav[i].Url ? 'hrefl="' + info.nav[i].Url + '"' : '');
                            navHtml += (info.nav[i].Id ? 'mid="' + info.nav[i].Id + '"' : '');
                            navHtml += (info.nav[i].Name ? 'title="' + info.nav[i].Name + '"' : '');
                            navHtml += (info.nav[i].blank ? 'blank="' + info.nav[i].blank + '">' : '>');
                            navHtml += '<span class="reture-nav-text sec-title-text">' + thisText + '</span> ';
                            navHtml += (info.nav[i].sub ? '<span class="sub-document-cont ' + (info.nav[i].subState ? "sub-state-active" : "") +
                                '" title="' + (info.nav[i].subState ? "取消订阅" : "订阅") + '">'
                                + (info.nav[i].subState ? '<i class="fa fa-star"></i>' : '<i class="fa fa-star-o"></i>') + '</span>' : '');
                            navHtml += (info.nav[i].Help ? '<span class="help-document-cont" title="帮助"><i class="fa fa-question-circle-o"></i></span>' : '');
                            navHtml += '</div>';
                            navHtml += texHtml.html;
                            navHtml += '</div>';
                        }
                    }
                    navHtml += '</div>';
                }
            }
            return { html: navHtml };
        },
        thrNav: function (info) {
            var navHtml = '', thisText = '';
            if (info.nav) {
                for (var i = 0; i < info.nav.length; i++) {
                    thisText = self.getThisText({ val: searVal, texts: (info.nav[i].Name ? info.nav[i].Name : ""), level: "thr", index: [info.f, info.s, info.t, i] });
                    thisText.indexOf('select-text-yellow') >= 0 ? seachSign = " search-state" : seachSign = "";
                    if ((typeof info.nav[i].State == "boolean" ? info.nav[i].State : true)) {
                        navHtml += '<div class="nav-trre-click thr-cont-list';
                        navHtml += (info.nav[i].Newest ? ' thr-cont-listnew' : '') + seachSign + '"';
                        navHtml += '" signend="' + info.f + '-' + info.s + '-' + info.t + '-' + i + '"';
                        //navHtml += (info.nav[i].href?'hrefl="'+(info.nav[i].href.indexOf(self.host)>=0?info.nav[i].href.split(self.host)[1]:info.nav[i].href)+'"': '');
                        navHtml += (info.nav[i].Url ? 'hrefl="' + info.nav[i].Url + '"' : '');
                        navHtml += (info.nav[i].Id ? 'mid="' + info.nav[i].Id + '"' : '');
                        navHtml += (info.nav[i].Name ? ' title="' + info.nav[i].Name + '"' : '');
                        navHtml += (info.nav[i].blank ? ' blank="' + info.nav[i].blank + '">' : '>');
                        navHtml += '<span class="reture-nav-text" >' + thisText + '</span>';
                        navHtml += (info.nav[i].sub ? '<span class="sub-document-cont ' + (info.nav[i].subState ? "sub-state-active" : "") +
                            '" title="' + (info.nav[i].subState ? "取消订阅" : "订阅") + '">'
                            + (info.nav[i].subState ? '<i class="fa fa-star"></i>' : '<i class="fa fa-star-o"></i>') + '</span>' : '');
                        navHtml += (info.nav[i].Help ? '<span class="help-document-cont" title="帮助"><i class="fa fa-question-circle-o"></i></span>' : '');
                        navHtml += '</div>';
                    }
                }
            }
            return { html: navHtml };
        }
    }
    myFun.createMune();//菜单创建
    $('.content-container').hasClass('cross-screen-style') ? self.TrreInitializationCross() : self.TrreInitialization({ scroll: true });
    //调用滚动条
    self.onesF.clearView({ viewClass: self.cont + ' .frist-cont-parent' });
    self.twosF.clearView({ viewClass: self.cont + ' .mune-cont-move-c', outerMove: false });
    $(self.cont).find('.scroll-cont-move').css({ 'width': '100%' });
    self.tool.serollFunC($('.frist-cont-parent'));//滚动条高度配置
    //是否展开子级菜单end
    self.division();
}
DropDownMenuTree.prototype.TrreInitialization = function (init) {
    var self = this;
    $('#conterNavTrre').css({ 'width': '' });
    $('#conterNavTrre .frist-cont-parent').css({ 'width': '' });
    $(self.labelNav).css({ 'width': self.getWidth($(self.labelNav), 'heandNavCtrl') - 7 });//头部导航标签宽度控制
    //高度控制strat
    $('.container-right').css({ 'height': '' });
    $(self.cont).css({
        'min-height': 100,
        'height': typeof self.viweH == "number" ? self.viweH : self.getHeight($(self.cont), 'trre-cont-shaow')
    })
    $(self.cont).find('.frist-cont-parent').css({
        'width': typeof self.viweW == "number" ? self.viweW : self.viweW,
        'height': $(self.cont).height() - $(self.cont).find('.tiem-search-tiem').outerHeight()
    });
    if (!$('.content-bottom').hasClass('content-bottom-disp')) {
        $(self.iframes).css({ 'height': $(window).height() - $('.container-top').outerHeight() - $('#collectionNav').outerHeight() });
    } else {
        $(self.iframes).css({ 'height': $(window).height() - $('.container-top').outerHeight() });
    }
    //高度控制end
    for (var i = 0; i < $(self.cont).find('.system-nav-text').length; i++) {//设置文本区域的宽度
        $(self.cont).find('.system-nav-text').eq(i).css({ "width": self.getWidth($(self.cont).find('.system-nav-text').eq(i), 'system-nav-text') - 15 });
    }
    for (var i = 0; i < $(self.cont).find('.nav-tiem-text').length; i++) {//设置文本区域的宽度
        $(self.cont).find('.nav-tiem-text').eq(i).css({ "width": self.getWidth($(self.cont).find('.nav-tiem-text').eq(i), 'nav-tiem-text') - 20 });
    }

    if (!$('.content-container').hasClass('cross-screen-style') && $('.container-left').hasClass('container-left-sm')) {
        $(self.cont).find('.tiem-search-tiem').css({ 'margin-right': '', 'margin-left': '' });
    } else {
        $(self.cont).find('.tiem-search-tiem').css({ 'margin-right': 12, 'margin-left': 12 });
    }
    $(self.cont).find('.system-cont-box.this-hidden').css({ "height": 0 });//不显示菜单
    self.labelDisplacement();
}
DropDownMenuTree.prototype.TrreInitializationCross = function () {
    var self = this;
    $('.container-right').css({ 'height': $(window).height() - $('.container-left').outerHeight() });
    if (!$('.content-bottom').hasClass('content-bottom-disp')) {
        $(self.iframes).css({ 'height': $(window).height() - $('.container-left').outerHeight() - $('.container-top').outerHeight() - $('#collectionNav').outerHeight() });
    } else {
        $(self.iframes).css({ 'height': $(window).height() - $('.container-left').outerHeight() - $('.container-top').outerHeight() });
    }
    if (!$('.content-container').hasClass('cross-screen-style') && $('.container-left').hasClass('container-left-sm')) {
        $(self.cont).find('.tiem-search-tiem').css({ 'margin-right': '', 'margin-left': '' });
    } else {
        $(self.cont).find('.tiem-search-tiem').css({ 'margin-right': 12, 'margin-left': 12 });
    }
    $(self.labelNav).css({ 'width': self.getWidth($(self.labelNav), 'heandNavCtrl') - 7 });//头部导航标签宽度控制
    $('#conterNavTrre').css({ 'width': self.getWidth($('.trre-cont-shaow'), 'trre-cont-shaow'), 'height': '', 'min-height': '' });
    $('#conterNavTrre .frist-cont-parent').css({ 'width': self.getWidth($('.frist-cont-parent'), 'frist-cont-parent') - 24 });
    $(self.cont).find('.system-cont-box.this-hidden').css({ "height": 0 });//不显示菜单
    self.labelDisplacement();
}
DropDownMenuTree.prototype.locationHref = function () {//菜单点击跳转
    var self = this;
    var workeClase = typeof self.defualtPage.close == "boolean" ? self.defualtPage.close : false;
    $(self.labelNav).find('.label-nav-cont').length <= 0 ? $(self.labelNav).append('<div class="label-nav-cont"></div>') : null;//添加标签容器
    if (self.defualtPage && $(self.labelNav).find('div[signend="mywroke"]').length <= 0 && !self.defualtPageClose) {
        self.tool.addTab({ id: 'mywroke', title: (self.defualtPage.text ? self.defualtPage.text : "工作台"), href: "/../" +self.defualtPage.href, cont: false, ifarm: false, close: workeClase }, false);
        $(self.labelNav).parents('.container-top').find('.move-right').removeClass('move-label-active');//移除移动按钮激活状态
    }
    var myFun = {
        retureNavText: function () {
            var this_ = $(this).parents('.nav-trre-click ').eq(0);
            var thisText = this_.find('.reture-nav-text').text(),//$(this).text(),
                thisUrl = this_.attr('hrefl') ? this_.attr('hrefl') : false,
                thisId = this_.attr('mid') ? this_.attr('mid') : false,
                thisBlank = this_.attr('blank') ? (this_.attr('blank') === "true" ? true : false) : false;
            //判断打开页面方式
            thisUrl ? (thisBlank ? window.open(thisUrl) : self.tool.addTab({ id: thisId, title: thisText, href: thisUrl, cont: false, ifarm: false }, false)) : null;
        },
        helpDocument: function () {//帮助信息跳转
            self.tool.uiMessage({ type: 'info', text: '暂时无帮助信息！' });
        },
        subDocument: function () {//订阅事件
            var this_ = $(this).parents('.nav-trre-click ').eq(0);
            self.tool.Subscribe({
                Url: 'http://192.168.2.58/Home/GetMenuJson',
                Fun: function () {
                    this_.find('i').hasClass('fa-star') ? this_.find('i').removeClass('fa-star').addClass('fa-star-o') : this_.find('i').removeClass('fa-star-o').addClass('fa-star');
                }
            })
        },
        openCollection: function (e) {
            var this_ = $(this), e = e || window.event;
            var ev = e.target ? e.target : e.srcElement;//e.button的值：0左键，1中间中间滚轮按下，2右键！(IE9 以下：1左键，2右键，4中间滚轮按下)
            this_.parent()[0].oncontextmenu = function (eve) { return false };
            //e.defaultPrevented();
            if (e.button != 2) {
                if (this_.hasClass('collection-btn') && !this_.hasClass('collection-mywroke')) {//设为常用
                    var info = $('.nav-trre-click[mid="' + $('.clicked-nav.label-nav-active').attr('mid') + '"]');
                    if (this_.attr('cancel') == 'true') {
                        self.tool.cancelCollection({
                            mid: info.attr('mid'),
                            title: info.attr('title'),
                            success: function () {
                                this_.attr({ 'cancel': 'false' });
                                this_.find('.collert-text').text('收藏此页');
                                this_.find('.fa').removeClass('fa-minus-square-o').addClass('fa-plus-square-o');
                                self.tool.uiMessage({ type: 'success', text: '您取消已收藏的 “' + info.attr('title') + '” 菜单成功!' });
                            },
                            fail: function (Message) {
                                self.tool.uiMessage({ type: 'error', text: Message + '！' });
                            }
                        });
                    } else {
                        self.tool.Collection({
                            mid: info.attr('mid'),
                            title: info.attr('title'),
                            muneCont: '',
                            success: function () {
                                this_.attr({ 'cancel': 'true' });
                                this_.find('.collert-text').text('取消收藏');
                                this_.find('.fa').removeClass('fa-plus-square-o').addClass('fa-minus-square-o');
                                self.tool.uiMessage({ type: 'success', text: '您收藏 “' + info.attr('title') + '” 菜单成功!' });
                            },
                            fail: function (Message) {
                                self.tool.uiMessage({ type: 'error', text: Message + '！' });
                            }
                        });
                    }
                } else if (!this_.hasClass('collection-movering')) {//打开常用菜单
                    $('.nav-trre-click[mid="' + this_.attr('mid') + '"]').find('.reture-nav-text').click();
                }
            }
            if (e.button == 2 && !this_.hasClass('collection-btn')) {//收藏右键
                var left = this_.offset().left,
                    top = this_.offset().top,
                    signend = this_.attr('mid');
                html = '';
                $('#clickRightMenu').length > 0 ? (
                    $('#clickRightMenu').remove(),
                    $('.clicked-nav-right').length > 0 ? $('.clicked-nav-right').removeClass('clicked-nav-right') : null,
                    $('.bottom-nav-hover').length > 0 ? $('.bottom-nav-hover').removeClass('bottom-nav-hover') : null
                ) : null;
                html += '<div class="click-right-menu click-right-bottom" id="clickRightMenu" signend="' + signend + '">';
                for (var i = 0; i < self.rightMenu.length; i++) {
                    if (self.rightMenu[i].list) {
                        if (self.rightMenu[i].sign == 'collection') {
                            html += getHtml({ info: self.rightMenu[i], newt: '取消收藏', icon: false });
                        }
                        if (self.rightMenu[i].sign == 'openother') {
                            html += getHtml({ info: self.rightMenu[i] });
                        }
                        if (self.rightMenu[i].sign == 'defaultopen') {
                            if (this_.attr('opendefualt') == 'true' || this_.attr('homedefualt') == 'true') {
                                html += getHtml({ info: self.rightMenu[i], icon: true });
                            } else {
                                html += getHtml({ info: self.rightMenu[i], icon: false });
                            }
                        }
                        if (self.rightMenu[i].sign == 'defaulthome') {
                            if (this_.attr('homedefualt') == 'true') {
                                html += getHtml({ info: self.rightMenu[i], icon: true });
                            } else {
                                html += getHtml({ info: self.rightMenu[i], icon: false });
                            }
                        }
                    }
                }
                html += '<div class="click-right-bot"></div></div>';
                $(window.document).find('.content-container').append(html);
                function getHtml(tInfo) {
                    var ghtml = '';
                    ghtml += '<div class= "right-menu-list" rightclick="' + tInfo.info.sign + '" cancel="true">';
                    ghtml += '<span class="right-menu-ico"><i class="' + (typeof tInfo.icon == "boolean" ? tInfo.icon ? tInfo.info.icon : '' : tInfo.info.icon) + '"></i></span>';
                    ghtml += '<span class="right-menu-text">' + (tInfo.newt ? tInfo.newt : tInfo.info.Text) + '</span>';
                    ghtml += '</div>';
                    return ghtml;
                }
                this_.addClass('bottom-nav-hover');
                $('#clickRightMenu').css({
                    'left': left,
                    'top': top - $('#clickRightMenu').outerHeight(),
                });
                $('.click-right-menu').find('.right-menu-list').one('mousedown', function () {
                    self.swicthRightMenu({ tc: $(this), men: this_ });
                })
            }
        },
        domeMove: function (e) {//收藏栏的拖动
            var this_ = $(this), down = e || window.event, par = $(this).parent();
            if (!this_.hasClass('collection-btn') && down.button != 2) {
                var downv = down.target ? down.target : down.srcElement;
                downX = down.clientX,
                    Html = '', oldMid = '', oldLeft = 0,
                    marginLeft = self.tool.getThisLeft({ dom: par }),
                    allChildW = self.tool.getThisLeft({ dom: par.parent().children().eq(par.parent().children().length - 1) }),
                    starL = self.tool.getThisLeft({ dom: par.parent().children().eq(1) }) + par.parent().children().eq(1).outerWidth() / 2,
                    thisW = par.outerWidth(),
                    indx = par.index();
                if ($('.bottom-nav-box[domemoveing="true"]').length > 0) {
                    oldMid = $('.bottom-nav-box[domemoveing="true"]').find('.bottom-nav-btn').attr('mid');
                    oldLeft = parseInt($('.bottom-nav-box[domemoveing="true"]').css('margin-left'))
                    $('.bottom-nav-box[domemoveing="true"]').remove();
                };
                Html += '<div class="bottom-nav-box" domemoveing="true"><div class="bottom-nav-btn collection-movering" mid="' + this_.attr('mid') + '">';
                Html += '<span class="bottom-nav-text">' + this_.find('.bottom-nav-text').text() + '</span>';
                Html += '</div></div>';
                this_.addClass('bottom-nav-movesta');
                window.document.onmousemove = function (e) {//鼠标移动
                    var e = e || window.event;
                    var ev = e.target ? e.target : e.srcElement,
                        moveL = e.clientX - down.clientX;
                    if (typeof moveL == "number" && moveL != 0) {
                        $('.bottom-nav-box[domemoveing="true"]').length > 0 ? null : $('#bottomNavContShouC .nav-collection-cont').append(Html);
                        $('#mousemoveMoveingStaingd').length <= 0 && $(window.document).find('body ' + self.iframes).append('<div id="mousemoveMoveingStaingd"></div>');
                        $('.bottom-nav-box[domemoveing="true"]').css({
                            'position': 'fixed',
                            'margin-left': marginLeft + moveL,
                            'background': 'rgba(255,255,255,0.8)',
                            'opacity': 0.8
                        })
                        var numb = self.tool.getChildIndex({ lef: marginLeft + moveL, dom: par.parent() });
                        if (numb && numb > 0) {
                            par.parent().children().removeClass('bottom-nav-inset');
                        }
                        if (indx != numb && indx + 1 != numb && numb > 0) {
                            par.parent().children().eq(numb - 1).addClass('bottom-nav-inset');
                        }

                        $('#clickRightMenu').length > 0 ? (
                            $('#clickRightMenu').remove(),
                            $('.clicked-nav-right').length > 0 ? $('.clicked-nav-right').removeClass('clicked-nav-right') : null,
                            $('.bottom-nav-hover').length > 0 ? $('.bottom-nav-hover').removeClass('bottom-nav-hover') : null
                        ) : null;
                    }
                }
                $(window.document).one('mouseup', function (e) {//鼠标抬起
                    var e = e || window.event;
                    var ev = e.target ? e.target : e.srcElement,
                        upX = e.clientX,
                        mid = '', Left = 0,
                        dom = $('.bottom-nav-box[domemoveing="true"]'),
                        insetb = 0, collectionOrder = [];
                    window.document.onmousemove = null;
                    if ($('.bottom-nav-box.bottom-nav-inset').length > 0) {
                        insetb = $('.bottom-nav-box.bottom-nav-inset').index() + 1
                    }
                    if (insetb > 0) {
                        par.insertBefore(par.parent().children().eq(insetb));
                    }
                    par.parent().children().removeClass('bottom-nav-inset');
                    dom.remove();
                    $('#mousemoveMoveingStaingd').length > 0 ? $('#mousemoveMoveingStaingd').remove() : null;
                    this_.removeClass('bottom-nav-movesta');
                    for (var i = 0; i < par.parent().children().length; i++) {
                        if (!par.parent().children().eq(i).find('.bottom-nav-btn').hasClass('collection-btn')) {
                            collectionOrder.push({
                                "MenuKey": par.parent().children().eq(i).find('.bottom-nav-btn').attr('mid'),
                                "MenuName": par.parent().children().eq(i).find('.bottom-nav-btn .bottom-nav-text').text(),
                            });
                        }
                    }
                    if (collectionOrder.length > 0) {
                        self.tool.removeLocalCache({ strKey: location.pathname.replace(/[^\w]/g, '') + 'collectionOrderJSON' });// 删除缓存数据
                        self.tool.saveLocalCache({ strKey: location.pathname.replace(/[^\w]/g, '') + 'collectionOrderJSON', strValue: JSON.stringify(collectionOrder) });//保存到缓存
                    }
                })
            }
        }
    };
    $(window.document).on('click', self.cont + ' .nav-tiem-icon', myFun.retureNavText); //点击打开页面
    $(window.document).on('click', self.cont + ' .reture-nav-text', myFun.retureNavText); //点击打开页面
    $(window.document).on('click', self.cont + ' .help-document-cont', myFun.helpDocument);//帮助跳转
    $(window.document).on('click', self.cont + ' .sub-document-cont', myFun.subDocument);//订阅事件
    $(window.document).on('mouseup', '.bottom-nav-btn', myFun.openCollection);//打开常用菜单(或者点击收藏)
    $(window.document).on('mousedown', '.nav-collection-cont .bottom-nav-btn', myFun.domeMove);//常用菜单按压拖动
}
DropDownMenuTree.prototype.containerCtrl = function () {//容器大小控制
    var self = this;
    $(self.labelNav).addClass('heandNavCtrl');
    $('#bottomNavContShouC').is(':hidden') ? $(self.iframes).css({ 'height': $(window).height() - $(self.iframes).offset().top }) :
        $(self.iframes).css({ 'height': $(window).height() - $(self.iframes).offset().top - $('#collectionNav').outerHeight() });//iframe容器高度控制
    $(self.labelNav).css({ 'width': self.getWidth($(self.labelNav), 'heandNavCtrl') });//头部导航标签宽度控制
    $('#bottomNavContShouC').css({ 'width': self.getWidth($('#bottomNavContShouC'), 'bottom-nav-cont') - 10 }); //nav-collection-cont
    if (!$('.content-container').hasClass('cross-screen-style') && self.winWidth >= $(window).width() && $('.container-left').hasClass('container-left-lg')) {
        $('.container-left').removeClass('container-left-lg').addClass('container-left-sm');
        $('.container-top .heand-btn-nav .fa').removeClass('fa-outdent').addClass('fa-indent');
    }
    $(self.labelNav).css({ 'width': self.getWidth($(self.labelNav), 'heandNavCtrl') - 7 });//头部导航标签宽度控制
    if ($('.content-container').hasClass('cross-screen-style')) {
        $('#conterNavTrre').css({ 'width': self.getWidth($('.trre-cont-shaow'), 'trre-cont-shaow') });
        $('#conterNavTrre .frist-cont-parent').css({ 'width': self.getWidth($('.frist-cont-parent'), 'frist-cont-parent') - 24 });
    }
    if (!$('.content-container').hasClass('cross-screen-style') && $('.container-left').hasClass('container-left-sm')) {
        $(self.cont).find('.tiem-search-tiem').css({ 'margin-left': '', 'margin-right': '' });
    } else if ($('.container-left').hasClass('container-left-sm')) {
        $(self.cont).find('.tiem-search-tiem').css({ 'margin-left': '', 'margin-right': '' });
    } else {
        $(self.cont).find('.tiem-search-tiem').css({ 'margin-left': 12, 'margin-right': 12 });
    }
    $(window).resize(function () {//systemtViweviwe
        if (!$('.content-container').hasClass('cross-screen-style')) {
            $('.container-right').css({ 'height': '' });
            $(self.cont).css({
                'min-height': 100,
                'height': typeof self.viweH == "number" ? self.viweH : self.getHeight($(self.cont), 'trre-cont-shaow')
            })
            $(self.cont).find('.frist-cont-parent').css({
                'width': typeof self.viweW == "number" ? self.viweW : self.viweW,
                'height': $(self.cont).height() - $(self.cont).find('.tiem-search-tiem').outerHeight()
            });
            if (self.winWidth + 17 >= $(window).width()) {
                if ($('.container-left').hasClass('container-left-lg')) {
                    $('.heand-btn-nav').click();
                    $('.container-left').addClass('container-left-smign');
                }
            } else {
                if ($('.container-left').hasClass('container-left-sm') && $('.container-left').hasClass('container-left-smign')) {
                    $('.heand-btn-nav').click();
                    $('.container-left').addClass('container-left-smign');
                }
            }
        } else {
            $('.container-right').css({ 'height': $(window).height() - $('.container-left').outerHeight() });
            $('#conterNavTrre').css({ 'width': self.getWidth($('.trre-cont-shaow'), 'trre-cont-shaow') });
            $('#conterNavTrre .frist-cont-parent').css({ 'width': self.getWidth($('.frist-cont-parent'), 'frist-cont-parent') - 24 });
        }
        $(self.labelNav).css({ 'width': self.getWidth($(self.labelNav), 'heandNavCtrl') - 7 });//头部导航标签宽度控制
        $('#bottomNavContShouC').is(':hidden') ? $(self.iframes).css({ 'height': $(window).height() - $(self.iframes).offset().top }) :
            $(self.iframes).css({ 'height': $(window).height() - $(self.iframes).offset().top - $('#collectionNav').outerHeight() });//iframe容器高度控制
        $('#bottomNavContShouC').css({ 'width': self.getWidth($('#bottomNavContShouC'), 'bottom-nav-cont') - 10 });
        if (!$('#bottomNavContShouC').is(':hidden')) {
            $('#bottomNavContShouC .nav-collection-cont').css({ 'width': self.getWidth($('#bottomNavContShouC'), 'bottom-nav-cont') - 35 });
        }
        //头部导航标签自动适应
        self.labelDisplacement();
        $('#clickRightMenu').length > 0 ? (
            $('#clickRightMenu').remove(),
            $('.clicked-nav-right').length > 0 ? $('.clicked-nav-right').removeClass('clicked-nav-right') : null,
            $('.bottom-nav-hover').length > 0 ? $('.bottom-nav-hover').removeClass('bottom-nav-hover') : null
        ) : null;
        self.tool.bindWindSize();
        setTimeout(function () { $('.frist-cont-parent').find('.scroll-sbar-box').eq($('.frist-cont-parent').find('.scroll-sbar-box').length - 1).hide() }, 50)//隐藏滚动条
        self.BeyondWrapMove.wrapHandle();
    })
}
DropDownMenuTree.prototype.labelDisplacement = function (state) {//标签移除或者宽度改变时的位移
    var self = this;
    function showMoveIco(active, activeParent) {//激活移动标签按钮函数
        activeParent.position().left >= 0 ?
            ($(self.labelNav).parents('.container-top').find('.move-left').removeClass('move-label-active'),
                (self.getAllWidth('.label-nav-cont') > active.parents(self.labelNav).width() ?
                    $(self.labelNav).parents('.container-top').find('.move-right').addClass('move-label-active') :
                    $(self.labelNav).parents('.container-top').find('.move-right').removeClass('move-label-active'))
            ) : ($(self.labelNav).parents('.container-top').find('.move-left').addClass('move-label-active'),
                (self.getAllWidth('.label-nav-cont') + activeParent.position().left > active.parents(self.labelNav).width() ?
                    $(self.labelNav).parents('.container-top').find('.move-right').addClass('move-label-active') :
                    $(self.labelNav).parents('.container-top').find('.move-right').removeClass('move-label-active'))
            );
    }
    if (state) {
        //左边
        state.active.position().left + state.activeParent.position().left < 0 ? state.activeParent.css({ 'left': -(state.active.position().left) }) : null;
        //右边
        (state.active.position().left + state.activeParent.position().left + state.active.outerWidth() > state.active.parents(self.labelNav).width()) ?
            state.activeParent.css({ 'left': -(state.active.position().left - state.active.parents(self.labelNav).width() + state.active.outerWidth()) }) : null;
    } else {
        if ($(self.labelNav).find('.label-nav-active').length > 0) {
            var activeChildL = $(self.labelNav).find('.label-nav-active').position().left,//当前激活块距离左边的距离
                activeChildW = $(self.labelNav).find('.label-nav-active').outerWidth(), //当前激活块宽度
                labelW = $(self.labelNav).width(),//可视宽度
                labelNavL = $(self.labelNav).find('.label-nav-cont').position().left,//标签容器移动的距离
                allChildW = self.getAllWidth('.label-nav-cont');//所有标签的宽度之和
            if (allChildW > labelW) {
                if ((activeChildL + labelNavL + activeChildW) > labelW) {
                    $(self.labelNav).find('.label-nav-cont').css({ 'left': labelW - (activeChildL + labelNavL + activeChildW) + labelNavL });
                    $(self.labelNav).css({ 'min-width': $(self.labelNav).find('.label-nav-active').outerWidth() });
                } else {
                    if ((activeChildL + labelNavL) < 0) {
                        $(self.labelNav).find('.label-nav-cont').css({ 'left': -activeChildL });
                    }
                }
                if (allChildW + labelNavL < labelW) {
                    $(self.labelNav).find('.label-nav-cont').css({ 'left': (labelW - (allChildW + labelNavL)) + labelNavL });
                    $(self.labelNav).css({ 'min-width': $(self.labelNav).find('.label-nav-active').outerWidth() });
                    $(self.labelNav).parents('.container-top').find('.move-right').hasClass('move-label-active') ?
                        $(self.labelNav).parents('.container-top').find('.move-right').removeClass('move-label-active') : null;
                }
            } else {
                $(self.labelNav).find('.label-nav-cont').css({ 'left': 0 });
                $(self.labelNav).parents('.container-top').find('.move-left').removeClass('move-label-active');
            }
        }
    }
    $(self.labelNav).find('.label-nav-cont').length > 0 ? showMoveIco($(self.labelNav).find('.label-nav-active'), $(self.labelNav).find('.label-nav-cont')) : null;
}
DropDownMenuTree.prototype.getWidth = function (wid, classN) {//返回classN父元素宽度减于所有子元素（除了classN）宽度之和
    var allwid = 0,
        widP = wid.parent();
    for (var i = 0; i < widP.children().length; i++) {
        if (!widP.children().eq(i).hasClass(classN) && !widP.children().eq(i).is(':hidden')) {
            allwid += widP.children().eq(i).outerWidth();
        }
    }
    return Math.ceil(widP.width() - allwid);
}
DropDownMenuTree.prototype.getAllWidth = function (classN) {//返回所有子元素的宽度之和
    var allwid = 0;
    for (var i = 0; i < $(classN).children().length; i++) {
        allwid += $(classN).children().eq(i).outerWidth();
    }
    return allwid;
}
DropDownMenuTree.prototype.getHeight = function (wid, classN) {//返回classN父元素高度减于所有子元素（除了classN）高度之和//隐藏的块高度为0
    var allwid = 0,
        widP = wid.parent();
    for (var i = 0; i < widP.children().length; i++) {
        if (!widP.children().eq(i).hasClass(classN) && widP.children().eq(i).get(0).tagName != "SCRIPT" && widP.children().eq(i).css('display') != 'none') {
            allwid += Math.ceil(widP.children().eq(i).outerHeight());
        }
    }
    return (widP.height() - allwid);
}
DropDownMenuTree.prototype.DownNavMove = function () {//移动头部导航标签
    var self = this;
    var contTop = $(self.labelNav).parents('.container-top'),
        moveT,
        signInde = true;
    contTop.find('.move-left').on('mousedown', function (e) {//左边
        var this_ = $(this);
        if (this_.hasClass('move-label-active')) {
            LabelMove({
                btn: '.move-left',
                direction: 'left'
            });
        }
    });
    contTop.find('.move-right').on('mousedown', function (e) {//右边
        var this_ = $(this);
        if (this_.hasClass('move-label-active')) {
            LabelMove({
                btn: '.move-right',
                direction: 'right'
            });
        }
    });
    function LabelMove(move) {//移动函数
        //启动连续移动
        setTimeout(function () {
            moveT = setInterval(function () {
                LabelMoveing({
                    btn: move.btn,
                    direction: move.direction
                })
            }, self.moveTime)
            signInde = false;
        }, self.moveSpeel);
        contTop.find(move.btn).one('mouseup', function () {//鼠标抬起
            //停止继续移动
            setTimeout(function () {
                clearInterval(moveT);
                signInde = true;
            }, self.moveSpeel);
            clearInterval(moveT);
            LabelMoveing({
                btn: move.btn,
                direction: move.direction,
                sign: signInde
            });
        })
    }
    function LabelMoveing(move) {
        if (move.sign) {
            $(self.labelNav).find('.label-nav-cont').css({
                'left': ($(self.labelNav).find('.label-nav-cont').position().left + (self.moevDistance * (move.direction == "right" ? -1 : 1)))
            })
        } else {
            $(self.labelNav).find('.label-nav-cont').css({
                'left': ($(self.labelNav).find('.label-nav-cont').position().left + (self.moevShort * (move.direction == "right" ? -1 : 1)))
            })
        }
        //点击左边时边界判断
        $(self.labelNav).find('.label-nav-cont').position().left >= 0 ?
            ($(self.labelNav).find('.label-nav-cont').css({ 'left': 0 }),
                (contTop.find(move.btn).hasClass('move-label-active') ? contTop.find(move.btn).removeClass('move-label-active') : null), judgment(move.direction)) :
            ((contTop.find(move.btn).hasClass('move-label-active') ? contTop.find(move.btn).addClass('move-label-active') : null), judgment(move.direction));
        //点击右边时边界判断
        self.getAllWidth('.label-nav-cont') + $(self.labelNav).find('.label-nav-cont').position().left <= $(self.labelNav).width() ?
            ($(self.labelNav).find('.label-nav-cont').css({ 'left': $(self.labelNav).width() - self.getAllWidth('.label-nav-cont') }),
                (contTop.find(move.btn).hasClass('move-label-active') ? contTop.find(move.btn).removeClass('move-label-active') : null), judgment(move.direction)) :
            ((contTop.find(move.btn).hasClass('move-label-active') ? contTop.find(move.btn).addClass('move-label-active') : null), judgment(move.direction));

        function judgment(dirte) {
            dirte == "left" ? (self.getAllWidth('.label-nav-cont') + $(self.labelNav).find('.label-nav-cont').position().left > $(self.labelNav).width() ?
                contTop.find('.move-right').addClass('move-label-active') : null) :
                ($(self.labelNav).find('.label-nav-cont').position().left < 0 ? contTop.find('.move-left').addClass('move-label-active') : null);
        }
    }
}
DropDownMenuTree.prototype.TextMenu = function () {//单击右键自定义菜单
    var self = this;
    var RightMenu = {
        myMenu: function (men) {
            for (var i in self.rightNone) {
                $(self.rightNone[i]).hide();
            }
            var menuHtml = '',
                menClose = typeof men.attr('thiscolse') == "string" ? (men.attr('thiscolse') == "true" ? true : false) : true;
            menuHtml += '<div class="click-right-menu" id="clickRightMenu" signend="' + men.attr('signend') + '">';
            if ($('#showSelectViweviwe').length > 0) {
                $('#showSelectViweviwe').remove();
            }
            if ($('#show-viwe-list').length > 0) {
                $('#show-viwe-list').remove();
            }
            for (var i = 0; i < self.rightMenu.length; i++) {
                if (self.rightMenu[i].Text) {
                    if (self.rightMenu[i].mywroke && men.attr('signend') == 'mywroke') {
                        if (!menClose && self.rightMenu[i].Text != "关闭") {
                            menuHtml += RightMenu.menuHtml({ data: self.rightMenu[i], icon: true });
                        }
                        if (menClose) {
                            menuHtml += RightMenu.menuHtml({ data: self.rightMenu[i], icon: true });
                        }
                    }
                    if (men.attr('signend') != 'mywroke') {
                        menuHtml += RightMenu.HandleList({ menu: self.rightMenu[i], men: men });
                    }
                }
            }
            menuHtml += '<div class="click-right-top"></div>';
            menuHtml += '</div>';
            if ($('#clickRightMenu').length > 0) {
                $('#clickRightMenu').remove();
                $(window.document).find('.content-container').append(menuHtml);
                $('.clicked-nav-right').length > 0 ? $('.clicked-nav-right').removeClass('clicked-nav-right') : null;
                $('.bottom-nav-hover').length > 0 ? $('.bottom-nav-hover').removeClass('bottom-nav-hover') : null;
                men.hasClass('label-nav-active') ? null : men.addClass('clicked-nav-right');
            } else {
                men.hasClass('label-nav-active') ? null : men.addClass('clicked-nav-right');
                $(window.document).find('.content-container').append(menuHtml);
            }
            $('.click-right-menu').css({
                "top": men.offset().top + men.outerHeight() - 1,
                "left": men.offset().left
            });
            $('.click-right-menu').find('.right-menu-list').one('mousedown', function () {
                self.swicthRightMenu({ tc: $(this), men: men });
            })
        },
        menuHtml: function (info) {
            var Html = '',
                className = (typeof info.icon == "boolean" ?
                    info.icon ? info.data.icon ? "class='" + info.data.icon + "'" : "" : ""
                    : typeof info.icon == "string" ? "class='" + info.icon + "'" : "");
            Html += '<div class="right-menu-list"  rightclick= "' + info.data.sign + '"' + (info.Text ? ' cancel="true"' : '') + ' >';
            Html += '<span class="right-menu-ico"><i ' + className + '></i></span>';
            Html += '<span class="right-menu-text">' + (info.Text ? info.Text : info.data.Text) + '</span>';
            Html += '</div>';
            return Html;
        },
        HandleList: function (menu) {
            var menuHtml = '', id = $('.nav-trre-click[mid="' + menu.men.attr('mid') + '"]').attr('mid');
            if (menu.menu.sign == "collection" && id) {
                $('#bottomNavContShouC').find('div[mid="' + id + '"]').length > 0 ? (menuHtml += RightMenu.menuHtml({ data: menu.menu, Text: '取消收藏', icon: false })) :
                    (menuHtml += RightMenu.menuHtml({ data: menu.menu, icon: true }));
            } else {
                if (menu.menu.sign == 'defaulthome' && $('#bottomNavContShouC').find('div[mid="' + id + '"]').length > 0) {
                    menuHtml += RightMenu.menuHtml({
                        data: menu.menu,
                        icon: ($('.bottom-nav-btn[mid="' + id + '"]').attr('homedefualt') == 'true' ? true : false)
                    });
                }
                if (menu.menu.sign == 'defaultopen' && $('#bottomNavContShouC').find('div[mid="' + id + '"]').length > 0) {
                    menuHtml += RightMenu.menuHtml({
                        data: menu.menu,
                        icon: ($('.bottom-nav-btn[mid="' + id + '"]').attr('opendefualt') == 'true' ? true :
                            ($('.bottom-nav-btn[mid="' + id + '"]').attr('homedefualt') == 'true' ? true : false))
                    });
                }
                if (menu.menu.sign.indexOf('defaul') < 0) {
                    if (menu.men.attr('nocont') != "true") {
                        menuHtml += RightMenu.menuHtml({ data: menu.menu, icon: true });
                    } else if (menu.menu.sign != "collection") {
                        menuHtml += RightMenu.menuHtml({ data: menu.menu, icon: true });
                    }
                }
            }
            return menuHtml;
        }
    };
    $(window.document).on('mouseup', function (e) {//移除自定义右键菜单
        var e = e || window.event;
        var this_ = $(this),
            ev = e.target ? e.target : e.srcElement,
            clickClass = ((ev.className).replace(/(^\s*)|(\s*$)/g, '')).replace(/[\ ]/g, ".") != '' ? '.' + ((ev.className).replace(/(^\s*)|(\s*$)/g, '')).replace(/[\ ]/g, ".") : '';
        if (e.button != 2) {
            $(self.labelNav).find('.clicked-nav').removeClass('clickRight clicked-nav-right');
            $('#clickRightMenu').length > 0 ? $('#clickRightMenu').remove() : null;
            $('.bottom-nav-hover').length > 0 ? $('.bottom-nav-hover').removeClass('bottom-nav-hover') : null;
        }
        if (e.button == 2) {
            var arr = ['clicked-nav', 'bottom-nav-btn', 'nav-trre-click'];
            for (var i = 0; i < arr.length; i++) {
                if (dis()) {
                    $(self.labelNav).find('.clicked-nav').removeClass('clickRight clicked-nav-right');
                    $('#clickRightMenu').length > 0 ? $('#clickRightMenu').remove() : null;
                    $('.bottom-nav-hover').length > 0 ? $('.bottom-nav-hover').removeClass('bottom-nav-hover') : null;
                }
            }
            function dis() {
                var ind = 0;
                for (var i = 0; i < arr.length; i++) {
                    if (!$(ev).hasClass(arr[i]) && $(ev).parents('.' + arr[i]).length <= 0) {
                        ind++;
                    }
                }
                return (ind == arr.length ? true : false);
            }
        }
    })
    $(window.document).on('mouseenter', self.cont, function () {
        $(self.labelNav).find('.clicked-nav').removeClass('clickRight clicked-nav-right');
        $('#clickRightMenu').length > 0 ? $('#clickRightMenu').remove() : null;
        $('.bottom-nav-hover').length > 0 ? $('.bottom-nav-hover').removeClass('bottom-nav-hover') : null;
    })
    $(window.document).on('mouseleave', '#clickRightMenu', function () {
        $(self.labelNav).find('.clicked-nav').removeClass('clickRight clicked-nav-right');
        $('#clickRightMenu').length > 0 ? $('#clickRightMenu').remove() : null;
        $('.bottom-nav-hover').length > 0 ? $('.bottom-nav-hover').removeClass('bottom-nav-hover') : null;
    })
    return RightMenu;
}
DropDownMenuTree.prototype.swicthRightMenu = function (menu) {//右键菜单的操作
    var self = this;
    var swictFun = {
        resetLoad: function () {//刷新
            //$('#iframe-'+menu.men.attr('mid')).attr('src',$('#iframe-'+menu.men.attr('mid')).attr('src'));
            var getSrcB = $('.iframe-list[signend="' + menu.men.attr('mid') + '"]').find('iframe');
            getSrcB.attr('src', getSrcB.attr('src'));
        },
        closeThis: function () {//关闭
            for (var i = menu.men.index(); i < menu.men.parents('.heandNavCtrl').find('.clicked-nav').length; i++) {
                menu.men.parents('.heandNavCtrl').find('.clicked-nav').eq(i).css({
                    'left': menu.men.parents('.heandNavCtrl').find('.clicked-nav').eq(i).position().left - menu.men.outerWidth()
                })
            }
            if (menu.men.hasClass('label-nav-active')) {//活动状态移动以及显示iframe
                menu.men.parents('.heandNavCtrl').find('.clicked-nav').length >= (menu.men.index() + 2) ?
                    (menu.men.parents('.heandNavCtrl').find('.clicked-nav').eq(menu.men.index() + 1).addClass('label-nav-active'),
                        self.tool.showIframe({ sign: menu.men.parents('.heandNavCtrl').find('.clicked-nav').eq(menu.men.index() + 1).attr("signend"), swit: false })) :
                    (menu.men.index() >= 1 ? (menu.men.parents('.heandNavCtrl').find('.clicked-nav').eq(menu.men.index() - 1).addClass('label-nav-active'),
                        self.tool.showIframe({ sign: menu.men.parents('.heandNavCtrl').find('.clicked-nav').eq(menu.men.index() - 1).attr("signend"), swit: false })) : null);
            }
            $(self.iframes).find('div[signend="' + menu.men.attr('mid') + '"]').remove();//移除iframe
            menu.men.remove();//移除标签
            self.labelDisplacement();//头部导航标签自动适应

        },
        closeOther: function () {//关闭其他clickRight
            menu.men.parents('.label-nav-cont').css({ 'left': 0 });
            menu.men.css({
                'left': ($(self.labelNav).find('div[signend="mywroke"]').length > 0 ? (menu.men.attr('mid') == "mywroke" ? 0 :
                    ($(self.labelNav).find('div[signend="mywroke"]').attr('thiscolse') == "false" ? $(self.labelNav).find('div[signend="mywroke"]').outerWidth() : 0)) : 0)
            });
            menu.men.removeClass('clicked-nav');
            $(self.iframes).find('div[signend="' + menu.men.attr('mid') + '"]').removeClass('iframe-list');
            $(self.cont).find('div[signend="' + menu.men.attr('mid') + '"]').removeClass('nav-trre-click');
            if ($(self.iframes).find('div[signend="mywroke"]').length > 0) {
                if ($(self.labelNav).find('div[signend="mywroke"]').attr('thiscolse') == "false") {//判断是否关闭
                    $(self.iframes).find('div[signend="mywroke"]').removeClass('iframe-list');
                    $(self.cont).find('div[signend="mywroke"]').length > 0 ? $(self.cont).find('div[signend="mywroke"]').removeClass('nav-trre-click') : null;
                    $(self.labelNav).find('div[signend="mywroke"]').length > 0 ? $(self.labelNav).find('div[signend="mywroke"]').removeClass('clicked-nav') : null;
                }
            }
            $(self.iframes).find('.iframe-list').remove();
            $(self.labelNav).find('.clicked-nav').remove();
            menu.men.hasClass('label-nav-active') ? null : (menu.men.addClass('label-nav-active'),
                $(self.iframes).find('div[signend="' + menu.men.attr('mid') + '"]').addClass('iframe-active').removeClass('iframe-unactive'));
            menu.men.addClass('clicked-nav');
            $(self.iframes).find('div[signend="mywroke"]').length > 0 ? $(self.iframes).find('div[signend="mywroke"]').addClass('iframe-list') : null;
            $(self.labelNav).find('div[signend="mywroke"]').length > 0 ? $(self.labelNav).find('div[signend="mywroke"]').addClass('clicked-nav') : null;
            $(self.iframes).find('div[signend="' + menu.men.attr('mid') + '"]').addClass('iframe-list');
            $(self.cont).find('div[signend="' + menu.men.attr('mid') + '"]').addClass('nav-trre-click');
            $('.container-top .move-left').removeClass('move-label-active');
            $('.container-top .move-right').removeClass('move-label-active');
        },
        closeAll: function () {//关闭所有
            menu.men.parents('.label-nav-cont').css({ 'left': 0 });
            $(self.labelNav).find('div[signend="mywroke"]').length > 0 ? $(self.labelNav).find('div[signend="mywroke"]').css({ 'left': 0 }) : null;
            if ($(self.labelNav).find('.clicked-nav[signend="mywroke"]').length > 0) {
                if ($(self.labelNav).find('.clicked-nav[signend="mywroke"]').attr('thiscolse') == "false") {//判断是否关闭
                    $(self.iframes).find('div[signend="mywroke"]').removeClass('iframe-list');
                    $(self.cont).find('div[signend="mywroke"]').removeClass('nav-trre-click');
                    $(self.labelNav).find('div[signend="mywroke"]').removeClass('clicked-nav');
                }
            }
            $(self.iframes).find('.iframe-list').remove();
            $(self.labelNav).find('.clicked-nav').remove();
            if ($(self.labelNav).find('div[signend="mywroke"]').length > 0) {
                $(self.iframes).find('div[signend="mywroke"]').removeClass('iframe-unactive').addClass('iframe-list iframe-active');
                $(self.cont).find('div[signend="mywroke"]').addClass('nav-trre-click');
                $(self.labelNav).find('div[signend="mywroke"]').addClass('label-nav-active clicked-nav');
            }
            $('.container-top .move-left').removeClass('move-label-active');
            $('.container-top .move-right').removeClass('move-label-active');
        },
        SetCommon: function () {//设为常用
            var info = $('.nav-trre-click[mid="' + menu.tc.parent().attr('signend') + '"]');
            if (menu.tc.attr('cancel') == 'true') {
                self.tool.cancelCollection({
                    mid: info.attr('mid'),
                    title: info.attr('title'),
                    success: function () {//clicked-nav
                        var getStrValue = self.tool.getLocalCache({ strKey: location.pathname.replace(/[^\w]/g, '') + 'collectionOrderJSON' }),//获取收藏
                            getOpenValue = self.tool.getLocalCache({ strKey: location.pathname.replace(/[^\w]/g, '') + 'defaultOpnePage' }),//获取默认打开
                            getHomeValue = self.tool.getLocalCache({ strKey: location.pathname.replace(/[^\w]/g, '') + 'defaultHomePage' }),//获取默认首页
                            newStrValue = [], newOpenValue = [];
                        self.tool.judgeCollection({ id: $(self.labelNav).find('.label-nav-active').attr('mid'), ifarm: false });
                        getStrValue = getStrValue.indexOf('[{') >= 0 ? JSON.parse(getStrValue) : [];
                        getOpenValue = getOpenValue.indexOf('[{') >= 0 ? JSON.parse(getOpenValue) : [];
                        for (var i = 0; i < getStrValue.length; i++) {
                            if (menu.men.attr('mid') != getStrValue[i].MenuKey) {
                                newStrValue.push({ mid: getStrValue[i].MenuKey, MenuName: getStrValue[i].MenuName });
                            }
                        }
                        for (var i = 0; i < getOpenValue.length; i++) {
                            if (menu.men.attr('mid') != getOpenValue[i].mid) {
                                newOpenValue.push({ mid: getOpenValue[i].mid, MenuName: getOpenValue[i].title });
                            }
                        }
                        self.tool.removeLocalCache({ strKey: location.pathname.replace(/[^\w]/g, '') + 'collectionOrderJSON' });//移除收藏
                        self.tool.removeLocalCache({ strKey: location.pathname.replace(/[^\w]/g, '') + 'defaultHomePage' });//移除默认首页
                        self.tool.removeLocalCache({ strKey: location.pathname.replace(/[^\w]/g, '') + 'defaultOpnePage' });//移除默认打开
                        self.tool.saveLocalCache({ strKey: location.pathname.replace(/[^\w]/g, '') + 'collectionOrderJSON', strValue: JSON.stringify(newStrValue) });//保存收藏
                        self.tool.saveLocalCache({ strKey: location.pathname.replace(/[^\w]/g, '') + 'defaultOpnePage', strValue: JSON.stringify(newOpenValue) });//保存默认打开
                        self.tool.uiMessage({ type: 'success', text: '您取消已收藏的 “' + info.attr('title') + '” 菜单成功!' });
                    },
                    fail: function (Message) {
                        self.tool.uiMessage({ type: 'error', text: Message + '！' });
                    }
                });
            } else {
                self.tool.Collection({
                    mid: info.attr('mid'),
                    title: info.attr('title'),
                    muneCont: '',
                    success: function () {
                        if ($('.clicked-nav[signend="' + menu.tc.parent().attr('signend') + '"]').hasClass('label-nav-active')) {
                            $('.bottom-nav-btn.collection-btn').attr({ 'cancel': 'true' });
                            $('.bottom-nav-btn.collection-btn').find('.fa').removeClass('fa-plus-square-o').addClass('fa-minus-square-o');
                            $('.bottom-nav-btn.collection-btn').find('.collert-text').text('取消收藏');
                        }
                        self.tool.uiMessage({ type: 'success', text: '您收藏 “' + info.attr('title') + '” 菜单成功!' });
                        self.tool.judgeCollection({ id: $(self.labelNav).find('.label-nav-active').attr('mid'), ifarm: false });
                    },
                    fail: function (Message) {
                        self.tool.uiMessage({ type: 'error', text: Message + '！' });
                    }
                });
            }
        },
        newOpne: function () {//新窗口打开
            var herfl = $('.nav-trre-click[mid="' + menu.men.attr('mid') + '"]').attr('hrefl');
            herfl = herfl ? herfl : menu.men.attr('href') ? menu.men.attr('href') : '';
            window.open(herfl);
        },
        defaultOpne: function (defualt) {
            var strValue = [], id = menu.men.attr('mid'),
                getStrValue = self.tool.getLocalCache({ strKey: location.pathname.replace(/[^\w]/g, '') + 'defaultOpnePage' });//获取
            getStrValue = getStrValue.indexOf('[{') >= 0 ? JSON.parse(getStrValue) : [],
                home = defualt ? (typeof defualt.home == "boolean" ? defualt.home : false) : false;
            if ($('.bottom-nav-btn[mid="' + id + '"]').attr('opendefualt') == 'true' && !home) {
                if ($('.bottom-nav-btn[mid="' + id + '"]').attr('homedefualt') != 'true') {
                    $('.bottom-nav-btn[mid="' + id + '"]').attr({ 'opendefualt': 'false' });
                }
            } else {
                $('.bottom-nav-btn[mid="' + id + '"]').attr({ 'opendefualt': 'true' });
                strValue.push({ mid: id, title: $('.nav-trre-click[mid="' + id + '"]').attr('title') });
            }
            for (var i = 0; i < getStrValue.length; i++) {
                if ($('.bottom-nav-btn[mid="' + getStrValue[i].mid + '"]').length > 0 && $('.bottom-nav-btn[mid="' + getStrValue[i].mid + '"]').attr('opendefualt') == 'true') {
                    strValue.push({ mid: getStrValue[i].mid, title: getStrValue[i].title });
                }
            }
            self.tool.removeLocalCache({ strKey: location.pathname.replace(/[^\w]/g, '') + 'defaultOpnePage' });//移除
            self.tool.saveLocalCache({ strKey: location.pathname.replace(/[^\w]/g, '') + 'defaultOpnePage', strValue: JSON.stringify(strValue) });//保存
        },
        defaultHome: function () {
            var id = menu.men.attr('mid'),
                strValue = { mid: menu.men.attr('mid'), title: $('.nav-trre-click[mid="' + menu.men.attr('mid') + '"]').attr('title') };
            if ($('.bottom-nav-btn[mid="' + id + '"]').attr('homedefualt') == 'true') {
                $('.bottom-nav-btn[mid="' + id + '"]').attr({ 'homedefualt': 'false' });
                self.tool.removeLocalCache({ strKey: location.pathname.replace(/[^\w]/g, '') + 'defaultHomePage' });//移除
            } else {
                $('.bottom-nav-btn[homedefualt="true"]').attr({ 'homedefualt': 'false' });
                $('.bottom-nav-btn[mid="' + id + '"]').attr({ 'homedefualt': 'true' });
                $('.bottom-nav-btn[mid="' + id + '"]').attr({ 'opendefualt': 'true' });
                self.tool.saveLocalCache({ strKey: location.pathname.replace(/[^\w]/g, '') + 'defaultHomePage', strValue: JSON.stringify(strValue) });//保存
            }
            $('.bottom-nav-btn[mid="' + id + '"]').attr('opendefualt') == 'true' ? swictFun.defaultOpne({ home: true }) : null;
        },
    };
    switch (menu.tc.attr('rightclick')) {
        case "refreshthis": swictFun.resetLoad() //刷新
            break;
        case "closethis": swictFun.closeThis()  //关闭当前页面
            break;
        case "closeother": swictFun.closeOther() //关闭其他页面
            break;
        case "closeall": swictFun.closeAll()  //全部关闭
            break;
        case "collection": swictFun.SetCommon()  //收藏
            break;
        case "openother": swictFun.newOpne()   //新窗口打开
            break;
        case "defaultopen": swictFun.defaultOpne() // 默认打开页面
            break;
        case "defaulthome": swictFun.defaultHome()  //默认首页
            break;
    }
    $(self.cont + ' .trre-text-active').parent().find('.sec-cont-parent').hide();
    $(self.cont).find('.trre-text-active').removeClass('trre-text-active');
    $(self.cont).find('.scroll-cont-move').css({ 'z-index': 1 });
    $(self.cont).find('.scroll-sbar-box').css({ 'z-index': 10 });
}
DropDownMenuTree.prototype.otherCtrl = function () {//其他的操作
    var self = this,
        tiem = "nav-tiem-text";
    var myFun = {
        showLeftMenu: function () {//收回左边菜单，只显示图标
            $('.container-left').removeClass('container-left-smign');

            if ($('.container-left').hasClass('container-left-lg')) {
                $('.container-left').removeClass('container-left-lg').addClass('container-left-sm');
                $(self.cont).find('.tiem-search-tiem').css({ 'margin-right': 0, 'margin-left': 0 });
                $('#leftTrreMenuSearch').css({ "visibility": "" });
                $(this).find('.fa').removeClass('fa-outdent').addClass('fa-indent');
                //$(self.cont).find('.tiem-search-tiem i').addClass('ColorCtrl');
                $(self.cont).find('.tiem-search-tiem i').css({ 'right': $(self.cont).find('.tiem-search-tiem').width() / 2 - $(self.cont).find('.tiem-search-tiem i').outerWidth() / 2 });
                ($(self.cont).find('.tiem-search-tiem').find('.search-ctrl').hasClass('search-ctrl-active') ?
                    $(self.cont).find('.tiem-search-tiem').find('.search-ctrl').addClass('fa-search').removeClass('fa-close search-ctrl-active') : null);

            } else {
                $('.container-left').removeClass('container-left-sm').addClass('container-left-lg');
                if (!$('.content-container').hasClass('cross-screen-style') && $('.container-left').hasClass('container-left-sm')) {
                    $(self.cont).find('.tiem-search-tiem').css({ 'margin-right': '', 'margin-left': '' });
                } else {
                    $(self.cont).find('.tiem-search-tiem').css({ 'margin-right': 12, 'margin-left': 12 });
                }
                //$(self.cont).find('.tiem-search-tiem i').removeClass('ColorCtrl');
                $(self.cont).find('.tiem-search-tiem i').css({ 'right': '' });
                $(this).find('.fa').removeClass('fa-indent').addClass('fa-outdent');
                ($('#leftTrreMenuSearch').val() != "" ? ($(self.cont).find('.tiem-search-tiem').find('.search-ctrl').hasClass('search-ctrl-active') ? null :
                    $(self.cont).find('.tiem-search-tiem').find('.search-ctrl').removeClass('fa-search').addClass('fa-close search-ctrl-active')
                ) : null);
            }
            $(self.cont).find('.scroll-cont-move').css({ 'width': '100%' });//容器宽度
            for (var i = 0; i < $(self.cont).find('.system-nav-text').length; i++) {//设置文本区域的宽度
                parseInt($(self.cont).find('.system-nav-text').eq(i).css('width')) < self.getWidth($(self.cont).find('.system-nav-text').eq(i), 'system-nav-text') - 15 ?
                    $(self.cont).find('.system-nav-text').eq(i).css({ "width": self.getWidth($(self.cont).find('.system-nav-text').eq(i), 'system-nav-text') - 15 }) : null;
            }
            for (var i = 0; i < $(self.cont).find('.' + tiem).length; i++) {//设置文本区域的宽度
                parseInt($(self.cont).find('.' + tiem).eq(i).css('width')) < self.getWidth($(self.cont).find('.' + tiem).eq(i), 'nav-tiem-text') - 20 ?
                    $(self.cont).find('.' + tiem).eq(i).css({ "width": self.getWidth($(self.cont).find('.' + tiem).eq(i), 'nav-tiem-text') - 20 }) : null;
            }
            //$('.content-container .container-right').css({'margin-left':$(self.cont).width()});//控制右边距离左边的距离

            $(self.labelNav).width() != (self.getWidth($(self.labelNav), 'heandNavCtrl') - 7) ? $(self.labelNav).css({ 'width': self.getWidth($(self.labelNav), 'heandNavCtrl') - 7 }) : null;//头部导航标签宽度控制
            self.labelDisplacement();//控制激活移动标签按钮
            self.BeyondWrapMove.wrapHandle();
        },
        changMunH: function () {//
            var contBox = $(self.cont).find('.system-cont-box');
            if ($('.container-left').hasClass('container-left-lg')) {
                for (var i = 0; i < contBox.length; i++) {
                    self.showMun[contBox.eq(i).attr('signind')] ? contBox.eq(i).addClass('mune-nav-stop') : null;
                }
            } else {
                for (var i = 0; i < contBox.length; i++) {
                    contBox.eq(i).hasClass('system-cont-hover') ? contBox.eq(i).removeClass('mune-nav-stop') : null;
                }
            }
        },
        //触发显示系统名按钮
        showSystem: function () {
            if ($('.system-menu-cont').is(':hidden')) {
                $('.system-menu-cont').show();
                $('.container-left').addClass('systemtViweviwezindex');
                $(this).find('.content-more').addClass('systemtViweviweshow');
                $(window.document).find('.content-container').append('<div class="systemtViweviwe" id="showSelectViweviwe"></div>');
                $(window.document).find('.content-container').append('<div class="close-system-cont"></div>');
                $(self.cont).addClass('container-filter-blur');
                $('.content-container .container-left .content-logo-text').addClass('container-filter-blur');
                $('.content-container .container-right').addClass('container-filter-blur');
            } else {
                $('.system-menu-cont').hide();
                $('#showSelectViweviwe').remove();
                $('.close-system-cont').remove();
                $('.container-left').removeClass('systemtViweviwezindex');
                $(this).find('.content-more').removeClass('systemtViweviweshow');
                $(self.cont).removeClass('container-filter-blur');
                $('.content-container .container-left .content-logo-text').removeClass('container-filter-blur');
                $('.content-container .container-right').removeClass('container-filter-blur');
            }
        },
        showMenuChild: function (e) {
            var e = e || window.event;
            var this_ = $(this),
                ev = e.target ? e.target : e.srcElement,
                thisP = $(this).parents('.system-cont-box');
            if (self.showMun[thisP.attr('signind')]) {//展开
                $(self.cont).find('.frist-cont-parent .system-name-nav').each(function (i, dom) {
                    if ($(dom).parent().hasClass('system-cont-hover')) { $(dom).click() }
                });
                thisP.removeClass('mune-nav-stop');
                this_.find('.fa-angle-down').addClass('fa-angle-up').removeClass('fa-angle-down');
                self.showMun[thisP.attr('signind')] = false;
                thisP.addClass('system-cont-hover');
            } else {//收起
                thisP.addClass('mune-nav-stop');
                this_.find('.fa-angle-up').addClass('fa-angle-down').removeClass('fa-angle-up');
                self.showMun[thisP.attr('signind')] = true;
                thisP.removeClass('system-cont-hover');
            }
            setTimeout(function () { self.tool.serollFunC($('.frist-cont-parent')) }, 300);//滚动条高度配置
        },
        showMenuName: function (e) {
            var e = e || window.event;
            var this_ = $(this),
                ev = e.target ? e.target : e.srcElement;
            if (this_.parents('.container-left').hasClass('container-left-sm') && !$('.content-container').hasClass('cross-screen-style')) {
                var sysSign = '';
                sysSign += '<div class="system-sign-name">';
                sysSign += this_.find('.system-nav-text').attr('title');
                sysSign += '</div>';
                $('.system-sign-name').length > 0 ? ($('.system-sign-name').remove(), $(window.document).find('.content-container').append(sysSign)) :
                    $(window.document).find('.content-container').append(sysSign);
                $('.system-sign-name').css({
                    "left": this_.offset().left + this_.outerWidth() + 5,
                    "top": this_.offset().top + (this_.outerHeight() > $('.system-sign-name').outerHeight() ? (this_.outerHeight() - $('.system-sign-name').outerHeight()) / 2 : 0)
                });
            }
        },
        removeMenuName: function () {
            var this_ = $(this);
            if (this_.parents('.container-left').hasClass('container-left-sm')) {
                $('.system-sign-name').remove();
            }
        },
        topNavMouseover: function () {
            $(this).hasClass('label-nav-active') ? null : $(this).addClass('clicked-nav-hover');
        },
        topNavMouseleave: function () {
            $(this).removeClass('clicked-nav-hover');
        },
        showMenu: function (e) {
            var this_ = $(this);
            if (self.showMun[this_.parents('.system-cont-box').attr('signind')]) {
                this_.parents('.system-cont-box').removeClass('mune-nav-stop');
                this_.find('.fa-angle-down').addClass('fa-angle-up').removeClass('fa-angle-down');
                self.showMun[this_.parents('.system-cont-box').attr('signind')] = false;
            } else {
                this_.parents('.system-cont-box').addClass('mune-nav-stop');
                this_.find('.fa-angle-up').addClass('fa-angle-down').removeClass('fa-angle-up');
                self.showMun[this_.parents('.system-cont-box').attr('signind')] = true;
            }
        },
        openAllMenu: function () {
            var contChild = $(self.cont).find('.system-cont-box');
            for (var i = 0; i < contChild.length; i++) {
                if (contChild.eq(i).hasClass('mune-nav-stop')) {
                    contChild.eq(i).removeClass('mune-nav-stop');
                }
                if (!contChild.eq(i).hasClass('system-cont-hover')) {
                    contChild.eq(i).addClass('system-cont-hover');
                    contChild.eq(i).find('.system-name-nav .remove-system .fa').removeClass('fa-angle-down').addClass('fa-angle-up');
                }
                self.showMun[contChild.eq(i).attr('signind')] = false;
            }
            setTimeout(function () {
                self.tool.serollFunC($('.frist-cont-parent'));//滚动条高度配置
            }, 300)
        },
        stopAllMenu: function () {
            var contChild = $(self.cont).find('.system-cont-box');
            for (var i = 0; i < contChild.length; i++) {
                if (!contChild.eq(i).hasClass('mune-nav-stop')) {
                    contChild.eq(i).addClass('mune-nav-stop system-cont-hover');
                }
                if (contChild.eq(i).hasClass('system-cont-hover')) {
                    contChild.eq(i).removeClass(' system-cont-hover');
                    contChild.eq(i).find('.system-name-nav .remove-system .fa').removeClass('fa-angle-up').addClass('fa-angle-down');
                }
                self.showMun[contChild.eq(i).attr('signind')] = true;
            }
            $('.frist-cont-parent>.scroll-container>.scroll-cont-move').css({ 'margin-top': 0 });
            $('.frist-cont-parent>.scroll-container>.scroll-sbar-box .scroll-sbar-move').css({ 'top': 0 });
            setTimeout(function () {
                self.tool.serollFunC($('.frist-cont-parent'));//滚动条高度配置
            }, 300)
        },
        bottomCollection: function (e) {
            if ($('#collectionNav').hasClass('content-bottom-disp')) {
                $('#collectionNav').removeClass('content-bottom-disp');
                $(this).find('i.fa').addClass('fa-long-arrow-right').removeClass('fa-long-arrow-left');
                if ($('.content-container').hasClass('cross-screen-style')) {
                    $(self.iframes).css({ 'height': $(window).height() - $('.container-top.nuselect-text').outerHeight() - $('#collectionNav').outerHeight() - $('.content-container .container-left').outerHeight() });
                } else {
                    $(self.iframes).css({ 'height': $(window).height() - $('.container-top.nuselect-text').outerHeight() - $('#collectionNav').outerHeight() });
                }
                $('#bottomNavContShouC').show();
                $(this).attr({ 'title': '收起收藏菜单' });
                var showTime = setInterval(function () {
                    $('#bottomNavContShouC').css({ 'width': self.getWidth($('#bottomNavContShouC'), 'bottom-nav-cont') - 10 });
                }, 10);
                setTimeout(function () { clearInterval(showTime) }, 300);
            } else {
                $('#collectionNav').addClass('content-bottom-disp');
                $(this).find('i.fa').removeClass('fa-long-arrow-right').addClass('fa-long-arrow-left');
                $(this).attr({ 'title': '展开收藏菜单' });
                setTimeout(function () {
                    $('#bottomNavContShouC').hide();
                    if ($('.content-container').hasClass('cross-screen-style')) {
                        $(self.iframes).css({ 'height': $(window).height() - $('.container-top.nuselect-text').outerHeight() - $('.content-container .container-left').outerHeight() });
                    } else {
                        $(self.iframes).css({ 'height': $(window).height() - $('.container-top.nuselect-text').outerHeight() });
                    }
                }, 300);
            }
        },
        keydownPosition: function () {//移动
            var infoCont = $('.local-cache-history .history-cont-list');
            if ($('.local-cache-history .scroll-cont-move').length > 0) {
                var info = infoCont.find('.history-list-active');
                var infoT = info.outerHeight() * (info.index() + 1),
                    contH = $('.history-cont-list').height(),
                    sorT = $('.local-cache-history .scroll-cont-move').css('margin-top').indexOf('px') >= 0 ?
                        parseInt($('.local-cache-history .scroll-cont-move').css('margin-top')) : 0;
                if (infoT + sorT > contH) {
                    $('.local-cache-history .scroll-cont-move').css({ 'margin-top': -(infoT - contH) });
                    $('.scroll-sbar-move').css({ 'top': (infoT - contH) * $('.local-cache-history .scroll-sbar-box').height() / $('.local-cache-history .scroll-cont-move').height() });
                }
                if (infoT + sorT < info.outerHeight()) {
                    $('.local-cache-history .scroll-cont-move').css({ 'margin-top': -(infoT - info.outerHeight()) });
                    $('.scroll-sbar-move').css({
                        'top': (infoT - info.outerHeight()) * $('.local-cache-history .scroll-sbar-box').height() / $('.local-cache-history .scroll-cont-move').height()
                    });
                }
            } else {
                var info = infoCont.find('.history-list-active');
                var infoH = infoCont.height(),
                    infoThisT = info.position().top,
                    infoST = infoCont.scrollTop();
                if (infoThisT >= infoH) {
                    infoCont.scrollTop(infoST + infoThisT - infoH + info.outerHeight() - 6);
                } else if (infoThisT < 0) {
                    infoCont.scrollTop(infoST + infoThisT - 6);
                }
            }
        },
        keydown: function (e) {
            var e = e || window.event;
            if ($('#leftTrreMenuSearch').is(':focus')) {
                var dom = $('.local-cache-history .history-cont-list .history-list-row');
                var info = $('.local-cache-history .history-cont-list').find('.history-list-active');
                if (e.keyCode == 38) {//上 self.tnumbers
                    if (info.length <= 0) {
                        if (!$('.show-more-history').is(':hidden')) {
                            $('.show-more-history').click();
                            dom = $('.local-cache-history .history-cont-list .history-list-row');
                        }
                        dom.eq(dom.length - 1).addClass('history-list-active');
                    } else if (info.index() - 1 >= 0) {
                        info.removeClass('history-list-active');
                        dom.eq(info.index() - 1).addClass('history-list-active');
                    } else {
                        if (!$('.show-more-history').is(':hidden')) {
                            $('.show-more-history').click();
                            dom = $('.local-cache-history .history-cont-list .history-list-row');
                        }
                        info.removeClass('history-list-active');
                        dom.eq(dom.length - 1).addClass('history-list-active');
                    }
                    e.preventDefault();
                    myFun.keydownPosition();
                }
                if (e.keyCode == 40) {//下
                    if (info.length <= 0) {
                        dom.eq(0).addClass('history-list-active');
                    } else if (info.index() + 1 < dom.length) {
                        info.removeClass('history-list-active');
                        dom.eq(info.index() + 1).addClass('history-list-active');
                    } else {
                        info.removeClass('history-list-active');
                        if ($('.show-more-history').is(':hidden')) {
                            dom.eq(0).addClass('history-list-active');
                        } else {
                            $('.show-more-history').click();
                            dom = $('.local-cache-history .history-cont-list .history-list-row');
                            dom.eq(self.showSearchNum).addClass('history-list-active');
                        }
                    }
                    myFun.keydownPosition();
                }
                if (e.keyCode == 13) {//回车
                    var info = $('.local-cache-history').find('.history-list-active');
                    info.length > 0 ? $('.nav-trre-click[mid="' + info.attr('mid') + '"] .reture-nav-text').click() : null;
                    myFun.saveLocalCache({ title: info.attr('tit'), mid: info.attr('mid'), tit: info.attr('tit') });
                    $('#leftTrreMenuSearch').blur();
                }
            }
        },
        searchDroDown: function () {
            $('.nav-trre-click[mid="' + $(this).attr('mid') + '"] .reture-nav-text').click();
            myFun.saveLocalCache({ title: $(this).attr('tit'), mid: $(this).attr('mid'), tit: $(this).attr('tit') });
            $('#leftTrreMenuSearch').focus();
        },
        saveLocalCache: function (save) {
            var get = self.tool.getLocalCache({ strKey: location.pathname.replace(/[^\w]/g, '') + 'searchLocalCache' }),
                newJson = [save]
            localCacheNum = 0;  //获取
            if (typeof get == "string") {
                get = (get.indexOf("{") >= 0 ? JSON.parse(get) : []);
            }
            for (var i = 0; i < get.length; i++) {
                if ($('.nav-trre-click[mid="' + get[i].mid + '"]').length > 0 && get[i].mid != save.mid) {
                    localCacheNum < self.localCacheNum ? newJson.push({ title: get[i].title, mid: get[i].mid, tit: get[i].title }) : null;
                    localCacheNum++;
                }
            }
            self.tool.removeLocalCache({ strKey: location.pathname.replace(/[^\w]/g, '') + 'searchLocalCache' });  // 删除
            self.tool.saveLocalCache({ strKey: location.pathname.replace(/[^\w]/g, '') + 'searchLocalCache', strValue: JSON.stringify(newJson) });  //保存
            //console.log(newJson);
        },
        insetMenu: function () {
            $('.local-cache-history').hide();
            $('#leftTrreMenuSearch').blur();
        },
        keydownBtn: function () {
            $('.local-cache-history .history-cont-list').find('.history-list-row').removeClass('history-list-active');
            $(this).addClass('history-list-active');
        },
        InToMnenu: function () {//移进一级
            var vari = { t: $(this), s: $(window.document).scrollTop(), w: $(window).width(), h: $(window).height() };
            $(self.cont).find('.scroll-cont-move').css({ 'width': '100%' });
            //滚动条的层级和菜单内容的层级切换
            vari.t.find('.trre-text-info .next-right-opa').length > 0 ?
                ($(self.cont).find('.scroll-cont-move').css({ 'z-index': 1 }), $(self.cont).find('.scroll-sbar-box').css({ 'z-index': 10 })) :
                ($(self.cont).find('.scroll-sbar-box').css({ 'z-index': 1 }), $(self.cont).find('.scroll-cont-move').css({ 'z-index': 100 }));

            vari.t.find('.sec-cont-active').length > 0 ? (vari.t.find('.sec-cont-active').show(), vari.t.find('.trre-text-info').addClass('thr-yes-active')) : null;
            vari.t.find('.trre-text-info').addClass('trre-text-active');
            vari.t.find('.sec-cont-parent').css({//控制弹窗宽度和距离
                'max-width': self.maxW,
                'min-width': self.minW,
                'margin-top': -vari.t.outerHeight() - vari.s
            });
            if ($('.content-container').hasClass('cross-screen-style')) {
                vari.t.find('.sec-cont-parent').css({ 'max-height': vari.h - $('.container-left').outerHeight() });
                if ($(window).width() - vari.t.offset().left - vari.t.width() > vari.t.find('.sec-cont-parent').outerWidth()) {
                    vari.t.find('.sec-cont-parent').css({//控制弹窗宽度和距离
                        'left': vari.t.offset().left + vari.t.width(),
                    });
                } else {
                    vari.t.find('.sec-cont-parent').css({//控制弹窗宽度和距离
                        'left': vari.t.offset().left - vari.t.find('.sec-cont-parent').outerWidth(),
                    });//thr-yes-active
                    vari.t.find('.trre-text-info').addClass('thr-yes-activeL');
                }
            } else {
                vari.t.find('.sec-cont-parent').css({//控制弹窗宽度和距离
                    'left': $(self.cont).width() - $(window.document).scrollLeft(),
                    'max-height': vari.h,
                });
            }

            function getMarginBottm(dom) {
                var domB = 0;
                dom.css('margin-bottom').indexOf('px') >= 0 ? domB = parseInt(dom.css('margin-bottom')) : null;
                return domB;
            }
            vari.t.find('.mune-cont-move-c').css({
                'height': vari.t.find('.sec-cont-parent').height() - (vari.t.find('.show-system-name').is(':visible') ?
                    (vari.t.find('.show-system-name').outerHeight() + getMarginBottm(vari.t.find('.show-system-name'))) : 0)
            });
            self.tool.positionCtrl(vari.t);//弹窗位置控制
            self.tool.serollFunC(vari.t.find('.mune-cont-move-c'));//滚动条高度配置
            vari.t.find('.scroll-sbar-box').css('width', '10px');
            vari.t.find('.scroll-sbar-move').css('width', '8px');
            vari.t.find('.sec-cont-parent').css({//重置
                'width': vari.t.find('.cont-list-col').length > 0 ?
                    (vari.t.find('.cont-list-col').length * vari.t.find('.cont-list-col').outerWidth() + (vari.t.find('.scroll-sbar-box').is(':visible') ? 50 : 40))
                    : (vari.t.find('.scroll-sbar-box').is(':visible') ? 210 : 200),
                'padding-right': vari.t.find('.scroll-sbar-box').is(':visible') ? 5 : ''
            });
        },
        OutToMnenu: function () {//移出一级
            $(self.cont + ' .trre-text-active').parent().find('.sec-cont-parent').hide();
            $(self.cont).find('.trre-text-active').removeClass('trre-text-active');
            $(self.cont).find('.scroll-cont-move').css({ 'z-index': 1 });
            $(self.cont).find('.scroll-sbar-box').css({ 'z-index': 10 });
            $(self.cont).find('.trre-text-info').removeClass('thr-yes-activeL');
        },
        sysSlect: function (config) {
            var sysS = $('.system-menu-cont .system-menu-list'), showChild = [], signStatas = {};
            if (typeof config.all == "boolean") {
                if (config.all) {
                    sysS.each(function (i, dom) {
                        if (!$(dom).hasClass('system-menu-state')) {
                            $(dom).addClass('system-menu-state');
                            $(self.cont).find('.system-cont-box[signind="' + $(dom).attr('signind') + '"]').css({ "height": "" }).removeClass('this-hidden');
                        }
                    })
                } else {
                    sysS.each(function (i, dom) {
                        if ($(dom).hasClass('system-menu-state')) {
                            $(dom).removeClass('system-menu-state');
                            $(self.cont).find('.system-cont-box[signind="' + $(dom).attr('signind') + '"]').css({ "height": 0 }).addClass('this-hidden');
                        }
                    })
                }
            } else {
                var this_ = $(this);
                if (this_.hasClass('system-menu-state')) {
                    $(self.cont).find('.system-cont-box[signind="' + this_.attr('signind') + '"]').css({ "height": 0 }).addClass('this-hidden');
                    this_.removeClass('system-menu-state');
                } else {
                    $(self.cont).find('.system-cont-box[signind="' + this_.attr('signind') + '"]').css({ "height": "" }).removeClass('this-hidden');
                    this_.addClass('system-menu-state');
                }
            }
            sysS.each(function (i, dom) {
                if ($(dom).hasClass('system-menu-state')) {
                    showChild.push(false);
                    signStatas[$(dom).attr('signind')] = false;
                } else {
                    signStatas[$(dom).attr('signind')] = true;
                }
            })
            self.division();
            if (typeof config.all != "boolean" && showChild.length == sysS.length) {
                $('.system-all-select').addClass('btn-whether-check').removeClass('btn-whether-uncheck');
            } else if (typeof config.all != "boolean") {
                $('.system-all-select').removeClass('btn-whether-check').addClass('btn-whether-uncheck');
            }
            self.tool.saveLocalCache({ strKey: location.pathname.replace(/[^\w]/g, '') + 'systemMenuState', strValue: JSON.stringify(signStatas) });//缓存选择的系统
        },
        showSelectViwe: function () {
            var Html = '', partitid = '';//show-viwe-list
            var lists = $(self.labelNav).find('.label-nav-cont .clicked-nav'), mid = '', titleInfo;
            var column = Math.ceil(lists.length / 10);
            if ($('#showSelectViweviwe').length <= 0) {
                $(window.document).find('.content-container').append('<div class="show-viwe-list" id="show-viwe-list"><div class="viwe-visual-area"><div class="viwe-list-cont"></div></div></div>');
                $(window.document).find('.content-container').append('<div class="showSelectViweviwe" id="showSelectViweviwe"></div>');//遮罩层
                for (var col = 0; col < column; col++) {
                    Html += '<div class="show-viwe-list-col">';
                    for (var i = 10 * col; i<(lists.length>(10 * (col + 1)) ? (10 * (col + 1)) : lists.length); i++){
                        mid = (lists.eq(i).attr('mid') != 'mywroke' ? lists.eq(i).attr('mid') : 'mywroke');
                        titleInfo = (mid != 'mywroke' ? myFun.getTitleText({ 'mid': mid }) : {
                            sysTit: lists.eq(i).find('.label-nav-text').text(),
                            childT: lists.eq(i).find('.label-nav-text').text(),
                            chin: lists.eq(i).find('.label-nav-text').text()
                        });
                        partitid = myFun.getTitleText({ 'mid': lists.eq(i).attr('partitid') });
                        //Html += '<div class="viwe-list-single" mid="'+mid+'">';
                        Html += '<div class="viwe-list-single" signend="' + lists.eq(i).attr('signend') + '">';
                        Html += '<div class="viwe-list-default">';
                        Html += '<div class="viwe-list-title">' + (titleInfo ? titleInfo.chin : lists.eq(i).find('.label-nav-text').html()) + '</div>';
                        Html += '<div class="viwe-list-mintitle">' + (titleInfo ? titleInfo.sysTit : partitid ? partitid.sysTit : '') + '</div>';
                        Html += '<div class="viwe-list-img"></div>';
                        Html += '</div>';
                        Html += '<div class="viwe-list-active">';
                        Html += '<div class="viwe-title-active">' + (titleInfo ? titleInfo.chin : lists.eq(i).find('.label-nav-text').html()) + '</div>';
                        Html += '<div class="viwe-title-cont">'
                            + '<div class="viwe-system-title">' + (titleInfo ? titleInfo.sysTit : partitid ? partitid.sysTit : '') + '</div>'
                            + '<div class="viwe-child-title">' + (titleInfo ? titleInfo.childT : partitid ? partitid.childT + ' > ' + lists.eq(i).find('.label-nav-text').html() : '') + '</div>'
                            + '</div>';
                        Html += (lists.eq(i).attr('signend') == "mywroke" ? '' : '<div class="viwe-list-close"></div>');
                        Html += '</div>';
                        Html += '</div>';
                    }
                    Html += '</div>';
                }
                $('#show-viwe-list .viwe-list-cont').append(Html);
                self.tool.bindWindSize();
            }
        },
        getTitleText: function (titles) {//返回菜单层级关系
            var titleInfo;
            for (var i = 0; i < self.searchChinesList.length; i++) {
                if (titles.mid == self.searchChinesList[i].mid) {
                    titleInfo = self.searchChinesList[i];
                }
            }
            return (typeof titleInfo == "object" ? titleInfo : false);
        },
        changeTopWidth: function () {
            if ($(window).width() > 900) {
                if ($(self.labelNav).find('.clicked-nav').length > 5 || self.getAllWidth('.label-nav-cont') > $(self.labelNav).width()) {//显示视图列表
                    $('.container-top .show-select-viwe').css({ 'display': 'inline-block' });
                } else {
                    $('.container-top .show-select-viwe').css({ 'display': 'none' });
                    $(self.labelNav).css({ 'width': self.getWidth($(self.labelNav), 'heandNavCtrl') - 7 });//头部导航标签宽度控制
                }
            }
        },
        switchLabel: function (e) {
            var e = e || window.event;
            var ev = e.target ? e.target : e.srcElement;
            if ($(ev).hasClass('viwe-list-close')) {
                if ($('.clicked-nav[signend="' + $(this).attr('signend') + '"]').find('.label-close').length > 0) {
                    var thisParent = $('.clicked-nav[signend="' + $(this).attr('signend') + '"]');
                    myFun.closeIframHaed({ c: thisParent.find('.label-close'), t: thisParent });
                    $(this).remove();
                    self.tool.bindWindSize();
                }
            } else {
                var this_ = $('.clicked-nav[signend="' + $(this).attr('signend') + '"]');
                this_.click();
                myFun.switchIframHaed({ t: this_, p: this_.parent() });
            }
            myFun.changeTopWidth();
            self.labelDisplacement();
        },
        closeIfram: function (e) {//关闭
            var e = e || window.event;
            var this_ = $(this),
                ev = e.target ? e.target : e.srcElement;
            var thisParent = this_.parents('.clicked-nav');
            if (this_.hasClass(ev.className) || this_.find('i').hasClass(ev.className)) {//位移
                myFun.closeIframHaed({ c: this_, t: thisParent });
            }
            myFun.changeTopWidth();
        },
        switchIfram: function (e) {//切换
            var e = e || window.event,
                this_ = $(this);
            var ev = e.target ? e.target : e.srcElement,//e.button的值：0左键，1中间中间滚轮按下，2右键！(IE9 以下：1左键，2右键，4中间滚轮按下)
                thisParent = this_.parent();

            $('.heand-user-info .user-info-ctrl').removeClass('user-info-active');
            $('.background-switch').hide();
            $('.auxiliary-dropdown').hide();
            if (e.button == 2) {
                // 阻止默认右键菜单
                this_[0].oncontextmenu = function (eve) { return false }
                $(self.labelNav).find('.clicked-nav').removeClass('clickRight clicked-nav-right');
                this_.addClass('clickRight');
                this_.hasClass('label-nav-active') ? null : this_.addClass('clicked-nav-right');
                self.TextMenus.myMenu(this_);
            } else {
                if (this_.hasClass('clicked-nav')) {
                    myFun.switchIframHaed({ t: this_, p: thisParent });
                }
                if (this_.hasClass('nav-move')) {//启动拖拽
                    self.DownDragNavMove({ t: this_, e: e });
                }
            }
        },
        closeIframHaed: function (cl) {
            for (var i = cl.t.index(); i < cl.c.parents('.heandNavCtrl').find('.clicked-nav').length; i++) {
                cl.c.parents('.heandNavCtrl').find('.clicked-nav').eq(i).css({
                    'left': cl.c.parents('.heandNavCtrl').find('.clicked-nav').eq(i).position().left - cl.t.outerWidth()
                })
            }
            if (cl.t.hasClass('label-nav-active')) {//活动状态移动以及显示iframe
                cl.c.parents('.heandNavCtrl').find('.clicked-nav').length >= (cl.t.index() + 2) ?
                    (cl.c.parents('.heandNavCtrl').find('.clicked-nav').eq(cl.t.index() + 1).addClass('label-nav-active'),
                        self.tool.showIframe({ sign: cl.c.parents('.heandNavCtrl').find('.clicked-nav').eq(cl.t.index() + 1).attr("signend"), swit: false })) :
                    (cl.t.index() >= 1 ? (cl.c.parents('.heandNavCtrl').find('.clicked-nav').eq(cl.t.index() - 1).addClass('label-nav-active'),
                        self.tool.showIframe({ sign: cl.c.parents('.heandNavCtrl').find('.clicked-nav').eq(cl.t.index() - 1).attr("signend"), swit: false })) : null);
            }
            $(self.iframes).find('div[signend="' + cl.t.attr('signend') + '"]').remove();//移除iframe
            cl.t.remove();//移除标签
            self.labelDisplacement();//头部导航标签自动适应
            cl.t.attr('signend') == 'mywroke' ? self.defualtPageClose = true : null;//阻止sign:"",noShow时候再次打开工作台
            myFun.switchIframHaed({ t: $('.clicked-nav.label-nav-active'), p: $('.clicked-nav.label-nav-active').parents('.clicked-nav') });
        },
        switchIframHaed: function (sw) {
            var mid = $('.nav-trre-click[signend="' + sw.t.attr('signend') + '"]').attr('mid') || sw.t.attr('signend');
            sw.p.find('.label-nav-active').removeClass('label-nav-active');
            sw.t.addClass('label-nav-active').removeClass('clicked-nav-hover');
            self.tool.judgeCollection({ id: mid, ifarm: false });
            //切换时选中被遮挡一半的则全部显示出来
            self.labelDisplacement();
            self.tool.showIframe({ sign: sw.t.attr('signend'), swit: false });
        },
        horizontalVersion: function (e) {
            self.tool.removeLocalCache({ strKey: location.pathname.replace(/[^\w]/g, '') + 'defaulCrossScreen' });//删除横屏设置
            if ($(this).hasClass('switch-open')) {//竖
                $('.content-container').removeClass('cross-screen-style');
                $(this).removeClass('switch-open').addClass('switch-close');
                $('.content-container .container-top .heand-right').append($('.content-container .heand-user-info'));
                if ($('.container-left').hasClass('container-left-sm')) {
                    $('#conterNavTrre .nav-tiem-text').css({ 'width': 0 });
                }
                self.TrreInitialization({ scroll: false });
                self.tool.saveLocalCache({ strKey: location.pathname.replace(/[^\w]/g, '') + 'defaulCrossScreen', strValue: 'false' });//保存横屏设置
            } else {//横
                $('.content-container').addClass('cross-screen-style');
                $(this).removeClass('switch-close').addClass('switch-open');
                $('#conterNavTrre .frist-cont-parent').css({ 'height': '', 'min-height': '', 'width': '' });
                $('#conterNavTrre .frist-cont-parent').css({ 'height': '', 'min-height': '', 'width': '' });
                $('.tiem-search-tiem .search-ctrl').css({ 'right': '' });
                $('#conterNavTrre .system-nav-text').css({ 'width': '' });
                $('.content-container .container-left').append($('.content-container .heand-user-info'));
                $('#conterNavTrre .nav-tiem-text').css({ 'width': '' });
                $('#conterNavTrre .frist-cont-parent>.scroll-container>.scroll-cont-move').css({ 'margin-top': 0 });
                $('#conterNavTrre .frist-cont-parent>.scroll-container>.scroll-sbar-box .scroll-sbar-move').css({ 'top': 0 });
                self.TrreInitializationCross();
                self.tool.saveLocalCache({ strKey: location.pathname.replace(/[^\w]/g, '') + 'defaulCrossScreen', strValue: 'true' });//保存横屏设置
            }
            $(self.labelNav + ' .label-nav-cont .clicked-nav').each(function (i, dom) { $(dom).css({ 'left': self.tool.getThisLeft({ dom: $(dom) }) }) });
            $(window).resize();
        },
        viweListSwitch: function (e) {
            var allN = $('#show-viwe-list .viwe-list-spot '),
                inde = $('#show-viwe-list .viwe-spot-active').index(),
                viweW = $('#show-viwe-list').width(),
                contW = $('#show-viwe-list .viwe-list-cont').outerWidth();
            if ($(this).hasClass('viwe-list-spot')) {
                if ($(this).index() > inde) { next($(this).index() - inde) }
                if ($(this).index() < inde) { prev(inde - $(this).index()) }
            } else {
                if ($(this).hasClass('viwe-list-prev') && inde > 0) { prev(1) }
                if ($(this).hasClass('viwe-list-next') && inde < allN.length - 1) { next(1) }
            }
            function prev(ind) {
                if (inde - ind == 0) {
                    $('#show-viwe-list .viwe-list-cont').css({ 'margin-left': 0 });
                } else {
                    $('#show-viwe-list .viwe-list-cont').css({ 'margin-left': -viweW * (inde - ind) });
                }
                allN.eq(inde).removeClass('viwe-spot-active');
                allN.eq(inde - ind).addClass('viwe-spot-active');
                $('#show-viwe-list .viwe-list-next').removeClass('viwe-list-nobtn');
                if (inde - ind == 0) {
                    //					$('#show-viwe-list .viwe-list-prev').hide();
                    $('#show-viwe-list .viwe-list-prev').addClass('viwe-list-nobtn');
                }
            }
            function next(ind) {
                if (inde + ind == allN.length - 1) {
                    $('#show-viwe-list .viwe-list-cont').css({ 'margin-left': -contW + viweW });
                } else {
                    $('#show-viwe-list .viwe-list-cont').css({ 'margin-left': -viweW * (inde + ind) });
                }
                allN.eq(inde).removeClass('viwe-spot-active');
                allN.eq(inde + ind).addClass('viwe-spot-active');
                $('#show-viwe-list .viwe-list-prev').removeClass('viwe-list-nobtn');
                if (inde + ind == allN.length - 1) {
                    $('#show-viwe-list .viwe-list-next').addClass('viwe-list-nobtn');
                }
            }
        },
        leftNavRightMenu: function (e) {
            var e = e || window.event, this_ = $(this);
            this_[0].oncontextmenu = function (eve) { return false }
            if (e.button == 2 && this_.attr('hrefl') && this_.attr('hrefl').length > 0) {
                var html = '',
                    signend = this_.attr('mid'),
                    cancel = $('.bottom-nav-btn[mid="' + this_.attr('mid') + '"]').length > 0 ? true : false;
                html += '<div class="click-right-menu click-right-bottom" id="clickRightMenu" signend="' + signend + '">';
                for (var i = 0; i < self.rightMenu.length; i++) {
                    if (self.rightMenu[i].list) {
                        if (!cancel && self.rightMenu[i].sign.indexOf('default') < 0) {
                            getHtml({ mt: self.rightMenu[i], icon: true });
                        }
                        if (cancel) {
                            if (self.rightMenu[i].sign == "openother") {
                                getHtml({ mt: self.rightMenu[i], icon: true });
                            }
                            if (self.rightMenu[i].sign == "collection") {
                                getHtml({ mt: self.rightMenu[i], icon: false, Text: '取消收藏', cancel: true });
                            }
                            if (self.rightMenu[i].sign == "defaultopen") {
                                getHtml({
                                    mt: self.rightMenu[i],
                                    icon: (($('.bottom-nav-btn[mid="' + this_.attr('mid') + '"]').attr('opendefualt') == 'true' ||
                                        $('.bottom-nav-btn[mid="' + this_.attr('mid') + '"]').attr('homedefualt') == 'true') ? true : false)
                                });
                            }
                            if (self.rightMenu[i].sign == "defaulthome") {
                                getHtml({
                                    mt: self.rightMenu[i],
                                    icon: ($('.bottom-nav-btn[mid="' + this_.attr('mid') + '"]').attr('homedefualt') == 'true' ? true : false)
                                });
                            }
                        }
                    }
                }
                function getHtml(infoS) {
                    html += '<div class= "right-menu-list" rightclick="' + infoS.mt.sign + '" ' + (infoS.cancel ? 'cancel="' + infoS.cancel + '"' : '') + '>';
                    html += '<span class="right-menu-ico"><i class="' + (infoS.icon ? infoS.mt.icon : '') + '"></i></span>';
                    html += '<span class="right-menu-text">' + (infoS.Text ? infoS.Text : infoS.mt.Text) + '</span>';
                    html += '</div>';
                }
                html += '<div class="right-click-position click-right-top"></div>';
                html += '</div>';
                $('#clickRightMenu').length > 0 ? $('#clickRightMenu').remove() : null;
                this_.append(html);

                $('.click-right-menu').css({
                    "top": this_.offset().top + this_.outerHeight() - 1,
                    "left": $(this).hasClass('trre-text-info') ? this_.offset().left + 35 : this_.offset().left
                });

                $('.click-right-menu').find('.right-menu-list').one('mousedown', function () {
                    self.swicthRightMenu({ tc: $(this), men: this_ });
                });
                if (self.tool.offsetTopHeight($('#clickRightMenu'))[0] > $(window).height() - 10) {
                    if ($('#clickRightMenu .right-click-position').hasClass('click-right-top')) {
                        $('#clickRightMenu .right-click-position').removeClass('click-right-top').addClass('click-right-bot');
                        $('#clickRightMenu').css({ 'top': $('#clickRightMenu').offset().top - $('#clickRightMenu').outerHeight() - $(this).outerHeight() })
                    }
                }
            }
        },
        showMyBottomList: function (e) {
            if ($(this).parent().find('.bottom-more-list').is(':hidden')) {
                $(this).parent().find('.bottom-more-list').show();
                $(this).find('.fa').removeClass('fa-sort-down').addClass('fa-sort-up');
            } else {
                $(this).parent().find('.bottom-more-list').hide();
                $(this).find('.fa').removeClass('fa-sort-up').addClass('fa-sort-down');
            }
        },
        hideMyBottomList: function (e) {
            if ($('#clickRightMenu').length <= 0) {
                $(this).hide();
                $('#bottomNavContShouC .bottom-more-btn').find('.fa').removeClass('fa-sort-up').addClass('fa-sort-down');
            }
        },
        herfUrl: function (e) {
            var this_ = $(this);
            if (this_.attr('rel')) {//页面跳转
                if (this_.attr('bank') == 'true') { window.open(this_.attr('rel')) }
                if (this_.attr('bank') == 'false') { window.location = this_.attr('rel') }
                if (this_.attr('label') == 'true') { self.tool.addTab({ id: this_.attr('id'), title: this_.attr('tit'), href: this_.attr('rel') }, false) }//加标签
                if (this_.attr('alert') == 'true') {//弹窗
                    if (this_.attr('prompt') && this_.attr('prompt') == 'true') {
                        layer.confirm(this_.find('.prompt-texts').text(), { btn: ['确定'] }, function (index) { self.tool.ifrmaeWindow({ t: this_ }); layer.close(index) });
                    } else {
                        self.tool.ifrmaeWindow({ t: this_ });
                    }
                }
            } else {//未知功能
                tab.uiMessage({ type: 'info', text: '暂时无该功能，敬请期待...' });
            }
        },
        systemAllSelect: function () {
            if ($(this).hasClass('btn-whether-uncheck')) {
                $(this).removeClass('btn-whether-uncheck').addClass('btn-whether-check');
                myFun.sysSlect({ all: true });
            } else {
                $(this).removeClass('btn-whether-check').addClass('btn-whether-uncheck');
                myFun.sysSlect({ all: false });
            }
        }
    };
    $(window.document).on('click', '.container-top .heand-btn-nav', myFun.showLeftMenu);
    $(window.document).on('click', self.sysMenu, myFun.showSystem);
    $(window.document).on('click', '.system-name-nav', myFun.showMenuChild);//点击块收起子菜单
    //$(window.document).on('click','.remove-system',myFun.showMenu); //点击图标收起子菜单
    $(window.document).on('mouseover', '.system-name-nav', myFun.showMenuName);
    $(window.document).on('mouseleave', '.system-name-nav', myFun.removeMenuName);
    $(window.document).on('mouseover', '.clicked-nav', myFun.topNavMouseover);
    $(window.document).on('mouseleave', '.clicked-nav', myFun.topNavMouseleave); //
    $(window.document).on('click', '.menu-all-open', myFun.openAllMenu); // 全部展开
    $(window.document).on('click', '.menu-all-stop', myFun.stopAllMenu);  //全部收起
    $(window.document).on('click', '#collectionNav .bottom-nav-more', myFun.bottomCollection);//收起底部收藏菜单
    $(window.document).on('keydown', myFun.keydown);//按方向键选择
    $(window.document).on('click', '.history-list-active', myFun.searchDroDown);  //点击sign:"",noShow下拉列表菜单
    $(window.document).on('mouseenter', self.cont + ' .frist-cont-parent', myFun.insetMenu); //移进菜单导航时候
    $(window.document).on('mouseover', '.local-cache-history .history-list-row', myFun.keydownBtn);
    $(window.document).on('mouseenter', self.cont + ' .frist-cont-list', myFun.InToMnenu);//移进一级
    $(window.document).on('mouseleave', self.cont + ' .frist-cont-list', myFun.OutToMnenu);//移出一级
    $(window.document).on('click', '.system-menu-list', myFun.sysSlect);//系统选中
    $(window.document).on('click', '.auxiliary-dropdown .right-menu-list', myFun.herfUrl);//下拉链接
    $(window.document).on('click', '.show-select-viwe', myFun.showSelectViwe);//
    $(window.document).on('click', '#show-viwe-list .viwe-list-single', myFun.switchLabel);//
    $(window.document).on('click', '.clicked-nav .label-close', myFun.closeIfram);//关闭
    $(window.document).on('mousedown', '.clicked-nav', myFun.switchIfram);//切换
    $(window.document).on('click', '.background-switch .switch-open-close', myFun.horizontalVersion);//切换横竖版
    $(window.document).on('click', '.system-all-select', myFun.systemAllSelect);//系统菜单选择
    $(window.document).on('click', '#show-viwe-list .viwe-list-switch', myFun.viweListSwitch);//点击轮播
    $(window.document).on('click', '#show-viwe-list .viwe-list-spot', myFun.viweListSwitch);//点击轮播
    $(window.document).on('mousedown', self.cont + ' .nav-trre-click', myFun.leftNavRightMenu);//左侧菜单单击右键时候右键菜单  sec-cont-parent
    $(window.document).on('click', '#bottomNavContShouC .bottom-more-btn', myFun.showMyBottomList)//显示超出显示区域的菜单
    $(window.document).on('mouseleave', '#bottomNavContShouC .bottom-more-list', myFun.hideMyBottomList)//显示超出显示区域的菜单
    //showSelectViweviwe
    $(window.document).on('mouseover', self.cont, function (e) {
        for (var i = 0; i < self.intRemove.length; i++) { $(self.intRemove[i]).length > 0 ? $(self.intRemove[i]).remove() : null }
    });
    $(window.document).on('mouseleave', '.local-cache-history', function (e) { $(this).hide() });
    $(window.document).on('mouseenter', '.container-left', function (e) {
        self.tool.serollFunC($('.frist-cont-parent'));//滚动条高度配置
    });
    $(window.document).on('mouseleave', '.container-left', function (e) {
        $('.frist-cont-parent').find('.scroll-sbar-box').eq($('.frist-cont-parent').find('.scroll-sbar-box').length - 1).hide();//隐藏滚动条
    });
    $(window.document).on('mouseleave', '.background-switch', function () {
        $(this).hide();
        $('.heand-user-info').find('.user-info-ctrl').removeClass('user-info-active');
    });
}
DropDownMenuTree.prototype.nuselectTxt = function (selectArry) {//阻止选中文本 
    for (var i = 0; i < selectArry.length; i++) {
        $(selectArry[i]).addClass("nuselect-text");
        $(selectArry[i]).attr("onselectstart", "return false");//兼容IE9以下
    }
}
//系统分割线
DropDownMenuTree.prototype.division = function (selectArry) {
    var self = this;
    var myval = {
        system: $(self.cont).find('.system-cont-box')
    };
    myval.system.css({ 'box-shadow': '' });
    for (var i = 0; i < myval.system.length; i++) {
        if (myval.system.eq(i).height() > 0) {
            myval.system.eq(i).css({ 'box-shadow': 'none' });
            return false;
        }
    }
}
DropDownMenuTree.prototype.boxHideArr = function () {//点击页面，隐藏或者删除event
    var self = this;
    $(window.document).on('click', function (e) {//隐藏
        var e = e ? e : window.event;
        var ev = e.target ? e.target : e.srcElement;
        var myFunct = {
            haed: function (info) {

                if (self.hideArr[info.ind].removeDom && self.hideArr[info.ind].removeDom.length > 0) {
                    if (self.hideArr[info.ind].noAction && self.hideArr[info.ind].noAction.length > 0) {
                        if (dis(self.hideArr[info.ind].noAction)) {
                            for (var j = 0; j < self.hideArr[info.ind].removeDom.length; j++) {
                                $('.' + self.hideArr[info.ind].removeDom[j]).length > 0 ? $('.' + self.hideArr[info.ind].removeDom[j]).remove() : null;
                            }
                            if (self.hideArr[info.ind].hid) {
                                for (var j = 0; j < self.hideArr[info.ind].hid.length; j++) {
                                    $('.' + self.hideArr[info.ind].hid[j]).hide();
                                    $(self.cont).removeClass('container-filter-blur');
                                    $('.content-container .container-left .content-logo-text').removeClass('container-filter-blur');
                                    $('.content-container .container-right').removeClass('container-filter-blur');
                                }
                            }
                        }

                    } else {
                        for (var j = 0; j < self.hideArr[info.ind].removeDom.length; j++) {
                            if ($(ev).parents('.' + self.hideArr[info.ind].noAction[info.ind]).length <= 0) {
                                $('.' + self.hideArr[info.ind].removeDom[j]).length > 0 ? $('.' + self.hideArr[info.ind].removeDom[j]).remove() : null;
                            }
                        }
                        if (self.hideArr[info.ind].hid) {
                            for (var j = 0; j < self.hideArr[info.ind].hid.length; j++) {
                                $('.' + self.hideArr[info.ind].hid[j]).hide();
                            }
                        }
                    }
                } else {
                    if (self.hideArr[info.ind].noAction && self.hideArr[info.ind].noAction.length > 0) {
                        if (dis(self.hideArr[info.ind].noAction)) {
                            $(window.document).find('.' + self.hideArr[info.ind].show).hide();
                        }
                    } else {
                        $(window.document).find('.' + self.hideArr[info.ind].show).hide();
                    }
                }
                function dis(dom) {
                    var d = 0;
                    for (var k = 0; k < dom.length; k++) {
                        if (!$(ev).hasClass(dom[k]) && $(ev).parents('.' + dom[k]).length <= 0) {
                            d++;
                        }
                    }
                    if (d > 0 && d == dom.length) {
                        return true;
                    } else {
                        return false;
                    }
                }
                if (self.hideArr[info.ind].removeClass && self.hideArr[info.ind].removeClass.length > 0) {
                    for (var j = 0; j < self.hideArr[info.ind].removeClass.length; j++) {
                        if (self.hideArr[info.ind].noAction && self.hideArr[info.ind].noAction.length > 0) {
                            for (var k = 0; k < self.hideArr[info.ind].noAction.length; k++) {
                                if (!$(ev).hasClass(self.hideArr[info.ind].noAction[k]) && $(ev).parents('.' + self.hideArr[info.ind].noAction[k]).length <= 0) {
                                    $('.' + self.hideArr[info.ind].removeClass[j]).removeClass(self.hideArr[info.ind].removeClass[j]);
                                }
                            }
                        } else {
                            $('.' + self.hideArr[info.ind].removeClass[j]).removeClass(self.hideArr[info.ind].removeClass[j]);
                        }
                    }
                }
            }
        }
        for (var i in self.hideArr) {
            if (!$(ev).hasClass(self.hideArr[i].trig)) {
                if (!$(ev).parents('.' + self.hideArr[i].trig).hasClass(self.hideArr[i].trig)) {
                    if (self.hideArr[i].tN) {
                        if (!$(ev).hasClass(self.hideArr[i].show)) {
                            if (!$(ev).parents('.' + self.hideArr[i].show).hasClass(self.hideArr[i].show)) {
                                myFunct.haed({ ind: i });
                            }
                        }
                    } else {
                        myFunct.haed({ ind: i });
                    }
                }
            }
        }
    });
    $(window.document).on('click', function (e) {//删除
        var e = e ? e : window.event;
        var ev = e.target ? e.target : e.srcElement;
        for (var i in self.removeArr) {
            if (!$(ev).hasClass(self.removeArr[i].trig)) {
                if (!$(ev).parents('.' + self.removeArr[i].trig).hasClass(self.removeArr[i].trig)) {
                    if (self.removeArr[i].tN) {
                        if (!$(ev).hasClass(self.removeArr[i].show)) {
                            if (!$(ev).parents('.' + self.removeArr[i].show).hasClass(self.removeArr[i].show)) {
                                $(window.document).find('.' + self.removeArr[i].show).remove();
                            }
                        }
                    } else {
                        $(window.document).find('.' + self.removeArr[i].show).remove();
                    }
                }
            }
        }
    });
}
DropDownMenuTree.prototype.LeftTrreSearch = function (sear) {//菜单搜索
    var self = this,
        searchValF = '';
    zTree_Menu = self.tool.getNodesByParam(sear.Content);
    var Handle = {
        showCollection: function (data) {
            var Chtml = '', locaHtml = '',
                MenuList = data.Content ? data.Content.MenuList ? data.Content.MenuList : '' : '',
                locaData = self.tool.getLocalCache({ strKey: location.pathname.replace(/[^\w]/g, '') + 'collectionOrderJSON' }),//获取缓存数据
                openA = self.tool.getLocalCache({ strKey: location.pathname.replace(/[^\w]/g, '') + 'defaultOpnePage' }),
                homeP = self.tool.getLocalCache({ strKey: location.pathname.replace(/[^\w]/g, '') + 'defaultHomePage' }),
                openJ = '', newOpen = [], homeJ = '', homeMid = '', defualtOpen = [], forClickIndex = -1, defualtOpenO = false;
            locaData = (locaData.indexOf('[{') >= 0 ? JSON.parse(locaData) : []);
            openA = (openA.indexOf('[{') >= 0 ? JSON.parse(openA) : []);
            homeP = (homeP.indexOf('{') >= 0 ? JSON.parse(homeP) : []);
            Chtml += '<div class="nav-collection-cont">';
            Chtml += '<div class="bottom-nav-box">';
            Chtml += '<div class="bottom-nav-btn collection-btn">';
            Chtml += '<i class="fa fa-plus-square-o"></i>';
            Chtml += '<span class="collert-text">收藏此页</span>';
            Chtml += '</div>';
            Chtml += '</div>';
            if (MenuList.length > 0) {
                for (var i = 0; i < locaData.length; i++) {
                    if (self.tool.contain({ str: locaData[i].MenuKey, arr: MenuList }) && $('.nav-trre-click[mid="' + locaData[i].MenuKey + '"]').length > 0) {
                        self.tool.judge({ jsons: openA, str: locaData[i].MenuKey }) ? openJ = 'opendefualt="true"' : openJ = 'opendefualt="false"';
                        locaData[i].MenuKey == homeP.mid ? homeJ = 'homedefualt="true"' : homeJ = 'homedefualt="false"';
                        Chtml += '<div class="bottom-nav-box">';
                        Chtml += '<div class="bottom-nav-btn" mid="' + locaData[i].MenuKey + '" ' + openJ + ' ' + homeJ + '>';
                        Chtml += '<span class="bottom-nav-text">' + locaData[i].MenuName + '</span>';
                        Chtml += '</div>';
                        Chtml += '</div>';
                        if (self.tool.judge({ jsons: openA, str: locaData[i].MenuKey }) || locaData[i].MenuKey == homeP.mid) {//打开默认页
                            defualtOpen.push(locaData[i].MenuKey);
                        }
                        locaData[i].MenuKey == homeP.mid ? homeMid = locaData[i].MenuKey : null;
                    }
                }
                for (var i = 0; i < MenuList.length; i++) {
                    if ($('.nav-trre-click[mid="' + MenuList[i].MenuKey + '"]').length > 0) {
                        if (!self.tool.contain({ str: MenuList[i].MenuKey, arr: locaData })) {
                            self.tool.judge({ jsons: openA, str: MenuList[i].MenuKey }) ? openJ = 'opendefualt="true"' : openJ = 'opendefualt="false"';
                            MenuList[i].MenuKey == homeP.mid ? homeJ = 'homedefualt="true"' : homeJ = 'homedefualt="false"';
                            Chtml += '<div class="bottom-nav-box">';
                            Chtml += '<div class="bottom-nav-btn" mid="' + MenuList[i].MenuKey + '" ' + openJ + ' ' + homeJ + '>';
                            Chtml += '<span class="bottom-nav-text">' + MenuList[i].MenuName + '</span>';
                            Chtml += '</div>';
                            Chtml += '</div>';
                            if (self.tool.judge({ jsons: openA, str: MenuList[i].MenuKey }) || MenuList[i].MenuKey == homeP.mid) {//打开默认页
                                defualtOpen.push(MenuList[i].MenuKey)
                            }
                            MenuList[i].MenuKey == homeP.mid ? homeMid = MenuList[i].MenuKey : null;
                        }
                    } else {
                        self.tool.cancelCollection({ mid: MenuList[i].MenuKey, title: MenuList[i].MenuName, lop: (i + 1) == MenuList.length ? true : false });
                    }
                    if (self.tool.judge({ jsons: openA, str: MenuList[i].MenuKey })) {
                        newOpen.push({ mid: MenuList[i].MenuKey, title: MenuList[i].MenuName });
                    }
                }
                self.tool.saveLocalCache({ strKey: location.pathname.replace(/[^\w]/g, '') + 'defaultOpnePage', strValue: JSON.stringify(newOpen) });//清除没有收藏的缓存
            }
            Chtml += '</div>';
            Chtml += '<div class="bottom-more-btn">';
            Chtml += '<i class="fa fa-sort-down"></i>';
            Chtml += '</div>';
            Chtml += '<div class="bottom-more-list"></div>';
            $('#bottomNavContShouC').children().remove();
            $('#bottomNavContShouC').append(Chtml);
            $('#bottomNavContShouC .nav-collection-cont').css({ 'width': self.getWidth($('#bottomNavContShouC'), 'bottom-nav-cont') - 35, 'height': '100%' });
            self.BeyondWrapMove.wrapHandle();
            self.tool.judgeCollection({ id: $(self.labelNav).find('.label-nav-active').attr('mid'), ifarm: false });
            var forTimeClick = setInterval(function () {
                if (defualtOpen.length > 0 && forClickIndex < defualtOpen.length) {
                    forClickIndex++;
                    $('.nav-trre-click[mid="' + defualtOpen[forClickIndex] + '"]').find('.reture-nav-text').click();
                    defualtOpen[forClickIndex] == homeP.mid ? defualtOpenO = true : null;
                    if (homeMid == homeP.mid && defualtOpenO) {//默认首页
                        $('.nav-trre-click[mid="' + homeMid + '"]').find('.reture-nav-text').click();
                    }
                } else {
                    if (window.location.search.length > 0 && window.location.search.indexOf('menuKey') >= 0) {
                        $('.nav-trre-click[mid="' + window.location.search.split("?menuKey=")[1] + '"]').find('.reture-nav-text').click();
                    }
                    clearInterval(forTimeClick);
                }
            }, 300);
        },
        localCache: function (data) {
            var html = '', tnumber = 0;
            self.tnumbers = -1;

            $('.local-cache-history .history-cont-list').children().remove();
            //$('.local-cache-history .show-more-history').remove();
            var arr = (data.val == '' ? self.tool.getLocalCache({ strKey: location.pathname.replace(/[^\w]/g, '') + 'searchLocalCache' })
                : self.tool.getSearchListData({ val: data.val, data: data.data }));
            if (typeof arr === "string") {
                arr = (arr.indexOf("{") >= 0 ? JSON.parse(arr) : []);
            }
            for (var i = 0; i < arr.length; i++) {
                if ((data.val == '' ? $('.nav-trre-click[mid="' + arr[i].mid + '"]').length > 0 : true) && !self.signStata[arr[i].sysId]) {
                    self.tnumbers++;
                    if (data.allShow) {
                        html += "<div class='history-list-row' tit='" + arr[i].tit + "' mid='" + arr[i].mid + "'>" + arr[i].title + "</div>";
                    } else {
                        self.tnumbers < self.showSearchNum ? html += "<div class='history-list-row' tit='" + arr[i].tit + "' mid='" + arr[i].mid + "'>" + arr[i].title + "</div>" : tnumber++;
                    }
                }
            }
            //console.log(self.tnumbers);
            $('.local-cache-history .history-cont-list').append(html);
            if (tnumber > 0 || data.allShow) {
                $('.local-cache-history .history-cont-list').css({ "height": self.showSearchNum * 24, "max-height": "" });
                $('.local-cache-history .show-more-history').length > 0 ? null : $('.local-cache-history').append('<div class="show-more-history"></div>');
                $('.local-cache-history .show-more-history').text("点击继续查看剩下的" + tnumber + "条记录");
                tnumber > 0 ? $('.local-cache-history .show-more-history').show() : $('.local-cache-history .show-more-history').hide();
            } else {
                $('.local-cache-history .show-more-history').hide();
                $('.local-cache-history .history-cont-list').css({ "max-height": self.showSearchNum * 24, "height": "" });
            }
            html.length > 0 ? $('.local-cache-history').show() : $('.local-cache-history').hide();
        },
        showMoreSearch: function () {
            Handle.localCache({ val: $('#leftTrreMenuSearch').val(), data: sear, allShow: true });
            if (self.tnumbers < 100) {
                $('.local-cache-history .history-cont-list').css({
                    "height": $('.local-cache-history .history-cont-list').outerHeight() + $(this).outerHeight(),
                    "max-height": "",
                    'overflow': 'hidden',
                });
                self.thrsF.clearView({ viewClass: self.cont + ' .history-cont-list', outerMove: false });//调用滚动条
            } else {
                $('.local-cache-history .history-cont-list').css({
                    "height": $('.local-cache-history .history-cont-list').outerHeight() + $(this).outerHeight(),
                    "max-height": "",
                    'overflow-y': 'auto',
                });
            }
            $('#leftTrreMenuSearch').focus();
        }
    };
    self.MenuArrySeparate({ data: sear });//转为拼音
    self.creaTree({ data: sear });//创建菜单
    //创建收藏菜单
    $.ajax({
        url: self.hostName + '/Account/GetCollectionList',
        dataType: 'jsonp',
        jsonp: 'callback',
        data: { email: $('#userEmail').text() },
        success: function (data) {
            Handle.showCollection(data);
            typeof self.comp == "function" ? self.comp() : null; //菜单加载完成后执行
        },
        error: function (err) {
            console.log('收藏列表获取错误');
            self.tool.uiMessage({ type: 'error', text: '无法获取到收藏列表！' });
            typeof self.comp == "function" ? self.comp() : null; //菜单加载完成后执行
            Handle.showCollection();
        }
    })

    $(window.document).on('click', '.container-left-sm .tiem-search-tiem', function () {//收起时候
        if (!$('.content-container').hasClass('cross-screen-style')) {
            $('#leftTrreMenuSearch').val() != "" ? $(self.cont).find('.search-ctrl').addClass('fa-close search-ctrl-active').removeClass('fa-search') : null;
            $('#leftTrreMenuSearch').css({ 'width': 180, 'margin-left': 10, 'visibility': 'visible' });
            $(this).find('.search-ctrl').css({ 'right': -(180 - 50) })
            setTimeout(function () {
                $('#leftTrreMenuSearch').focus();
                $(self.cont).find('.search-ctrl').addClass('ColorCtrl');
            }, 100)
        }
    })
    $('#leftTrreMenuSearch').blur(function () {//失去焦点
        var this_ = $(this);
        if (!this_.is(':focus') && !$('.content-container').hasClass('cross-screen-style')) {
            this_.css({ 'width': '', 'margin-left': '' });
            if ($(self.cont).parents('.container-left-sm').length > 0) {
                $(self.cont).find('.tiem-search-tiem').css({ 'margin-left': 0, 'margin-right': 0 });
                $(self.cont).find('.search-ctrl').css({ 'right': $(self.cont).find('.tiem-search-tiem').width() / 2 - $(self.cont).find('.search-ctrl').outerWidth() / 2 });
                $(self.cont).find('.search-ctrl').removeClass('ColorCtrl');
                this_.css({ 'visibility': '' }),
                    $(self.cont).find('.search-ctrl').removeClass('fa-close').addClass('fa-search');
            } else {
                $(self.cont).find('.search-ctrl').css({ 'right': '' });
            }
        }
    })
    $('#leftTrreMenuSearch').click(function () {//得到焦点
        var this_ = $(this);
        var searchVal = this_.val().replace(/(^\s*)|(\s*$)/g, '').toLowerCase();
        if ($('.local-cache-history').is(':hidden')) {
            Handle.localCache({ val: searchVal });
        } else {
            $('.local-cache-history').hide();
        }
    })
    $('#leftTrreMenuSearch').unbind('input propertychange');
    $('#leftTrreMenuSearch').bind('input propertychange', function () {//输入框改变
        var this_ = $(this);
        var searchVal = this_.val().replace(/(^\s*)|(\s*$)/g, '').toLowerCase();
        //searchVal = searchVal.replace(/[\ |\~|\·|\`|\!|\！|\@|\#|\$|\￥|\%|\^|\……|\&|\*|\(|\)|\（|\）|\-|\_|\+|\=|\||\\|\[|\]|\【|\】|\{|\}|\;|\；|\:|\"|\'|\,|\，|\、|\<|\.|\。|\？|\>|\/|\?]/g, "");//去掉标点符号
        searchVal != "" ? ($('.tiem-search-tiem').find('.search-ctrl').hasClass('search-ctrl-active') ? null :
            ($('.tiem-search-tiem').find('.search-ctrl').removeClass('fa-search').addClass('fa-close search-ctrl-active'), clearVal())) :
            $('.tiem-search-tiem').find('.search-ctrl').removeClass('fa-close search-ctrl-active').addClass('fa-search');

        function clearVal() {
            this_.css({ 'width': this_.outerWidth() });
            $('.tiem-search-tiem').find('.search-ctrl').on('click', function (e) {
                //console.log($(this).hasClass('search-ctrl-active'));
                if ($(this).hasClass('search-ctrl-active')) {
                    $(this).unbind('click');
                    this_.val("");
                    $('.tiem-search-tiem').find('.search-ctrl').removeClass('fa-close search-ctrl-active').addClass('fa-search');
                    this_.focus();
                    searchValF = '';
                    self.creaTree({ data: sear });//创建菜单
                    Handle.localCache({ val: '' });
                }
            })
        }
        if (searchValF != searchVal) {
            searchValF = searchVal;
            self.creaTree({ data: sear, searVal: searchVal });//创建菜单
        }
        Handle.localCache({ val: searchVal, data: sear });
    })
    $(window.document).on('click', '.show-more-history', Handle.showMoreSearch);//点击把搜索的数据全部显示在下拉列表中
    $('.frist-cont-parent').find('.scroll-sbar-box').eq($('.frist-cont-parent').find('.scroll-sbar-box').length - 1).hide()//隐藏滚动条
    //阻止选中文本,参数为数组 
    self.nuselectTxt([".container-top", ".iframe-container", ".scroll-cont-move", ".nav-tiem-text", ".background-switch", ".content-logo", ".content-bottom", ".bottom-more-list"]);

}
//返回搜索匹配的字符串
DropDownMenuTree.prototype.getThisText = function (textinfo) {
    var self = this,
        thisT = '';
    var toolFun = {
        seclectText: function (navDate) {
            thisT = self.pinyin.AgainTreatment({ chinas: textinfo.texts, pinyin: navDate, vals: textinfo.val, getHtml: true, Division: 'label', className: 'select-text-yellow' });
        }
    };
    if (typeof textinfo.val != "undefined" && textinfo.val != "") {
        //搜索匹配
        switch (textinfo.level) {
            case "sys": toolFun.seclectText(self.sysData[textinfo.index[0]]); break;
            case "fri": toolFun.seclectText(self.friData[textinfo.index[0]][textinfo.index[1]]); break;
            case "sec": toolFun.seclectText(self.secData[textinfo.index[0]][textinfo.index[1]][textinfo.index[2]]); break;
            case "thr": toolFun.seclectText(self.thrData[textinfo.index[0]][textinfo.index[1]][textinfo.index[2]][textinfo.index[3]]); break;
            default: thisT = textinfo.texts; break;
        }
    } else {
        thisT = textinfo.texts;
    }
    return thisT;
}
DropDownMenuTree.prototype.MenuArrySeparate = function (info) {//将菜单数组转为拼音数组
    var self = this,
        data = info.data;//sysData
    var fir, sec, thr, tho;
    self.sysData = [];
    self.friData = [];
    self.secData = [];
    self.thrData = [];
    for (var i = 0; i < data.length; i++) {
        fri = self.pinyin.ConvertPinyin({ chinas: data[i].Name, arr: true, first: false, firstCase: false });
        self.sysData.push(fri);
        if (data[i].href) {
            self.searchChinesList.push({
                tit: data[i].Name,
                title: '',
                chin: data[i].Name,
                sysTit: data[i].Name,
                childT: data[i].Name,
                mid: data[i].id,
                sysId: data[i].id,
                pinyinArr: fri,
                pinyin: fri
            });
        }
        self.friData.push([]);
        self.secData.push([]);
        self.thrData.push([]);
        self.showMun[data[i].id] = true;  //初始化时候，隐藏全部一级菜单
        if (data[i].children != "" && data[i].children) {
            for (var f = 0; f < data[i].children.length; f++) {
                sec = self.pinyin.ConvertPinyin({ chinas: data[i].children[f].Name, arr: true, first: false, firstCase: false });
                self.friData[i].push(sec);
                if (data[i].children[f].href) {
                    self.searchChinesList.push({
                        tit: data[i].Name + ' > ' + data[i].children[f].Name,
                        title: data[i].Name + ' > ',
                        chin: data[i].children[f].Name,
                        sysTit: data[i].Name,
                        childT: data[i].children[f].Name,
                        mid: data[i].children[f].id,
                        sysId: data[i].id,
                        pinyinArr: self.tool.arrayMerge({ arr: [fri, sec], str: '>', interval: ' ' }),
                        pinyin: sec
                    });
                }
                self.secData[i].push([]);
                self.thrData[i].push([]);
                if (data[i].children[f].children != "" && data[i].children[f].children) {
                    for (var s = 0; s < data[i].children[f].children.length; s++) {
                        thr = self.pinyin.ConvertPinyin({ chinas: data[i].children[f].children[s].Name, arr: true, first: false, firstCase: false });
                        self.secData[i][f].push(thr);
                        if (data[i].children[f].children[s].href) {
                            self.searchChinesList.push({
                                tit: data[i].Name + ' > ' + data[i].children[f].Name + ' > ' + data[i].children[f].children[s].Name,
                                title: data[i].Name + ' > ' + data[i].children[f].Name + ' > ',
                                chin: data[i].children[f].children[s].Name,
                                sysTit: data[i].Name,
                                childT: data[i].children[f].Name + ' > ' + data[i].children[f].children[s].Name,
                                mid: data[i].children[f].children[s].id,
                                sysId: data[i].id,
                                pinyinArr: self.tool.arrayMerge({ arr: [fri, sec, thr], str: '>', interval: ' ' }),
                                pinyin: thr
                            });
                        }
                        self.thrData[i][f].push([]);
                        if (data[i].children[f].children[s].children != "" && data[i].children[f].children[s].children) {
                            for (var t = 0; t < data[i].children[f].children[s].children.length; t++) {
                                tho = self.pinyin.ConvertPinyin({ chinas: data[i].children[f].children[s].children[t].Name, arr: true, first: false, firstCase: false });
                                self.thrData[i][f][s].push(tho);
                                if (data[i].children[f].children[s].children[t].href) {
                                    self.searchChinesList.push({
                                        tit: data[i].Name + ' > ' + data[i].children[f].Name + ' > ' + data[i].children[f].children[s].Name + ' > ' + data[i].children[f].children[s].children[t].Name,
                                        title: data[i].Name + ' > ' + data[i].children[f].Name + ' > ' + data[i].children[f].children[s].Name + ' > ',
                                        chin: data[i].children[f].children[s].children[t].Name,
                                        sysTit: data[i].Name,
                                        childT: data[i].children[f].Name + ' > ' + data[i].children[f].children[s].Name + ' > ' + data[i].children[f].children[s].children[t].Name,
                                        mid: data[i].children[f].children[s].children[t].id,
                                        sysId: data[i].id,
                                        pinyinArr: self.tool.arrayMerge({ arr: [fri, sec, thr, tho], str: '>', interval: ' ' }),
                                        pinyin: tho
                                    });
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}
DropDownMenuTree.prototype.DownDragNavMove = function (mov) {//拖拽头部导航标签
    var self = this;
    var Var = {
        left: mov.t.position().left,
        FrontL: $('.cont-heand.heand-left').outerWidth(),
        maxL: $(self.labelNav).width(),
        x: mov.e.clientX,
        y: mov.e.clientY,
        tparh: $(self.labelNav).height(),
        nav: $(self.labelNav + ' .label-nav-cont'),
        child: $(self.labelNav + ' .label-nav-cont').find('.clicked-nav'),
        Url: $('.nav-trre-click[mid="' + mov.t.attr('mid') + '"]').attr('hrefl'),
        Html: '<div class="staeMoveSign-move" id="staeMoveSign" style="height:' + mov.t.outerHeight() + 'px;line-height:' + mov.t.outerHeight() + 'px;">'
        + mov.t.find('.label-nav-text').text() + '</div>',
        otherOpen: false,
        sorLeft: 0,
        moveLeft: 0,
        index: 0
    };
    var myFun = {
        clickFun: function () {
            if ($('#staeMoveSign').length > 0) {
                $('#staeMoveSign').remove();
            }
            if (mov.t.hasClass('mousemove-moveing-sta')) {
                mov.t.removeClass('mousemove-moveing-sta');
            }
            if ($('#showSelectViweviwe').length > 0) {
                $('#showSelectViweviwe').remove();
            }
            if ($('#show-viwe-list').length > 0) {
                $('#show-viwe-list').remove();
            }
        },
        moveFun: function (e) {
            var e = e || window.event;
            var ev = e.target ? e.target : e.srcElement,
                mx = e.clientX,
                my = e.clientY;
            Var.sorLeft = $(self.labelNav + ' .label-nav-cont').position().left;
            if (typeof Var.x === "number" && mx - Var.x != 0 || my - Var.y != 0) {
                mov.t.hasClass('mousemove-moveing-sta') ? null : mov.t.addClass('mousemove-moveing-sta');
                Var.moveLeft = (Var.left + Var.sorLeft + (mx - Var.x) + Var.FrontL) > Var.FrontL ? ((Var.left + Var.sorLeft + (mx - Var.x) + Var.FrontL) < (Var.maxL - Var.FrontL) ?
                    (Var.left + Var.sorLeft + (mx - Var.x) + Var.FrontL) : Var.maxL - Var.FrontL) : Var.FrontL
                $('#staeMoveSign').length <= 0 && $('.container-top').append(Var.Html);
                $('#mousemoveMoveingStaingd').length <= 0 && $(window.document).find('body ' + self.iframes).append('<div id="mousemoveMoveingStaingd"></div>');
                $('#staeMoveSign').css({
                    'margin-top': (my - Var.y) > 0 ? (my - Var.y) : 0,
                    'margin-left': Var.moveLeft
                });
                if (my - Var.y > Var.tparh + 1) {
                    Var.otherOpen = true;
                    $('.top-nav-inset').removeClass('top-nav-inset');
                    $('#mousemoveMoveingStaingd').css({ 'background': 'rgba(255,255,255,0.9)' });
                    $('#mousemoveMoveingStaingd').find('.this-colose-window').length <= 0 && $('#mousemoveMoveingStaingd').append('<div class="this-colose-window nuselect-text" onselectstart="return false"></div>');
                    $('#mousemoveMoveingStaingd').find('.this-colose-window').text('放开拖动标签即关闭该标签对应窗口！');
                    $('#mousemoveMoveingStaingd').find('.this-colose-window').css({
                        'top': 80,
                        'left': ($(self.iframes).width() - $('#mousemoveMoveingStaingd').find('.this-colose-window').outerWidth()) / 2,
                        'opacity': 1
                    })
                } else {
                    Var.otherOpen = false;
                    $('#mousemoveMoveingStaingd').find('.this-colose-window').remove();
                    $('#mousemoveMoveingStaingd').css({ 'background': '' });
                    //$('#mousemoveMoveingStaingd').css({'background':''});
                    Var.index = self.tool.getChildIndex({ lef: Var.moveLeft - Var.sorLeft - mov.t.outerWidth() / 2, dom: mov.t.parent() });
                    $('.top-nav-inset').removeClass('top-nav-inset');
                    if (Var.index != mov.t.index() && Var.index != mov.t.index() + 1) {
                        if (!Var.index && Var.index != 0 && mov.t.index() != mov.t.parent().children().length - 1) {
                            mov.t.parent().children().eq(mov.t.parent().children().length - 1).addClass('top-nav-inset');
                        } else {
                            if (Var.index == 0) {
                                mov.t.parent().children().eq(0).addClass('top-nav-inset');
                            } else if (Var.index != mov.t.parent().children().length) {
                                mov.t.parent().children().eq(Var.index - 1).addClass('top-nav-inset');
                            }
                        }
                    }
                }
            }
        },
        mouseUp: function (e) {
            var e = e || window.event,
                par = mov.t.parent(),
                mx = e.clientX;
            $(window.document).unbind('mousemove', myFun.moveFun);
            //if(typeof Var.x==="number"&&mx-Var.x!=0) {
            var ind = $('.top-nav-inset').index() + 1;
            mov.t.parent().append('<div class="top-move-state"></div>');
            if (mov.t.hasClass('mousemove-moveing-sta')) {
                if (Var.otherOpen) {
                    //window.open(Var.Url);//新窗口打开
                    mov.t.find('.label-close').click();
                }
                mov.t.removeClass('mousemove-moveing-sta');
                $('#staeMoveSign').length > 0 ? $('#staeMoveSign').remove() : null;
                $('#mousemoveMoveingStaingd').length > 0 ? $('#mousemoveMoveingStaingd').remove() : null;
            }
            if (ind > 0) {
                mov.t.insertBefore(mov.t.parent().children().eq(ind));
            }
            $('.top-nav-inset').removeClass('top-nav-inset');
            $('.top-move-state').remove();
            for (var i = 0; i < par.children().length; i++) {
                par.children().eq(i).css({
                    'left': self.tool.getThisLeft({ dom: par.children().eq(i) })
                });
            }
            //}
        }
    };
    myFun.clickFun();
    $(window.document).on('mousemove', myFun.moveFun);//绑定移动
    $(window.document).one('mouseup', myFun.mouseUp);//鼠标抬起
}
DropDownMenuTree.prototype.switchBGFun = function () {//换肤
    var self = this;
    var myFun = {
        showAuxiliary: function (e) { //用户 信息下拉
            if (!$(this).find('.auxiliary-dropdown').is(':visible')) {
                $('.auxiliary-dropdown').hide();
                $('.auxiliary-dropdown').find('.auxiliary-dropdown-triangle').remove();
                $('.user-info-ctrl').removeClass('user-info-active');
                if ($(this).find('.auxiliary-dropdown').children().length > 0 && !$(this).hasClass('skin-peeler')) {
                    $(this).find('.auxiliary-dropdown').show();
                    $(this).addClass('user-info-active');
                }
                $(this).find('.auxiliary-dropdown').append('<div class="auxiliary-dropdown-triangle"></div>');
                var wl = $(this).offset().left + $(this).find('.auxiliary-dropdown').outerWidth() - $(window).width();
                //auxiliary-dropdown user-info-active
                if (wl > 2) {
                    $(this).find('.auxiliary-dropdown').css({ 'left': -(wl + 2) });
                    $(this).find('.auxiliary-dropdown-triangle').css({ 'left': wl + $(this).outerWidth() / 2 - 4 });
                } else {
                    $(this).find('.auxiliary-dropdown').css({ 'left': '' });
                    if ($(this).find('.auxiliary-dropdown').outerWidth() > $(this).outerWidth()) {
                        $(this).find('.auxiliary-dropdown-triangle').css({ 'left': $(this).outerWidth() / 2 - 4 });
                    } else {
                        $(this).find('.auxiliary-dropdown-triangle').css({ 'left': $(this).find('.auxiliary-dropdown').outerWidth() / 2 - 4 });
                    }
                }
                $(this).find('.auxiliary-dropdown').css({ 'top': $(this).offset().top + $(this).outerHeight() + 1 })
            } else {
                $(this).find('.auxiliary-dropdown').hide();
                $(this).removeClass('user-info-active');
                $(this).find('.auxiliary-dropdown .auxiliary-dropdown-triangle').remove();
            }
            if ($('.background-switch').is(':hidden') && $(this).hasClass('skin-peeler')) {
                $('.background-switch').show();
                $(this).addClass('user-info-active');
                var wl = ($(this).offset().left + $('.background-switch').outerWidth() > $(window).width() ? $(window).width() - $('.background-switch').outerWidth() :
                    $(this).offset().left);
                var wls = $(this).offset().left + $(this).outerWidth() / 2 - wl;
                var wt = $(this).offset().top + $(this).outerHeight() + 1;
                $('.background-switch').css({ 'left': wl - 2 });
                $('.background-style-triangle').css({ 'left': wls - 4 });
                $('.background-switch').css({ 'top': wt });
            } else if ($(this).hasClass('skin-peeler')) {
                $('.background-switch').hide();
                $(this).removeClass('user-info-active');
            }
        },
        auxiliaryEvent: function (e) {//换肤
            var Html = '',
                backArr = [
                    { backC: 'content-change-color' },
                    { backC: 'content-change-color1' },
                    { backC: 'content-change-color2' },
                    { backC: 'content-change-color3' },
                    { backC: 'content-change-color4' },
                    { backC: 'content-change-color5' },
                ];
            backStyle = [
                { backC: 'change-style' },
                { backC: 'change-style1' },
            ],
                defaultstyle = self.tool.getLocalCache({ strKey: location.pathname.replace(/[^\w]/g, '') + 'themeAndStyle' }), //获取默认主题
                defaultbg = self.tool.getLocalCache({ strKey: location.pathname.replace(/[^\w]/g, '') + 'themeAndColor' }); //获取默认背景色
            if (defaultstyle.length <= 0) { defaultstyle = 'change-style1' }
            if (defaultbg.length <= 0) { defaultbg = 'content-change-color1' }
            Html += '<div class = "background-switch">';
            //横竖选择
            if (self.colScreen) {
                Html += '<div class="background-anyway-select">';
                Html += '<div class="background-select-text"><span>水平式导航</span><span class="switch-open-close '
                    + (self.switchStypeOpen ? 'switch-open' : 'switch-close') + '"></span></div>';
                Html += '</div>';
            }
            //样式
            Html += '<div class = "background-style-select">';
            Html += '<span class = "background-select-text">样式</span>';
            for (var i = 0; i < backStyle.length; i++) {
                Html += '<div class="background-style-cont" signstyle="' + backStyle[i].backC + '">';
                Html += '<div class="background-style-left"><div class="background-style-top style-m"></div><div class="background-style-m style-m"></div></div>';
                Html += '<div class="background-style-right">';
                Html += '<div class="background-style-top"></div>'
                    + '<div class="background-style-m" >'
                    + '<span class="switch-style-ico" defaultst="' + backStyle[i].backC + '"><i class ="fa fa-check"></i></span>'
                    + '</div>';
                Html += '</div>';
                Html += '</div>';
            }
            Html += '</div>';
            //颜色
            Html += '<div class = "background-color-select">';
            Html += '<span class = "background-select-text">颜色</span>';
            for (var i = 0; i < backArr.length; i++) {
                Html += '<div class = "switch-color" signback="' + backArr[i].backC + '">';
                Html += '<span class="switch-ico" defaultbg="' + backArr[i].backC + '"><i class ="fa fa-check"></i></span>';
                Html += '</div>';
            }
            Html += '</div>';
            //引导提示
            Html += '<div class="background-anyway-select background-anyway-sign">';
            Html += '<div class="background-select-text "><span>引导提示：</span><i class="fa fa-lightbulb-o"></i></div>';
            Html += '</div>';
            //图标
            Html += '<div class = "background-style-triangle"></div>';
            Html += '</div>';
            $('.background-switch').length > 0 ? null : ($('.content-container').append(Html), myFun.swithBack(backArr), myFun.swithStyle(backStyle));
            //switchBG
            $('span[defaultbg="' + defaultbg + '"]').click();
            $('span[defaultst="' + defaultstyle + '"]').click();
        },
        swithBack: function (back) {//皮肤切换
            $(window.document).on('click', '.switch-color', function () {
                var this_ = $(this);
                function switchBGS() {
                    for (var i in back) {
                        back[i].backC != '' ? $('.content-container').removeClass(back[i].backC) : null;
                    }
                    $('.content-container').addClass(this_.attr('signback'));
                    $('.switch-color').find('.switch-ico').hide();
                    this_.find('.switch-ico').show();
                    self.tool.removeLocalCache({ strKey: location.pathname.replace(/[^\w]/g, '') + 'themeAndColor' }); //删除默认背景色
                    self.tool.saveLocalCache({ strKey: location.pathname.replace(/[^\w]/g, '') + 'themeAndColor', strValue: this_.attr('signback') }); //保存默认背景色
                }
                var links = document.createElement('link');
                links.type = "text/css";
                links.rel = "stylesheet";
                links.href = "https://content.starmile.com.cn/css/themes/"+ this_.attr('signback') + ".css";
                $(window.document).find('head link').each(function (i, dom) {
                    if ($(dom).attr('href').indexOf('content-change-color') >= 0) {
                        $(window.document).find('head')[0].appendChild(links);
                        links.onload = function () { $(dom).remove(); switchBGS(); }
                        return false;
                    } else if (i + 1 >= $(window.document).find('head link').length) {
                        $(window.document).find('head')[0].appendChild(links);
                        links.onload = function () { switchBGS() }
                        return false;
                    }
                })
            })
        },
        swithStyle: function (back) {
            $(window.document).on('click', '.background-style-cont', function () {
                for (var i in back) {
                    back[i].backC != '' ? $('.content-container').removeClass(back[i].backC) : null;
                }
                $('.content-container').addClass($(this).attr('signstyle'));
                $('.background-style-cont').find('.switch-style-ico').hide();
                $(this).find('.switch-style-ico').show();
                self.tool.removeLocalCache({ strKey: location.pathname.replace(/[^\w]/g, '') + 'themeAndStyle' }); //删除默认主题
                self.tool.saveLocalCache({ strKey: location.pathname.replace(/[^\w]/g, '') + 'themeAndStyle', strValue: $(this).attr('signstyle') }); //保存默认主题
            })
        }
    };
    myFun.auxiliaryEvent();
    $(window.document).on('click', '.user-info-ctrl', myFun.showAuxiliary);
}
DropDownMenuTree.prototype.tools = function () {//工具模块
    var self = this;
    //console.log(unescape(encodeURIComponent(JSON.stringify(localStorage))).length/(1024*1024));//容量
    var Handle = {
        localStorageTest: function () {//判断localStorage是否被禁用
            var test = 'test';
            try {
                localStorage.setItem(test, test);
                localStorage.removeItem(test);
                return true;
            } catch (e) {
                return false;
            }
        },
        saveLocalCache: function (str) {//保存到缓存
            //self.tool().saveLocalCache({strKey:"baocun",strValue:"strValue"}); //调用
            var storage = (Handle.localStorageTest() ? window.localStorage : false);
            storage ? storage.setItem(str.strKey, str.strValue) : (typeof Cookie !== 'undefined' ? Cookie.write(str.strKey, str.strValue, date) : null);
        },
        getLocalCache: function (str) {//获取缓存
            //self.tool().getLocalCache({strKey:"baocun"});//调用
            var strValue = '', storage = (Handle.localStorageTest() ? window.localStorage : false);
            storage ? (storage.getItem(str.strKey) != null ? strValue = storage.getItem(str.strKey) :
                (typeof Cookie !== 'undefined' ? strValue = Cookie.read(str.strKey) : null)) : null;
            return strValue;
        },
        removeLocalCache: function (str) {//移除缓存
            //self.tool().removeLocalCache({strKey:"baocun"});//调用
            var storage = (Handle.localStorageTest() ? window.localStorage : false);
            storage ? storage.removeItem(str.strKey) : (typeof Cookie !== 'undefined' ? Cookie.remove(str.strKey) : null);
        },
        getSearchListData: function (sear) {//返回搜索下拉列表数组
            var getSearch = [], searchList = [];
            for (var i = 0; i < self.searchChinesList.length; i++) {
                getSearch = self.pinyin.AgainTreatment({
                    chinas: self.searchChinesList[i].chin,
                    pinyin: self.searchChinesList[i].pinyin,
                    vals: sear.val,
                    getHtml: true,
                    Division: 'span',
                    className: "search-font-color"
                });
                if (getSearch.indexOf('search-font-color') >= 0) {
                    searchList.push({ title: self.searchChinesList[i].title + getSearch, mid: self.searchChinesList[i].mid, sysId: self.searchChinesList[i].sysId, tit: self.searchChinesList[i].tit });
                }
            }
            return searchList;
        },
        arrayMerge: function (merge) { //数组合并
            //self.tool().arrayMerge({arr:[arr1,arr2,...],str:'数组直接的间隔符号'});//调用
            var newArr = [];
            for (var i = 0; i < merge.arr.length; i++) {
                for (var j = 0; j < merge.arr[i].length; j++) {
                    newArr.push(merge.arr[i][j]);
                }
                if (i < (merge.arr.length - 1) && merge.str.length > 0) {
                    newArr.push(merge.interval);
                    newArr.push(merge.str);
                    newArr.push(merge.interval);//interval
                }
            }
            return newArr;
        },
        removeRepeat: function (arr) {//数组去重
            var result = [], temp = {};
            for (var i in arr) { typeof temp[arr[i]] !== "number" && (result.push(arr[i]), temp[arr[i]] = 1) }
            return result;
        },
        deleteRepeat: function (before, after) {//删除2个数组相同的元素
            var spare = [],
                bef = (before.length > after.length ? before : after),
                aft = (before.length > after.length ? after : before);
            for (var i = 0; i < bef.length; i++) { Repeat({ str: bef[i], arr: aft }) ? null : spare.push(bef[i]) }
            function Repeat(arr) {
                var ret = false;
                for (var i = 0; i < arr.arr.length; i++) { arr.str == arr.arr[i] ? ret = true : null }
                return ret
            }
            return Handle.removeRepeat(spare);
        },
        getThisLeft: function (dom) {//返回第几个的前面距离
            var l = 0, ind = dom.dom.index(), par = dom.dom.parent();
            for (var i = 0; i < ind; i++) {
                l += par.children().eq(i).outerWidth();
                if (par.children().eq(i).css('miargin-left') && par.children().eq(i).css('miargin-left').indexOf('px') >= 0) {
                    l += parseInt(par.children().eq(i).css('miargin-left'));
                }
                if (par.children().eq(i).css('miargin-right') && par.children().eq(i).css('miargin-right').indexOf('px') >= 0) {
                    l += parseInt(par.children().eq(i).css('miargin-right'));
                }
            }
            return l;
        },
        getChildIndex: function (dom) {//根据距离获取第几个
            for (var i = 0; i < dom.dom.children().length; i++) {
                if (Handle.getThisLeft({ dom: dom.dom.children().eq(i) }) > dom.lef - dom.dom.children().eq(i).outerWidth() / 2) {
                    return i;
                }
            }
        },
        bindWindSize: function () {
            var colIndex = $('#show-viwe-list .show-viwe-list-col');
            $('#show-viwe-list .viwe-list-cont').css({ 'width': $('#show-viwe-list .show-viwe-list-col').length * $('#show-viwe-list').width() });
            colIndex.each(function (ind, dom) {
                if ($(dom).outerWidth() < $('#show-viwe-list').width()) {
                    $(dom).css({ 'margin-left': ($('#show-viwe-list').width() - $(dom).outerWidth()) / 2 });
                }
                if ($(dom).outerHeight() < $('#show-viwe-list').height()) {
                    $(dom).css({ 'margin-top': ($('#show-viwe-list').height() - $(dom).outerHeight()) / 2 });
                }
            })
            if ($('.system-menu-cont').is(':hidden')) {
                $('#showSelectViweviwe').css({//遮罩层设置
                    'left': $('.content-container').hasClass('cross-screen-style') ? 0 : $('.container-left').outerWidth(),
                    'top': $('.content-container').hasClass('cross-screen-style') ? $('.container-right .container-top').outerHeight() + $('.container-left').outerHeight() : $('.container-right .container-top').outerHeight(),
                    'width': $(window).width() - ($('.content-container').hasClass('cross-screen-style') ? 0 : $('.container-left').outerWidth()),
                    'height': $(window).height() - $('.container-right .container-top').outerHeight()
                    - ($('#bottomNavContShouC').is(':hidden') ? 0 : $('#bottomNavContShouC').outerHeight()) - ($('.content-container').hasClass('cross-screen-style') ?
                        $('.container-left').outerHeight() : 0)
                });
            }
            if ($('#show-viwe-list .viwe-list-cont').outerWidth() > $('#show-viwe-list .viwe-visual-area').width()) {//启用轮播
                var Html = '<div class="viwe-list-switch viwe-list-prev viwe-list-nobtn"><i class="fa fa-angle-left"></i></div><div class="viwe-list-switch viwe-list-next"><i class="fa fa-angle-right"></i></div>',
                    pageN = Math.ceil($('#show-viwe-list .viwe-list-cont').outerWidth() / $('#show-viwe-list .viwe-visual-area').width()),
                    pHtml = '',
                    inde = 0;
                $('#show-viwe-list .viwe-list-switch').length <= 0 ? $('#show-viwe-list').append(Html) : null;
                var prevT = ($('#show-viwe-list').outerHeight() - $('#show-viwe-list .viwe-list-prev .fa').outerHeight()) / 2,
                    nextT = ($('#show-viwe-list').outerHeight() - $('#show-viwe-list .viwe-list-next .fa').outerHeight()) / 2;
                if (prevT > nextT) {
                    $('#show-viwe-list .viwe-list-prev .fa').css({ 'top': nextT });
                    $('#show-viwe-list .viwe-list-next .fa').css({ 'top': nextT });
                } else {
                    $('#show-viwe-list .viwe-list-prev .fa').css({ 'top': prevT });
                    $('#show-viwe-list .viwe-list-next .fa').css({ 'top': prevT });
                }
                inde = $('#show-viwe-list .viwe-spot-active').length > 0 ? $('#show-viwe-list .viwe-spot-active').index() : 0;
                pHtml += '<div class="viwe-list-paging">';
                for (var i = 0; i < pageN; i++) {
                    pHtml += '<div class="viwe-list-spot ' + (i <= inde ? 'viwe-spot-active' : '') + '"></div>';
                }
                pHtml += '</div>';
                if ($('#show-viwe-list .viwe-list-paging').length > 0) {
                    $('#show-viwe-list .viwe-list-paging').remove();
                    $('#show-viwe-list').append(pHtml);
                    $('#show-viwe-list .viwe-spot-active').click();
                } else {
                    $('#show-viwe-list').append(pHtml);
                }
                $('#show-viwe-list .viwe-list-paging').css({ 'margin-left': ($('#show-viwe-list').width() - $('#show-viwe-list .viwe-list-paging').outerWidth()) / 2 });
            }
            $('#show-viwe-list').css({//定位
                'top': ($(window).height() - $('#show-viwe-list').outerHeight()) / 2,
                'left': ($(window).width() + ($('.content-container').hasClass('cross-screen-style') ? 0 : $('.container-left').outerWidth()) - $('#show-viwe-list').outerWidth()) / 2
            });
        },
        contain: function (info) {
            for (var i = 0; i < info.arr.length; i++) {
                if (info.arr[i].MenuKey == info.str) {
                    return true;
                }
            }
            return false;
        },
        serollFunC: function (clas) {//重置滚动条高度和位置
            if (clas.find('>.scroll-container>.scroll-cont-move').eq(0).outerHeight() > clas.find('>.scroll-container').eq(0).height() + 1) {
                var contH = clas.find('>.scroll-container').eq(0).height(),
                    contMH = clas.find('>.scroll-container>.scroll-cont-move').eq(0).outerHeight(),
                    contMT = clas.find('>.scroll-container>.scroll-cont-move').eq(0).position().top,
                    serBH = clas.find('>.scroll-container>.scroll-sbar-box').eq(0).height(),
                    sbarEle = clas.find('>.scroll-container>.scroll-sbar-box .scroll-sbar-move').eq(0);

                clas.find('>.scroll-container>.scroll-sbar-box').eq(0).show();
                sbarEle.css({ 'height': contH * serBH / contMH });
                (sbarEle.height() + sbarEle.position().top) > serBH ? sbarEle.css({ 'top': serBH - sbarEle.height() }) : null;
                (-contMT + contH) > contMH ? clas.find('>.scroll-container>.scroll-cont-move').eq(0).css({ 'top': contMT + (-contMT + contH) - contMH }) : null;
                clas.find('>.scroll-container>.scroll-cont-move').eq(0).position().top > 0 ? clas.find('>.scroll-container>.scroll-cont-move').eq(0).css({ 'top': 0 }) : null;
            } else {
                clas.find('>.scroll-container>.scroll-sbar-box').eq(0).hide();
                clas.find('>.scroll-container>.scroll-cont-move').eq(0).css({ 'margin-top': 0 });
            }
        },
        positionCtrl: function (pos) {//弹窗位置控制
            var thisH = pos.height(),
                winHeight = $(window).height(),
                ScrollT = $(window.document).scrollTop(),
                secTop = pos.find('.sec-cont-parent').offset().top,
                secHeight = pos.find('.sec-cont-parent').outerHeight();
            (secTop + secHeight + 1) > winHeight ? (pos.find('.sec-cont-parent').css({ 'margin-top': winHeight - (secTop + secHeight + thisH) })) : null;
        },
        Collection: function (getInfo) {//收藏菜单函数
            $.ajax({
                type: 'POST',
                url: self.hostName + '/Account/ConfirmCollection',
                data: { email: $('#userEmail').text(), "MenuKey": getInfo.mid, "MenuUrl": getInfo.Url, "MenuName": getInfo.title, "MenuContent": getInfo.muneCont },
                dataType: 'json',
                success: function (data) {
                    if (data.IsSuccess) {
                        $('#bottomNavContShouC .nav-collection-cont').append('<div class="bottom-nav-box"><div class="bottom-nav-btn" mid="' + getInfo.mid
                            + '"><span class="bottom-nav-text">' + getInfo.title + '</span></div></div>');
                        typeof getInfo.success == "function" ? getInfo.success() : null;
                        self.BeyondWrapMove.wrapHandle();
                    } else {
                        typeof getInfo.fail == "function" ? getInfo.fail(data.Message) : null;
                        console.log(data.Message);
                    }
                },
                error: function (err) {
                    console.error('收藏失败,请联系管理员!');
                    self.tool.uiMessage({ type: 'error', text: '收藏失败,请联系管理员!' });
                }
            });
        },
        cancelCollection: function (getInfo) {//取消收藏菜单函数
            $.ajax({
                type: 'POST',
                url: self.hostName + '/Account/CancelCollection',
                data: { email: $('#userEmail').text(), "MenuKey": getInfo.mid, "MenuName": getInfo.title },
                dataType: 'json',
                success: function (data) {
                    if (data.IsSuccess) {
                        $('#bottomNavContShouC').find('div[mid="' + getInfo.mid + '"]').parent().remove();
                        typeof getInfo.success == "function" ? getInfo.success() : null;
                        self.BeyondWrapMove.wrapHandle();
                    } else {
                        if (typeof getInfo.lop == "boolean") {
                            if (getInfo.lop) {
                                typeof getInfo.fail == "function" ? getInfo.fail(data.Message) : null;
                                console.log(data.Message);
                            }
                        } else {
                            typeof getInfo.fail == "function" ? getInfo.fail(data.Message) : null;
                            console.log(data.Message);
                        }
                    }
                },
                error: function (err) {
                    console.error('取消失败,请联系管理员!');
                    self.tool.uiMessage({ type: 'error', text: '取消失败,请联系管理员!' });
                }
            })
        },
        Subscribe: function (getInfo) {//订阅函数
            $.ajax({
                async: false,
                url: getInfo.Url,
                dataType: 'jsonp',
                jsonp: 'callback',
                success: function (data) {
                    getInfo.Fun();
                },
                error: function (err) { console.error('失败!') }
            })
        },
        offsetTopHeight: function (dom) {
            var num = [];
            if (dom.length > 0) {
                for (var i = 0; i < dom.length; i++) {
                    num.push(dom.offset().top + dom.outerHeight());
                }
            }
            return num;
        },
        offsetLeftWidth: function (dom) {
            var num = [];
            if (dom.length > 0) {
                for (var i = 0; i < dom.length; i++) {
                    num.push(dom.offset().left + dom.outerWidth());
                }
            }
            return num;
        },
        height: function (dom) {//返回块所占据的高度
            return (dom.length > 0 ? (dom.outerHeight() + Handle.margin({ dom: dom, dire: ['top', 'bottom'] })) : false);
        },
        width: function (dom) {//返回块所占据的宽度
            return (dom.length > 0 ? (dom.outerWidth() + Handle.margin({ dom: dom, dire: ['left', 'right'] })) : false);
        },
        margin: function (dom) {//获取外边距 Handle.margin({dom:$('类名'),dire:['left','right']});
            var numb = 0;
            if (dom.dom.length > 0 && dom.dire.length > 0) {
                for (var i = 0; i < dom.dire.length; i++) {
                    if (dom.dire == 'top') { dom.dom.css('margin-top').indexOf('px') >= 0 ? numb += parseInt(dom.dom.css('margin-top')) : null }
                    if (dom.dire == 'right') { dom.dom.css('margin-right').indexOf('px') >= 0 ? numb += parseInt(dom.dom.css('margin-right')) : null }
                    if (dom.dire == 'bottom') { dom.dom.css('margin-bottom').indexOf('px') >= 0 ? numb += parseInt(dom.dom.css('margin-bottom')) : null }
                    if (dom.dire == 'left') { dom.dom.css('margin-left').indexOf('px') >= 0 ? numb += parseInt(dom.dom.css('margin-left')) : null }
                }
            }
            return numb;
        },
        padding: function (dom) {//获取内边距  Handle.padding({dom:$('类名'),dire:['left','right']});
            var numb = 0;
            if (dom.dom.length > 0 && dom.dire.length > 0) {
                for (var i = 0; i < dom.dire.length; i++) {
                    if (dom.dire[i] == 'top') { dom.dom.css('padding-top').indexOf('px') >= 0 ? numb += parseInt(dom.dom.css('padding-top')) : null }
                    if (dom.dire[i] == 'right') { dom.dom.css('padding-right').indexOf('px') >= 0 ? numb += parseInt(dom.dom.css('padding-right')) : null }
                    if (dom.dire[i] == 'bottom') { dom.dom.css('padding-bottom').indexOf('px') >= 0 ? numb += parseInt(dom.dom.css('padding-bottom')) : null }
                    if (dom.dire[i] == 'left') { dom.dom.css('padding-left').indexOf('px') >= 0 ? numb += parseInt(dom.dom.css('padding-left')) : null }
                }
            }
            return numb;
        },
        judge: function (judge) {
            for (var i = 0; i < judge.jsons.length; i++) {
                if (judge.str == judge.jsons[i].mid) {
                    return true;
                }
            }
            return false;
        },
        isContain: function (val) {
            for (var i = 0; i < val.arr.length; i++) {
                if (val.str.indexOf(val.arr[i]) >= 0) {
                    return true;
                }
            }
            return false;
        },
        addTab: function (config, reload) {
            var newLabel = '',
                systit = typeof config.ifarm == "boolean" ? false : $('.label-nav-active.clicked-nav').find('.label-nav-text').html(),
                partitid = typeof config.ifarm == "boolean" ? false : $('.label-nav-active.clicked-nav').attr('mid'),
                close = typeof config.close == "boolean" ? config.close : true,
                navMove = config.id != 'mywroke' ? 'nav-move' : '';
            if ($(self.labelNav).find('.clicked-nav[signend="' + config.id + '"]').length <= 0) {
                newLabel += '<div class="label-nav-active clicked-nav ' + navMove + '" signend="' + config.id + '" mid="' + config.id + '"' + (systit ? ' systit="' + systit + '"' : '')
                    + (partitid ? 'partitid="' + partitid + '"' : '')
                    + ' nocont="' + (typeof config.cont == "boolean" ? config.cont : true) + '"'
                    + (typeof config.ifarm == "boolean" ? '' : ' href="' + config.href + '"')
                    + (close ? 'thiscolse="' + close + '"' : 'thiscolse="false"') + '>';//thiscolse
                newLabel += '<span class="label-nav-text">' + config.title + '</span>';
                newLabel += (close ? '<span class="label-close" title="关闭"><i class="label-close-btn"></i></span>' : '');;
                newLabel += '</div>';
                $(self.labelNav).find('.label-nav-active').removeClass('label-nav-active');
                $(self.labelNav).find('.label-nav-cont').append(newLabel);//添加标签
                $(self.labelNav).find('.label-nav-active').css({ 'left': Handle.getThisLeft({ dom: $(self.labelNav).find('.label-nav-active') }) });//位置
                if (Handle.getAllWidth('.label-nav-cont') > $(self.labelNav).width()) {//判断是否被遮挡
                    $(self.labelNav).find('.label-nav-cont').css({
                        'left': $(self.labelNav).width() - Handle.getAllWidth('.label-nav-cont')
                    });
                    $(self.labelNav).parents('.container-top').find('.move-left').addClass('move-label-active');
                }
                $(self.labelNav).parents('.container-top').find('.move-right').removeClass('move-label-active');//移除移动按钮激活状态
            } else {
                if ($(self.labelNav).find('.clicked-nav').hasClass('label-nav-active')) {
                    $(self.labelNav).find('.clicked-nav').removeClass('label-nav-active')
                }
                for (var i = 0; i < $(self.labelNav).find('.clicked-nav').length; i++) {
                    if ($(self.labelNav).find('.clicked-nav').eq(i).attr('signend') == config.id) {
                        $(self.labelNav).find('.clicked-nav').eq(i).addClass('label-nav-active');
                        Handle.showIframe({ sign: config.id, swit: false });
                    }
                }
                if ($(self.labelNav).find('.label-nav-active').length > 0) {//显示选中被遮挡的标签
                    Handle.labelDisplacement({
                        active: $(self.labelNav).find('.label-nav-active'),
                        activeParent: $(self.labelNav).find('.label-nav-cont')
                    });
                }
            }
            Handle.creaIfram({ href: config.href, id: config.id, reload: reload }); //添加（创建）ifram
            if ($(self.labelNav).find('.clicked-nav').length == 6) {
                $('.container-top .show-select-viwe').css({ 'display': 'inline-block' });
                $(self.labelNav).css({ 'width': self.getWidth($(self.labelNav), 'heandNavCtrl') - 7 });//头部导航标签宽度控制
            } else if ($(self.labelNav).find('.clicked-nav').length < 6) {
                $('.container-top .show-select-viwe').css({ 'display': 'none' });
            }
            $(self.cont).find('.scroll-cont-move').css({ 'width': '100%' });//容器宽度
            config.id != "mywroke" ? setTimeout(function () { Handle.serollFunC($('.frist-cont-parent')) }, 50) : null;//滚动条高度配置
            Handle.judgeCollection({ id: config.id, ifarm: config.ifarm });
        },
        creaIfram: function (config) {
            if ($(self.iframes + ' div[signend="' + config.id + '"]').length <= 0) {
                var iframHtml = '';
                iframHtml += '<div class="iframe-list" signend="' + config.id + '">';
                iframHtml += '<iframe id="iframe-' + config.id + '" src="' + config.href + '" name="' + config.id + '"></iframe>';
                iframHtml += '</div>';
                Handle.showIframe({ sign: config.id, swit: false });
                $(self.iframes).append(iframHtml);
                Handle.clickIframeRemove({ removes: 'iframe-' + config.id });
            }
            if (config.reload) {
                $(self.iframes + ' div[signend="' + config.id + '"]').remove();
                if ($(self.iframes + ' div[signend="' + config.id + '"]').length <= 0) {
                    var iframHtml = '';
                    iframHtml += '<div class="iframe-list" signend="' + config.id + '">';
                    iframHtml += '<iframe id="iframe-' + config.id + '" src="' + config.href + '" name="' + config.id + '"></iframe>';
                    iframHtml += '</div>';
                    Handle.showIframe({ sign: config.id, swit: false });
                    $(self.iframes).append(iframHtml);
                    Handle.clickIframeRemove({ removes: 'iframe-' + config.id });
                }
            }
        },
        judgeCollection: function (config) {
            if ($('.nav-trre-click[mid="' + config.id + '"]').length > 0) {
                $('.bottom-nav-btn.collection-btn').hasClass('collection-mywroke') ? $('.bottom-nav-btn.collection-btn').removeClass('collection-mywroke') : null;
                if (typeof config.ifarm == "boolean" && $('.bottom-nav-btn[mid="' + config.id + '"]').length > 0) {//mid=config.id
                    $('.bottom-nav-btn.collection-btn').find('i.fa').removeClass('fa-plus-square-o').addClass('fa-minus-square-o');
                    $('.bottom-nav-btn.collection-btn').find('.collert-text').text('取消收藏');
                    $('.bottom-nav-btn.collection-btn').attr({ 'cancel': 'true' });
                } else {
                    $('.bottom-nav-btn.collection-btn').find('i.fa').addClass('fa-plus-square-o').removeClass('fa-minus-square-o');
                    $('.bottom-nav-btn.collection-btn').find('.collert-text').text('收藏此页');
                    $('.bottom-nav-btn.collection-btn').attr({ 'cancel': 'false' });
                }
            } else {
                $('.bottom-nav-btn.collection-btn').hasClass('collection-mywroke') ? null : $('.bottom-nav-btn.collection-btn').addClass('collection-mywroke');
                $('.bottom-nav-btn.collection-btn').find('.collert-text').text('收藏此页');
            }
        },
        getAllWidth: function (classN) {//返回所有子元素的宽度之和
            var allwid = 0;
            for (var i = 0; i < $(classN).children().length; i++) { allwid += $(classN).children().eq(i).outerWidth() }
            return allwid;
        },
        showIframe: function (frmae) {//切换关闭时显示对应的iframe窗口
            if (!frmae.swit) {
                $(self.iframes).find('.iframe-list').removeClass('iframe-active').addClass('iframe-unactive');
                $(self.iframes).find('div[signend="' + frmae.sign + '"]').removeClass('iframe-unactive').addClass('iframe-active')
            }
        },
        labelDisplacement: function (state) {//标签移除或者宽度改变时的位移
            var labelNav = self.labelNav;
            function showMoveIco(active, activeParent) {//激活移动标签按钮函数
                activeParent.position().left >= 0 ?
                    ($(labelNav).parents('.container-top').find('.move-left').removeClass('move-label-active'),
                        (Handle.getAllWidth('.label-nav-cont') > active.parents(labelNav).width() ?
                            $(labelNav).parents('.container-top').find('.move-right').addClass('move-label-active') :
                            $(labelNav).parents('.container-top').find('.move-right').removeClass('move-label-active'))
                    ) : ($(labelNav).parents('.container-top').find('.move-left').addClass('move-label-active'),
                        (Handle.getAllWidth('.label-nav-cont') + activeParent.position().left > active.parents(labelNav).width() ?
                            $(labelNav).parents('.container-top').find('.move-right').addClass('move-label-active') :
                            $(labelNav).parents('.container-top').find('.move-right').removeClass('move-label-active'))
                    );
            }
            if (state) {
                //左边
                state.active.position().left + state.activeParent.position().left < 0 ? state.activeParent.css({ 'left': -(state.active.position().left) }) : null;
                //右边
                (state.active.position().left + state.activeParent.position().left + state.active.outerWidth() > state.active.parents(labelNav).width()) ?
                    state.activeParent.css({ 'left': -(state.active.position().left - state.active.parents(labelNav).width() + state.active.outerWidth()) }) : null;
            } else {
                if ($(labelNav).find('.label-nav-active').length > 0) {
                    var activeChildL = $(labelNav).find('.label-nav-active').position().left,//当前激活块距离左边的距离
                        activeChildW = $(labelNav).find('.label-nav-active').outerWidth(), //当前激活块宽度
                        labelW = $(labelNav).width(),//可视宽度
                        labelNavL = $(labelNav).find('.label-nav-cont').position().left,//标签容器移动的距离
                        allChildW = Handle.getAllWidth('.label-nav-cont');//所有标签的宽度之和
                    if (allChildW > labelW) {
                        if ((activeChildL + labelNavL + activeChildW) > labelW) {
                            $(labelNav).find('.label-nav-cont').css({ 'left': labelW - (activeChildL + labelNavL + activeChildW) + labelNavL });
                            $(labelNav).css({ 'min-width': $(labelNav).find('.label-nav-active').outerWidth() });
                        } else {
                            if ((activeChildL + labelNavL) < 0) {
                                $(labelNav).find('.label-nav-cont').css({ 'left': -activeChildL });
                            }
                        }
                        if (allChildW + labelNavL < labelW) {
                            $(labelNav).find('.label-nav-cont').css({ 'left': (labelW - (allChildW + labelNavL)) + labelNavL });
                            $(labelNav).css({ 'min-width': $(labelNav).find('.label-nav-active').outerWidth() });
                            $(labelNav).parents('.container-top').find('.move-right').hasClass('move-label-active') ?
                                $(labelNav).parents('.container-top').find('.move-right').removeClass('move-label-active') : null;
                        }
                    } else {
                        $(labelNav).find('.label-nav-cont').css({ 'left': 0 });
                        $(labelNav).parents('.container-top').find('.move-left').removeClass('move-label-active');
                    }
                }
            }
            $(labelNav).find('.label-nav-cont').length > 0 ? showMoveIco($(labelNav).find('.label-nav-active'), $(labelNav).find('.label-nav-cont')) : null;
        },
        clickIframeRemove: function (rem) {//点击iframe移除所有弹窗contents()removeOther
            function tryIframe(idName) {
                var test = 'test';
                try {
                    return ($(idName).contents().find('body').html().length > 0 ? true : false);
                } catch (e) {
                    return false;
                }
            }
            $(window.document).on('mousedown', function (e) {
                if (tryIframe('#' + rem.removes)) {
                    $('#' + rem.removes).contents().on('mousedown', function () {
                        for (var i = 0; i < self.removeEven.length; i++) {
                            $(self.removeEven[i].removeDom).length > 0 ? $(self.removeEven[i].removeDom).remove() : null;
                            for (var j = 0; j < self.removeEven[i].removeClass.length; j++) {
                                $('.' + self.removeEven[i].removeClass[j]).length > 0 ? $('.' + self.removeEven[i].removeClass[j]).removeClass(self.removeEven[i].removeClass[j]) : null;
                            }
                        }
                    })
                }
            });
            $(window.document).on('mousedown', function (e) {
                if (tryIframe('#' + rem.removes)) {
                    $('#' + rem.removes).contents().one('mousedown', function () {
                        for (var i = 0; i < self.hideEven.length; i++) {
                            $(self.hideEven[i].hid).hide();
                            if (self.hideEven[i].removeClass) {
                                for (var j = 0; j < self.hideEven[i].removeClass.length; j++) {
                                    $('.' + self.hideEven[i].removeClass[j]).removeClass(self.hideEven[i].removeClass[j]);
                                }
                            }
                            if (self.hideEven[i].removeDom) {
                                for (var j = 0; j < self.hideEven[i].removeDom.length; j++) {
                                    $('.' + self.hideEven[i].removeDom[j]).length > 0 ? $('.' + self.hideEven[i].removeDom[j]).remove() : null;
                                }
                            }
                        }
                    })
                }
            });
        },
        popup: function (config) {//自定义页面层弹窗
            if (self.control.layer) {
                //console.log( config.id?(id:config.id):'2' );
                self.control.layer.open({
                    type: config.type,    //弹窗类型
                    title: config.title,   //弹窗标题
                    skin: config.className, //增加样式(class名)
                    id: config.id ? config.id : 'MySelfLayuiLayerContent',  //内容ID
                    area: [config.width, config.height], //宽高
                    content: config.html,              // 自定义html
                    closeBtn: typeof config.closeBtn == "number" ? config.closeBtn : 1,
                    shade: [0.6, '#000'],
                    shadeClose: typeof config.shadeClose == "boolean" ? config.shadeClose : false,   //点击遮罩层是否关闭
                    btn: config.btn,//['按钮1','按钮2','按钮3']
                    yes: function (index, layero) { config.yes(index, layero) },//按钮【按钮1】的回调;index为序号，layero当前弹窗的对象
                    btn2: function (index, layero) { config.btn2(index, layero) },//按钮【按钮2】的回调，return false 开启该代码可禁止点击该按钮关闭
                    btn3: function (index, layero) { config.btn3(index, layero) },//按钮【按钮3】的回调
                    btn4: function (index, layero) { config.btn4(index, layero) },//按钮【按钮4】的回调
                    cancel: function () { typeof config.cancel == "function" && config.cancel() }, //右上角关闭回调return false 开启该代码可禁止点击该按钮关闭
                    success: function (index, layero) {
                        typeof config.success == "function" && config.success();//成功弹窗后的回调
                        $('.content-container').addClass('container-filter-blur');
                    },
                    end: function () {
                        typeof config.end == "function" && config.end();//弹窗关闭完成后的回调
                        $('.content-container').removeClass('container-filter-blur');
                    }
                });
            }
        },
        popupClose: function (index) { self.control.layer && self.control.layer.close(index) },//关闭popup弹窗
        redirectLogin: function (config) {//登录状态超时
            Handle.popup({
                type: 2,
                title: config ? config.title ? config.title : '登录' : '登录',
                className: 'popup-box-shadow',
                shadeClose: false,
                width: '520px',
                height: '240px',
                closeBtn: 0,
                //html:config?config.herf?config.herf:'http://127.0.0.1/tool/trre/RedirectLogin.html':'http://127.0.0.1/tool/trre/RedirectLogin.html',
                html: config ? config.herf ? config.herf : 'Home/RedirectLogin' : 'Home/RedirectLogin',
                yes: function (index, layero) { },
                cancel: function () { }
            });
        },
        ifrmaeWindow(config) {
            Handle.popup({
                type: config.t.attr('iframe') == "true" ? 2 : 1,
                className: 'popup-box-shadow',
                shadeClose: true,
                title: config.t.attr('tit') ? config.t.attr('tit') : '弹出框',
                width: '520px',
                height: '240px',
                html: config.t.attr('iframe') == "true" ? config.t.attr('rel') : config.htmls,
                yes: function (index, layero) { },
                btn2: function (index, layero) { },
                cancel: function () { }
            });
        },
        newMessage: self.control.ui ? self.control.ui.message : '',//使用文档 参考 http://192.168.1.102:801/document/2016/09/21/component/ui.message/
        uiMessage: function (config) {//信息提示
            if (self.control.ui) {
                switch (config.type) {
                    case 'loading': return self.control.ui.message.loading({ content: config.text, timeout: config.timeout }); break; //loading 类型的消息提示框默认不会自动关闭。需通过 uiMessage实例的.destroy()或.close()（二者效果一样）方法关闭。
                    case 'success': self.control.ui.message.success(config.text); break;
                    case 'error': self.control.ui.message.error(config.text); break;
                    case 'warning': self.control.ui.message.warning(config.text); break;
                    default: self.control.ui.message.info(config.text); break;
                }
            }
        },
        layer: self.control.layer ? self.control.layer : '',//使用文档 参考 http://www.layui.com/doc/modules/layer.html
        control: self.control ? self.control : '', //传过来的控件
        getNodesByParam: function (data) {
            var f = {
                getNodesByParam: function (key, value, parentNode) {
                    var jsons = [], sdata, tdata, fdata, fidata;
                    for (var i = 0; i < data.length; i++) {
                        sdata = data[i];
                        if (sdata.text == value) {
                            jsons.push({ id: sdata.id, name: sdata.text, href: sdata.href });
                        }
                        for (var s = 0; s < sdata.children.length; s++) {
                            tdata = sdata.children[s];
                            if (tdata.text == value) {
                                jsons.push({ id: tdata.id, name: tdata.text, href: tdata.href });
                            }
                            for (var t = 0; t < tdata.children.length; t++) {
                                fdata = tdata.children[t];
                                if (fdata.text == value) {
                                    jsons.push({ id: fdata.id, name: fdata.text, href: fdata.href });
                                }
                                for (var h = 0; h < fdata.children.length; h++) {
                                    fidata = fdata.children[h];
                                    if (fidata.text == value) {
                                        jsons.push({ id: fidata.id, name: fidata.text, href: fidata.href });
                                    }
                                }
                            }
                        }
                    }
                    return jsons;
                }
            }
            return f;
        },
    }
    return Handle;
}









































