<template>
  <div class="home">
    <img alt="Vue logo" src="../assets/logo.png">
    <UserHeader/>
    <!--HelloWorld msg="Welcome to Your Vue.js App" -->
  </div>
</template>

<script>
// @ is an alias to /src
    import HelloWorld from '@/components/HelloWorld.vue'
    import UserHeader from '@/components/Header.vue'

export default {
  name: 'home',
  components: {
      UserHeader
  }
}
</script>
