var PageScrollMouse = function(socrInfo){
	this.moveDistance = 120; //最大滚动幅度
	this.minSbarbt=35; //滑块最小长度
	if(socrInfo){
		this.bottomCall = socrInfo.bottomCall;
		this.viewH = socrInfo.viewH?socrInfo.viewH:"100%";
		this.viewW = socrInfo.viewW?socrInfo.viewW:"100%";
		this.viewClass = socrInfo.viewClass;
		this.direction = socrInfo.direction?socrInfo.direction:'vertical';
		this.outerMove = typeof socrInfo.outerMove==="boolean"?socrInfo.outerMove:true;
		this.childAll=[];
		this.clearView();
	}
}
PageScrollMouse.prototype.clearView = function(socrInfo) {
	var self = this;
	if(socrInfo){
		self.bottomCall = socrInfo.bottomCall;
		self.viewH = socrInfo.viewH?socrInfo.viewH:"100%";
		self.viewW = socrInfo.viewW?socrInfo.viewW:"100%";
		self.viewClass = socrInfo.viewClass;
		self.direction = socrInfo.direction?socrInfo.direction:'vertical';
		self.outerMove = typeof socrInfo.outerMove==="boolean"?socrInfo.outerMove:true;
	}
	var className = self.direction =='vertical'?'scroll-sbar-box scroll-vertical':'scroll-sbar-box scroll-transverse';
	
	$(self.viewClass).each(function(i,dom){
		if($(dom).children().length>0){
			self.childAll=[];
			$(dom).children().each(function(j,doms){self.childAll.push($(doms))});
			$(dom).append('<div class="scroll-container"><div class="'+className+'"><div class="scroll-sbar-move"></div></div>  <div class="scroll-cont-move"></div> </div>');
			for(var k=0;k<self.childAll.length;k++){
				$(dom).find('.scroll-cont-move').append(self.childAll[k]);
			}
		}else{
			$(dom).html('<div class="scroll-container"><div class="'+className+'"><div class="scroll-sbar-move"></div></div><div class="scroll-cont-move">'+$(dom).html()+'</div></div>');
		}
		if(self.direction=='vertical'){
			$(dom).find('.scroll-container').css({'width': self.viewW,'height': self.viewH});
		}else{
			$(dom).find('.scroll-container').css({'height': self.viewH,'width':'100%'});
			$(dom).find('.scroll-container .scroll-cont-move').css({'width': self.viewW});
		}
		self.scrollMouse({
			direction: self.direction,
			sollClass: $(dom).find('.scroll-sbar-box'), //滚动条容器
			mainClass: $(dom).find('.scroll-container'), //滚动内容外容器
			contClass: $(dom).find('.scroll-cont-move') //滚动内容容器
		});
	});
}
//滚动条事件函数
PageScrollMouse.prototype.scrollMouse=function(socrInfo) {
	var self=this;
	var obj = socrInfo.mainClass[0],
		direction = socrInfo.direction,
		sollClass = socrInfo.sollClass,
		mainClass = socrInfo.mainClass,
		contClass = socrInfo.contClass,
		ctrlSeroll = false,
		moveD=true,
		ctrlnumber=0;
	//加鼠标滚轮
	function addMouseWheel(obj, fn) {
		obj.onmousewheel = fnWheel;
		obj.addEventListener?obj.addEventListener('DOMMouseScroll', fnWheel, false):null;
		function fnWheel(e) {
			var oEvent = e || event;
			var d = oEvent.wheelDelta ? oEvent.wheelDelta > 0 : oEvent.detail < 0;
			moveD=d;
			fn(d);
			oEvent.preventDefault ? oEvent.preventDefault():null;
			if(self.outerMove){
				ctrlSeroll?null:(oEvent.stopPropagation?oEvent.stopPropagation():oEvent.cancelBubble=true);//cancelBubble兼容IE
			}else{
				oEvent.stopPropagation?oEvent.stopPropagation():oEvent.cancelBubble=true;
			}
			return false;
		}
	}
	//滑块
	function scrollBar(obj, direction) {
		var oSbar = sollClass[0];
		var oSbtn = oSbar.children[0];
		var oMain = mainClass[0];
		var oCont = contClass[0];
		self.scrollMoveH({oSbar:oSbar,oSbtn:oSbtn,oMain:oMain,oCont:oCont,dire:direction});
		$(window).resize(function(){//监听浏览器窗口大小变化
			self.scrollMoveH({oSbar:oSbar,oSbtn:oSbtn,oMain:oMain,oCont:oCont,dire:direction});
		})
		//判断滚动距离
		function setDirection(json) {
			if(direction=='vertical'){
				if(!isNaN(json.down)) {
					if(json.down <= 0) {
						json.down = 0;
					} else if(json.down > oSbar.offsetHeight - oSbtn.offsetHeight) {
						json.down = oSbar.offsetHeight - oSbtn.offsetHeight;
					}
					oSbtn.style.top = json.down + 'px';
					var scale = json.down / (oSbar.offsetHeight - oSbtn.offsetHeight);
				}
				oCont.style.marginTop = -(oCont.offsetHeight - oMain.offsetHeight) * scale + 'px';
			}else{
				if(!isNaN(json.down)) {
					if(json.down <= 0) {
						json.down = 0;
					} else if(json.down > oSbar.offsetWidth - oSbtn.offsetWidth) {
						json.down = oSbar.offsetWidth - oSbtn.offsetWidth;
					}
					oSbtn.style.left = json.down + 'px';
					var scale = json.down / (oSbar.offsetWidth - oSbtn.offsetWidth);
				}
				oCont.style.marginLeft = -(oCont.offsetWidth - oMain.offsetWidth) * scale + 'px';
			}
			if(direction=='vertical'){
				if(parseInt($(oSbar).height()-$(oSbtn).outerHeight())<=parseInt($(oSbtn).position().top)) {//滚动到底部时执行
					ctrlnumber++;
					ctrlnumber>2?[ctrlSeroll = true,ctrlnumber=3]:ctrlSeroll = false;//滚动到底部，再转动滚轮3次触发外容器滚动条
					//滚动到底部回调函数
					self.bottomCall?(typeof self.bottomCall=="function" ?(self.bottomCall(),self.scrollMoveH({oSbar:oSbar,oSbtn:oSbtn,oMain:oMain,oCont:oCont,dire:direction})):null):null;
				}else{
					if((oCont.offsetHeight - oMain.offsetHeight) * scale>=0 && moveD){
						ctrlnumber--;
						ctrlnumber < 0?[ctrlSeroll = true,ctrlnumber= -1]:ctrlSeroll = false;
					}else{
						ctrlSeroll = false;
					}
				}
			}
			if(direction=='transverse'){
				if(parseInt($(oSbar).width()-$(oSbtn).outerWidth())<=parseInt($(oSbtn).position().left)) {//滚动到底部时执行
					self.bottomCall?(typeof self.bottomCall=="function" ?(self.bottomCall(),self.scrollMoveH({oSbar:oSbar,oSbtn:oSbtn,oMain:oMain,oCont:oCont,dire:direction})):null):null;
				}else{
					if((oCont.offsetWidth - oMain.offsetWidth) * scale>=0 && moveD){
						ctrlnumber--;
						ctrlnumber < 0?[ctrlSeroll = true,ctrlnumber= -1]:ctrlSeroll = false;
					}else{
						ctrlSeroll = false;
					}
				}
			}
		}
		//滚动条拖拽
		oSbtn.onmousedown = function(e) {
			var oEvent = e || event;
			var disX = oEvent.clientX - oSbtn.offsetLeft;
			var disY = oEvent.clientY - oSbtn.offsetTop;
			function fnMove(e) {
				var oEvent = e || event;
				switch(direction) {
					case 'transverse': // 横向
						var l = oEvent.clientX - disX;
						setDirection({down: l});
						break;
					case 'vertical': // 竖向
						var t = oEvent.clientY - disY;
						setDirection({down: t});
						break;
				}
			}
			function fnUp() {
				this.onmousemove = null;
				this.onmouseup = null;
				oSbar.releaseCapture? oSbar.releaseCapture():null;
			}
			oSbar.setCapture?[oSbar.onmousemove = fnMove,oSbar.onmouseup = fnUp,oSbar.setCapture()]:[document.onmousemove = fnMove,document.onmouseup = fnUp];
			return false;
		};
		//加鼠标滚轮
		switch(direction) {
			case 'transverse': // 横向
				addMouseWheel(obj, function(d) {
					var mouseY = 0;
					var mouseTop = parseInt((oMain.offsetWidth * oSbar.offsetWidth) / (2*oCont.offsetWidth));
					var mouseMove = ((oCont.offsetWidth - oMain.offsetWidth) * mouseTop) / (oSbar.offsetWidth - oSbtn.offsetWidth);
					var contMove = oMain.offsetWidth/2;
					mouseMove>self.moveDistance?mouseTop = parseInt(((oSbar.offsetWidth - oSbtn.offsetWidth)*self.moveDistance)/(oCont.offsetWidth - oMain.offsetWidth)):mouseTop;
					mouseMove = ((oCont.offsetWidth - oMain.offsetWidth) * mouseTop) / (oSbar.offsetWidth - oSbtn.offsetWidth);
					mouseMove > contMove?mouseTop = parseInt( (contMove/oCont.offsetWidth)*oSbar.offsetWidth ):mouseTop;
					mouseTop<0?mouseTop = 0:mouseTop;  //offsetTop 
					d?mouseY = oSbtn.offsetLeft - mouseTop:mouseY = oSbtn.offsetLeft + mouseTop;
					setDirection({down: mouseY});
				});
				break;
			case 'vertical': // 竖向
			addMouseWheel(obj, function(d) {
				var mouseY = 0;
				var mouseTop = parseInt((oMain.offsetHeight * oSbar.offsetHeight) / (2*oCont.offsetHeight));
				var mouseMove = ((oCont.offsetHeight - oMain.offsetHeight) * mouseTop) / (oSbar.offsetHeight - oSbtn.offsetHeight);
				var contMove = oMain.offsetHeight/2;
				mouseMove>self.moveDistance?mouseTop = parseInt(((oSbar.offsetHeight - oSbtn.offsetHeight)*self.moveDistance)/(oCont.offsetHeight - oMain.offsetHeight)):mouseTop;
				mouseMove = ((oCont.offsetHeight - oMain.offsetHeight) * mouseTop) / (oSbar.offsetHeight - oSbtn.offsetHeight);
				mouseMove > contMove?mouseTop = parseInt( (contMove/oCont.offsetHeight)*oSbar.offsetHeight ):mouseTop;
				mouseTop<0?mouseTop = 0:mouseTop;
				d?mouseY = oSbtn.offsetTop - mouseTop:mouseY = oSbtn.offsetTop + mouseTop;
				setDirection({down: mouseY});
			})
			break;
		}
		//加点击滚动事件
		$(oSbar).on('click',function(ev){
			var ev = ev?ev:window.event;
			var evc= ev.srcElement ? ev.srcElement : ev.target;
			if(evc.className.indexOf(oSbtn.className)<0 && direction=="vertical"){
				var mouseY = 0;
				mouseY = ev.clientY - ($(oSbar).offset().top - $(window.document).scrollTop()) - oSbtn.offsetHeight/2;
				setDirection({down: mouseY});
			}
			if(evc.className.indexOf(oSbtn.className)<0 && direction=="transverse"){
				var mouseY = 0;
				mouseY = ev.clientX - ($(oSbar).offset().left - $(window.document).scrollLeft()) - oSbtn.offsetWidth/2;
				setDirection({down: mouseY});
			}
		})
	}
	scrollBar(obj, direction); //调用滑块
}
//滚动条滑块高度
PageScrollMouse.prototype.scrollMoveH = function(socrInfo) {
	var self=this;
	var oSbtn=socrInfo.oSbtn,
		oCont=socrInfo.oCont,
		oSbar=socrInfo.oSbar,
		oMain=socrInfo.oMain;
	function changHeight(){
		if(socrInfo.dire=="vertical"){
			if(oCont.offsetHeight > oMain.offsetHeight){
				var topT = oCont.offsetTop;
				oSbar.style.display='block';
				oSbtn.style.height = (oMain.offsetHeight / oCont.offsetHeight) * oSbar.offsetHeight + "px"; //设置滑块的高度
				if(oSbtn.offsetHeight < self.minSbarbt) {
					oSbtn.style.height = self.minSbarbt + "px";
				}
				if( topT + oCont.offsetHeight < oMain.offsetHeight){
					oCont.style.marginTop= (oMain.offsetHeight - oCont.offsetHeight) +"px";
				}
				if(oSbtn.offsetHeight + oSbtn.offsetTop > oSbar.offsetHeight){
					oSbtn.style.top =  (oSbar.offsetHeight - oSbtn.offsetHeight) + "px";
				}
				$(oCont).css({'width':$(oMain).width()- $(oSbar).outerWidth()});
			}else{
				oCont.style.marginTop = 0 + "px";
				oSbtn.style.top = 0 + "px";
				oSbar.style.display='none';
				$(oCont).css({'width':'100%'});
			}
		}
		if(socrInfo.dire=="transverse"){
			if(oCont.offsetWidth > oMain.offsetWidth){
				var topT = oCont.offsetTop;
				oSbar.style.display='block';
				oSbtn.style.width = (oMain.offsetWidth / oCont.offsetWidth) * oSbar.offsetWidth + "px"; //设置滑块的高度
				if(oSbtn.offsetWidth < self.minSbarbt) {
					oSbtn.style.width = self.minSbarbt + "px";
				}
				if( topT + oCont.offsetWidth < oMain.offsetWidth){
					oCont.style.marginLeft= (oMain.offsetWidth - oCont.offsetWidth) +"px";
				}
				if(oSbtn.offsetWidth + oSbtn.offsetTop > oSbar.offsetWidth){
					oSbtn.style.left =  (oSbar.offsetWidth - oSbtn.offsetWidth) + "px";
				}
				$(oCont).css({'margin-bottom':$(oSbar).outerHeight()});
			}else{
				oCont.style.marginLeft = 0 + "px";
				oSbtn.style.left = 0 + "px";
				oSbar.style.display='none';
				$(oCont).css({'height':'100%'});
			}
		}
	}
	changHeight();
}



























//
