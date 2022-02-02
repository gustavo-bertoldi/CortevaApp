/**
 * First we will load all of this project's JavaScript dependencies which
 * includes Vue and other libraries. It is a great starting point when
 * building robust, powerful web applications using Vue and Laravel.
 */

require('./bootstrap');

window.Vue = require('vue').default;


import Vue from 'vue';
import store from './store/index';
import VueI18n from 'vue-i18n' // Import de vue-i18n

Vue.use(VueI18n); // Utilisation de VueI18n dans Vue

function loadLocaleMessages () {
    const locales = require.context('../lang', true, /[A-Za-z0-9-_,\s]+\.json$/i)
    const messages = {}
    locales.keys().forEach(key => {
        const matched = key.match(/([A-Za-z0-9-_]+)\./i);
        if (matched && matched.length > 1) {
            const locale = matched[1];
            messages[locale] = locales(key);
        }
    });
    return messages
}


// Création d'une instance i18n exportée pour être utilisée partout dans Vue
export const i18n = new VueI18n({
    locale: 'en',
    fallbackLocale: 'en',
    messages: loadLocaleMessages(),

});

/**
 * The following block of code may be used to automatically register your
 * Vue components. It will recursively scan this directory for the Vue
 * components and automatically register them with their "basename".
 *
 * Eg. ./components/ExampleComponent.vue -> <example-component></example-component>
 */

// const files = require.context('./', true, /\.vue$/i)
// files.keys().map(key => Vue.component(key.split('/').pop().split('.')[0], files(key).default))
Vue.component('navbar', require('./components/navbar.vue').default);

Vue.component('downtimesReport', require('./components/downtimesReport.vue').default);

Vue.component('packagingID', require('./components/packagingLineID.vue').default);

Vue.component('choiceLogin', require('./components/choiceLogin.vue').default);

Vue.component('monthlyLoadFactor', require('./components/monthlyLoadFactor.vue').default);

Vue.component('login', require('./components/login.vue').default);

Vue.component('teamInfo', require('./components/teamInfo.vue').default);

Vue.component('topSecondPage', require('./components/topSecondPage.vue').default);

Vue.component('bottomSecondPage', require('./components/bottomSecondPage.vue').default);

Vue.component('choice_planned_unplanned', require('./components/choice_planned_unplanned.vue').default);

Vue.component('unplanned_pannel1', require('./components/unplanned_pannel1.vue').default);

Vue.component('unplanned_pannel_unplanned2', require('./components/unplanned_pannel_unplanned2.vue').default);

Vue.component('navbarSaisie', require('./components/navbarSaisie.vue').default);

Vue.component('test', require('./components/test.vue').default);

Vue.component('CIP_Declaration', require('./components/CIP_Declaration.vue').default);

Vue.component('Changingformat_Declaration', require('./components/Changingformat_Declaration.vue').default);

Vue.component('ClientChanging_Declaration', require('./components/Clientchanging_Declaration.vue').default);

Vue.component('plannedDowntime_Declaration', require('./components/plannedDowntime_Declaration.vue').default);

Vue.component('endPO_Declaration', require('./components/endPO_Declaration.vue').default);

Vue.component('unplannedDowntimeDashboard', require('./components/unplannedDowntimeDashboard.vue').default);

Vue.component('productionDashboard', require('./components/productionDashboard.vue').default);

Vue.component('overallLineEffectivness', require('./components/overallLineEffectivness').default);

Vue.component('unplannedDowntimeShutdowns', require('./components/unplannedDowntimeShutdowns.vue').default);

Vue.component('unplannedDowntimeSpeedLosses', require('./components/unplannedDowntimeSpeedLosses.vue').default);

Vue.component('qualityLossesDashboard2', require('./components/qualityLossesDashboard2.vue').default);

/**
 *
 * Next, we will create a fresh Vue application instance and attach it to
 * the page. Then, you may begin adding components to this application
 * or customize the JavaScript scaffolding to fit your unique needs.
 */

const app = new Vue({
    i18n, // Utilisation de i18n
    el: '#app',
    store
});
