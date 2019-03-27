<template>
  <div class="join_room">
    <h1>Присоединиться к комнате</h1>
    <form class="form" v-on:submit.prevent="joinRoom">
      <div class="form-control">
        <label class="form-control__label">id комнаты</label>
        <input type="text" v-model="roomId"/>
      </div>
      <div class="form-control">
        <button class="button button_form button_submit">Присоединиться</button>
      </div>
    </form>
  </div>
</template>

<script>
  export default {
    data() {
      return {
        roomId: ''
      };
    },
    methods: {
      joinRoom() {
        this.$router.push({
          name: 'room',
          params: {
            id: this.roomId
          }
        });
      }
    }
  }
</script>