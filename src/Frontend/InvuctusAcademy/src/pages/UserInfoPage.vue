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
import constants from '../static/constants'

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
    },
    loginedUserEmail:
    {
      type: String
    }
  },
  methods:{
    getUserData(){
      if(this.logined)
      {
        axios.get("https://localhost:7210/User/GetUserData", { params: {email : this.loginedUserEmail}}, constants.loginConfig)
        .then(ret => {
          this.data = ret.data; 
        }).catch(ret => {
          console.log(ret);
        });
      }
    }
  },
  updated() {
    this.getUserData()
  },
  mounted() {
    this.getUserData()
  }
})
</script>
