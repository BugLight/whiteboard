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
      path: '/room/:id',
      name: 'room',
      component: () => import('./views/Room.vue')
    }
  ]
})
