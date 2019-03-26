<template>
    <div id="app">
        <navigation v-if="['home'].indexOf($route.name) > -1"></navigation>
        <navigation v-else>
            <div class="header">
                <UserHeader />
            </div>
        </navigation>


        <div id="nav">
            <router-link to="/">Home</router-link> |
            <router-link to="/about">About</router-link>
        </div>
        <router-view />
    </div>
</template>
<script>
    import UserHeader from '@/components/Header.vue'

export default {
  name: 'header',
  components: {
      UserHeader
  }
}
</script>
<style lang="stylus">
html, body
  margin 0
  

#app
  font-family 'Avenir', Helvetica, Arial, sans-serif
  -webkit-font-smoothing antialiased
  -moz-osx-font-smoothing grayscale
  text-align center
  color #2c3e50

#nav
  padding 30px
  a
    font-weight bold
    color #2c3e50
    &.router-link-exact-active
      color #42b983
</style>
