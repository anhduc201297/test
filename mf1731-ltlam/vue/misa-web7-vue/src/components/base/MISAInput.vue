<template>
  <p class="text-boil" v-if="showTitle">
    {{ title }}
    <span v-if="showNote" >*</span>
  </p>

  <div class="search-bar">
    <input type="text" placeholder="" v-model="inputValue" />
    <label v-if="showIcon" for="search-box" class="down-icon-black"></label>
  </div>
  <!-- <label v-if="showNote" class="validate-form">{{title}} không được để trống</label> -->
</template>
<script>
export default {
  name: "MisaInput",
  props: ["showNote", "title","showTitle","showIcon"],
  data() {
    return {
      inputValue: "",
    };
  },
  methods: {},
};
</script>

<style></style>
