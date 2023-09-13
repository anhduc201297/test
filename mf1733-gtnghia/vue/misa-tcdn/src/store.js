import { createStore } from "vuex";

const store = createStore({
  state() {
    return {
      loading: false,
      language: "VN",
    };
  },
  mutations: {
    toggleLoading(state) {
      state.loading = !state.loading;
    },
    changeLanguage(state, newLang) {
      state.language = newLang;
    },
  },
  actions: {
    updateLoading({ commit }) {
      commit("toggleLoading");
    },
    updateLanguage({ commit }, newLang) {
      commit("changeLanguage", newLang);
    },
  },
  getters: {
    getLanguage(state) {
      return state.language;
    },
  },
});

export default store;
