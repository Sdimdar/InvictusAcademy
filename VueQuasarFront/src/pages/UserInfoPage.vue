<template>
  <q-page>
    <q-list bordered>
      <q-item v-for="(value, name) in data">
        <q-item-section>
          <q-item-label>{{ name }}</q-item-label>
          <q-item-label caption>{{ value }}</q-item-label>
        </q-item-section>
      </q-item>
    </q-list>
  </q-page>
</template>

<script>
import { defineComponent } from 'vue'
import axios from 'axios'

export default defineComponent({
  name: 'UserInfoPage',
  data(){
    return{
      data: {}
    }
  },
  props: {
    logined: {
      type: Boolean,
      required: true
    }
  },
  mounted() {
    if(this.logined)
    {
      let config = {
          headers: {
              "Content-Type": "application/json",
          },
          withCredentials: true
      }
      axios.get("https://localhost:7243/Account/GetUserInfo", config)
      .then(ret => {
        this.data = ret.data; 
      }).catch(ret => {
        console.log(ret);
      });
    }
  }
})
</script>
