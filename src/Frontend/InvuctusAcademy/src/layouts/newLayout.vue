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

        <div class="q-pa-md">
          <q-btn-dropdown dense rounded icon="account_circle" color="accent">
            <q-list>
              <q-item clickable v-close-popup @click="onItemClick">
                <q-item-section>
                  <q-item-label>Личный кабинет</q-item-label>
                </q-item-section>
              </q-item>

              <q-item clickable v-close-popup @click="onItemClick">
                <q-item-section>
                  <q-item-label>Настройки</q-item-label>
                </q-item-section>
              </q-item>

              <q-item clickable v-close-popup @click="onItemClick">
                <q-item-section>
                  <q-item-label>Выйти</q-item-label>
                </q-item-section>
              </q-item>
            </q-list>
          </q-btn-dropdown>
        </div>

      </q-toolbar>
    </q-header>

    <q-drawer
        class="auto-layout"
        v-model="drawer"
        show-if-above

        :mini="!drawer || miniState"
        @click.capture="drawerClick"

        :width="250"
        :breakpoint="500"
      >
      <div class="logo">
        <img src="img/logo.svg">
      </div>

        <q-scroll-area class="fit" :horizontal-thumb-style="{ opacity: 0 }">
          <q-list padding>
            <q-item clickable v-ripple>
              <q-item-section avatar>
                <img src="img/icons/home.svg" />
              </q-item-section>

              <q-item-section>
                Главная
              </q-item-section>
            </q-item>

            <q-item active clickable v-ripple>
              <q-item-section avatar>
                <img src="img/icons/shopping.svg" />
              </q-item-section>

              <q-item-section>
                Каталог курсов
              </q-item-section>
            </q-item>

            <q-item clickable v-ripple>
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

        <div class="q-mini-drawer-hide absolute" style="top: 100px; right: 0px">
          <q-btn
            dense
            round
            unelevated
            color="accent"
            icon="chevron_left"
            @click="miniState = true"
          />
        </div>
      </q-drawer>

    <q-page-container>
      <router-view />
    </q-page-container>

    <q-footer class="footer">
      <q-toolbar class="row">

          <div class="col-3">
            <div class="column" style="height: 250px">
                <div class="col">
                  <img src="img/logo_white.svg" />
                </div>
                <div class="col">
                  <p style="font-size: 12px;">Мы в соц сетях</p>
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
            <div class="col" style="font-size: 16px; font-weight: 300;">
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
            <div class="col" style="font-size: 18px; font-weight: 400;">
                <p>Главная</p>
                <p>Курсы</p>
                <p>Чат</p>
                <p>Статьи</p>
            </div>
          </div>
          </div>

          <div class="col-4">
            <div class="column" style="height: 220px">
              <div class="col" style="font-size: 20px; font-weight: 500;">
                 Подпишитесь на нашу рассылку, чтобы первым быть в курсе акций и скидок!
                 <p style="font-size: 14px; font-weight: 300; color: #B3B3B3;">
                  Никакого спама. Только самые ценные советы</p>
            </div>
            <div class="col">
              <div class="row">
                <q-input style="background-color: white; width: 250px; margin-right: 5px;" filled outlined label="example@mail.com" />
                 <q-btn  color="accent" label="Подписаться" />
              </div>
            </div>
          </div>
          </div>
      </q-toolbar>
    </q-footer>

  </q-layout>
</template>

<script>
import { ref } from 'vue'

export default {
  setup () {
    const leftDrawerOpen = ref(false)
    const miniState = ref(false)

    return {
      drawer: ref(false),
      miniState,
      leftDrawerOpen,
      toggleLeftDrawer () {
        leftDrawerOpen.value = !leftDrawerOpen.value
      },
      drawerClick (e) {
        if (miniState.value) {
          miniState.value = false
          e.stopPropagation()
        }
      }

    }
  }
}
</script>

<style>
.auto-layout{
  display: flex;
  flex-direction: column;
  justify-content: space-between;
  align-items: flex-start;
  padding: 40px 0px 48px;
  gap: 40px;
  isolation: isolate;

  position: absolute;
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

.logo{
  margin-left: 12px;
  margin-right: 15px;
  margin-top: 7px;
}

.header{
  margin-left: 20px;
  background-color: #F9F9F9;
}

.search{
  margin-left: 30px;
  width: 300px;
  border: 10px;
  border-radius: 20%;
}

.footer{
  margin-left: 20px;
  display: flex;
  flex-direction: row;
  justify-content: space-between;
  align-items: flex-start;
  padding: 48px 58px 60px;
  gap: 60px;

  position: relative;
  height: 271px;
  top: 100px;

  background: #242424;
}

.icon-space{
  padding: 0px 25px 25px 0px;
}

</style>
