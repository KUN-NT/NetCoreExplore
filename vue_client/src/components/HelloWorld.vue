<template>
  <div class="hello">
    <h3>jsonp</h3>
    <p>{{userJsonp}}</p>
    <hr>
    <h3>Proxy</h3>
    <p>{{userProxy}}</p>
    <hr>
    <h3>Cors</h3>
    <p>{{userCors}}</p>
    <hr>
  </div>
</template>

<script>
import axios from 'axios';

export default {
  name: 'HelloWorld',
  data() {
    return{
      userJsonp: '',
      userProxy:'',
      userCors:''
    }
  },
  methods:{
    //1、安装vue-jsonp，并在main.js引用
    //2、只能是get请求，且接口输出方式是responseS
    getDataByJsonp(params){
      this.$jsonp('http://localhost:5000/api/Login/TestJsonpForVue',params).then(
        res=>{
          this.userJsonp=res;
        }
      )
    },
    //1、配置vue.config.js 跨域代理 注意规则
    //2、发送任意请求
    //3、只能用在开发环境
    getDataByProxy(){
      axios.post('/api/Login/TestProxyForVue').then(res=>{
        this.userProxy=res;
      })
    },
    //1、后端配置Cors代理
    //2、发送任意请求
    getDataByCors(){
      axios.put('http://localhost:5000/api/Login/TestCorsForVue').then(res=>{
        this.userCors=res;
      })
    }
  },
  mounted(){
    this.getDataByJsonp();
    this.getDataByProxy();
    this.getDataByCors();
  }
}
</script>

<style scoped>
h3 {
  margin: 40px 0 0;
}
</style>
