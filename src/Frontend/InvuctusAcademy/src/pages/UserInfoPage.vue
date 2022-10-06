<template>
  <q-layout view="lHh Lpr lFf" class="bg-grey-10 text-white">
    <q-drawer v-model="leftDrawerOpen" show-if-above class="bg-grey-8">
      <q-list dark>
        <q-item-label header>Essential Links</q-item-label>
        <q-item
          clickable
          target="_blank"
          rel="noopener"
          href="#"
        >
          <q-item-section avatar>
            <q-icon name="school" />
          </q-item-section>
          <q-item-section>
            <q-item-label>Мои курсы</q-item-label>
          </q-item-section>
        </q-item>
        <q-item
          clickable
          target="_blank"
          rel="noopener"
          href="#"
        >
          <q-item-section avatar>
            <q-icon name="code" />
          </q-item-section>
          <q-item-section>
            <q-item-label>Успеваемосить</q-item-label>
          </q-item-section>
        </q-item>
      </q-list>
    </q-drawer>

    <q-page-container>
      <div class="q-pa-md items-start q-gutter-md">
        <q-card class="my-card" flat bordered>
          <q-card-section horizontal>
            <q-card-section class="q-pt user">
              <div class="text-h5 text-grey-10 q-mt-sm q-mb-xs">
                Личный кабинет
              </div>
              
              <div class="user-data flex">
                <table>
                  <tr v-for="(value,key) in data" >
                    <td style="font-weight:bold; color:black; font-size:15px">{{key}} : </td>
                    <td class="text-grey-10">{{ value }}</td>
                  </tr>
                </table>
              </div>
              <!-- <div class="text-caption text-grey" v-for="(value, name) in data">
                <div>{{ name }}</div>
                <div>{{ value }}</div>
              </div> -->
            </q-card-section>

            <q-card-section class="col-5 flex flex-center">
              <q-img
                class="rounded-borders"
                src="https://cdn.quasar.dev/img/parallax2.jpg"
              />
            </q-card-section>
          </q-card-section>

          <q-separator />

          <q-card-actions>
            <q-btn flat round icon="event" />
            <q-btn flat color="primary"> 7:30PM </q-btn>
            <q-btn flat color="primary"> Reserve </q-btn>
          </q-card-actions>
        </q-card>
      </div>
      <router-view />
    </q-page-container>
  </q-layout>
</template>

<script>
import { defineComponent } from "vue";
import { fetchUserData } from "boot/axios";
import { fetchLoginedUserData } from 'boot/axios'

export default defineComponent({
  name: "UserInfoPage",
  data() {
    return {
      data: [],
      autorizeEmail:"",
    };
  },
  methods: {
    async getUserData() {
      try {
        const autorize = await fetchLoginedUserData();
        this.autorizeEmail = autorize.data.value.email
        const response = await fetchUserData(this.autorizeEmail);
        this.data = response.data.value;
      } catch (error) {
        console.log(error.message);
        this.$router.push({ name: 'homepage' })
      }
    },
  },
  mounted() {
    this.getUserData();
  },
  updated() {
    this.getUserData();
  }
});
</script>

<style scoped>
.my-card
{
  width: 100%;
  max-width: 100%;
}
.user
{
  width: 50%;
}
.user-data
{
  display: flex;
  flex-direction: column;
}
</style>
