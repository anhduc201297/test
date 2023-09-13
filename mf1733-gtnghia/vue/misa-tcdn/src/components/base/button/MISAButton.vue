<template>
  <button v-if="type === 'primary'" :class="`btn-pri-${size}`">
    <p class="body1">{{ title }}</p>
  </button>
  <button
    v-else-if="type === 'icon'"
    class="transparent icon-container"
    :class="`btn-icon-${size}`"
    :title="title"
  >
    <i class="icon" :class="icon"></i>
  </button>
  <button v-else-if="type === 'link'" :class="`btn-link-${size}`">
    <p class="body2">{{ title }}</p>
  </button>
  <button v-else-if="type === 'extra'" :class="`btn-extra-${size}`">
    <p class="body2">{{ title }}</p>
  </button>
  <button v-else-if="type === 'error'" :class="`btn-error-${size}`">
    <p class="body2">{{ title }}</p>
  </button>
  <router-link v-else-if="type === 'route'" :to="{ name: route }">
    <button :class="`btn-pri-${size}`">
      <p class="body2">{{ title }}</p>
    </button>
  </router-link>
</template>

<script>
export default {
  name: "m-button",
  props: {
    type: {
      type: String,
      default: "primary",
    },
    title: String,
    icon: String,
    size: {
      type: String,
      default: "m",
    },
    route: String,
  },
  data() {
    return {};
  },
};
</script>

<style scoped>
@import url(./button.css);
@import url(@/css/icon.css);
</style>
