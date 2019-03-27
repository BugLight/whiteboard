<template>
  <div class="new-room">
    <h1>Создание комнаты</h1>
    <form class="form" v-on:submit.prevent="createRoom">
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
    },
    methods: {
      createRoom() {
        this.$http.post('http://localhost:5000/api/rooms', this.room)
          .then(response => response.json())
          .then(room => {
            alert('Комната успешно создана!');
            this.$router.push({
              name: 'room',
              params: {
                id: room.id
              }
            });
          })
          .catch(() => {
            alert('Ошибка создания комнаты. Попробуйте нажать кнопку еще раз.\nЕсли проблема не решена, сообщите о ней по адресу buglight@kistriver.com');
          });
      }
    }
  }
</script>
