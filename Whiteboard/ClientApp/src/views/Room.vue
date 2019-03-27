<template>
  <div class="room">
    <header>
      <div class="room__name">
        <span>{{ activeRoom.name }}</span>
        <button class="button button_submit" v-on:click="share">
          Поделиться ссылкой
        </button>
      </div>
      <div class="room__status">
        Подключено пользователей: {{ activeRoom.connectionsCount }} / {{ activeRoom.maxConnections }}
      </div>
    </header>
  </div>
</template>

<script>
  const copyToClipboard = str => {
    const el = document.createElement('textarea');  // Create a <textarea> element
    el.value = str;                                 // Set its value to the string that you want copied
    el.setAttribute('readonly', '');                // Make it readonly to be tamper-proof
    el.style.position = 'absolute';
    el.style.left = '-9999px';                      // Move outside the screen to make it invisible
    document.body.appendChild(el);                  // Append the <textarea> element to the HTML document
    const selected =
      document.getSelection().rangeCount > 0        // Check if there is any content selected previously
        ? document.getSelection().getRangeAt(0)     // Store selection if found
        : false;                                    // Mark as false to know no selection existed before
    el.select();                                    // Select the <textarea> content
    document.execCommand('copy');                   // Copy - only works as a result of a user action (e.g. click events)
    document.body.removeChild(el);                  // Remove the <textarea> element
    if (selected) {                                 // If a selection existed before copying
      document.getSelection().removeAllRanges();    // Unselect everything on the HTML document
      document.getSelection().addRange(selected);   // Restore the original selection
    }
  };

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
    methods: {
      share() {
        copyToClipboard(window.location);
        alert('Скопировано в буфер обмена!');
      }
    },
    sockets: {
      UserJoined(room) {
        alert('Присоединился новый пользователь!');
        this.activeRoom.name = room.name;
        this.activeRoom.id = room.id;
        this.activeRoom.connectionsCount = room.connectionsCount;
        this.activeRoom.maxConnections = room.maxConnections;
      },
      UserLeft(room) {
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
