<template>
  <q-layout view="lHh Lpr lFf" class="bg-grey-10 text-white">
    <left-bar />
  </q-layout>
</template>
<script>
import { defineComponent } from "vue";
import { fetchLoginedUserData } from "boot/axios";
import LeftBar from "components/LeftBar.vue";
export default {
  name: "AllCoursesPage",

  components: {
    LeftBar,
  },
};
</script>