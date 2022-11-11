<template>
  <div class="q-pa-md">
    <q-layout view="hHh Lpr lff">
      <q-header elevated>
        <q-toolbar>
          <q-btn flat @click="drawer = !drawer" round dense icon="menu" />
          <q-item to="/admin-panel" >
          <img class="logo-img" src="img/invictus_academy_logo.png" />
        </q-item>
        <q-space />
        <template v-if="!logined" >
          <login-button class="nav-button" :logined="logined" @autorize="autorize"/>
        </template>
        <template v-else >
          <logout-button class="nav-button" :logined="logined" @unautorize="unautorize" />
        </template>
        </q-toolbar>
      </q-header>
      <template v-if="logined" >
        <q-drawer
        v-model="drawer"
        show-if-above

        :mini="miniState"
        @mouseover="miniState = false"
        @mouseout="miniState = true"
        mini-to-overlay

        :width="300"
        :breakpoint="500"
        bordered
        class="bg-grey-3"
      >
        <q-scroll-area class="fit" :horizontal-thumb-style="{ opacity: 0 }">
          <q-list padding>
            <q-item clickable v-ripple @click="">
              <q-item-section avatar>
                <q-icon name="people" />
              </q-item-section>

              <q-item-section >
                <q-btn to="/admin-panel/showAllUser" class="nav-button" label="Список пользователей"/>
              </q-item-section>
            </q-item>

            <q-separator />

            <q-item clickable v-ripple>
              <q-item-section avatar>
                <q-icon name="phone" />
              </q-item-section>

              <q-item-section>
                <q-btn to="/admin-panel/requests" class="nav-button" label="Список заявок"/>
              </q-item-section>
            </q-item>

            <q-separator />

            <q-item clickable v-ripple>
              <q-item-section avatar>
                <q-icon name="slow_motion_video" />
              </q-item-section>

              <q-item-section>
                <q-btn to="/admin-panel/content" class="nav-button" label="Учебные материалы"/>
              </q-item-section>
            </q-item>

            <q-separator />

            <q-item clickable v-ripple>
              <q-item-section avatar>
                <q-icon name="star" />
              </q-item-section>

              <q-item-section>
                <q-btn to="/admin-panel/createAdmin" class="nav-button" label="Создать админа"/>
              </q-item-section>
            </q-item>

            <q-separator />

            <q-item clickable v-ripple>
              <q-item-section avatar>
                <q-icon name="star" />
              </q-item-section>

              <q-item-section>
                <q-btn to="/admin-panel/createCourse" class="nav-button" label="Создать курс"/>
              </q-item-section>
            </q-item>

            <q-separator />

            <q-item clickable v-ripple>
              <q-item-section avatar>
                <q-icon name="star" />
              </q-item-section>

              <q-item-section>
                <q-btn to="/admin-panel/editCourse" class="nav-button" label="Редактировать курс"/>
              </q-item-section>
            </q-item>

            <q-separator />

            <q-item clickable v-ripple>
              <q-item-section avatar>
                <q-icon name="star" />
              </q-item-section>

            <q-item-section>
              <q-btn to="/admin-panel/allCourses" class="nav-button" label="Все курсы"/>
            </q-item-section>
            </q-item>

            <q-separator />

          </q-list>
        </q-scroll-area>
      </q-drawer>

      <q-page-container>
        <q-page padding>
          <router-view />
        </q-page>
      </q-page-container>

      </template>
    </q-layout>
  </div>

</template>

<script>
import LogoutButton from 'components/LogoutButton.vue'
import LoginButton from 'components/LoginButton.vue'
import { fetchLoginedUserData } from 'boot/axios'
import { ref } from 'vue'

export default {
  name: 'AdminLayout',
  components:{
    LoginButton,
    LogoutButton
  },
  setup () {
    return {
      drawer: ref(false),
      miniState: ref(true)
    }
  },
  data(){
    return{
      logined: false,
      loginedUserEmail: "",
      initialized: false
    }
  },
  methods:{
    autorize: function(email){
      this.logined = true;
      this.loginedUserEmail = email;
    },
    unautorize: function(){
      this.logined = false;
      this.loginedUserEmail = ""
    },
    async checkLogin(){
      try {
        const response = await fetchLoginedUserData();
        console.log(response)
        if (response.data.isSuccess) {
          this.autorize(response.data.value.email);
        }
        else{
          response.data.errors.forEach(error => {
            console.log(error)
          });
        }
      } catch (e) {
        console.log(e.message);
        this.unautorize()
      }
    }
  },
  async created() {
    await this.checkLogin()
    this.initialized = true
  }
}
</script>

<style>
  .logo-img{
    width: 200px;
    object-fit: contain;
  }
  .nav-button{
    font-size: 11px;
    border-radius: 0;
    align-self: stretch;
  }
</style>
