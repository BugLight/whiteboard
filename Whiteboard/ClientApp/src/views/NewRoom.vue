<template>
  <div class="new-room">
    <h1>Создание комнаты</h1>
    <form class="form">
      <div class="form-control">
        <label class="form-control__label">Название</label>
        <input type="text"/>
      </div>
      <div class="form-control">
        <label class="form-control__label">Максимум пользователей</label>
        <input type="number"/>
      </div>
      <div class="form-control">
        <button class="button button_form button_submit">Создать</button>
      </div>
    </form>
  </div>
</template>

<style lang="stylus">
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
