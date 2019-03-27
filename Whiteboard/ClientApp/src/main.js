import Vue from 'vue'
import VueSignalR from '@latelier/vue-signalr'
import VueResource from 'vue-resource'
import App from './App.vue'
import router from './router'
import store from './store'

Vue.config.productionTip = false;

Vue.use(VueSignalR, 'http://localhost:5000/ws/rooms');

Vue.use(VueResource);

new Vue({
  router,
  store,
  render: h => h(App),
  created() {
    this.$socket.start({
      log: false
    });
  }
}).$mount('#app')
