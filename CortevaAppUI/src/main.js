import Vue from 'vue'
import App from './App.vue'
import router from './router'
import store from './store'
import i18n from './i18n'
import { BootstrapVue, IconsPlugin } from 'bootstrap-vue'
import 'bootstrap/dist/css/bootstrap.css'
import 'bootstrap-vue/dist/bootstrap-vue.css'

Vue.config.productionTip = false

new Vue({
  router,
  store,
  i18n,
  BootstrapVue,
  IconsPlugin,
  render: h => h(App)
}).$mount('#app');

import axios from 'axios';
axios.defaults.headers.common['Authorization'] = `Bearer ${localStorage.getItem("auth-token")}`;
axios.interceptors.response.use(response => response, error => {
  if (error.response.status == 401) {
    console.log("Unauthorized");
    localStorage.removeItem('username');
    router.replace("/");
  }
});
