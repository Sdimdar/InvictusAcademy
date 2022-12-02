<template>
  <q-layout view="lHh lpR lFf">

    <q-header class="header">
      <q-toolbar>
        <div class="search">
          <q-input dense rounded outlined label="Ищете что-то конкретное?">
            <template v-slot:append>
              <q-icon name="search" />
            </template>
          </q-input>
        </div>

        <q-space />
        <TestComponent :label="loginedUserEmail" />
        <q-space />

        <div class="q-pa-md" v-if="logined">
          <q-btn-dropdown rounded color="accent" dense :label="userName" icon="account_circle">
            <q-list>
              <q-item clickable v-ripple to="/user">
                <q-item-section>
                  <q-item-label>Личный кабинет</q-item-label>
                </q-item-section>
              </q-item>

              <q-item clickable v-ripple to="">
                <q-item-section>
                  <q-item-label> Настройки</q-item-label>
                </q-item-section>
              </q-item>

              <q-item style="padding: 0px;">
                <q-item-section>
                  <logout-button :logined="logined" @unautorize="unautorize" />
                </q-item-section>
              </q-item>
            </q-list>
          </q-btn-dropdown>
        </div>

        <div class="q-pa-md" v-else>
          <q-btn-dropdown dense rounded icon="account_circle" color="accent">
            <q-list>
              <q-item>
                <q-item-section>
                  <login-button :logined="logined" @autorize="autorize" />
                </q-item-section>
              </q-item>

              <q-item>
                <q-item-section>
                  <register-button :logined="logined" @autorize="autorize" />
                </q-item-section>
              </q-item>

            </q-list>
          </q-btn-dropdown>
        </div>

      </q-toolbar>
    </q-header>

    <q-drawer class="auto-layout" v-model="drawer" show-if-above :mini="!drawer || miniState"
      @click.capture="drawerClick" :width="250" :breakpoint="500" v-if="logined">
      <div class="logo">
        <img src="img/logo.svg">
      </div>

      <q-scroll-area class="fit" :horizontal-thumb-style="{ opacity: 0 }">
        <q-list padding>
          <q-item clickable v-ripple to="/">
            <q-item-section avatar>
              <img src="img/icons/home.svg" />
            </q-item-section>

            <q-item-section>
              Главная
            </q-item-section>
          </q-item>

          <q-item active clickable v-ripple to="/user/AllCoursesPage">
            <q-item-section avatar>
              <img src="img/icons/shopping.svg" />
            </q-item-section>

            <q-item-section>
              Каталог курсов
            </q-item-section>
          </q-item>

          <q-item clickable v-ripple to="/user/courses">
            <q-item-section avatar>
              <img src="img/icons/courses.svg" />
            </q-item-section>

            <q-item-section>
              Мои курсы
            </q-item-section>
          </q-item>

          <q-item clickable v-ripple>
            <q-item-section avatar>
              <img src="img/icons/message.svg" />
            </q-item-section>

            <q-item-section>
              Чат
            </q-item-section>
          </q-item>

          <q-item clickable v-ripple>
            <q-item-section avatar>
              <img src="img/icons/book.svg" />
            </q-item-section>

            <q-item-section>
              Статьи/новости
            </q-item-section>
          </q-item>

          <div class="spacer"></div>

          <q-item clickable v-ripple>
            <q-item-section avatar>
              <img src="img/icons/support.svg" />
            </q-item-section>

            <q-item-section>
              Тех.поддержка
            </q-item-section>
          </q-item>

        </q-list>
      </q-scroll-area>

      <div class="absolute"  style="top: 110px; right: -15px;">
        <q-btn
          dense
          round
          unelevated
          color="accent"
          :icon="miniState?'chevron_right':'chevron_left'"
          @click="switchDrawerState"
        />
      </div>
    </q-drawer>

    <q-page-container style="padding-left: 50px; padding-bottom: 10px;">
      <router-view v-if="initialized" :logined="logined" :loginedUserEmail="loginedUserEmail" />

    </q-page-container>



  </q-layout>
  <footer class="footer">
      <q-toolbar class="row">

        <div class="col-3">
          <div class="column" style="height: 250px">
            <div class="col">
              <img src="img/logo_white.svg" />
            </div>
            <div class="col">
              <p style="font-size: 12px; color: #F9F9F9;">Мы в соц сетях</p>
              <div class="row">
                <div class="icon-space"> <img src="img/icons/telegram.svg" /></div>
                <div class="icon-space"> <img src="img/icons/whatsapp.svg" /></div>
                <div class="icon-space"> <img src="img/icons/instagram.svg" /> </div>
                <div class="icon-space"> <img src="img/icons/youtube.svg" /></div>
              </div>
            </div>
            <div class="col" style="font-size: 14px; font-weight: 400; color: #B3B3B3;">
              © 2022 Invictus Academy
            </div>
          </div>
        </div>

        <div class="col-3">
          <div class="column" style="height: 220px">
            <div class="col-2" style="font-size: 12px; font-weight: 400; color: #B3B3B3;">
              Контактная информация:
            </div>
            <div class="col" style="font-size: 16px; font-weight: 300; color: #F9F9F9">
              <p> +7 (775) 887 18 76</p>
              <p> +7 (775) 887 18 76</p>
              <p>arystan-bakhtiyarov@mail.ru</p>
              <p>www.invictus.kz</p>
            </div>
          </div>
        </div>

        <div class="col-2">
          <div class="column" style="height: 220px">
            <div class="col-2" style="font-size: 12px; font-weight: 400; color: #B3B3B3;">
              Навигация:
            </div>
            <div class="col" style="font-size: 18px; font-weight: 400; color: #F9F9F9">
              <p>Главная</p>
              <p>Курсы</p>
              <p>Чат</p>
              <p>Статьи</p>
            </div>
          </div>
        </div>

        <div class="col-4">
          <div class="column" style="height: 220px">
            <div class="col" style="font-size: 20px; font-weight: 500; color: #F9F9F9">
              Подпишитесь на нашу рассылку, чтобы первым быть в курсе акций и скидок!
              <p style="font-size: 14px; font-weight: 300; color: #B3B3B3;">
                Никакого спама. Только самые ценные советы</p>
            </div>
            <div class="col">
              <div class="row">
                <q-input style="background-color: white; width: 250px; margin-right: 5px;" filled outlined
                  label="example@mail.com" />
                <q-btn color="accent" label="Подписаться" />
              </div>
            </div>
          </div>
        </div>
      </q-toolbar>
    </footer>
