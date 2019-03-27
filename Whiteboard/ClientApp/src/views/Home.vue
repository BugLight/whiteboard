<template>
    <div class="home">
        <header>
            <h1>Whiteboard</h1>
        </header>
        <div id="button">
            <router-link to="/new_room" tag="button">
                Create new room
            </router-link>
        </div>
        <div id="button">
            <router-link to="/join_room" tag="button">
                Join existing room with ID
            </router-link>
        </div>

    </div>
</template>
<style scoped lang="stylus">
    header{
        background-color: lightcyan;
        padding: 20px;
        height: 130px;
    }

    h1 {
        color: #000000;
        text-align:center;
        padding: 20px;
        font-family: 'Franklin Gothic', 'Lucida Sans', 'Lucida Sans Regular', 'Lucida Grande', 'Lucida Sans Unicode', Geneva, Verdana, sans-serif;
        font-style: italic;
    }

    button {
        padding: 15px 35px;
        text-align: center;
        font-size: 16px;
        margin: 40px 10px 0px 10px;
        width: 400px;

    }
</style>

