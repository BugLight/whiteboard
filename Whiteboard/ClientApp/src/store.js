import Vue from 'vue'
import Vuex from 'vuex'

Vue.use(Vuex)

export default new Vuex.Store({
  state: {
    activeRoom: {
      id: '',
      name: 'Unnamed room',
      connectionsCount: 0,
      maxConnections: 0
    }
  },
  mutations: {
    updateActiveRoom(state, room) {
      state.activeRoom.id = room.id;
      state.activeRoom.name = room.name;
      state.activeRoom.connectionsCount = room.connectionsCount;
      state.activeRoom.maxConnections = room.maxConnections;
    }
  },
  actions: {
  }
})
