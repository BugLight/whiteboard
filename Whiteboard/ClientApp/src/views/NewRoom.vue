<template>
  <div class="new-room">
    <h1>Создание комнаты</h1>
    <form class="form">
      <div class="form-control">
        <label class="form-control__label">Название</label>
        <input type="text" v-model="room.name"/>
      </div>
      <div class="form-control">
        <label class="form-control__label">Максимум пользователей</label>
        <input type="number" min="1" v-model="room.maxConnections"/>
      </div>
      <div class="form-control">
        <button class="button button_form button_submit">Создать</button>
      </div>
    </form>
  </div>
</template>

<script>
  export default {
    data() {
      return {
        room: {
          name: '',
          maxConnections: 1
        }
      };
    }
  }
</script>
