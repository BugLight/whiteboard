<template>
    <div class="room">
        <header>
            <h3>{{ activeRoom.name }} </h3>
            <router-link to="/room_link" tag="button">
                Поделиться ссылкой
            </router-link>
            <h4> {{ activeRoom.connectionsCount }} / {{ activeRoom.maxConnections }} </h4>
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

    h3 {
        display:inline-block;
        color: #000000;
        text-align: left;
        font-style: italic;
    }

    button {
        display: inline-block;
        text-align: center;
        font-size: 14px;
        margin: 0px 10px 0px 20px;
    }

    h4 {
        display: inline-block;
        color: #000000;
        text-align: right;
        margin: 0px 10px 0px 10px;
    }

</style>
