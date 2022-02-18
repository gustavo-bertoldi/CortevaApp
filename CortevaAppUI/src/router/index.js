import Vue from 'vue'
import VueRouter from 'vue-router'
import ChoiceLoginPage from '../views/ChoiceLoginPage.vue'
import TeamInfo from "@/views/UserInputs/TeamInfo";

Vue.use(VueRouter)

const routes = [
  {
    path: '/',
    name: 'ChoiceLoginPage',
    component: ChoiceLoginPage
  },
  {
    path: '/about',
    name: 'About',
    // route level code-splitting
    // this generates a separate chunk (about.[hash].js) for this route
    // which is lazy-loaded when the route is visited.
    component: () => import(/* webpackChunkName: "about" */ '../views/About.vue')
  },
  {
    path: '/login',
    name: 'Login',
    // route level code-splitting
    // this generates a separate chunk (about.[hash].js) for this route
    // which is lazy-loaded when the route is visited.
    component: () => import(/* webpackChunkName: "about" */ '../views/UserInputs/LoginPage.vue')
  },
  {
    path: '/teamInfo',
    name: 'TeamInfo',
    component : TeamInfo
  },
  {
    path: '/homePage',
    name: 'HomePage',
    // route level code-splitting
    // this generates a separate chunk (about.[hash].js) for this route
    // which is lazy-loaded when the route is visited.
    component: () => import(/* webpackChunkName: "about" */ '../views/UserInputs/HomePage.vue')
  },
  {
    path: '/eventDeclaration/:productionLine',
    name: 'IncidentDeclaration',
    // route level code-splitting
    // this generates a separate chunk (about.[hash].js) for this route
    // which is lazy-loaded when the route is visited.
    component: () => import(/* webpackChunkName: "about" */ '../views/UserInputs/Unplanned_Planned_Pannel1.vue')
  },
  {
    path: '/eventDeclaration/:productionLine/:downtimeType',
    name: 'Unplanned_Planned_Choice_Reason_Pannel2',
    // route level code-splitting
    // this generates a separate chunk (about.[hash].js) for this route
    // which is lazy-loaded when the route is visited.
    component: () => import(/* webpackChunkName: "about" */ '../views/UserInputs/Planned_Event_Declaration_Pannel2.vue')
  },
  {
    path: '/eventDeclaration/:productionLine/plannedDowntime/:reason',
    name: 'Unplanned_Pannel1',
    // route level code-splitting
    // this generates a separate chunk (about.[hash].js) for this route
    // which is lazy-loaded when the route is visited.
    component: () => import(/* webpackChunkName: "about" */ '../views/UserInputs/Planned_Event_Declaration_Pannel3.vue')
  },
  {
    path: '/eventDeclaration/:productionLine/unplannedDowntime/CIP',
    name: 'CIPDeclaration',
    // route level code-splitting
    // this generates a separate chunk (about.[hash].js) for this route
    // which is lazy-loaded when the route is visited.
    component: () => import(/* webpackChunkName: "about" */ '../views/UserInputs/CIP_Declaration_Unplanned_Panel3.vue')
  },
  {
    path: '/eventDeclaration/:productionLine/unplannedDowntime/clientChanging',
    name: 'ChangingClient',
    // route level code-splitting
    // this generates a separate chunk (about.[hash].js) for this route
    // which is lazy-loaded when the route is visited.
    component: () => import(/* webpackChunkName: "about" */ '../views/UserInputs/ClientChanging_Declaration_Unplanned_Panel3.vue')
  },
  {
    path: '/eventDeclaration/:productionLine/unplannedDowntime/formatChanging',
    name: 'ChangingFormat',
    // route level code-splitting
    // this generates a separate chunk (about.[hash].js) for this route
    // which is lazy-loaded when the route is visited.
    component: () => import(/* webpackChunkName: "about" */ '../views/UserInputs/FormatChanging_Declaration_Unplanned_Panel3.vue')
  },
  {
    path: '/eventDeclaration/:productionLine/unplannedDowntime/unplannedDowntime',
    name: 'UnplannedDowntimeDeclaration',
    // route level code-splitting
    // this generates a separate chunk (about.[hash].js) for this route
    // which is lazy-loaded when the route is visited.
    component: () => import(/* webpackChunkName: "about" */ '../views/UserInputs/UnplannedDowntime_Declaration_Unplanned_Panel3.vue')
  },
  {
    path: '/endPO/:productionLine/endPO',
    name: 'EndPODeclaration',
    // route level code-splitting
    // this generates a separate chunk (about.[hash].js) for this route
    // which is lazy-loaded when the route is visited.
    component: () => import(/* webpackChunkName: "about" */ '../views/UserInputs/End_PO_Declaration.vue')
  },
  {
    path: '/Dashboard/downtimesReport',
    name: 'DowntimesReportDashboard',
    // route level code-splitting
    // this generates a separate chunk (about.[hash].js) for this route
    // which is lazy-loaded when the route is visited.
    component: () => import(/* webpackChunkName: "about" */ '../views/Dashboards/DowntimesDashboard.vue')
  },
  {
    path: '/Dashboard/packagingLineID',
    name: 'PackagingLineIDDashboard',
    // route level code-splitting
    // this generates a separate chunk (about.[hash].js) for this route
    // which is lazy-loaded when the route is visited.
    component: () => import(/* webpackChunkName: "about" */ '../views/Dashboards/PackagingLineID_Dashboard.vue')
  },
  {
    path: '/Dashboard/qualityLossesDashboard',
    name: 'QualityLossesDashboard',
    // route level code-splitting
    // this generates a separate chunk (about.[hash].js) for this route
    // which is lazy-loaded when the route is visited.
    component: () => import(/* webpackChunkName: "about" */ '../views/Dashboards/QualityLosses_Dashboard.vue')
  },
  {
    path: '/Dashboard/productionDashboard',
    name: 'ProductionReportDashboard',
    // route level code-splitting
    // this generates a separate chunk (about.[hash].js) for this route
    // which is lazy-loaded when the route is visited.
    component: () => import(/* webpackChunkName: "about" */ '../views/Dashboards/ProductionReport_Dashboard.vue')
  },
  {
    path: '/Dashboard/overallLineEffectivness',
    name: 'OLEDashboard',
    // route level code-splitting
    // this generates a separate chunk (about.[hash].js) for this route
    // which is lazy-loaded when the route is visited.
    component: () => import(/* webpackChunkName: "about" */ '../views/Dashboards/OLE_Dashboard.vue')
  },
  {
    path: '/Dashboard/unplannedDowntimeDashboard',
    name: 'UnplannedDowntimesDashboard',
    // route level code-splitting
    // this generates a separate chunk (about.[hash].js) for this route
    // which is lazy-loaded when the route is visited.
    component: () => import(/* webpackChunkName: "about" */ '../views/Dashboards/UnplannedDowtimes_Dashboard.vue')
  },
  {
    path: '/Dashboard/unplannedDowntimeShutdowns',
    name: 'UnplannedDowntimeShutdownsDashboard',
    // route level code-splitting
    // this generates a separate chunk (about.[hash].js) for this route
    // which is lazy-loaded when the route is visited.
    component: () => import(/* webpackChunkName: "about" */ '../views/Dashboards/UnplannedDowntimes_Shutdowns_Dashboard.vue')
  },
  {
    path: '/Dashboard/unplannedDowntimeSpeedLosses',
    name: 'UnplannedSpeedlossesDashboard',
    // route level code-splitting
    // this generates a separate chunk (about.[hash].js) for this route
    // which is lazy-loaded when the route is visited.
    component: () => import(/* webpackChunkName: "about" */ '../views/Dashboards/UnplannedSpeedlosses_Dashboard.vue')
  },

]

const router = new VueRouter({
  mode: 'history',
  base: process.env.BASE_URL,
  routes
});

router.beforeEach((to, from, next) => {
  // redirect to login page if not logged in and trying to access a restricted page
  const publicPages = ['/login'];
  const authRequired = !publicPages.includes(to.path);
  const loggedIn = localStorage.getItem('username');

  if (authRequired && !loggedIn) {
    return next('/login');
  }
  next();
})

export default router
