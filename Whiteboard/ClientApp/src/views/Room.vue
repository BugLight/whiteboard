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
          id: '',
          name: 'Unnamed room',
          connectionsCount: 0,
          maxConnections: 0
        }
      };
    },
    created() {
      this.$socket.invoke('UserJoin', this.$route.params.id)
        .catch(() => alert("Ошибка присоединения"));
    },
    beforeRouteUpdate(to, from, next) {
      this.$socket.invoke('UserLeave')
        .then(() => this.$socket.invoke('UserJoin', to.params.id))
        .catch(() => alert('Ошибка присоединения'));
      next();
    },
    sockets: {
      UserJoined(room) {
        alert('Joined');
        this.activeRoom.name = room.name;
        this.activeRoom.id = room.id;
        this.activeRoom.connectionsCount = room.connectionsCount;
        this.activeRoom.maxConnections = room.maxConnections;
      },
      UserLeft(room) {
        alert('Left');
        this.activeRoom.name = room.name;
        this.activeRoom.id = room.id;
        this.activeRoom.connectionsCount = room.connectionsCount;
        this.activeRoom.maxConnections = room.maxConnections;
      }
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
