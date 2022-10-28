<template>
    <createAdmin-button  class="nav-button" />

</template>
    
<script>
import { createAdmin } from "boot/axios";
import { ref, onMounted } from 'vue';
import notify from "boot/notifyes";
import CreateAdminButton from 'components/CreateAdminButton.vue'

export default {
  name: 'CreateAdminPage',
  components:{
    CreateAdminButton
  },
  data(){
    return{
      logined: false,
      loginedUserEmail: "",
      initialized: false
    }
  },
  methods:{
}
}
</script>
  