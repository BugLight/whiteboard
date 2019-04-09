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
    <div class="room__canvas">
      <canvas id="canvas" width="1000" height="600" @mouseenter="initCanvas"></canvas>
    </div>
    <button @click="initCanvas">draw</button>
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


  function Draw(x, y, isDown) {
    if (isDown) {
      ctx.beginPath();
      ctx.strokeStyle = $('#selColor').val();
      ctx.lineWidth = $('#selWidth').val();
      ctx.lineJoin = "round";
      ctx.moveTo(lastX, lastY);
      ctx.lineTo(x, y);
      ctx.closePath();
      ctx.stroke();
    }
    lastX = x; lastY = y;
  }

  export default {
    computed: {
      activeRoom() {
        return this.$store.state.activeRoom;
      }
    },
    created() {
      this.$socket.invoke('UserJoin', this.$route.params.id)
        .catch(() => alert("Ошибка присоединения. Проверьте правильность ввода данных комнаты и попробуйте еще раз.\nЕсли проблема не решена, сообщите о ней по адресу buglight@kistriver.com"));
    },
    beforeRouteUpdate(to, from, next) {
      this.$socket.invoke('UserJoin', to.params.id)
        .catch(() => alert('Ошибка присоединения. Проверьте правильность ввода данных комнаты и попробуйте еще раз.\nЕсли проблема не решена, сообщите о ней по адресу buglight@kistriver.com'));
      next();
    },
    beforeRouteLeave(to, from, next) {
      this.$socket.invoke('UserLeave');
      next();
    },
    methods: {
      share() {
        copyToClipboard(window.location);
        alert('Скопировано в буфер обмена!');
      },
      initCanvas() {
        var canvas = document.getElementById('canvas');
        if (canvas && canvas.getContext) {
          var ctx = canvas.getContext('2d');

          var canvasElem = document.getElementById('canvas');

          canvasElem.addEventListener('mousedown', onMouseDown, false);
          canvasElem.addEventListener('mousemove', onMouseMove, false);
          document.addEventListener('mouseup', onMouseUp, false);
          canvasElem.addEventListener('mouseleave', onMouseLeave, false);

          var mousePressed = false;

          function onMouseDown(e) {
            mousePressed = true;
            ctx.strokeStyle = "#000000";
            ctx.beginPath();
            ctx.moveTo(e.pageX - canvasElem.offsetLeft, e.pageY - canvasElem.offsetTop);
          }

          function onMouseMove(e) {
            if (mousePressed) {
              ctx.lineTo(e.pageX - canvasElem.offsetLeft, e.pageY - canvasElem.offsetTop);
              ctx.stroke();
            }
          }

          function onMouseUp(e) {
            mousePressed = false;
          }

          function onMouseLeave(e) {
            if (mousePressed) {
              ctx.lineTo(e.pageX - canvasElem.offsetLeft, e.pageY - canvasElem.offsetTop);
              ctx.stroke();
            }
          }
          
        }
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

  #canvas
    border 1px solid black
</style>
