import Vue from 'vue'
import Router from 'vue-router'
import Home from './views/Home.vue'

Vue.use(Router)

export default new Router({
  mode: 'history',
  base: process.env.BASE_URL,
  routes: [
    {
      path: '/',
      name: 'home',
      component: Home
    },
    {
      path: '/about',
      name: 'about',
      // route level code-splitting
      // this generates a separate chunk (about.[hash].js) for this route
      // which is lazy-loaded when the route is visited.
      component: () => import(/* webpackChunkName: "about" */ './views/About.vue')
      },
      {
        path: '/new_room',
        name: 'new_room',
        component: () => import('./views/NewRoom.vue')
      },
      {
        path: '/join_room',
        name: 'join_room',
        component: () => import('./views/JoinRoom.vue')
      },
      {
        path: '/room',
        name: 'room',
        component: () => import('./views/Room.vue')
      }
  ]
})
