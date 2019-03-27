<template>
    <div class="room">
        <header>
            <h2>{{ activeRoom.name }} </h2>
            <router-link to="/room_link" tag="button">
                Поделиться ссылкой
            </router-link>
            <h4> Подключено пользователей: {{ activeRoom.connectionsCount }} / {{ activeRoom.maxConnections }} </h4>
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
    header {
        background-color: lightgoldenrodyellow;
        padding: 1px;
        height: 50px;
        font-family: 'Franklin Gothic', 'Lucida Sans', 'Lucida Sans Regular', 'Lucida Grande', 'Lucida Sans Unicode', Geneva, Verdana, sans-serif;
    }

    h2 {
        margin-left: 10px;
        display:inline-block;
        color: #000000;
        font-style: italic;
        float:left;
    }

    button {
        display: inline-block;
        text-align: center;
        font-size: 14px;
        margin: 25px 10px 0px 20px;
        float:left;
    }

    h4 {
        display: inline-block;
        color: #000000;
        margin: 25px 10px 0px 10px;
        float: right;
    }

</style>
