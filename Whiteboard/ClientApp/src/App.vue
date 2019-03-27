<template>
  <div id="app">
    <nav v-if="$route.name != 'home'">
      <div class="header">
        <Header/>
      </div>
    </nav>
    <router-view/>
  </div>
</template>
<script>
import Header from '@/components/Header.vue'

export default {
  components: {
    Header
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
    color #333333

  control-radius = 3px
  control-height = 24px
  form-width = 300px

  *
    box-sizing border-box

  .form
    margin 0 auto
    width auto

  .form-control
    margin 10px 0

  .form-control__label
    display block
    margin 5px 0

  input[type="text"], input[type="number"]
    padding 0 5px
    outline none
    border 1px solid #333333
    border-radius control-radius
    line-height control-height
    width form-width

  button, .button
    margin 0 5px
    outline none
    cursor pointer
    border-width 1px
    border-style solid
    border-radius control-radius
    line-height control-height

  .button_form
    width form-width

  .button_submit
    color #ffffff
    background-color #4444de
</style>
