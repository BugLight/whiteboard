<template>
  <div class="room" @mouseup="mouseUp">
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
    <button v-on:click="setColor('#000000')">
      Чёрный
    </button>
    <button v-on:click="setColor('ff0000')">
      Красный
    </button>
    <button v-on:click="setColor('#00ff00')">
      Зелёный
    </button>
    <button v-on:click="setColor('#0000ff')">
      Синий
    </button>
    <button v-on:click="setColor('#00ffff')">
      Бирюзовый
    </button>
    <div class="room__canvas">
      <canvas id="canvas" width="1000" height="600" @mousedown="mouseDown"
        @mousemove="mouseMove" @mouseleave="mouseMove"></canvas>
    </div>
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

  var mousePressed = false;
  var canvasElem, ctx;
  var prevX, prevY;
  var color = "#000000";

  export default {
    computed: {
      activeRoom() {
        return this.$store.state.activeRoom;
      }
    },
    mounted() {
      this.initCanvas();
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
    sockets: {
      Drew(m) {
        ctx.beginPath();
        ctx.moveTo(m.from.x, m.from.y);
        ctx.lineTo(m.to.x, m.to.y);
        ctx.stroke();
      }
    },
    methods: {
      setColor(clr) {
        color = clr;
        alert(color);
      },
      share() {
        copyToClipboard(window.location);
        alert('Скопировано в буфер обмена!');
      },
      initCanvas() {
        canvasElem = document.getElementById('canvas');
        if (canvasElem && canvasElem.getContext) {
          ctx = canvasElem.getContext('2d');
        }
      },
      mouseDown(e) {
        mousePressed = true;
        ctx.strokeStyle = "#000000";
        ctx.beginPath();
        prevX = e.pageX - canvasElem.offsetLeft;
        prevY = e.pageY - canvasElem.offsetTop;
        //color = 
      },
      mouseUp() {
        mousePressed = false;
      },
      mouseMove(e) {
        if (mousePressed) {
          var curX = e.pageX - canvasElem.offsetLeft;
          var curY = e.pageY - canvasElem.offsetTop;
          ctx.moveTo(prevX, prevY);
          this.$socket.invoke("Draw", {
            from: {
              x: prevX,
              y: prevY
            },
            to: {
              x: curX,
              y: curY
            }
          })
          .then(() => {
            ctx.lineTo(curX, curY);
            ctx.stroke();
          })
          .catch(() => {
            console.error("Ошибка отправки точек на сервер");
          });
          prevX = curX;
          prevY = curY;
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
