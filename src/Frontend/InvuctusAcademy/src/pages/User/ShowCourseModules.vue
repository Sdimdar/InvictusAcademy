<template>
  <q-layout view="lHh Lpr lFf" class="bg-grey-10 text-white">
    <left-bar />
  </q-layout>
</template>
<script>
import { defineComponent } from "vue";
import { useRoute } from 'vue-router';
import { fetchLoginedUserData } from "boot/axios";
import LeftBar from "components/LeftBar.vue";
import { route } from 'quasar/wrappers';
export default {
  name: "ShowCourseModules",
  components: {
    LeftBar,
  },
  setup() {
    const route = useRoute();
    console.log(route.query);
    try {
      const modules = getCourseModules(1);
      console.log(modules);

    } catch (error) {
      
    }
    var id = route.query;
  },
  methods: {
    
  },
};
</script>