<template>
    <div id="demo">
        <button @click="doClick">触发一个方法函数</button>
    </div>
</template>

<script>
import Vue from 'vue'

export default {
    name: 'orderparent',
    data() {
        return {
            msg: 'Welcome to Your Vue.js App'
        }
    }
}
ajax({ 
  type:"POST", 
  url:"ajax.php", 
  dataType:"json", 
  data:{"val1":"abc","val2":123,"val3":"456"}, 
  beforeSend:function(){ 
    //some js code 
  }, 
  success:function(msg){ 
    console.log(msg) 
  }, 
  error:function(){ 
    console.log("error") 
  } 
})
new Vue({
    el: 'demo',
    data: {
        name: 'Vue.js'
    },
    // 在 `methods` 对象中定义方法  
    methods: {
        doClick: function(event) {
            // 方法内 `this` 指向 vm  
            alert('Hello ' + this.name + '!')
            // `event` 是原生 DOM 事件  
            alert(event.target.tagName)
        }
    }
})
//vm.doClick()
</script>

<style>

</style>
