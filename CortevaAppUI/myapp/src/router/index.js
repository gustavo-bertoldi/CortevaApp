import Vue from 'vue'
import VueRouter from 'vue-router'
import ChoiceLoginPage from '../views/ChoiceLoginPage.vue'
import TeamInfo from "@/views/TeamInfo";

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
    component: () => import(/* webpackChunkName: "about" */ '../views/LoginPage.vue')
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
    component: () => import(/* webpackChunkName: "about" */ '../views/HomePage.vue')
  },
  {
    path: '/eventDeclaration/:productionLine',
    name: 'IncidentDeclaration',
    // route level code-splitting
    // this generates a separate chunk (about.[hash].js) for this route
    // which is lazy-loaded when the route is visited.
    component: () => import(/* webpackChunkName: "about" */ '../views/Unplanned_Planned_Pannel1.vue')
  },
  {
    path: '/eventDeclaration/:productionLine/:downtimeType',
    name: 'Unplanned_Planned_Choice_Reason_Pannel2',
    // route level code-splitting
    // this generates a separate chunk (about.[hash].js) for this route
    // which is lazy-loaded when the route is visited.
    component: () => import(/* webpackChunkName: "about" */ '../views/Planned_Event_Declaration_Pannel2.vue')
  },
  {
    path: '/eventDeclaration/:productionLine/plannedDowntime/:reason',
    name: 'Unplanned_Pannel1',
    // route level code-splitting
    // this generates a separate chunk (about.[hash].js) for this route
    // which is lazy-loaded when the route is visited.
    component: () => import(/* webpackChunkName: "about" */ '../views/Planned_Event_Declaration_Pannel3.vue')
  },
  {
    path: '/eventDeclaration/:productionLine/unplannedDowntime/CIP',
    name: 'CIPDeclaration',
    // route level code-splitting
    // this generates a separate chunk (about.[hash].js) for this route
    // which is lazy-loaded when the route is visited.
    component: () => import(/* webpackChunkName: "about" */ '../views/CIP_Declaration_Unplanned_Panel3.vue')
  },
  {
    path: '/eventDeclaration/:productionLine/unplannedDowntime/clientChanging',
    name: 'ChangingClient',
    // route level code-splitting
    // this generates a separate chunk (about.[hash].js) for this route
    // which is lazy-loaded when the route is visited.
    component: () => import(/* webpackChunkName: "about" */ '../views/ClientChanging_Declaration_Unplanned_Panel3.vue')
  },
  {
    path: '/eventDeclaration/:productionLine/unplannedDowntime/formatChanging',
    name: 'ChangingFormat',
    // route level code-splitting
    // this generates a separate chunk (about.[hash].js) for this route
    // which is lazy-loaded when the route is visited.
    component: () => import(/* webpackChunkName: "about" */ '../views/FormatChanging_Declaration_Unplanned_Panel3.vue')
  },
  {
    path: '/eventDeclaration/:productionLine/unplannedDowntime/unplannedDowntime',
    name: 'ChangingFormat',
    // route level code-splitting
    // this generates a separate chunk (about.[hash].js) for this route
    // which is lazy-loaded when the route is visited.
    component: () => import(/* webpackChunkName: "about" */ '../views/UnplannedDowntime_Declaration_Unplanned_Panel3.vue')
  },
  {
    path: '/endPO/:productionLine/endPO',
    name: 'ChangingFormat',
    // route level code-splitting
    // this generates a separate chunk (about.[hash].js) for this route
    // which is lazy-loaded when the route is visited.
    component: () => import(/* webpackChunkName: "about" */ '../views/End_PO_Declaration.vue')
  },


]

const router = new VueRouter({
  mode: 'history',
  base: process.env.BASE_URL,
  routes
})

export default router
