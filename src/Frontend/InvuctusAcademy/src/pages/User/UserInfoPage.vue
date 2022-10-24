<template>
  <q-layout view="lHh Lpr lFf" class="bg-grey-10 text-white">
    <q-drawer v-model="leftDrawerOpen" show-if-above class="bg-grey-8">
      <q-list dark>
        <q-item-label header>Essential Links</q-item-label>
        <q-item clickable rel="noopener" to="/user/courses" >
          <q-item-section avatar>
            <q-icon name="school" />
          </q-item-section>
          <q-item-section>
            <q-item-label >Мои курсы</q-item-label>
          </q-item-section>
        </q-item>
        <q-item clickable target="_blank" rel="noopener" href="#">
          <q-item-section avatar>
            <q-icon name="code" />
          </q-item-section>
          <q-item-section>
            <q-item-label>Успеваемость</q-item-label>
          </q-item-section>
        </q-item>
      </q-list>
    </q-drawer>

    <q-page-container>


      <div class="q-pa-md items-start q-gutter-md">
        <q-card class="my-card" flat bordered>
          <q-card-section class="text-center">
            <q-avatar size="100px">
              <img src="https://cdn.quasar.dev/img/boy-avatar.png">
            </q-avatar>
            <div class="text-h5 text-grey-10 q-mt-sm q-mb-xs"> {{ lastName }} {{ firstName }} {{ middleName }}</div>
          </q-card-section>

          <q-separator />

          <q-card-section class="text-center text-grey-10 q-mt-sm q-mb-xs">
            <q-item> Email: {{ autorizeEmail }} </q-item>
            <q-item> Номер телефона: {{ phoneNumber }} </q-item>
            <q-item> Instagram: {{ instagramLink }} </q-item>
            <q-item> Гражданство: {{ citizenship }} </q-item>
          </q-card-section>

          <q-separator />

          <q-card-actions>
            <editProfile-button @autorize="getUserData" />
            <editPassword-button />
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
import EditProfileButton from 'components/EditProfileButton.vue'
import EditPasswordButton from 'components/EditPasswordButton.vue'

export default defineComponent({
  name: "UserInfoPage",
  components: {
    EditProfileButton,
    EditPasswordButton
  },
  data() {
    return {
      data: [],
      autorizeEmail: "",
      phoneNumber: "",
      firstName: "",
      middleName: "",
      lastName: "",
      instagramLink: "",
      citizenship: ""
    };
  },
  methods: {
    async getUserData() {
      try {
        const autorize = await fetchLoginedUserData();
        this.autorizeEmail = autorize.data.value.email
        const response = await fetchUserData(this.autorizeEmail);
        this.data = response.data.value;
        this.firstName = response.data.value.firstName
        this.lastName = response.data.value.lastName
        this.middleName = response.data.value.middleName
        this.phoneNumber = response.data.value.phoneNumber
        this.instagramLink = response.data.value.instagramLink
        this.citizenship = response.data.value.citizenship
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
.my-card {
  width: 100%;
  max-width: 100%;
}

.user {
  width: 50%;
}

.user-data {
  display: flex;
  flex-direction: column;
}
</style>
