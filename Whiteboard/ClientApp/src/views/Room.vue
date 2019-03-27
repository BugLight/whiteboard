<template>
  <div class="room">
    <header>
      <div class="room__name">
        <span>{{ activeRoom.name }}</span>
        <router-link to="/room_link" tag="button" class="button button_submit">
          Поделиться ссылкой
        </router-link>
      </div>
      <div class="room__status">
        Подключено пользователей: {{ activeRoom.connectionsCount }} / {{ activeRoom.maxConnections }}
      </div>
    </header>
  </div>
</template>

<script>
  export default {
    data() {
      return {
        activeRoom: {
          name: 'Unnamed room',
          connectionsCount: 0,
          maxConnections: 0
        }
      };
    }
  }
</script>

<style scoped lang="stylus">
  header
    background-color lightgoldenrodyellow
    overflow hidden

  .room__name
    margin 10px
    font-style italic
    float left

  .room__status
    margin 10px
    color #000000
    float right
</style>
