<template>
  <q-page-container v-if="!logined">
    <request-button :logined="logined" @autorize="autorize" />
  </q-page-container>
</template>

<script>
import { defineComponent } from 'vue'
import RequestButton from 'components/RequestButton.vue'
import { fetchLoginedUserData } from 'boot/axios'

export default defineComponent({
  name: 'IndexPage',
  components: {
    RequestButton
  },
  data() {
    return {
      logined: false,
      loginedUserEmail: ""
    }
  },
  methods: {
    autorize: function (email) {
      this.logined = true;
      this.loginedUserEmail = email;
    },
    unautorize: function () {
      this.logined = false;
      this.loginedUserEmail = ""
    },
    async checkLogin() {
      try {
        const response = await fetchLoginedUserData();
        this.autorize(response.data.email);
      } catch (e) {
        console.log(e.message);
        this.unautorize()
      }
    }
  },
  mounted() {
    this.checkLogin()
  }
})
</script>
