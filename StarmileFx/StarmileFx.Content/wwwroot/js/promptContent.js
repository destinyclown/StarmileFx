var promptContentMethod = function(introJs,againTrigger) {
	var self = this;
	self.customFun = self.customFunction();
	self.vars = {
		introArr:[], saveIntro:[], getData:[], oldDate:[], saveDate:[], ideData:[],
		thisHost:(self.customFun.isContain({arr:['192.168.1.102','127.0.0.1','localhost'],str:location.hostname})?'http://192.168.2.58':''),
		introIndex:0,
	};
	self.step = self.customArr();
	//按照顺序来执行
	self.vars.introArr = [
		{"intro":self.customFun.intros({introJs:introJs,steps:self.step.stepOne.steps,before:self.step.stepOne.before,after:self.step.stepOne.after}),"Text":"intro"},
	]; 
	self.customFun.getData({//请求服务器，获取是否查看过
		success:function(data){//请求成功
			self.vars.saveIntro = data.data.indexOf('{')>=0?JSON.parse(data.data):[];
			self.vars.saveIntro = self.vars.saveIntro.Message?self.vars.saveIntro.Message.indexOf('{')>=0?JSON.parse(self.vars.saveIntro.Message):[]:[];
			if(self.vars.saveIntro.length>0){
				if(self.vars.thisHost.length>0){
					self.vars.getData = self.customFun.deleteRepeat({oldData:self.vars.introArr,newData:[]});
				}else{
					self.vars.getData = self.customFun.deleteRepeat({oldData:self.vars.introArr,newData:self.vars.saveIntro});
				}
				self.customFun.loop({arr:self.vars.getData,save:true});
			}else{
				self.customFun.loop({arr:self.vars.introArr,save:true});
			}
			$(window.document).on('click',againTrigger,function(){self.vars.introIndex=0;self.customFun.loop({arr:self.vars.introArr,save:false})});
		},
		fail:function(){//请求失败
			self.customFun.loop({arr:self.vars.introArr,save:true});
			$(window.document).on('click',againTrigger,function(){self.vars.introIndex=0;self.customFun.loop({arr:self.vars.introArr,save:false})});
		}
	});
}
promptContentMethod.prototype.customFunction=function(){
	var self = this;
	var customFun = {
		loop:function(intr){//按照顺序依次提示
			if(self.vars.introIndex<intr.arr.length){
				intr.arr[self.vars.introIndex].intro.start();
				intr.arr[self.vars.introIndex].intro.oncomplete(function() {//演示完
					customFun.exit({arr:intr.arr,save:intr.save?true:false});
					$(window.document).click();
				});
				intr.arr[self.vars.introIndex].intro.onexit(function(){//退出演示
					customFun.exit({arr:intr.arr,save:intr.save?true:false});
					$(window.document).click();
				});
			}
		},
		exit:function(intr){//退出执行
			if(intr.save){
				self.vars.oldDate = [],self.vars.saveDate=[],self.vars.ideData=[];
				self.vars.saveIntro.push({"Text":intr.arr[self.vars.introIndex].Text});
				for(var i=0;i<self.vars.saveIntro.length;i++){
					self.vars.oldDate.push(self.vars.saveIntro[i].Text);
				}
				for(var i=0;i<self.vars.introArr.length;i++){
					self.vars.ideData.push(self.vars.introArr[i].Text);
				}
				self.vars.oldDate = customFun.removeRepeat(self.vars.oldDate);//数组去重
				self.vars.oldDate = customFun.arrContain({oldD:self.vars.oldDate,newD:self.vars.ideData});//去掉数据库多余的标识
				for(var i=0;i<self.vars.oldDate.length;i++){
					self.vars.saveDate.push({"Text":self.vars.oldDate[i]});
				}
				customFun.saveData({data:self.vars.saveDate}); //保存退出的状态
			}
			if(self.vars.introIndex+1<intr.arr.length) {
				setTimeout(function(){
					self.vars.introIndex++;
					customFun.loop({arr:intr.arr,save:intr.save?true:false});
				},200);
			}
		},
		getData:function(cust){
			$.ajax({
				async:false,
			    //url:self.vars.thisHost+'/Home/GetCollectionMessage?userId=liyunquan@sellercube.cn',
			    url:self.vars.thisHost+'/Home/GetCollectionMessage',
			    success:function(data){
			    	cust.success({data:data});
			    },
			    error:function(err){
			    	cust.fail();
			    }
			})
		},
		saveData:function(datas){
			$.ajax({
				type:'POST',
				url:self.vars.thisHost+'/Home/ConfirmCollectionMessage', 
				//data:{"userId":"liyunquan@sellercube.cn","message":JSON.stringify(datas.data)},
				data:{"message":JSON.stringify(datas.data)},
				dataType:'json',
				success:function(data){
					//console.log(data);
					if(data.Success){
						//console.log('保存成功！');
					}else{
						//console.log('保存失败！');
					}
				},
				error:function(err){console.error(err)}
			});
		},
		removeRepeat:function(arr){//数组去重
			var result = [],temp = {};
			for(var i in arr){ typeof temp[arr[i]] !=="number"&&(result.push(arr[i]),temp[arr[i]] = 1) }
			return result;
		},
		arrContain:function(val){//返回2个数组都包含的元素
			var data = [];
			for(var i=0;i<val.newD.length;i++){
				if(customFun.identArr({str:val.newD[i],arr:val.oldD})){
					data.push(val.newD[i]);
				}
			}
			return data;
		},
		identArr:function(val){//判断一个数组是否字符串包含
			for(var i=0;i<val.arr.length;i++){
				if(val.arr[i] == val.str ){
					return true;
				}
			}
			return false;
		},
		deleteRepeat:function(val){//2个JSON去掉重复的
			var data = [];
			for(var i=0;i<val.oldData.length;i++){
				if(!customFun.identical({str:val.oldData[i].Text,arr:val.newData})){
					data.push({"Text":val.oldData[i].Text,"intro":val.oldData[i].intro});
				}
			}
			return data;
		},
		identical:function(val){
			for(var i=0;i<val.arr.length;i++){
				if(val.arr[i].Text == val.str ){
					return true;
				}
			}
			return false;
		},
		isContain:function(val){
			for(var i=0;i<val.arr.length;i++){
				if(val.str.indexOf(val.arr[i])>=0){
					return true;
				}
			}
			return false;
		},
		intros:function(info){
			var intro = info.introJs();
			intro.setOptions({
				nextLabel: '下一条',
				prevLabel: '上一条',
				skipLabel: '退出',
				doneLabel: '看完了',
				exitOnOverlayClick: false,
				disableInteraction: true,
				tooltipPosition: 'auto',
				steps: info.steps,
			});
			intro.onbeforechange(function(targetElement) {//某一步骤提示前回调
				typeof info.before =="function"&&info.before(targetElement);
			});
			intro.onafterchange(function(targetElement) {//某一步骤提示后回调
				typeof info.after =="function"&&info.after(targetElement);
			});
			return intro;
		}
	}
	$(window.document).on('click','.introjs-overlay',function(){//点击退出
		if($('.introjs-tooltip .introjs-button.introjs-nextbutton').hasClass('introjs-disabled')){
			$('.introjs-tooltip .introjs-button.introjs-skipbutton').click();	
		}else{
			$('.introjs-tooltip .introjs-button.introjs-nextbutton').click();	
		}
	});
	return customFun;
}
promptContentMethod.prototype.customArr=function(){//返回执行步骤以及步骤中所用的回调
	return {
		stepOne:{
			steps:[
				{element: '#contentMore',intro: '点击可查看到系统名，可选择在菜单栏可见系统下的菜单'}, 
				{element: '#leftTrreMenuSearch',intro: '在输入框输入关键字可搜索到相关的菜单，并且会把搜索过的菜单保存到浏览器本地缓存'},
				{element: '.system-name-nav',intro: '点击可展开当前子级菜单，在子级菜单上单击右键有选择菜单'}, 
				$('.menu-open-btn').is(':visible')&&{element: '.menu-open-btn',intro: '这里可以把菜单全部展开或者全部折叠'},
				$('.menu-open-btn').is(':visible')&&{element: '.menu-open-btn .menu-all-open',intro: '点击可把所有菜单子级展开'},
				$('.menu-open-btn').is(':visible')&&{element: '.menu-open-btn .menu-all-stop',intro: '点击可把所有菜单子级折叠'}, 
				$('.container-top .heand-btn-nav').is(':visible')&&{element: '.container-top .heand-btn-nav',intro: '点击左侧菜单靠左缩小或者放大'}, 
				{element: '.container-top .clicked-nav',intro: '单击右键弹出选择菜单，单击左键则进行窗口切换，按下拖动左右拖动则排放顺序，往下拖出标签栏则新窗口打开页面'},
				$('.container-top .label-close').length>0&&{element: '.container-top .label-close',intro: '关闭窗口'},
				{element: '.skin-peeler',intro: '页面样式切换（换肤功能）'},
				{element: '.background-switch',intro: '页面样式切换'},
				{element: '#bottomNavContShouC',intro: '此处为收藏的菜单列表，单击左键打开该菜单，单击右键选择菜单，按住拖动可以进行排序，顺序保存到浏览器缓存'}, 
				{element: '#bottomNavContShouC .bottom-nav-box',intro: '点击可收藏当前打开页面或者取消收藏当前打开页面'},
				{element: '#collectionNav .bottom-nav-more',intro: '点击可最小化收藏菜单'}
			],
			before:function(targetElement){
				if($(targetElement).hasClass('system-name-nav')) {
					$(targetElement).click();
				}
				if($(targetElement).hasClass('background-switch')) {
					if($(targetElement).is(':hidden')) {
						$('.skin-peeler').click();
					}
				}
			},
			after:function(targetElement){
			}
		}
	}
}





























////gfhfghkl