<template>

 <createModule-button  class="nav-button" />
</template>
    
<script>
import {  } from "boot/axios";
import { ref, onMounted } from 'vue';
import notify from "boot/notifyes";
import CreateModuleButton from 'components/CreateModuleButton.vue'

export default {
  name: 'Content',
  components:{
   CreateModuleButton
  },
  data(){
    return{
      
    }
  },
  methods:{
}
}
</script>