</template>

<script>
import { onBeforeMount, ref } from 'vue'
import { fetchLoginedUserData } from 'boot/axios'
import LogoutButton from 'components/User/LogoutButton.vue'
import LoginButton from 'components/User/LoginButton.vue'
import RegisterButton from 'components/User/RegisterButton.vue'

export default {
  setup() {
    const leftDrawerOpen = ref(false)
    const miniState = ref(false)

    return {
      drawer: ref(false),
      miniState,
      leftDrawerOpen,
      toggleLeftDrawer() {
        leftDrawerOpen.value = !leftDrawerOpen.value
      },
      drawerClick(e) {
        if (miniState.value) {
          miniState.value = false
          e.stopPropagation()
        }
      }

    }
  },
  components: {
    LoginButton,
    LogoutButton,
    RegisterButton
  },
  data() {
    return {
      logined: false,
      loginedUserEmail: "",
      userName: "",
      initialized: false
    }
  },
  methods: {
    switchDrawerState: function() {
      this.miniState = !this.miniState;
    },
    autorize: async function () {
      await this.getUserData();
    },
    unautorize: function () {
      this.logined = false;
      this.loginedUserEmail = ""
    },
    async getUserData() {
      try {
        fetchLoginedUserData().then(response => {
          console.log(response)
          if (response.data.isSuccess) {
            this.logined = true;
            this.loginedUserEmail = response.data.value.email;
            this.userName = response.data.value.firstName + " " + response.data.value.lastName[0]
          }
          else {
            response.data.errors.forEach(error => {
              console.log(error)
            });
          }
        });
      } catch (e) {
        console.log(e.message);
        this.unautorize()
      }
    }
  },
  async created() {
    await this.getUserData()
    this.initialized = true
  }
}
</script>

<style>
.auto-layout {
  display: flex;
  flex-direction: column;
  justify-content: space-between;
  align-items: flex-start;
  padding: 40px 0px 48px;
  gap: 40px;
  isolation: isolate;

  /* position: absolute; */
  width: 257px;
  height: 920px;
  left: 20px;
  top: 20px;

  /* BG_white */
  background: #F9F9F9;
  /* Shadow_1 */
  box-shadow: 0px 4px 33px rgba(0, 0, 0, 0.08);
  border-radius: 0px 20px 20px 0px;
}

.logo {
  margin-left: 12px;
  margin-right: 15px;
  margin-top: 7px;
}

.header {
  margin-left: 20px;
  background-color: #F9F9F9;
}

.search {
  margin-left: 30px;
  width: 300px;
  border: 10px;
  border-radius: 20%;
}

.footer {
  display: flex;
  flex-direction: row;
  justify-content: space-between;
  align-items: flex-start;
  padding: 48px 58px 60px;
  margin-left: 0px;

  position: relative;
  height: 271px;

  width: 100%;

  background: #242424;
}

.icon-space {
  padding: 0px 25px 25px 0px;
}

.spacer{
  height: 250px;
}
</style>
