<template>
  <div class="minput-checkbox-container">
    <div class="checkbox-pseudobox">
      <div class="icons tickv-circle"></div>
    </div>
    <div class="checkbox-box">
      <input type="checkbox" :checked="checked" />
    </div>
  </div>
</template>
<script>
export default {
  props: ["checked"],
};
</script>
<style>
:root {
  --width-minput-checkbox: 24px;
}

.minput-checkbox-container {
  width: 24px;
  height: 24px;
}

.checkbox-pseudobox {
  width: var(--width-minput-checkbox);
  height: var(--width-minput-checkbox);
  background-color: #2ca012;
  border-radius: 4px;
  fill: #2ca012;
  display: flex;
  justify-content: center;
  align-items: center;
}

.checkbox-box {
  margin-top: -24px;
}

.checkbox-box input[type="checkbox"] {
  display: inline-block;
  width: var(--width-minput-checkbox);
  height: var(--width-minput-checkbox);
}

.checkbox-box input[type="checkbox"]:checked {
  opacity: 0;
}

.tickv-circle {
  background-position: -1602px -258px;
  width: 20px;
  height: 20px;
}
.ti-minus {
  width: 16px;
  height: 2px;
  border-radius: 4px;
  background-color: #fff;
}
</style>
